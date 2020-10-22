using Npgsql;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Net;
using System.Drawing.Printing;
using System.Collections.Generic;

namespace Flawless_ex
{
    public partial class Statement : Form //計算書/納品書作成メニュー
    {
        public int staff_id;
        int itemMainCategoryCode;       //担当者ごとの大分類
        int MainCategoryCode;           //大分類コード（コンボボックスの初期値）
        int mainCategoryCode;           //大分類コード（登録用）
        int itemCode;                   //品名コード
        string ItemDetail;              //品物詳細
        decimal Weight;                 //重量
        decimal UnitPrice;              //単価
        int COUNT;                      //数量
        decimal Money;                  //金額
        string Remark;                  //備考
        int code = 1;                   //計算書用
        int CODE = 1;                   //納品書用
        public int ClientCode;                 //顧客コード
        decimal unitWeight;             //総重量を計算する用
        int unitCount;                  //総数を計算する用
        decimal unitMoney;              //小計を計算するため
        decimal TotalMoney;

        decimal WeightTotal;            //計算した総重量用
        int CountTotal;                 //計算した総数用
        public int type = 0;
        public string path;
        public decimal total;
        public int Grade;
        int AntiqueNumber;
        int ID_Number;
        bool screan = true;
        string Sql;                     //計算書登録
        string SQL;                     //画像登録（更新）
        string deliveryRemarks;                 //納品書の表の下にある備考
        int record = 0;
        string ArticleText;
        string taxCertificateText;
        string sealCertificateText;
        string residenceCardText;
        string residencePeriodStay;

        #region"計算書・納品書の共通する枠外"
        decimal TotalWeight;            //総重量
        decimal TotalCount;             //総数
        decimal sum;                    //小計
        decimal TaxAmount;              //税額
        string SettlementMethod;        //決済方法（支払方法）
        string SettlementDate;          //決済日
        string DeliveryDate;            //受渡日
        string remarks = "";            //顧客マスタの備考（計算書）
        string remarks1 = "";           //顧客マスタの備考（納品書）
        #endregion

        #region "再登録"
        int Re;
        string sql_str;
        string sql_str2;
        string sql_str3;
        string sql_str4;
        #endregion
        #region "買取販売履歴の引数"
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

        #region"変数"
        decimal Tax;        //計算用の税（表示用を別に用意）        
        int countsum;        //総数計算用
        decimal weisum;     //総重量計算用
        decimal subSum;     //税抜き時の合計金額用
        decimal sub;          //計算書の小計、合計
        decimal SUB;           //納品書の小計、合計
        decimal taxAmount;
        public DataTable clientDt = new DataTable();//顧客情報
        public int count = 0;
        public int count1 = 0;
        string docuNum;     //計算書の伝票番号
        string Num;         //計算書の伝票番号の数字部分（F を切り取った直後）
        int conNum;           //納品書の管理番号
        bool second = false;     //ロード時無視用
        bool NotLoad = false;       //ロード時に通過するため（計算書）
        bool NotLoad1 = false;      //ロード時に通過するため（納品書）
        bool cellfirst = false;
        bool Delivery = true;       //納品書を開いた一番最初のときだけ管理番号を登録
        #endregion

        int PreviewRow;

        DataTable table = new DataTable();                              //計算書の表用
        DataTable statementTotalTable = new DataTable();                //計算書の合計・決算方法・受取方法など用
        DataTable clientTable = new DataTable();                        //計算書の顧客情報用

        string antiqueLicense;
        string totalMoney;
        string totalCount;
        string totalWeight;

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

        decimal weight = 0;
        decimal unitprice = 0;
        int Count = 0;

        #region"画像のPATH"
        string AolFinancialShareholder = "";
        string TaxCertification = "";
        string SealCertification = "";
        string ResidenceCard = "";
        string ResidencePeriod = "";
        string AntiqueLicence = "";
        #endregion

        #region"datagridview"
        DataGridView dataGridView;
        DataGridViewComboBoxCell dataGridViewComboBoxCell;
        int RowNumber;
        #endregion

        public string client_staff_name;
        public string address;
        string register_date;
        public string access_auth;
        public string document;
        public int control;
        public string data;
        string item;
        string main;
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

        int number;     //伝票番号の数字五桁（計算書）
        int Number;     //管理番号の数字（納品書）

        MainMenu mainMenu;
        TopMenu topMenu;

        #region"DataTable"
        DataTable dt = new DataTable();//大分類

        DataTable dt2 = new DataTable();//品名と大分類関連付け
        DataTable dt8 = new DataTable();//6行目
        DataTable dt9 = new DataTable();//7行目

        #region"datagridview 大分類と各行の品名のコンボボックス"
        DataTable dt4 = new DataTable();        //計算書の大分類
        DataTable dt5 = new DataTable();        //計算書の一行目の品名
        DataTable dt6 = new DataTable();        //納品書の大分類
        DataTable dt7 = new DataTable();        //納品書の一行目の品名

        //計算書の datagridview の品名のコンボボックス
        DataTable dt12 = new DataTable();
        DataTable dt13 = new DataTable();
        DataTable dt14 = new DataTable();
        DataTable dt15 = new DataTable();
        DataTable dt16 = new DataTable();
        DataTable dt17 = new DataTable();
        DataTable dt18 = new DataTable();
        DataTable dt19 = new DataTable();
        DataTable dt20 = new DataTable();
        DataTable dt21 = new DataTable();
        DataTable dt22 = new DataTable();
        DataTable dt23 = new DataTable();

        //納品書の datagridview の品名のコンボボックス
        DataTable dt32 = new DataTable();
        DataTable dt33 = new DataTable();
        DataTable dt34 = new DataTable();
        DataTable dt35 = new DataTable();
        DataTable dt36 = new DataTable();
        DataTable dt37 = new DataTable();
        DataTable dt38 = new DataTable();
        DataTable dt39 = new DataTable();
        DataTable dt40 = new DataTable();
        #endregion

        DataTable dt3 = new DataTable();// 品名情報全て
        DataTable dt300 = new DataTable();//地金
        DataTable dt400 = new DataTable();//ダイヤ
        DataTable dt500 = new DataTable();//ブランド
        DataTable dt600 = new DataTable();//製品・ジュエリー
        DataTable dt700 = new DataTable();//その他

        #region"既存の計算書の各列"
        DataTable dtStatement = new DataTable();
        #endregion
        #region"既存の納品書の各列"
        DataTable dtDelivery = new DataTable();
        #endregion

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
        NpgsqlDataAdapter adapter2;
        NpgsqlDataReader reader;
        NpgsqlTransaction transaction;
        DataRow row;

        public Statement(MainMenu main, int id, int type, string client_staff_name, string address, string access_auth, decimal Total, string Pass, string document, int control,
            string data, string name1, string phoneNumber1, string addresskana1, string code1, string item1, string date1, string date2, string method1, string amountA,
            string amountB, string antiqueNumber, string documentNumber, string address1, int Grade)
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
            this.Grade = Grade;
            #region "買取販売履歴"
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
                this.reasonTextBox.Visible = true;
                this.DeliveryPreviewButton.Enabled = false;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
            }
            else
            {
                this.label10.Visible = false;
                this.textBox2.Visible = false;
                this.label9.Visible = false;
                this.reasonTextBox.Visible = false;
                this.previewButton.Enabled = false;
                this.RecordListButton.Enabled = false;
                this.DeliveryPreviewButton.Enabled = false;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
            }
            #endregion

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            string sql_str = "select * from staff_m where staff_code = " + staff_id + ";";　//担当者名取得用
            string sql;                                                //伝票番号・管理番号取得
            sql = "select document_number from statement_data order by NotFNumber;";     //伝票番号の取得用
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
                    Num = docuNum.Substring(1);        //伝票番号の数字部分
                }
                else
                {
                    docuNum = "F0";
                    Num = docuNum.Substring(1);       //伝票番号の数字部分
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
                sql = "select control_number from delivery_m order by control_number;";         //管理番号取得
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

                Number = conNum + 1;
                documentNumberTextBox2.Text = Number.ToString();
            }
            #endregion

            DateTime date = DateTime.Now;
            DateTime AssessmentDate = date.Date;

            if (data != "S" && data != "D" && antiqueNumber == null && documentNumber == null && Grade == 0)
            {
                string Str = "insert into statement_data (document_number, NotFNumber, staff_code, assessment_date) values ('" + documentNumberTextBox.Text + "','" + number + "', '" + staff_id + "','"+AssessmentDate+"');";
                cmd = new NpgsqlCommand(Str, conn);
                cmd.ExecuteNonQuery();
            }

            if ((data != "S" && data != "D" && antiqueNumber == null && documentNumber == null && Grade == 0 && control == 0) && tabControl1.SelectedIndex != 0) 
            {
                string Str = "insert into delivery_m (control_number, staff_code) values ('" + Number + "', '" + staff_id + "');";
                cmd = new NpgsqlCommand(Str, conn);
                cmd.ExecuteNonQuery();
            }

            #region"コメントアウト"
            ////担当者ごとの大分類の初期値を先頭に
            //string sql_str2 = "select * from main_category_m where invalid = 0 order by main_category_code asc;";
            //adapter = new NpgsqlDataAdapter(sql_str2, conn);
            //adapter.Fill(dt);

            ////品名検索用
            //string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + itemMainCategoryCode + ";";
            //adapter = new NpgsqlDataAdapter(sql_str3, conn);
            //adapter.Fill(dt2);

            //string sql_str4 = "select main_category_name from main_category_m where main_category_code = " + itemMainCategoryCode + ";";
            //adapter = new NpgsqlDataAdapter(sql_str4, conn);
            //adapter.Fill(dt3);

            //DataRow row;
            //row = dt3.Rows[0];
            //string MainName = row["main_category_name"].ToString();
            #endregion

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

            //デフォルトで税込み表示
            comboBox11.SelectedIndex = 0;

            //デフォルトで円表示
            CoinComboBox.SelectedIndex = 0;

            #region"コメントアウト"
            //if (data != "S")
            //{
            //    #region"一時コメントアウト"
            //    //#region "納品書"
            //    //#region "納品書　大分類"
            //    //#region "納品書1行目"
            //    //DataTable deliverydt = new DataTable();
            //    //deliverydt = dt.Copy();
            //    //mainCategoryComboBox00.DataSource = deliverydt;
            //    //mainCategoryComboBox00.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox00.ValueMember = "main_category_code";
            //    //mainCategoryComboBox00.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //#endregion
            //    //#region "納品書2行目"
            //    //DataTable deliverydt100 = new DataTable();
            //    //deliverydt100 = dt.Copy();
            //    //mainCategoryComboBox01.DataSource = deliverydt100;
            //    //mainCategoryComboBox01.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox01.ValueMember = "main_category_code";
            //    //mainCategoryComboBox01.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書3行目"
            //    //DataTable deliverydt101 = new DataTable();
            //    //deliverydt101 = dt.Copy();
            //    //mainCategoryComboBox02.DataSource = deliverydt101;
            //    //mainCategoryComboBox02.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox02.ValueMember = "main_category_code";
            //    //mainCategoryComboBox02.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書4行目"
            //    //DataTable deliverydt102 = new DataTable();
            //    //deliverydt102 = dt.Copy();
            //    //mainCategoryComboBox03.DataSource = deliverydt102;
            //    //mainCategoryComboBox03.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox03.ValueMember = "main_category_code";
            //    //mainCategoryComboBox03.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書5行目"
            //    //DataTable deliverydt103 = new DataTable();
            //    //deliverydt103 = dt.Copy();
            //    //mainCategoryComboBox04.DataSource = deliverydt103;
            //    //mainCategoryComboBox04.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox04.ValueMember = "main_category_code";
            //    //mainCategoryComboBox04.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書6行目"
            //    //DataTable deliverydt104 = new DataTable();
            //    //deliverydt104 = dt.Copy();
            //    //mainCategoryComboBox05.DataSource = deliverydt104;
            //    //mainCategoryComboBox05.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox05.ValueMember = "main_category_code";
            //    //mainCategoryComboBox05.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書7行目"
            //    //DataTable deliverydt105 = new DataTable();
            //    //deliverydt105 = dt.Copy();
            //    //mainCategoryComboBox06.DataSource = deliverydt105;
            //    //mainCategoryComboBox06.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox06.ValueMember = "main_category_code";
            //    //mainCategoryComboBox06.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書8行目"
            //    //DataTable deliverydt106 = new DataTable();
            //    //deliverydt106 = dt.Copy();
            //    //mainCategoryComboBox07.DataSource = deliverydt106;
            //    //mainCategoryComboBox07.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox07.ValueMember = "main_category_code";
            //    //mainCategoryComboBox07.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書9行目"
            //    //DataTable deliverydt107 = new DataTable();
            //    //deliverydt107 = dt.Copy();
            //    //mainCategoryComboBox08.DataSource = deliverydt107;
            //    //mainCategoryComboBox08.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox08.ValueMember = "main_category_code";
            //    //mainCategoryComboBox08.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書10行目"
            //    //DataTable deliverydt108 = new DataTable();
            //    //deliverydt108 = dt.Copy();
            //    //mainCategoryComboBox09.DataSource = deliverydt108;
            //    //mainCategoryComboBox09.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox09.ValueMember = "main_category_code";
            //    //mainCategoryComboBox09.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書11行目"
            //    //DataTable deliverydt109 = new DataTable();
            //    //deliverydt109 = dt.Copy();
            //    //mainCategoryComboBox010.DataSource = deliverydt109;
            //    //mainCategoryComboBox010.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox010.ValueMember = "main_category_code";
            //    //mainCategoryComboBox010.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書12行目"
            //    //DataTable deliverydt110 = new DataTable();
            //    //deliverydt110 = dt.Copy();
            //    //mainCategoryComboBox011.DataSource = deliverydt110;
            //    //mainCategoryComboBox011.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox011.ValueMember = "main_category_code";
            //    //mainCategoryComboBox011.SelectedIndex = 0;
            //    //#endregion
            //    //#region "納品書13行目"
            //    //DataTable deliverydt112 = new DataTable();
            //    //deliverydt112 = dt.Copy();
            //    //mainCategoryComboBox012.DataSource = deliverydt112;
            //    //mainCategoryComboBox012.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox012.ValueMember = "main_category_code";
            //    //mainCategoryComboBox012.SelectedIndex = 0;
            //    //#endregion
            //    //#endregion
            //    //#region "納品書　品名"
            //    //#region "納品書1行目"
            //    //deliverydt200 = dt2.Copy();
            //    //itemComboBox00.DataSource = deliverydt200;
            //    //itemComboBox00.DisplayMember = "item_name";
            //    //itemComboBox00.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書2行目"
            //    //deliverydt201 = dt2.Copy();
            //    //itemComboBox01.DataSource = deliverydt201;
            //    //itemComboBox01.DisplayMember = "item_name";
            //    //itemComboBox01.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書3行目"
            //    //deliverydt202 = dt2.Copy();
            //    //itemComboBox02.DataSource = deliverydt202;
            //    //itemComboBox02.DisplayMember = "item_name";
            //    //itemComboBox02.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書4行目"
            //    //deliverydt203 = dt2.Copy();
            //    //itemComboBox03.DataSource = deliverydt203;
            //    //itemComboBox03.DisplayMember = "item_name";
            //    //itemComboBox03.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書5行目"
            //    //deliverydt204 = dt2.Copy();
            //    //itemComboBox04.DataSource = deliverydt204;
            //    //itemComboBox04.DisplayMember = "item_name";
            //    //itemComboBox04.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書6行目"
            //    //deliverydt205 = dt2.Copy();
            //    //itemComboBox05.DataSource = deliverydt205;
            //    //itemComboBox05.DisplayMember = "item_name";
            //    //itemComboBox05.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書7行目"
            //    //deliverydt206 = dt2.Copy();
            //    //itemComboBox06.DataSource = deliverydt206;
            //    //itemComboBox06.DisplayMember = "item_name";
            //    //itemComboBox06.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書8行目"
            //    //deliverydt207 = dt2.Copy();
            //    //itemComboBox07.DataSource = deliverydt207;
            //    //itemComboBox07.DisplayMember = "item_name";
            //    //itemComboBox07.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書9行目"
            //    //deliverydt208 = dt2.Copy();
            //    //itemComboBox08.DataSource = deliverydt208;
            //    //itemComboBox08.DisplayMember = "item_name";
            //    //itemComboBox08.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書10行目"
            //    //deliverydt209 = dt2.Copy();
            //    //itemComboBox09.DataSource = deliverydt209;
            //    //itemComboBox09.DisplayMember = "item_name";
            //    //itemComboBox09.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書11行目"
            //    //deliverydt210 = dt2.Copy();
            //    //itemComboBox010.DataSource = deliverydt210;
            //    //itemComboBox010.DisplayMember = "item_name";
            //    //itemComboBox010.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書12行目"
            //    //deliverydt211 = dt2.Copy();
            //    //itemComboBox011.DataSource = deliverydt211;
            //    //itemComboBox011.DisplayMember = "item_name";
            //    //itemComboBox011.ValueMember = "item_code";
            //    //#endregion
            //    //#region "納品書13行目"
            //    ////納品書
            //    //deliverydt212 = dt2.Copy();
            //    //itemComboBox012.DataSource = deliverydt212;
            //    //itemComboBox012.DisplayMember = "item_name";
            //    //itemComboBox012.ValueMember = "item_code";
            //    //#endregion
            //    //#endregion
            //    //#endregion
            //    #endregion
            //}
            //if (data != "D")
            //{
            //    #region "計算書"
            //    #region "計算書　大分類コンボボックス"

            //    #region"一時コメントアウト"
            //    //mainCategoryComboBox0.DataSource = dt;
            //    //mainCategoryComboBox0.DisplayMember = "main_category_name";
            //    //mainCategoryComboBox0.ValueMember = "main_category_code";
            //    //mainCategoryComboBox0.SelectedIndex = 0;//担当者ごとの初期値設定                
            //    #endregion

            //    #endregion
            //    #region "計算書　品名コンボボックス"

            //    #region"一時コメントアウト"
            //    //itemComboBox0.DataSource = dt2;
            //    //itemComboBox0.DisplayMember = "item_name";
            //    //itemComboBox0.ValueMember = "item_code";
            //    #endregion

            //    #endregion
            //    #endregion
            //}

            //if (data == "S")
            //{
            //    #region "計算書の表の入力を呼び出し"
            //    DataTable dt19 = new DataTable();
            //    string str_document = "select * from statement_calc_data where document_number = '" + document + "';";
            //    adapter = new NpgsqlDataAdapter(str_document, conn);
            //    adapter.Fill(dt19);
            //    int st = dt19.Rows.Count;
            //    #endregion
            //    #region "計算書の表の外部の入力データを呼び出し"
            //    DataTable dt21 = new DataTable();
            //    string str_document1 = "select * from statement_data where document_number = '" + document + "';";
            //    adapter = new NpgsqlDataAdapter(str_document1, conn);
            //    adapter.Fill(dt21);
            //    DataRow row1;
            //    row1 = dt21.Rows[0];
            //    int types = (int)row1["type"];  //法人か個人か
            //    #region "法人"
            //    if (types == 0)
            //    {
            //        typeTextBox.Text = "法人";
            //        #region "入力する値"
            //        #region "顧客"
            //        this.companyTextBox.Text = row1["company_name"].ToString();
            //        this.shopNameTextBox.Text = row1["shop_name"].ToString();
            //        this.clientNameTextBox.Text = row1["name"].ToString();
            //        DataTable dt25 = new DataTable();
            //        string str_client = "select * from client_m where type = 0 and company_name = '" + this.companyTextBox.Text + "' and shop_name = '" + this.shopNameTextBox.Text + "' and name = '" + this.clientNameTextBox.Text + "';";
            //        adapter = new NpgsqlDataAdapter(str_client, conn);
            //        adapter.Fill(dt25);
            //        DataRow row2;
            //        row2 = dt25.Rows[0];
            //        this.antiqueLicenceTextBox.Text = row2["antique_license"].ToString();
            //        this.registerDateTextBox.Text = row2["registration_date"].ToString();
            //        this.clientRemarksTextBox.Text = row2["remarks"].ToString();
            //        #endregion
            //        #region "枠外"
            //        this.subTotal.Text = row1["sub_total"].ToString();
            //        subTotal.Text = string.Format("{0:C}", decimal.Parse(subTotal.Text, System.Globalization.NumberStyles.Number));
            //        int sum = int.Parse(row1["total"].ToString());
            //        this.sumTextBox.Text = row1["total"].ToString();
            //        sumTextBox.Text = string.Format("{0:C}", decimal.Parse(sumTextBox.Text, System.Globalization.NumberStyles.Number));
            //        this.taxAmount0.Text = row1["tax_amount"].ToString();
            //        taxAmount0.Text = string.Format("{0:C}", decimal.Parse(taxAmount0.Text, System.Globalization.NumberStyles.Number));
            //        this.paymentMethodsComboBox.SelectedItem = row1["payment_method"].ToString();
            //        this.deliveryComboBox.SelectedItem = row1["delivery_method"].ToString();
            //        /*this.totalCount.Text = row1["total_amount"].ToString();
            //        int cou = int.Parse(row1["total_amount"].ToString());
            //        totalCount.Text = string.Format("{0:#,0}", cou);
            //        decimal wei = decimal.Parse(row1["total_weight"].ToString());
            //        this.totalWeight.Text = row1["total_weight"].ToString();
            //        totalWeight.Text = string.Format("{0:#,0}", Math.Round(wei, 1, MidpointRounding.AwayFromZero));*/
            //        if (sum >= 2000000)
            //        {
            //            groupBox1.Show();
            //            groupBox1.BackColor = Color.OrangeRed;
            //        }
            //        #endregion
            //        #endregion
            //    }
            //    #endregion
            //    #region "個人"
            //    if (types == 1)
            //    {
            //        #region "入力する値"
            //        #region "顧客"
            //        label16.Text = "氏名";
            //        label17.Text = "生年月日";
            //        label18.Text = "職業";
            //        label38.Visible = false;
            //        registerDateTextBox.Visible = false;
            //        typeTextBox.Text = "個人";
            //        this.companyTextBox.Text = row1["name"].ToString();
            //        this.shopNameTextBox.Text = row1["birthday"].ToString();
            //        this.clientNameTextBox.Text = row1["occupation"].ToString();
            //        DataTable dt25 = new DataTable();
            //        string str_client = "select * from client_m where type = 1 and name = '" + this.companyTextBox.Text + "' and birthday = '" + this.shopNameTextBox.Text + "' and occupation = '" + this.clientNameTextBox.Text + "';";
            //        adapter = new NpgsqlDataAdapter(str_client, conn);
            //        adapter.Fill(dt25);
            //        DataRow row2;
            //        row2 = dt25.Rows[0];
            //        this.antiqueLicenceTextBox.Text = row2["antique_license"].ToString();
            //        this.clientRemarksTextBox.Text = row2["remarks"].ToString();
            //        #endregion
            //        #region "枠外"
            //        this.subTotal.Text = row1["sub_total"].ToString();
            //        subTotal.Text = string.Format("{0:C}", decimal.Parse(subTotal.Text, System.Globalization.NumberStyles.Number));
            //        this.sumTextBox.Text = row1["total"].ToString();
            //        int sum = int.Parse(row1["total"].ToString());
            //        sumTextBox.Text = string.Format("{0:C}", decimal.Parse(sumTextBox.Text, System.Globalization.NumberStyles.Number));
            //        this.taxAmount0.Text = row1["tax_amount"].ToString();
            //        taxAmount0.Text = string.Format("{0:C}", decimal.Parse(taxAmount0.Text, System.Globalization.NumberStyles.Number));
            //        this.paymentMethodsComboBox.SelectedItem = row1["payment_method"].ToString();
            //        this.deliveryComboBox.SelectedItem = row1["delivery_method"].ToString();
            //        /*this.totalCount.Text = row1["total_amount"].ToString();
            //        int cou = int.Parse(row1["total_amount"].ToString());
            //        totalCount.Text = string.Format("{0:#,0}", cou);
            //        decimal wei = decimal.Parse(row1["total_weight"].ToString());
            //        this.totalWeight.Text = row1["total_weight"].ToString();
            //        totalWeight.Text = string.Format("{0:#,0}", Math.Round(wei, 1, MidpointRounding.AwayFromZero));*/
            //        if (sum >= 2000000)
            //        {
            //            groupBox1.Show();
            //            groupBox1.BackColor = Color.OrangeRed;
            //        }
            //        #endregion
            //        #endregion
            //    }
            //    #endregion
            //    #endregion
            //    for (int St = 0; St <= (st - 1); St++)
            //    {
            //        #region"一時コメントアウト"
            //        //#region "1行目"
            //        //if (St == 0)
            //        //{
            //        //    DataTable dt22 = new DataTable();
            //        //    string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //        //    adapter = new NpgsqlDataAdapter(str_document2, conn);
            //        //    adapter.Fill(dt22);
            //        //    DataRow dataRow1;
            //        //    dataRow1 = dt22.Rows[0];
            //        //    int itemMainCategoryCode0 = (int)dataRow1["main_category_code"]; //大分類
            //        //    int itemCode0 = (int)dataRow1["item_code"];　　//品名
            //        //    #region "コンボボックス"
            //        //    #region "大分類"
            //        //    DataTable dt23 = new DataTable();
            //        //    string sql_document = "select * from main_category_m where invalid = 0;";
            //        //    adapter = new NpgsqlDataAdapter(sql_document, conn);
            //        //    adapter.Fill(dt23);
            //        //    mainCategoryComboBox0.DataSource = dt23;
            //        //    mainCategoryComboBox0.DisplayMember = "main_category_name";
            //        //    mainCategoryComboBox0.ValueMember = "main_category_code";
            //        //    mainCategoryComboBox0.SelectedIndex = 0;//担当者ごとの初期値設定
            //        //    mainCategoryComboBox0.SelectedValue = itemMainCategoryCode0;
            //        //    #endregion
            //        //    #region "品名"
            //        //    //品名検索用
            //        //    DataTable dt24 = new DataTable();
            //        //    string sql_item1 = "select * from item_m  where invalid = 0;";
            //        //    adapter = new NpgsqlDataAdapter(sql_item1, conn);
            //        //    adapter.Fill(dt24);
            //        //    itemComboBox0.DataSource = dt24;
            //        //    itemComboBox0.DisplayMember = "item_name";
            //        //    itemComboBox0.ValueMember = "item_code";
            //        //    itemComboBox0.SelectedValue = itemCode0;
            //        //    #endregion
            //        //    #endregion
            //        //    #region "入力された項目 1行目"
            //        //    this.weightTextBox0.Text = dataRow1["weight"].ToString();
            //        //    this.countTextBox0.Text = dataRow1["count"].ToString();
            //        //    this.unitPriceTextBox0.Text = dataRow1["unit_price"].ToString();
            //        //    unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
            //        //    this.moneyTextBox0.Text = dataRow1["amount"].ToString();
            //        //    moneyTextBox0.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox0.Text, System.Globalization.NumberStyles.Number));
            //        //    this.remarks0.Text = dataRow1["remarks"].ToString();
            //        //    #endregion
            //        //}
            //        //#endregion
            //        #endregion
            //        #region "一時コメントアウト"
            //        /*
            //        #region "2行目"
            //        if (St == 1)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode1 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode1 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt231 = new DataTable();
            //            string sql_document1 = "select * from main_category_m where invalid = 0;";
            //            adapter = new NpgsqlDataAdapter(sql_document1, conn);
            //            adapter.Fill(dt231);
            //            Column1.DataSource = dt231;
            //            Column1.DisplayMember = "main_category_name";
            //            Column1.ValueMember = "main_category_code";
            //            dataGridView1[0, 1].Value = itemMainCategoryCode1;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt241 = new DataTable();
            //            string sql_item2 = "select * from item_m  where invalid = 0;";
            //            adapter = new NpgsqlDataAdapter(sql_item2, conn);
            //            adapter.Fill(dt241);
            //            Column2.DataSource = dt241;
            //            Column2.DisplayMember = "item_name";
            //            Column2.ValueMember = "item_code";
            //            dataGridView1[1, 1].Value = itemCode1;
            //            #endregion
            //            #endregion

            //            #region "入力された項目 2行目"
            //            this.weightTextBox1.Text = dataRow1["weight"].ToString();
            //            this.countTextBox1.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox1.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox1.Text = dataRow1["amount"].ToString();
            //            moneyTextBox1.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox1.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks1.Text = dataRow1["remarks"].ToString();
            //            #endregion

            //        }
            //        #endregion
            //        */
            //        #endregion
            //        #region "一時コメントアウト"
            //        /*                    
            //        #region "3行目"
            //        if (St == 2)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode2 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode2 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt232 = new DataTable();
            //            string sql_document2 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document2, conn);
            //            adapter.Fill(dt232);
            //            mainCategoryComboBox2.DataSource = dt232;
            //            mainCategoryComboBox2.DisplayMember = "main_category_name";
            //            mainCategoryComboBox2.ValueMember = "main_category_code";
            //            mainCategoryComboBox2.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox2.SelectedValue = itemMainCategoryCode2;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt242 = new DataTable();
            //            string sql_item3 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item3, conn);
            //            adapter.Fill(dt242);
            //            itemComboBox2.DataSource = dt242;
            //            itemComboBox2.DisplayMember = "item_name";
            //            itemComboBox2.ValueMember = "item_code";
            //            itemComboBox2.SelectedValue = itemCode2;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 3行目"
            //            this.weightTextBox2.Text = dataRow1["weight"].ToString();
            //            this.countTextBox2.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox2.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox2.Text = dataRow1["amount"].ToString();
            //            moneyTextBox2.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox2.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks2.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "4行目"
            //        if (St == 3)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode3 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode3 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt233 = new DataTable();
            //            string sql_document3 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document3, conn);
            //            adapter.Fill(dt233);
            //            mainCategoryComboBox3.DataSource = dt233;
            //            mainCategoryComboBox3.DisplayMember = "main_category_name";
            //            mainCategoryComboBox3.ValueMember = "main_category_code";
            //            mainCategoryComboBox3.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox3.SelectedValue = itemMainCategoryCode3;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt243 = new DataTable();
            //            string sql_item4 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item4, conn);
            //            adapter.Fill(dt243);
            //            itemComboBox3.DataSource = dt243;
            //            itemComboBox3.DisplayMember = "item_name";
            //            itemComboBox3.ValueMember = "item_code";
            //            itemComboBox3.SelectedValue = itemCode3;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 4行目"
            //            this.weightTextBox3.Text = dataRow1["weight"].ToString();
            //            this.countTextBox3.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox3.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox3.Text = dataRow1["amount"].ToString();
            //            moneyTextBox3.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox3.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks3.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "5行目"
            //        if (St == 4)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document4 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document4, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode4 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode4 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt234 = new DataTable();
            //            string sql_document4 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document4, conn);
            //            adapter.Fill(dt234);
            //            mainCategoryComboBox4.DataSource = dt234;
            //            mainCategoryComboBox4.DisplayMember = "main_category_name";
            //            mainCategoryComboBox4.ValueMember = "main_category_code";
            //            mainCategoryComboBox4.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox4.SelectedValue = itemMainCategoryCode4;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt244 = new DataTable();
            //            string sql_item5 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item5, conn);
            //            adapter.Fill(dt244);
            //            itemComboBox4.DataSource = dt244;
            //            itemComboBox4.DisplayMember = "item_name";
            //            itemComboBox4.ValueMember = "item_code";
            //            itemComboBox4.SelectedValue = itemCode4;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 5行目"
            //            this.weightTextBox4.Text = dataRow1["weight"].ToString();
            //            this.countTextBox4.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox4.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox4.Text = dataRow1["amount"].ToString();
            //            moneyTextBox4.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox4.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks4.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "6行目"
            //        if (St == 5)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode5 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode5 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt235 = new DataTable();
            //            string sql_document5 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document5, conn);
            //            adapter.Fill(dt235);
            //            mainCategoryComboBox5.DataSource = dt235;
            //            mainCategoryComboBox5.DisplayMember = "main_category_name";
            //            mainCategoryComboBox5.ValueMember = "main_category_code";
            //            mainCategoryComboBox5.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox5.SelectedValue = itemMainCategoryCode5;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt245 = new DataTable();
            //            string sql_item6 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item6, conn);
            //            adapter.Fill(dt245);
            //            itemComboBox5.DataSource = dt245;
            //            itemComboBox5.DisplayMember = "item_name";
            //            itemComboBox5.ValueMember = "item_code";
            //            itemComboBox5.SelectedValue = itemCode5;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 6行目"
            //            this.weightTextBox5.Text = dataRow1["weight"].ToString();
            //            this.countTextBox5.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox5.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox5.Text = dataRow1["amount"].ToString();
            //            moneyTextBox5.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox5.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks5.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "7行目"
            //        if (St == 6)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode6 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode6 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt236 = new DataTable();
            //            string sql_document6 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document6, conn);
            //            adapter.Fill(dt236);
            //            mainCategoryComboBox6.DataSource = dt236;
            //            mainCategoryComboBox6.DisplayMember = "main_category_name";
            //            mainCategoryComboBox6.ValueMember = "main_category_code";
            //            mainCategoryComboBox6.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox6.SelectedValue = itemMainCategoryCode6;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt246 = new DataTable();
            //            string sql_item7 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item7, conn);
            //            adapter.Fill(dt246);
            //            itemComboBox6.DataSource = dt246;
            //            itemComboBox6.DisplayMember = "item_name";
            //            itemComboBox6.ValueMember = "item_code";
            //            itemComboBox6.SelectedValue = itemCode6;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 7行目"
            //            this.weightTextBox6.Text = dataRow1["weight"].ToString();
            //            this.countTextBox6.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox6.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox6.Text = dataRow1["amount"].ToString();
            //            moneyTextBox6.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox6.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks6.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "8行目"
            //        if (St == 7)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode7 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode7 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt237 = new DataTable();
            //            string sql_document7 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document7, conn);
            //            adapter.Fill(dt237);
            //            mainCategoryComboBox7.DataSource = dt237;
            //            mainCategoryComboBox7.DisplayMember = "main_category_name";
            //            mainCategoryComboBox7.ValueMember = "main_category_code";
            //            mainCategoryComboBox7.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox7.SelectedValue = itemMainCategoryCode7;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt247 = new DataTable();
            //            string sql_item8 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item8, conn);
            //            adapter.Fill(dt247);
            //            itemComboBox7.DataSource = dt247;
            //            itemComboBox7.DisplayMember = "item_name";
            //            itemComboBox7.ValueMember = "item_code";
            //            itemComboBox7.SelectedValue = itemCode7;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 8行目"
            //            this.weightTextBox7.Text = dataRow1["weight"].ToString();
            //            this.countTextBox7.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox7.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox7.Text = dataRow1["amount"].ToString();
            //            moneyTextBox7.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox7.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks7.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "9行目"
            //        if (St == 8)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode8 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode8 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt238 = new DataTable();
            //            string sql_document8 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document8, conn);
            //            adapter.Fill(dt238);
            //            mainCategoryComboBox8.DataSource = dt238;
            //            mainCategoryComboBox8.DisplayMember = "main_category_name";
            //            mainCategoryComboBox8.ValueMember = "main_category_code";
            //            mainCategoryComboBox8.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox8.SelectedValue = itemMainCategoryCode8;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt248 = new DataTable();
            //            string sql_item9 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item9, conn);
            //            adapter.Fill(dt248);
            //            itemComboBox8.DataSource = dt248;
            //            itemComboBox8.DisplayMember = "item_name";
            //            itemComboBox8.ValueMember = "item_code";
            //            itemComboBox8.SelectedValue = itemCode8;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 9行目"
            //            this.weightTextBox8.Text = dataRow1["weight"].ToString();
            //            this.countTextBox8.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox8.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox8.Text = dataRow1["amount"].ToString();
            //            moneyTextBox8.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox8.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks8.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "10行目"
            //        if (St == 9)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode9 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode9 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt239 = new DataTable();
            //            string sql_document9 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document9, conn);
            //            adapter.Fill(dt239);
            //            mainCategoryComboBox9.DataSource = dt239;
            //            mainCategoryComboBox9.DisplayMember = "main_category_name";
            //            mainCategoryComboBox9.ValueMember = "main_category_code";
            //            mainCategoryComboBox9.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox9.SelectedValue = itemMainCategoryCode9;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt249 = new DataTable();
            //            string sql_item10 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item10, conn);
            //            adapter.Fill(dt249);
            //            itemComboBox9.DataSource = dt249;
            //            itemComboBox9.DisplayMember = "item_name";
            //            itemComboBox9.ValueMember = "item_code";
            //            itemComboBox9.SelectedValue = itemCode9;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 10行目"
            //            this.weightTextBox9.Text = dataRow1["weight"].ToString();
            //            this.countTextBox9.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox9.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox9.Text = dataRow1["amount"].ToString();
            //            moneyTextBox9.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox9.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks9.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "11行目"
            //        if (St == 10)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode10 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode10 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt2310 = new DataTable();
            //            string sql_document10 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document10, conn);
            //            adapter.Fill(dt2310);
            //            mainCategoryComboBox10.DataSource = dt2310;
            //            mainCategoryComboBox10.DisplayMember = "main_category_name";
            //            mainCategoryComboBox10.ValueMember = "main_category_code";
            //            mainCategoryComboBox10.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox10.SelectedValue = itemMainCategoryCode10;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt2410 = new DataTable();
            //            string sql_item11 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item11, conn);
            //            adapter.Fill(dt2410);
            //            itemComboBox10.DataSource = dt2410;
            //            itemComboBox10.DisplayMember = "item_name";
            //            itemComboBox10.ValueMember = "item_code";
            //            itemComboBox10.SelectedValue = itemCode10;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 11行目"
            //            this.weightTextBox10.Text = dataRow1["weight"].ToString();
            //            this.countTextBox10.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox10.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox10.Text = dataRow1["amount"].ToString();
            //            moneyTextBox10.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox10.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks10.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "12行目"
            //        if (St == 11)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode11 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode11 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt2311 = new DataTable();
            //            string sql_document11 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document11, conn);
            //            adapter.Fill(dt2311);
            //            mainCategoryComboBox11.DataSource = dt2311;
            //            mainCategoryComboBox11.DisplayMember = "main_category_name";
            //            mainCategoryComboBox11.ValueMember = "main_category_code";
            //            mainCategoryComboBox11.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox11.SelectedValue = itemMainCategoryCode11;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt2411 = new DataTable();
            //            string sql_item12 = "select * from item_m  where invalid = 0;";
            //            adapter = new NpgsqlDataAdapter(sql_item12, conn);
            //            adapter.Fill(dt2411);
            //            itemComboBox11.DataSource = dt2411;
            //            itemComboBox11.DisplayMember = "item_name";
            //            itemComboBox11.ValueMember = "item_code";
            //            itemComboBox11.SelectedValue = itemCode11;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 12行目"
            //            this.weightTextBox11.Text = dataRow1["weight"].ToString();
            //            this.countTextBox11.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox11.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox1.Text = dataRow1["amount"].ToString();
            //            moneyTextBox11.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox11.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks11.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        #region "13行目"
            //        if (St == 12)
            //        {
            //            DataTable dt22 = new DataTable();
            //            string str_document2 = "select * from statement_calc_data where document_number = '" + document + "' and record_number = " + (St + 1) + ";";
            //            adapter = new NpgsqlDataAdapter(str_document2, conn);
            //            adapter.Fill(dt22);
            //            DataRow dataRow1;
            //            dataRow1 = dt22.Rows[0];
            //            int itemMainCategoryCode12 = (int)dataRow1["main_category_code"]; //大分類
            //            int itemCode12 = (int)dataRow1["item_code"];　　//品名
            //            #region "コンボボックス"
            //            #region "大分類"
            //            DataTable dt2312 = new DataTable();
            //            string sql_document12 = "select * from main_category_m where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_document12, conn);
            //            adapter.Fill(dt2312);
            //            mainCategoryComboBox12.DataSource = dt2312;
            //            mainCategoryComboBox12.DisplayMember = "main_category_name";
            //            mainCategoryComboBox12.ValueMember = "main_category_code";
            //            mainCategoryComboBox12.SelectedIndex = 0;//担当者ごとの初期値設定
            //            mainCategoryComboBox12.SelectedValue = itemMainCategoryCode12;
            //            #endregion
            //            #region "品名"
            //            //品名検索用
            //            DataTable dt2412 = new DataTable();
            //            string sql_item13 = "select * from item_m  where invalid = 0 ;";
            //            adapter = new NpgsqlDataAdapter(sql_item13, conn);
            //            adapter.Fill(dt2412);
            //            itemComboBox12.DataSource = dt2412;
            //            itemComboBox12.DisplayMember = "item_name";
            //            itemComboBox12.ValueMember = "item_code";
            //            itemComboBox12.SelectedValue = itemCode12;
            //            #endregion
            //            #endregion
            //            #region "入力された項目 13行目"
            //            this.weightTextBox12.Text = dataRow1["weight"].ToString();
            //            this.countTextBox12.Text = dataRow1["count"].ToString();
            //            this.unitPriceTextBox12.Text = dataRow1["unit_price"].ToString();
            //            unitPriceTextBox12.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox12.Text, System.Globalization.NumberStyles.Number));
            //            this.moneyTextBox12.Text = dataRow1["amount"].ToString();
            //            moneyTextBox12.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox12.Text, System.Globalization.NumberStyles.Number));
            //            this.remarks12.Text = dataRow1["remarks"].ToString();
            //            #endregion
            //        }
            //        #endregion
            //        */
            //        #endregion
            //    }
            //}
            //if (data == "D")
            //{
            //    #region"一時コメントアウト"
            //    //#region "納品書の表の入力を呼び出し"
            //    //DataTable dt20 = new DataTable();
            //    //string str_control = "select * from delivery_calc where control_number = " + control + ";";
            //    //adapter = new NpgsqlDataAdapter(str_control, conn);
            //    //adapter.Fill(dt20);
            //    //int de = dt20.Rows.Count;
            //    //#endregion
            //    //#region "納品書の表の外のデータを呼び出し"
            //    //DataTable dt21 = new DataTable();
            //    //string sql_control = "select * from delivery_m where control_number = " + control + ";";
            //    //adapter = new NpgsqlDataAdapter(sql_control, conn);
            //    //adapter.Fill(dt21);
            //    //DataRow row1;
            //    //row1 = dt21.Rows[0];
            //    //int type1 = (int)row1["types1"];
            //    //string yes = row1["seaal_print"].ToString();
            //    //if (type1 == 0)
            //    //{
            //    //    #region "枠外"
            //    //    this.name.Text = row1["name"].ToString();
            //    //    this.titleComboBox.SelectedItem = row1["honorific_title"].ToString();
            //    //    this.RemarkRegister.Text = row1["remarks2"].ToString();
            //    //    this.typeComboBox.SelectedItem = row1["type"].ToString();
            //    //    this.paymentMethodComboBox.SelectedItem = row1["payment_method"].ToString();
            //    //    this.PayeeTextBox1.Text = row1["account_payble"].ToString();
            //    //    this.CoinComboBox.SelectedItem = row1["currency"].ToString();
            //    //    this.comboBox11.SelectedItem = row1["vat"].ToString();
            //    //    if (yes == "する")
            //    //    {
            //    //        sealY.Checked = true;
            //    //    }
            //    //    if (yes == "しない")
            //    //    {
            //    //        sealN.Checked = true;
            //    //    }
            //    //    this.totalCount2.Text = row1["total_count"].ToString();
            //    //    totalCount2.Text = string.Format("{0:#,0}", this.totalCount2.Text);
            //    //    this.totalWeight2.Text = row1["total_weight"].ToString();
            //    //    totalWeight2.Text = string.Format("{0:#,0}", Math.Round(decimal.Parse(this.totalWeight2.Text), 1, MidpointRounding.AwayFromZero));
            //    //    this.sumTextBox2.Text = row1["total"].ToString();
            //    //    sumTextBox2.Text = string.Format("{0:C}", decimal.Parse(sumTextBox2.Text, System.Globalization.NumberStyles.Number));
            //    //    this.subTotal2.Text = row1["sub_total"].ToString();
            //    //    subTotal2.Text = string.Format("{0:C}", decimal.Parse(subTotal2.Text, System.Globalization.NumberStyles.Number));
            //    //    this.taxAmount2.Text = row1["vat_amount"].ToString();
            //    //    taxAmount2.Text = string.Format("{0:C}", decimal.Parse(taxAmount2.Text, System.Globalization.NumberStyles.Number));
            //    //    this.tax.Text = row1["vat_rate"].ToString() + ".00%";
            //    //    #endregion
            //    //    #region "顧客"
            //    //    int antique = (int)row1["antique_number"];
            //    //    typeTextBox2.Text = "法人";
            //    //    DataTable dt25 = new DataTable();
            //    //    string str_client = "select * from client_m_corporate where type = 0 and antique_number = " + antique + " ;";
            //    //    adapter = new NpgsqlDataAdapter(str_client, conn);
            //    //    adapter.Fill(dt25);
            //    //    DataRow row2;
            //    //    row2 = dt25.Rows[0];
            //    //    this.companyTextBox2.Text = row2["company_name"].ToString();
            //    //    this.shopNameTextBox2.Text = row2["shop_name"].ToString();
            //    //    this.clientNameTextBox2.Text = row2["name"].ToString();
            //    //    this.antiqueLicenceTextBox2.Text = row2["antique_license"].ToString();
            //    //    this.registerDateTextBox2.Text = row2["registration_date"].ToString();
            //    //    this.clientRemarksTextBox2.Text = row2["remarks"].ToString();
            //    //    #endregion
            //    //}
            //    //if (type1 == 1)
            //    //{
            //    //    typeTextBox2.Text = "個人";
            //    //    #region "枠外"
            //    //    this.name.Text = row1["name"].ToString();
            //    //    this.titleComboBox.SelectedItem = row1["honorific_title"].ToString();
            //    //    this.RemarkRegister.Text = row1["remarks2"].ToString();
            //    //    this.typeComboBox.SelectedItem = row1["type"].ToString();
            //    //    this.paymentMethodComboBox.SelectedItem = row1["payment_method"].ToString();
            //    //    this.PayeeTextBox1.Text = row1["account_payble"].ToString();
            //    //    this.CoinComboBox.SelectedItem = row1["currency"].ToString();
            //    //    this.comboBox11.SelectedItem = row1["vat"].ToString();
            //    //    if (yes == "する")
            //    //    {
            //    //        sealY.Checked = true;
            //    //    }
            //    //    if (yes == "しない")
            //    //    {
            //    //        sealN.Checked = true;
            //    //    }
            //    //    this.totalCount2.Text = row1["total_count"].ToString();
            //    //    totalCount2.Text = string.Format("{0:#,0}", this.totalCount2.Text);
            //    //    this.totalWeight2.Text = row1["total_weight"].ToString();
            //    //    totalWeight2.Text = string.Format("{0:#,0}", Math.Round(decimal.Parse(this.totalWeight2.Text), 1, MidpointRounding.AwayFromZero));
            //    //    this.sumTextBox2.Text = row1["total"].ToString();
            //    //    sumTextBox2.Text = string.Format("{0:C}", decimal.Parse(sumTextBox2.Text, System.Globalization.NumberStyles.Number));
            //    //    this.subTotal2.Text = row1["sub_total"].ToString();
            //    //    subTotal2.Text = string.Format("{0:C}", decimal.Parse(subTotal2.Text, System.Globalization.NumberStyles.Number));
            //    //    this.taxAmount2.Text = row1["vat_amount"].ToString();
            //    //    taxAmount2.Text = string.Format("{0:C}", decimal.Parse(taxAmount2.Text, System.Globalization.NumberStyles.Number));
            //    //    this.tax.Text = row1["vat_rate"].ToString() + ".00%";
            //    //    #endregion
            //    //    #region "顧客"
            //    //    typeTextBox2.Text = "個人";
            //    //    label75.Text = "氏名";
            //    //    label76.Text = "職業";
            //    //    label77.Text = "生年月日";
            //    //    label36.Visible = false;
            //    //    registerDateTextBox2.Visible = false;
            //    //    int idNumber = (int)row1["id_number"];
            //    //    typeTextBox2.Text = "法人";
            //    //    DataTable dt25 = new DataTable();
            //    //    string str_client = "select * from client_m_individual where type = 1 and id_number = " + idNumber + " ;";
            //    //    adapter = new NpgsqlDataAdapter(str_client, conn);
            //    //    adapter.Fill(dt25);
            //    //    DataRow row2;
            //    //    row2 = dt25.Rows[0];
            //    //    this.companyTextBox2.Text = row2["name"].ToString();
            //    //    this.shopNameTextBox2.Text = row2["birthday"].ToString();
            //    //    this.clientNameTextBox2.Text = row2["occupation"].ToString();
            //    //    this.antiqueLicenceTextBox2.Text = row2["antique_license"].ToString();
            //    //    this.clientRemarksTextBox2.Text = row2["remarks"].ToString();
            //    //    #endregion
            //    //}
            //    //#endregion

            //    //for (int De = 0; De <= (de - 1); De++)
            //    //{
            //    //    #region "1行目"
            //    //    if (De ==0)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode00 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode00 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt26 = new DataTable();
            //    //        string sql_control1 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control1, conn);
            //    //        adapter.Fill(dt26);
            //    //        mainCategoryComboBox00.DataSource = dt26;
            //    //        mainCategoryComboBox00.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox00.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox00.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox00.SelectedValue = itemMainCategoryCode00;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt27 = new DataTable();
            //    //        string sql_item1 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item1, conn);
            //    //        adapter.Fill(dt27);
            //    //        itemComboBox00.DataSource = dt27;
            //    //        itemComboBox00.DisplayMember = "item_name";
            //    //        itemComboBox00.ValueMember = "item_code";
            //    //        itemComboBox00.SelectedValue = itemCode00;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 1行目"
            //    //        this.weightTextBox00.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox00.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox00.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox00.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox00.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox00.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks00.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "2行目"
            //    //    if (De == 1)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode01 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode01 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt261 = new DataTable();
            //    //        string sql_control2 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control2, conn);
            //    //        adapter.Fill(dt261);
            //    //        mainCategoryComboBox01.DataSource = dt261;
            //    //        mainCategoryComboBox01.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox01.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox01.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox01.SelectedValue = itemMainCategoryCode01;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt271 = new DataTable();
            //    //        string sql_item2 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item2, conn);
            //    //        adapter.Fill(dt271);
            //    //        itemComboBox01.DataSource = dt271;
            //    //        itemComboBox01.DisplayMember = "item_name";
            //    //        itemComboBox01.ValueMember = "item_code";
            //    //        itemComboBox01.SelectedValue = itemCode01;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 2行目"
            //    //        this.weightTextBox01.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox01.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox01.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox01.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox01.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox01.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks01.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "3行目"
            //    //    if (De == 2)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode02 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode02 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt262 = new DataTable();
            //    //        string sql_control3 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control3, conn);
            //    //        adapter.Fill(dt262);
            //    //        mainCategoryComboBox02.DataSource = dt262;
            //    //        mainCategoryComboBox02.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox02.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox02.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox02.SelectedValue = itemMainCategoryCode02;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt272 = new DataTable();
            //    //        string sql_item3 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item3, conn);
            //    //        adapter.Fill(dt272);
            //    //        itemComboBox02.DataSource = dt272;
            //    //        itemComboBox02.DisplayMember = "item_name";
            //    //        itemComboBox02.ValueMember = "item_code";
            //    //        itemComboBox02.SelectedValue = itemCode02;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 3行目"
            //    //        this.weightTextBox02.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox02.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox02.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox02.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox02.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox02.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks02.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "4行目"
            //    //    if (De == 3)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode03 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode03 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt263 = new DataTable();
            //    //        string sql_control4 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control4, conn);
            //    //        adapter.Fill(dt263);
            //    //        mainCategoryComboBox03.DataSource = dt263;
            //    //        mainCategoryComboBox03.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox03.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox03.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox03.SelectedValue = itemMainCategoryCode03;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt273 = new DataTable();
            //    //        string sql_item4 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item4, conn);
            //    //        adapter.Fill(dt273);
            //    //        itemComboBox03.DataSource = dt273;
            //    //        itemComboBox03.DisplayMember = "item_name";
            //    //        itemComboBox03.ValueMember = "item_code";
            //    //        itemComboBox03.SelectedValue = itemCode03;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 4行目"
            //    //        this.weightTextBox03.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox03.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox03.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox03.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox03.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox03.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks03.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "5行目"
            //    //    if (De == 4)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode04 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode04 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt264 = new DataTable();
            //    //        string sql_control5 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control5, conn);
            //    //        adapter.Fill(dt264);
            //    //        mainCategoryComboBox04.DataSource = dt264;
            //    //        mainCategoryComboBox04.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox04.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox04.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox04.SelectedValue = itemMainCategoryCode04;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt274 = new DataTable();
            //    //        string sql_item4 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item4, conn);
            //    //        adapter.Fill(dt274);
            //    //        itemComboBox04.DataSource = dt274;
            //    //        itemComboBox04.DisplayMember = "item_name";
            //    //        itemComboBox04.ValueMember = "item_code";
            //    //        itemComboBox04.SelectedValue = itemCode04;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 5行目"
            //    //        this.weightTextBox04.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox04.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox04.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox04.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox04.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox04.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks04.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "6行目"
            //    //    if (De == 5)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode05 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode05 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt265 = new DataTable();
            //    //        string sql_control6 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control6, conn);
            //    //        adapter.Fill(dt265);
            //    //        mainCategoryComboBox05.DataSource = dt265;
            //    //        mainCategoryComboBox05.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox05.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox05.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox05.SelectedValue = itemMainCategoryCode05;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt275 = new DataTable();
            //    //        string sql_item6 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item6, conn);
            //    //        adapter.Fill(dt275);
            //    //        itemComboBox05.DataSource = dt275;
            //    //        itemComboBox05.DisplayMember = "item_name";
            //    //        itemComboBox05.ValueMember = "item_code";
            //    //        itemComboBox05.SelectedValue = itemCode05;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 6行目"
            //    //        this.weightTextBox05.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox05.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox05.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox05.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox05.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox05.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks05.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "7行目"
            //    //    if (De == 6)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode06 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode06 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt266 = new DataTable();
            //    //        string sql_control7 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control7, conn);
            //    //        adapter.Fill(dt266);
            //    //        mainCategoryComboBox06.DataSource = dt266;
            //    //        mainCategoryComboBox06.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox06.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox06.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox06.SelectedValue = itemMainCategoryCode06;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt276 = new DataTable();
            //    //        string sql_item7 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item7, conn);
            //    //        adapter.Fill(dt276);
            //    //        itemComboBox06.DataSource = dt276;
            //    //        itemComboBox06.DisplayMember = "item_name";
            //    //        itemComboBox06.ValueMember = "item_code";
            //    //        itemComboBox06.SelectedValue = itemCode06;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 7行目"
            //    //        this.weightTextBox06.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox06.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox06.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox06.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox06.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox06.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks06.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "8行目"
            //    //    if (De == 7)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode07 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode07 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt267 = new DataTable();
            //    //        string sql_control8 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control8, conn);
            //    //        adapter.Fill(dt267);
            //    //        mainCategoryComboBox07.DataSource = dt267;
            //    //        mainCategoryComboBox07.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox07.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox07.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox07.SelectedValue = itemMainCategoryCode07;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt277 = new DataTable();
            //    //        string sql_item8 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item8, conn);
            //    //        adapter.Fill(dt277);
            //    //        itemComboBox07.DataSource = dt277;
            //    //        itemComboBox07.DisplayMember = "item_name";
            //    //        itemComboBox07.ValueMember = "item_code";
            //    //        itemComboBox07.SelectedValue = itemCode07;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 8行目"
            //    //        this.weightTextBox07.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox07.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox07.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox07.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox07.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox07.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks07.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "9行目"
            //    //    if (De == 8)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode08 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode08 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt268 = new DataTable();
            //    //        string sql_control9 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control9, conn);
            //    //        adapter.Fill(dt268);
            //    //        mainCategoryComboBox08.DataSource = dt268;
            //    //        mainCategoryComboBox08.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox08.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox08.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox08.SelectedValue = itemMainCategoryCode08;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt278 = new DataTable();
            //    //        string sql_item9 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item9, conn);
            //    //        adapter.Fill(dt278);
            //    //        itemComboBox08.DataSource = dt278;
            //    //        itemComboBox08.DisplayMember = "item_name";
            //    //        itemComboBox08.ValueMember = "item_code";
            //    //        itemComboBox08.SelectedValue = itemCode08;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 9行目"
            //    //        this.weightTextBox08.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox08.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox08.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox08.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox08.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox08.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks08.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "10行目"
            //    //    if (De == 9)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode09 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode09 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt269 = new DataTable();
            //    //        string sql_control10 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control10, conn);
            //    //        adapter.Fill(dt269);
            //    //        mainCategoryComboBox09.DataSource = dt269;
            //    //        mainCategoryComboBox09.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox09.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox09.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox09.SelectedValue = itemMainCategoryCode09;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt279 = new DataTable();
            //    //        string sql_item10 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item10, conn);
            //    //        adapter.Fill(dt279);
            //    //        itemComboBox09.DataSource = dt279;
            //    //        itemComboBox09.DisplayMember = "item_name";
            //    //        itemComboBox09.ValueMember = "item_code";
            //    //        itemComboBox09.SelectedValue = itemCode09;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 10行目"
            //    //        this.weightTextBox09.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox09.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox09.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox09.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox09.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox09.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks09.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "11行目"
            //    //    if (De == 10)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode010 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode010 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt2610 = new DataTable();
            //    //        string sql_control11 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control11, conn);
            //    //        adapter.Fill(dt2610);
            //    //        mainCategoryComboBox010.DataSource = dt2610;
            //    //        mainCategoryComboBox010.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox010.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox010.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox010.SelectedValue = itemMainCategoryCode010;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt2710 = new DataTable();
            //    //        string sql_item11 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item11, conn);
            //    //        adapter.Fill(dt2710);
            //    //        itemComboBox010.DataSource = dt2710;
            //    //        itemComboBox010.DisplayMember = "item_name";
            //    //        itemComboBox010.ValueMember = "item_code";
            //    //        itemComboBox010.SelectedValue = itemCode010;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 11行目"
            //    //        this.weightTextBox010.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox010.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox010.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox010.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox010.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox010.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks010.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "12行目"
            //    //    if (De == 11)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode011 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode011 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt2611 = new DataTable();
            //    //        string sql_control12 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control12, conn);
            //    //        adapter.Fill(dt2611);
            //    //        mainCategoryComboBox011.DataSource = dt2611;
            //    //        mainCategoryComboBox011.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox011.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox011.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox011.SelectedValue = itemMainCategoryCode011;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt2711 = new DataTable();
            //    //        string sql_item12 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item12, conn);
            //    //        adapter.Fill(dt2711);
            //    //        itemComboBox011.DataSource = dt2711;
            //    //        itemComboBox011.DisplayMember = "item_name";
            //    //        itemComboBox011.ValueMember = "item_code";
            //    //        itemComboBox011.SelectedValue = itemCode011;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 12行目"
            //    //        this.weightTextBox011.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox011.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox011.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox011.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox011.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox011.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks011.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //    #region "13行目"
            //    //    if (De == 12)
            //    //    {
            //    //        DataTable dt25 = new DataTable();
            //    //        string str_control0 = "select * from delivery_calc where control_number = " + control + " and record_number = " + (De + 1) + ";";
            //    //        adapter = new NpgsqlDataAdapter(str_control0, conn);
            //    //        adapter.Fill(dt25);
            //    //        DataRow dataRow1;
            //    //        dataRow1 = dt25.Rows[0];
            //    //        int itemMainCategoryCode012 = (int)dataRow1["main_category_code"]; //大分類
            //    //        int itemCode012 = (int)dataRow1["item_code"];　　//品名
            //    //        #region "コンボボックス"
            //    //        #region "大分類"
            //    //        DataTable dt2612 = new DataTable();
            //    //        string sql_control13 = "select * from main_category_m where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_control13, conn);
            //    //        adapter.Fill(dt2612);
            //    //        mainCategoryComboBox012.DataSource = dt2612;
            //    //        mainCategoryComboBox012.DisplayMember = "main_category_name";
            //    //        mainCategoryComboBox012.ValueMember = "main_category_code";
            //    //        mainCategoryComboBox012.SelectedIndex = 0;//担当者ごとの初期値設定
            //    //        mainCategoryComboBox012.SelectedValue = itemMainCategoryCode012;
            //    //        #endregion
            //    //        #region "品名"
            //    //        //品名検索用
            //    //        DataTable dt2712 = new DataTable();
            //    //        string sql_item13 = "select * from item_m  where invalid = 0 ;";
            //    //        adapter = new NpgsqlDataAdapter(sql_item13, conn);
            //    //        adapter.Fill(dt2712);
            //    //        itemComboBox012.DataSource = dt2712;
            //    //        itemComboBox012.DisplayMember = "item_name";
            //    //        itemComboBox012.ValueMember = "item_code";
            //    //        itemComboBox012.SelectedValue = itemCode012;
            //    //        #endregion
            //    //        #endregion
            //    //        #region "入力された項目 13行目"
            //    //        this.weightTextBox012.Text = dataRow1["weight"].ToString();
            //    //        this.countTextBox012.Text = dataRow1["count"].ToString();
            //    //        this.unitPriceTextBox012.Text = dataRow1["unit_price"].ToString();
            //    //        unitPriceTextBox012.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox012.Text, System.Globalization.NumberStyles.Number));
            //    //        this.moneyTextBox012.Text = dataRow1["amount"].ToString();
            //    //        moneyTextBox012.Text = string.Format("{0:C}", decimal.Parse(moneyTextBox012.Text, System.Globalization.NumberStyles.Number));
            //    //        this.remarks012.Text = dataRow1["remarks"].ToString();
            //    //        #endregion
            //    //    }
            //    //    #endregion
            //    //}
            //    #endregion
            //}
            //if (subSum < 2000000)
            //{
            //    groupBox1.Hide();
            //}
            //else
            //{

            //}
            #endregion

            #region"datagridview"
            #region "初期値の取得（計算書）"
            #region "大分類の中身"
            string sql_str6 = "select * from main_category_m where invalid = 0 order by main_category_code asc;";
            adapter = new NpgsqlDataAdapter(sql_str6, conn);
            adapter.Fill(dt4);
            DataRow row4;
            row4 = dt4.Rows[0];
            MainCategoryCode = (int)row4["main_category_code"];
            #endregion
            #region "品名の中身"
            string sql_stritem = "select * from item_m where invalid = 0 and main_category_code = " + itemMainCategoryCode + ";";
            adapter = new NpgsqlDataAdapter(sql_stritem, conn);
            adapter.Fill(dt5);
            DataRow row5;
            row5 = dt5.Rows[0];
            itemCode = (int)row5["item_code"];
            #endregion
            #endregion

            //計算書
            dataGridView1[1, 0].Value = MainCategoryCode;
            dataGridView1[2, 0].Value = itemCode;

            MainCategoryColumn.DataSource = dt4;
            MainCategoryColumn.DisplayMember = "main_category_name";
            MainCategoryColumn.ValueMember = "main_category_code";

            dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[0].Cells[2];
            dataGridViewComboBoxCell.DataSource = dt5;
            dataGridViewComboBoxCell.DisplayMember = "item_name";
            dataGridViewComboBoxCell.ValueMember = "item_code";

            #region"初期値の取得（納品書）"
            #region "大分類の中身"
            string sql_MainItem = "select * from main_category_m where invalid = 0 order by main_category_code asc;";
            adapter = new NpgsqlDataAdapter(sql_MainItem, conn);
            adapter.Fill(dt6);
            DataRow row6;
            row6 = dt6.Rows[0];
            MainCategoryCode = (int)row6["main_category_code"];
            #endregion
            #region "品名の中身"
            string sql_Item = "select * from item_m where invalid = 0 and main_category_code = " + itemMainCategoryCode + ";";
            adapter = new NpgsqlDataAdapter(sql_Item, conn);
            adapter.Fill(dt7);
            DataRow row7;
            row7 = dt7.Rows[0];
            itemCode = (int)row7["item_code"];
            #endregion
            #endregion

            //納品書
            dataGridView2[1, 0].Value = MainCategoryCode;
            dataGridView2[2, 0].Value = itemCode;

            MainCategoryColumn1.DataSource = dt6;
            MainCategoryColumn1.DisplayMember = "main_category_name";
            MainCategoryColumn1.ValueMember = "main_category_code";

            dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 0];
            dataGridViewComboBoxCell.DataSource = dt7;
            dataGridViewComboBoxCell.DisplayMember = "item_name";
            dataGridViewComboBoxCell.ValueMember = "item_code";


            dataGridView1.Columns["WeightColumn"].DefaultCellStyle.Format = "n1";
            dataGridView1.Columns["UnitPriceColumn"].DefaultCellStyle.Format = "n0";
            dataGridView1.Columns["CountColumn"].DefaultCellStyle.Format = "n0";

            dataGridView2.Columns["WeightColumn1"].DefaultCellStyle.Format = "n1";
            dataGridView2.Columns["UnitPriceColumn1"].DefaultCellStyle.Format = "n0";
            dataGridView2.Columns["CountColumn1"].DefaultCellStyle.Format = "n0";
            #endregion


            #region"成績入力から計算書への遷移時"
            if (Grade != 0)
            {
                //該当する計算書の顧客番号取得
                sql = "select * from statement_data where document_number = '" + document + "';";
                cmd = new NpgsqlCommand(sql, conn);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClientCode = (int)reader["code"];
                    }
                }

                client_Button.Text = "顧客変更";
                client_searchButton1.Text = "顧客変更";

                //計算書枠外の情報を取得
                sql = "select* from statement_data A inner join client_m B on B.code = A.code left outer join revisions on upd_code = document_number where B.code = '" + ClientCode + "' and document_number = '" + document + "';";
                cmd = new NpgsqlCommand(sql, conn);

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        type = (int)reader["type"];
                        if (type == 0)  //法人のとき
                        {
                            #region"計算書の顧客情報（法人）"
                            typeTextBox.Text = "法人";
                            companyTextBox.Text = reader["company_name"].ToString();
                            shopNameTextBox.Text = reader["shop_name"].ToString();
                            clientNameTextBox.Text = reader["name"].ToString();
                            #endregion
                            #region"納品書の顧客情報（法人）"
                            typeTextBox2.Text = "法人";                                         //種別
                            companyTextBox2.Text = reader["company_name"].ToString();           //会社名
                            shopNameTextBox2.Text = reader["shop_name"].ToString();             //店舗名
                            clientNameTextBox2.Text = reader["name"].ToString();                //担当名
                            #endregion
                        }
                        else if (type == 1)     //個人のとき
                        {
                            #region"計算書の顧客情報（個人）"
                            label16.Text = "氏名";
                            label17.Text = "生年月日";
                            label18.Text = "職業";
                            typeTextBox.Text = "個人";
                            companyTextBox.Text = reader["name"].ToString();
                            shopNameTextBox.Text = reader["birthday"].ToString();
                            clientNameTextBox.Text = reader["occupation"].ToString();
                            label38.Visible = false;
                            //registerDateTextBox.Visible = false;
                            #endregion
                            #region"納品書の顧客情報（個人）"
                            typeTextBox2.Text = "個人";
                            label75.Text = "氏名";
                            label76.Text = "職業";
                            label77.Text = "生年月日";
                            clientNameTextBox2.Text = reader["occupation"].ToString();
                            companyTextBox2.Text = reader["name"].ToString();
                            shopNameTextBox2.Text = reader["birthday"].ToString();
                            label36.Visible = false;
                            //registerDateTextBox2.Visible = false;
                            #endregion
                        }
                        #region"計算書の顧客情報（法人・個人で共通）"
                        antiqueLicenceTextBox.Text = reader["antique_license"].ToString();
                        registerDateTextBox.Text = reader["registration_date"].ToString();
                        articlesTextBox.Text = reader["aol_financial_shareholder"].ToString();
                        taxCertificateTextBox.Text = reader["tax_certificate"].ToString();
                        sealCertificationTextBox.Text = reader["seal_certification"].ToString();
                        residenceCardTextBox.Text = reader["residence_card"].ToString();
                        residencePeriodStay = reader["period_stay"].ToString();
                        if (!string.IsNullOrEmpty(residencePeriodStay))
                        {
                            residencePerioddatetimepicker.Value = DateTime.Parse(residencePeriodStay);
                        }
                        clientRemarksTextBox.Text = reader["remarks"].ToString();
                        #endregion
                        #region"納品書の顧客情報（法人・個人で共通）"
                        registerDateTextBox2.Text = reader["registration_date"].ToString();             //登録日
                        clientRemarksTextBox2.Text = reader["remarks"].ToString();                      //備考
                        antiqueLicenceTextBox2.Text = reader["antique_license"].ToString();             //古物商許可証
                        #endregion
                        totalWeightTextBox.Text = ((decimal)reader["total_weight"]).ToString("n0");
                        totalCountTextBox.Text = ((int)reader["total_amount"]).ToString("n0");
                        SettlementDate = reader["settlement_date"].ToString();
                        if (!string.IsNullOrEmpty(SettlementDate))
                        {
                            settlementBox.Value = DateTime.Parse(SettlementDate);
                        }
                        paymentMethodsComboBox.Text = reader["payment_method"].ToString();
                        DeliveryDate = reader["delivery_date"].ToString();
                        if (!string.IsNullOrEmpty(DeliveryDate))
                        {
                            deliveryDateBox.Value = DateTime.Parse(DeliveryDate);
                        }
                        deliveryComboBox.Text = reader["delivery_method"].ToString();
                        subTotal.Text = ((decimal)reader["sub_total"]).ToString("c0");
                        sumTextBox.Text = ((decimal)reader["total"]).ToString("c0");
                        reasonTextBox.Text = reader["reason"].ToString();
                    }
                }

                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.RowsAdded -= DataGridView1_RowsAdded;
                dataGridView1.CellFormatting -= dataGridView1_CellFormatting;
                dataGridView1.ReadOnly = true;

                RemoveButton.Enabled = false;

                sql = "select B.main_category_name, C.item_name, A.detail, A.weight, A.unit_price, A.count, A.amount, A.remarks from statement_calc_data A inner join main_category_m B on B.main_category_code = A.main_category_code inner join item_m C on A.item_code = C.item_code" +
                    " where document_number = '" + document + "' order by record_number;";
                adapter = new NpgsqlDataAdapter(sql, conn);
                adapter.Fill(dtStatement);

                dataGridView1.DataSource = dtStatement;
                #region"ヘッダー名"
                dataGridView1.Columns[0].HeaderText = "大分類";
                dataGridView1.Columns[1].HeaderText = "品名";
                dataGridView1.Columns[2].HeaderText = "品物詳細";
                dataGridView1.Columns[3].HeaderText = "重量";
                dataGridView1.Columns[4].HeaderText = "単価";
                dataGridView1.Columns[5].HeaderText = "数量";
                dataGridView1.Columns[6].HeaderText = "金額";
                dataGridView1.Columns[7].HeaderText = "備考";
                #endregion

                #region"フォーマット処理"
                dataGridView1.Columns[3].DefaultCellStyle.Format = "n1";
                dataGridView1.Columns[4].DefaultCellStyle.Format = "n0";
                dataGridView1.Columns[5].DefaultCellStyle.Format = "n0";
                dataGridView1.Columns[6].DefaultCellStyle.Format = "c0";
                #endregion

                #region"datagridview の列幅"
                dataGridView1.Columns[0].Width = 120;
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].Width = 250;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 200;
                dataGridView1.Columns[5].Width = 60;
                dataGridView1.Columns[6].Width = 200;
                dataGridView1.Columns[7].Width = 300;
                #endregion

                #region"文字の右寄せ"
                dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                #endregion
            }
            #endregion

            #region"成績入力で納品書検索をした際"
            if (control != 0)
            {
                tabControl1.SelectedIndex = 1;
                client_Button.Text = "顧客変更";
                client_searchButton1.Text = "顧客変更";

                #region"納品書の表の下"

                //conn.Open();
                sql = "select * from delivery_m left outer join revisions on upd_code = cast(control_number as text) where control_number = '" + control + "';";
                cmd = new NpgsqlCommand(sql, conn);
                
                string clientCode = "";

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientCode = reader["code"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(clientCode))
                {
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documentNumberTextBox2.Text = reader["control_number"].ToString();
                            ClientCode = (int)reader["code"];
                            totalWeightTextBox1.Text = ((decimal)reader["total_weight"]).ToString("n1");
                            totalCountTextBox1.Text = ((int)reader["total_count"]).ToString("n0");
                            name.Text = reader["name"].ToString();
                            titleComboBox.Text = reader["honorific_title"].ToString();
                            typeComboBox.Text = reader["type"].ToString();
                            string sealPrint = reader["seaal_print"].ToString();
                            if (sealPrint == "する")
                            {
                                sealY.Checked = true;
                            }
                            else if (sealPrint == "しない")
                            {
                                sealN.Checked = true;
                            }
                            paymentMethodsComboBox.Text = reader["payment_method"].ToString();
                            PayeeTextBox1.Text = reader["account_payble"].ToString();
                            string orderDate = reader["order_date"].ToString();
                            if (!string.IsNullOrEmpty(orderDate))
                            {
                                orderDateTimePicker.Value = DateTime.Parse(orderDate);
                            }
                            string deliveryDate = reader["delivery_date"].ToString();
                            if (!string.IsNullOrEmpty(deliveryDate))
                            {
                                DeliveryDateTimePicker.Value = DateTime.Parse(deliveryDate);
                            }
                            string settlementDate = reader["settlement_date"].ToString();
                            if (!string.IsNullOrEmpty(settlementDate))
                            {
                                SettlementDateTimePicker.Value = DateTime.Parse(settlementDate);
                            }
                            CoinComboBox.Text = reader["currency"].ToString();
                            RemarkRegister.Text = reader["remark"].ToString();
                            textBox2.Text = reader["reason"].ToString();

                            subTotal2.Text = ((decimal)reader["sub_total"]).ToString("c0");
                            comboBox11.Text = reader["vat"].ToString();
                            tax.Text = ((int)reader["vat_rate"] / 100).ToString("P");
                            taxAmount = (decimal)reader["vat_amount"];
                            if (taxAmount != 0)
                            {
                                taxAmount2.Text = taxAmount.ToString();
                            }
                            else
                            {
                                taxAmount2.Text = "";
                            }
                            sumTextBox2.Text = ((decimal)reader["total"]).ToString("c0");
                        }
                    }
                }
                #endregion
                #region"納品書の顧客選択の下"
                sql = "select * from client_m where code = '" + ClientCode + "';";
                cmd = new NpgsqlCommand(sql, conn);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        type = (int)reader["type"];
                        if (type == 0)  //法人のとき
                        {
                            #region"計算書の顧客情報（法人）"
                            typeTextBox.Text = "法人";
                            companyTextBox.Text = reader["company_name"].ToString();
                            shopNameTextBox.Text = reader["shop_name"].ToString();
                            clientNameTextBox.Text = reader["name"].ToString();
                            #endregion
                            #region"納品書の顧客情報（法人）"
                            typeTextBox2.Text = "法人";                                         //種別
                            companyTextBox2.Text = reader["company_name"].ToString();           //会社名
                            shopNameTextBox2.Text = reader["shop_name"].ToString();             //店舗名
                            clientNameTextBox2.Text = reader["name"].ToString();                //担当名
                            #endregion
                        }
                        else if (type == 1)     //個人のとき
                        {
                            #region"計算書の顧客情報（個人）"
                            label16.Text = "氏名";
                            label17.Text = "生年月日";
                            label18.Text = "職業";
                            typeTextBox.Text = "個人";
                            companyTextBox.Text = reader["name"].ToString();
                            shopNameTextBox.Text = reader["birthday"].ToString();
                            clientNameTextBox.Text = reader["occupation"].ToString();
                            label38.Visible = false;
                            //registerDateTextBox.Visible = false;
                            #endregion
                            #region"納品書の顧客情報（個人）"
                            typeTextBox2.Text = "個人";
                            label75.Text = "氏名";
                            label76.Text = "職業";
                            label77.Text = "生年月日";
                            clientNameTextBox2.Text = reader["occupation"].ToString();
                            companyTextBox2.Text = reader["name"].ToString();
                            shopNameTextBox2.Text = reader["birthday"].ToString();
                            label36.Visible = false;
                            //registerDateTextBox2.Visible = false;
                            #endregion
                        }
                        #region"計算書の顧客情報（法人・個人で共通）"
                        antiqueLicenceTextBox.Text = reader["antique_license"].ToString();
                        registerDateTextBox.Text = reader["registration_date"].ToString();
                        articlesTextBox.Text = reader["aol_financial_shareholder"].ToString();
                        taxCertificateTextBox.Text = reader["tax_certificate"].ToString();
                        sealCertificationTextBox.Text = reader["seal_certification"].ToString();
                        residenceCardTextBox.Text = reader["residence_card"].ToString();
                        residencePeriodStay = reader["period_stay"].ToString();
                        if (!string.IsNullOrEmpty(residencePeriodStay))
                        {
                            residencePerioddatetimepicker.Value = DateTime.Parse(residencePeriodStay);
                        }
                        clientRemarksTextBox.Text = reader["remarks"].ToString();
                        #endregion
                        #region"納品書の顧客情報（法人・個人で共通）"
                        registerDateTextBox2.Text = reader["registration_date"].ToString();             //登録日
                        clientRemarksTextBox2.Text = reader["remarks"].ToString();                      //備考
                        antiqueLicenceTextBox2.Text = reader["antique_license"].ToString();             //古物商許可証
                        #endregion
                        //totalWeightTextBox1.Text = ((decimal)reader["total_weight"]).ToString("n0");
                        //totalCountTextBox1.Text = ((int)reader["total_amount"]).ToString("n0");
                        //SettlementDate = reader["settlement_date"].ToString();
                        //if (!string.IsNullOrEmpty(SettlementDate))
                        //{
                        //    SettlementDateTimePicker.Value = DateTime.Parse(SettlementDate);
                        //}
                        //paymentMethodsComboBox.Text = reader["payment_method"].ToString();
                        //DeliveryDate = reader["delivery_date"].ToString();
                        //if (!string.IsNullOrEmpty(DeliveryDate))
                        //{
                        //    deliveryDateBox.Value = DateTime.Parse(DeliveryDate);
                        //}
                        //deliveryComboBox.Text = reader["delivery_method"].ToString();
                        //subTotal.Text = ((decimal)reader["sub_total"]).ToString("c0");
                        //sumTextBox.Text = ((decimal)reader["total"]).ToString("c0");
                        //reasonTextBox.Text = reader["reason"].ToString();
                    }
                }
                #endregion
                #region"納品書の datagridview"

                dataGridView2.Columns.Clear();
                dataGridView2.Rows.Clear();
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.RowsAdded -= dataGridView2_RowsAdded;
                dataGridView2.CellFormatting -= dataGridView2_CellFormatting;
                dataGridView2.ReadOnly = true;

                sql = "select B.main_category_name, C.item_name, A.detail, A.weight, A.unit_price, A.count, A.amount, A.remarks from delivery_calc A inner join main_category_m B on B.main_category_code = A.main_category_code " +
                    "inner join item_m C on A.item_code = C.item_code where control_number = '" + control + "' order by record_number;";
                adapter = new NpgsqlDataAdapter(sql, conn);
                adapter.Fill(dtDelivery);

                dataGridView2.DataSource = dtDelivery;
                #region"ヘッダー名"
                dataGridView2.Columns[0].HeaderText = "大分類";
                dataGridView2.Columns[1].HeaderText = "品名";
                dataGridView2.Columns[2].HeaderText = "品物詳細";
                dataGridView2.Columns[3].HeaderText = "重量";
                dataGridView2.Columns[4].HeaderText = "単価";
                dataGridView2.Columns[5].HeaderText = "数量";
                dataGridView2.Columns[6].HeaderText = "金額";
                dataGridView2.Columns[7].HeaderText = "備考";
                #endregion

                #region"フォーマット処理"
                dataGridView2.Columns[3].DefaultCellStyle.Format = "n1";
                dataGridView2.Columns[4].DefaultCellStyle.Format = "n0";
                dataGridView2.Columns[5].DefaultCellStyle.Format = "n0";
                dataGridView2.Columns[6].DefaultCellStyle.Format = "c0";
                #endregion

                #region"datagridview の列幅"
                dataGridView2.Columns[0].Width = 120;
                dataGridView2.Columns[1].Width = 120;
                dataGridView2.Columns[2].Width = 250;
                dataGridView2.Columns[3].Width = 80;
                dataGridView2.Columns[4].Width = 200;
                dataGridView2.Columns[5].Width = 60;
                dataGridView2.Columns[6].Width = 200;
                dataGridView2.Columns[7].Width = 300;
                #endregion

                #region"文字の右寄せ"
                dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                #endregion

                #endregion
            }

            #endregion

            #region"コメントアウト"
            //#region "これから選択する場合"
            //if (count != 0)
            //{
            //    if (type == 0)
            //    {
            //        //顧客情報 法人
            //        #region "計算書"
            //        DataTable clientDt = new DataTable();
            //        string str_sql_corporate = "select * from client_m where invalid = 0 and type = 0 and name = '" + client_staff_name + "' and address = '" + address + "';";
            //        adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
            //        adapter.Fill(clientDt);

            //        DataRow row2;
            //        row2 = clientDt.Rows[0];
            //        //int type = (int)row2["type"];

            //        string companyNmae = row2["company_name"].ToString();
            //        string shopName = row2["shop_name"].ToString();
            //        string Staff_name = row2["name"].ToString();
            //        string register_date = row2["registration_date"].ToString();
            //        string remarks = row2["remarks"].ToString();
            //        string antique_license = row2["antique_license"].ToString();
            //        this.client_Button.Text = "顧客変更";
            //        typeTextBox.Text = "法人";                    //種別
            //        companyTextBox.Text = companyNmae;              //会社名   
            //        registerDateTextBox2.Text = antique_license;              //古物商許可証
            //        shopNameTextBox.Text = shopName;                    //店舗名
            //        clientNameTextBox.Text = Staff_name;                //担当名
            //        registerDateTextBox.Text = register_date;           //登録日
            //        clientRemarksTextBox.Text = remarks;                //備考
            //        #endregion
            //        #region "納品書"
            //        this.client_searchButton1.Text = "顧客変更";
            //        typeTextBox2.Text = "法人";                   //種別
            //        companyTextBox2.Text = companyNmae;             //会社名
            //        shopNameTextBox2.Text = shopName;               //店舗名
            //        clientNameTextBox2.Text = Staff_name;           //担当名
            //        registerDateTextBox2.Text = register_date;      //登録日
            //        clientRemarksTextBox2.Text = remarks;           //備考
            //        antiqueLicenceTextBox2.Text = antique_license;                //古物商許可証
            //        #endregion

            //    }
            //    else if (type == 1)
            //    {
            //        //顧客情報 個人
            //        DataTable clientDt = new DataTable();
            //        string str_sql_individual = "select * from client_m where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
            //        adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
            //        adapter.Fill(clientDt);

            //        DataRow row2;
            //        row2 = clientDt.Rows[0];

            //        string name = row2["name"].ToString();
            //        string register_date = row2["registration_date"].ToString();
            //        string remarks = row2["remarks"].ToString();
            //        string occupation = row2["occupation"].ToString();
            //        string birthday = row2["birthday"].ToString();
            //        string antique_license = row2["antique_license"].ToString();

            //        #region "計算書"
            //        label16.Text = "氏名";
            //        label17.Text = "生年月日";
            //        label18.Text = "職業";
            //        typeTextBox.Text = "個人";
            //        companyTextBox.Text = name;
            //        shopNameTextBox.Text = birthday;
            //        clientNameTextBox.Text = occupation;
            //        registerDateTextBox.Text = register_date;
            //        clientRemarksTextBox.Text = remarks;
            //        registerDateTextBox2.Text = antique_license;
            //        label38.Visible = false;
            //        registerDateTextBox.Visible = false;
            //        #endregion
            //        #region "納品書"
            //        typeTextBox2.Text = "個人";
            //        label75.Text = "氏名";
            //        label76.Text = "職業";
            //        label77.Text = "生年月日";
            //        clientNameTextBox2.Text = occupation;
            //        companyTextBox2.Text = name;
            //        shopNameTextBox2.Text = birthday;
            //        registerDateTextBox2.Text = register_date;
            //        clientRemarksTextBox2.Text = remarks;
            //        antiqueLicenceTextBox2.Text = antique_license;
            //        label36.Visible = false;
            //        registerDateTextBox2.Visible = false;
            //        #endregion
            //    }
            //}
            //#endregion
            //#region "１度選択して戻る場合"
            //else if (count == 0 && (address != null && client_staff_name != null) && data == null)
            //{
            //    if (type == 0)
            //    {
            //        //顧客情報 法人
            //        #region "計算書"
            //        DataTable clientDt = new DataTable();
            //        string str_sql_corporate = "select * from client_m where invalid = 0 and type = 0 and name = '" + client_staff_name + "' and address = '" + address + "';";
            //        adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
            //        adapter.Fill(clientDt);

            //        DataRow row2;
            //        row2 = clientDt.Rows[0];
            //        //int type = (int)row2["type"];

            //        string companyNmae = row2["company_name"].ToString();
            //        string shopName = row2["shop_name"].ToString();
            //        string Staff_name = row2["name"].ToString();
            //        string register_date = row2["registration_date"].ToString();
            //        string remarks = row2["remarks"].ToString();
            //        string antique_license = row2["antique_license"].ToString();
            //        this.client_Button.Text = "顧客変更";
            //        typeTextBox.Text = "法人";                    //種別
            //        companyTextBox.Text = companyNmae;              //会社名   
            //        registerDateTextBox2.Text = antique_license;              //古物商許可証
            //        shopNameTextBox.Text = shopName;                    //店舗名
            //        clientNameTextBox.Text = Staff_name;                //担当名
            //        registerDateTextBox.Text = register_date;           //登録日
            //        clientRemarksTextBox.Text = remarks;                //備考
            //        #endregion
            //        #region "納品書"
            //        this.client_searchButton1.Text = "顧客変更";
            //        typeTextBox2.Text = "法人";                     //種別
            //        companyTextBox2.Text = companyNmae;             //会社名
            //        shopNameTextBox2.Text = shopName;               //店舗名
            //        clientNameTextBox2.Text = Staff_name;           //担当名
            //        registerDateTextBox2.Text = register_date;      //登録日
            //        clientRemarksTextBox2.Text = remarks;           //備考
            //        antiqueLicenceTextBox2.Text = antique_license;                //古物商許可証
            //        #endregion

            //    }
            //    else if (type == 1)
            //    {
            //        //顧客情報 個人
            //        DataTable clientDt = new DataTable();
            //        string str_sql_individual = "select * from client_m where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
            //        adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
            //        adapter.Fill(clientDt);

            //        DataRow row2;
            //        row2 = clientDt.Rows[0];

            //        string name = row2["name"].ToString();
            //        string register_date = row2["registration_date"].ToString();
            //        string remarks = row2["remarks"].ToString();
            //        string occupation = row2["occupation"].ToString();
            //        string birthday = row2["birthday"].ToString();
            //        string antique_license = row2["antique_license"].ToString();

            //        #region "計算書"
            //        label16.Text = "氏名";
            //        label17.Text = "生年月日";
            //        label18.Text = "職業";
            //        typeTextBox.Text = "個人";
            //        companyTextBox.Text = name;
            //        shopNameTextBox.Text = birthday;
            //        clientNameTextBox.Text = occupation;
            //        registerDateTextBox.Text = register_date;
            //        clientRemarksTextBox.Text = remarks;
            //        registerDateTextBox2.Text = antique_license;
            //        label38.Visible = false;
            //        registerDateTextBox.Visible = false;
            //        #endregion

            //        #region "納品書"
            //        typeTextBox2.Text = "個人";
            //        label75.Text = "氏名";
            //        label76.Text = "職業";
            //        label77.Text = "生年月日";
            //        clientNameTextBox2.Text = occupation;
            //        companyTextBox2.Text = name;
            //        shopNameTextBox2.Text = birthday;
            //        registerDateTextBox2.Text = register_date;
            //        clientRemarksTextBox2.Text = remarks;
            //        antiqueLicenceTextBox2.Text = antique_license;
            //        label36.Visible = false;
            //        registerDateTextBox2.Visible = false;
            //        #endregion
            //    }
            //}
            //#endregion
            #endregion

            NotLoad = true;
            NotLoad1 = true;
            sealN.Checked = true;

            conn.Close();

        }


        private void returnButton_Click(object sender, EventArgs e)
        {
            if (data == "S")
            {
                this.Close();
            }
            else
            {
                MainMenu mainMenu = new MainMenu(topMenu, staff_id, pass, access_auth);
                screan = false;
                this.Close();
                mainMenu.Show();
            }

        }
        //納品書戻る
        private void return2_Click(object sender, EventArgs e)
        {
            if (data == "D")
            {
                this.Close();
            }
            else
            {
                MainMenu mainMenu = new MainMenu(topMenu, staff_id, pass, access_auth);
                screan = false;
                this.Close();
                mainMenu.Show();
            }
        }

        #region "顧客選択メニュー（計算書）"
        private void client_Button_Click(object sender, EventArgs e)//顧客選択メニュー（計算書）
        {
            client_search search2 = new client_search(this, staff_id, type, client_staff_name, address, total, number, document, access_auth, pass);
            this.data = search2.data;
            Properties.Settings.Default.Save();
            search2.ShowDialog();
            #region "これから選択する場合"
            if (count != 0)
            {
                if (type == 0)
                {
                    //顧客情報 法人
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m where invalid = 0 and type = 0 and code = '" + ClientCode + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    ArticleText = row2["aol_financial_shareholder"].ToString();
                    taxCertificateText = row2["tax_certificate"].ToString();
                    sealCertificateText = row2["seal_certification"].ToString();
                    residenceCardText = row2["residence_card"].ToString();
                    residencePeriodStay = row2["period_stay"].ToString();
                    //ClientCode = (int)row2["code"];
                    #region "計算書"
                    label16.Text = "会社名";
                    label17.Text = "店舗名";
                    label18.Text = "担当者名・個人名";
                    this.client_Button.Text = "顧客変更";
                    typeTextBox.Text = "法人";                    //種別
                    companyTextBox.Text = companyNmae;              //会社名   
                    antiqueLicenceTextBox.Text = antique_license;              //古物商許可証
                    shopNameTextBox.Text = shopName;                    //店舗名
                    clientNameTextBox.Text = Staff_name;                //担当名
                    registerDateTextBox.Text = register_date;           //登録日
                    clientRemarksTextBox.Text = remarks;                //備考
                    articlesTextBox.Text = ArticleText;                                                 //定款など
                    taxCertificateTextBox.Text = taxCertificateText;                                    //納税
                    sealCertificationTextBox.Text = sealCertificateText;                                //印鑑
                    residenceCardTextBox.Text = residenceCardText;                                      //在留カード
                    if (!string.IsNullOrEmpty(residencePeriodStay))
                    {
                        residencePerioddatetimepicker.Value = DateTime.Parse(residencePeriodStay);          //在留期限
                    }
                    #endregion
                    #region "納品書"
                    label75.Text = "会社名";
                    label77.Text = "店舗名";
                    label76.Text = "担当者名・個人名";
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
                    string str_sql_individual = "select * from client_m where invalid = 0 and type = 1 and code = '" + ClientCode + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    ArticleText = row2["aol_financial_shareholder"].ToString();
                    taxCertificateText = row2["tax_certificate"].ToString();
                    sealCertificateText = row2["seal_certification"].ToString();
                    residenceCardText = row2["residence_card"].ToString();
                    residencePeriodStay = row2["period_stay"].ToString();
                    //ClientCode = (int)row2["code"];

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
                    articlesTextBox.Text = ArticleText;                                                 //定款など
                    taxCertificateTextBox.Text = taxCertificateText;                                    //納税
                    sealCertificationTextBox.Text = sealCertificateText;                                //印鑑
                    residenceCardTextBox.Text = residenceCardText;                                      //在留カード
                    if (!string.IsNullOrEmpty(residencePeriodStay))
                    {
                        residencePerioddatetimepicker.Value = DateTime.Parse(residencePeriodStay);          //在留期限
                    }
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
                    string str_sql_corporate = "select * from client_m where invalid = 0 and type = 0 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    ClientCode = (int)row2["code"];
                    label16.Text = "会社名";
                    label17.Text = "店舗名";
                    label18.Text = "担当者名・個人名";
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
                    label75.Text = "会社名";
                    label77.Text = "店舗名";
                    label76.Text = "担当者名・個人名";
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
                    string str_sql_individual = "select * from client_m where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    ClientCode = (int)row2["code"];

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

        #region"計算書　登録ボタン"
        private void AddButton_Click(object sender, EventArgs e)        //計算書用登録ボタン
        {
            #region"確認事項"
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("顧客選択をしてください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (paymentMethodsComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("決済方法を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (deliveryComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("受け渡し方法を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (sub >= 2000000)
            {
                MessageBox.Show("200 万を超える取引です。" + "\r\n" + "入力不備がないか確認してください。", "入力内容の確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            DialogResult result = MessageBox.Show("計算書の登録をしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            #endregion

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            conn.Open();

            #region"計算書　枠外"
            //顧客番号は ClientCode、担当者コードは staff_id、身分は type、伝票番号は documentNumberTextBox
            if (!string.IsNullOrEmpty(totalWeightTextBox.Text))
            {
                TotalWeight = decimal.Parse(totalWeightTextBox.Text);
            }
            if (!string.IsNullOrEmpty(totalCountTextBox.Text))
            {
                TotalCount = decimal.Parse(totalCountTextBox.Text);
            }
            sum = sub;              //計算書は小計・合計ともに同じ変数を使用
            if (!string.IsNullOrEmpty(taxAmount0.Text))
            {
                TaxAmount = decimal.Parse(taxAmount0.Text.Substring(1));
            }
            else
            {
                TaxAmount = 0;
            }
            string DeliveyMethod = deliveryComboBox.Text;
            SettlementMethod = paymentMethodsComboBox.Text;
            SettlementDate = settlementBox.Text;
            DateTime settlementDate = DateTime.Parse(SettlementDate);
            DeliveryDate = deliveryDateBox.Text;
            DateTime date = DateTime.Now;
            DateTime AssessmentDate = date.Date;

            using (transaction = conn.BeginTransaction())
            {
                Sql = "update statement_data set (code, total_weight, total_amount, sub_total, tax_amount, total, delivery_method, payment_method, settlement_date, delivery_date, type, assessment_date)" +
                    " = ('" + ClientCode + "','" + TotalWeight + "','" + TotalCount + "','" + sum + "','" + TaxAmount + "','" + sum + "','" + DeliveyMethod + "','" + SettlementMethod + "','" + settlementDate + "','" + DeliveryDate + "','" + type + "','" + AssessmentDate + "')" +
                    "where document_number = '" + documentNumberTextBox.Text + "';";
                cmd = new NpgsqlCommand(Sql, conn);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }

            if (!string.IsNullOrEmpty(antiqueLicenceTextBox.Text))
            {
                AntiqueLicence = antiqueLicenceTextBox.Text;

                using (transaction = conn.BeginTransaction())
                {
                    if (type == 0)
                    {
                        string SQL_STR = @"update client_m set antique_license ='" + AntiqueLicence + "' where code = '" + ClientCode + "';";
                        cmd = new NpgsqlCommand(SQL_STR, conn);
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    else if (type == 1)
                    {
                        string SQL_STR = @"update client_m set antique_license ='" + AntiqueLicence + "'  where code = '" + ClientCode + "';";
                        cmd = new NpgsqlCommand(SQL_STR, conn);
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
            }

            if (remarks != clientRemarksTextBox.Text)
            {
                using (transaction = conn.BeginTransaction())
                {
                    string SQL_STR = @"update client_m set remarks = '" + remarks + "'where code = '" + ClientCode + "';";
                    cmd = new NpgsqlCommand(SQL_STR, conn);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }

            #region"200万以上の場合に画像更新"
            if (sub >= 2000000)
            {
                if (!string.IsNullOrEmpty(articlesTextBox.Text))
                {
                    AolFinancialShareholder = articlesTextBox.Text;

                    using (transaction = conn.BeginTransaction())
                    {
                        if (type == 0)
                        {
                            string SQL_STR = @"update client_m set aol_financial_shareholder='" + AolFinancialShareholder + "' where code = '" + ClientCode + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                        }
                        else if (type == 1)
                        {
                            string SQL_STR = @"update client_m set aol_financial_shareholder='" + AolFinancialShareholder + "' where code = '" + ClientCode + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(taxCertificateTextBox.Text))
                {
                    TaxCertification = taxCertificateTextBox.Text;
                    using (transaction = conn.BeginTransaction())
                    {
                        if (type == 0)
                        {
                            string SQL_STR = @"update client_m set tax_certificate='" + TaxCertification + "' where code = '" + ClientCode + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else if (type == 1)
                        {
                            string SQL_STR = @"update client_m set tax_certificate='" + TaxCertification + "'  where code = '" + ClientCode + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(sealCertificationTextBox.Text))
                {
                    SealCertification = sealCertificationTextBox.Text;
                    using (transaction = conn.BeginTransaction())
                    {
                        if (type == 0)
                        {
                            string SQL_STR = @"update client_m set seal_certification='" + SealCertification + "' where code = '" + ClientCode + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else if (type == 1)
                        {
                            string SQL_STR = @"update client_m set seal_certification='" + SealCertification + "' where code = '" + ClientCode + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(residenceCardTextBox.Text))
                {
                    ResidenceCard = residenceCardTextBox.Text;
                    ResidencePeriod = residencePerioddatetimepicker.Value.ToShortDateString();
                    using (transaction = conn.BeginTransaction())
                    {
                        if (type == 0)
                        {
                            string SQL_STR = @"update client_m set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "') where code = '" + ClientCode + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else if (type == 1)
                        {
                            string SQL_STR = @"update client_m set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "')  where code = '" + ClientCode + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }
            }
            #endregion

            #endregion
            #region"計算書　表のデータ"
            int index = dataGridView1.Rows.Count;

            #region"計算書　表のデータ　初回登録"
            if (index == 2)
            {
                index--;
            }

            if (dataGridView1.Rows[index - 1].IsNewRow)
            {
                index--;
            }


            for (int i = 0; i < index; i++)
            {
                #region"値の初期化"
                ItemDetail = "";
                Weight = 0;
                UnitPrice = 0;
                COUNT = 0;
                Money = 0;
                Remark = "";
                #endregion

                mainCategoryCode = int.Parse(dataGridView1[1, i].Value.ToString());

                itemCode = int.Parse(dataGridView1[2, i].Value.ToString());

                if (dataGridView1.Rows[i].Cells[3].Value != null && dataGridView1.Rows[i].Cells[3].Value.ToString() != "")
                {
                    ItemDetail = dataGridView1[3, i].Value.ToString();
                }

                if (dataGridView1.Rows[i].Cells[4].Value != null && dataGridView1.Rows[i].Cells[4].Value.ToString() != "")
                {
                    Weight = decimal.Parse(dataGridView1[4, i].Value.ToString());
                }

                if (dataGridView1.Rows[i].Cells[5].Value != null && dataGridView1.Rows[i].Cells[5].Value.ToString() != "")
                {
                    UnitPrice = decimal.Parse(dataGridView1[5, i].Value.ToString());
                }

                if (dataGridView1.Rows[i].Cells[6].Value != null && dataGridView1.Rows[i].Cells[6].Value.ToString() != "")
                {
                    COUNT = int.Parse(dataGridView1[6, i].Value.ToString());
                }

                if (dataGridView1.Rows[i].Cells[7].Value != null && dataGridView1.Rows[i].Cells[7].Value.ToString() != "")
                {
                    Money = decimal.Parse(dataGridView1[7, i].Value.ToString().Substring(1));
                }

                if (dataGridView1.Rows[i].Cells[8].Value != null && dataGridView1.Rows[i].Cells[8].Value.ToString() != "")
                {
                    Remark = dataGridView1[8, i].Value.ToString();
                }

                record = i + 1;

                Sql = "insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, detail, record_number)" +
                    " values ('" + mainCategoryCode + "','" + itemCode + "','" + Weight + "','" + COUNT + "','" + UnitPrice + "','" + Money + "','" + Remark + "','" + documentNumberTextBox.Text + "', '" + ItemDetail + "', '" + record + "');";
                cmd = new NpgsqlCommand(Sql, conn);
                cmd.ExecuteNonQuery();
            }
            #endregion
            #endregion

            conn.Close();
            MessageBox.Show("計算書の登録をしました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            addButton.Enabled = false;
            previewButton.Enabled = true;
            RecordListButton.Enabled=true;

            #region"一時コメントアウト"

            //    DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            //    if (dr == DialogResult.No)
            //    {
            //        return;
            //    }

            //    if (dr == DialogResult.Yes && subSum >= 2000000)
            //    {
            //        MessageBox.Show("200万以上の取引です。必要書類に不備がないか確認してください。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        DialogResult dialogResult = MessageBox.Show("入力不備がありましたか？", "入力確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //        if (dialogResult == DialogResult.Yes)
            //        {
            //            return;
            //        }
            //    }

            //    PostgreSQL postgre = new PostgreSQL();
            //    conn = postgre.connection();
            //    conn.Open();

            //    string DocumentNumber = documentNumberTextBox.Text;
            //    decimal TotalWeight = weisum;
            //    int Amount = countsum;
            //    decimal SubTotal = Math.Round(subSum, MidpointRounding.AwayFromZero);
            //    decimal Total = Math.Round(subSum, MidpointRounding.AwayFromZero);
            //    string SettlementDate = settlementBox.Text;
            //    string DeliveryDate = deliveryDateBox.Text;
            //    string DeliveryMethod = deliveryComboBox.Text;
            //    string PaymentMethod = paymentMethodsComboBox.Text;
            //    string Reason = this.textBox1.Text;

            //    int TYPE = 0;
            //    AntiqueNumber = 0;
            //    ID_Number = 0;
            //    string CompanyName = "";
            //    string ShopName = "";
            //    string StaffName = "";
            //    string Name = "";
            //    string Birthday = "";
            //    string TEL = "";
            //    string Work = "";

            //    if (typeTextBox.Text == "法人")
            //    {
            //        TYPE = 0;
            //        CompanyName = companyTextBox.Text;
            //        ShopName = shopNameTextBox.Text;
            //        StaffName = client_staff_name;

            //    }
            //    else if (typeTextBox.Text == "個人")
            //    {
            //        TYPE = 1;
            //        Name = companyTextBox.Text;
            //        Birthday = shopNameTextBox.Text;
            //        Work = clientNameTextBox.Text;
            //    }

            //    //古物番号、身分証番号、TEL の紐付け
            //    string NUMBER = "";

            //    #region"古物番号、身分証番号取得"
            //    using (transaction = conn.BeginTransaction())     
            //    {
            //        if (!string.IsNullOrEmpty(typeTextBox.Text)) {
            //            if (typeTextBox.Text == "法人")
            //            {
            //                NUMBER = @"select antique_number, phone_number from client_m_corporate where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                cmd = new NpgsqlCommand(NUMBER, conn);
            //                using (reader = cmd.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        AntiqueNumber = (int)reader["antique_number"];
            //                        TEL = reader["phone_number"].ToString();
            //                    }
            //                }
            //            }
            //            else if (typeTextBox.Text == "個人")
            //            {
            //                NUMBER = @"select id_number, phone_number from client_m_individual where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                cmd = new NpgsqlCommand(NUMBER, conn);
            //                using (reader = cmd.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        ID_Number = (int)reader["id_number"];
            //                        TEL = reader["phone_number"].ToString();
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("顧客選択をしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //    #endregion

            //    DataTable dt = new DataTable();
            //    DataTable revisiondt = new DataTable();
            //    #region"200万以上"
            //    if (subSum >= 2000000)
            //    {
            //        if (!string.IsNullOrEmpty(articlesTextBox.Text))
            //        {
            //            AolFinancialShareholder = articlesTextBox.Text;

            //            using (transaction = conn.BeginTransaction())
            //            {
            //                if (typeTextBox.Text == "法人")
            //                {
            //                    string SQL_STR = @"update client_m_corporate set aol_financial_shareholder='" + AolFinancialShareholder + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();

            //                }
            //                else if (typeTextBox.Text == "個人")
            //                {
            //                    string SQL_STR = @"update client_m_individual set aol_financial_shareholder='" + AolFinancialShareholder + "' where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(taxCertificateTextBox.Text))
            //        {
            //            TaxCertification = taxCertificateTextBox.Text;
            //            using (transaction = conn.BeginTransaction())
            //            {
            //                if (typeTextBox.Text == "法人")
            //                {
            //                    string SQL_STR = @"update client_m_corporate set tax_certificate='" + TaxCertification + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //                else if (typeTextBox.Text == "個人")
            //                {
            //                    string SQL_STR = @"update client_m_individual set tax_certificate='" + TaxCertification + "'  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(sealCertificationTextBox.Text))
            //        {
            //            SealCertification = sealCertificationTextBox.Text;
            //            using (transaction = conn.BeginTransaction())
            //            {
            //                if (typeTextBox.Text == "法人")
            //                {
            //                    string SQL_STR = @"update client_m_corporate set seal_certification='" + SealCertification + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //                else if (typeTextBox.Text == "個人")
            //                {
            //                    string SQL_STR = @"update client_m_individual set seal_certification='" + SealCertification + "' where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(residenceCardTextBox.Text))
            //        {
            //            ResidenceCard = residenceCardTextBox.Text;
            //            ResidencePeriod = residencePerioddatetimepicker.Value.ToShortDateString();
            //            using (transaction = conn.BeginTransaction())
            //            {
            //                if (typeTextBox.Text == "法人")
            //                {
            //                    string SQL_STR = @"update client_m_corporate set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "') where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //                else if (typeTextBox.Text == "個人")
            //                {
            //                    string SQL_STR = @"update client_m_individual set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "')  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(antiqueLicenceTextBox.Text))
            //        {
            //            AntiqueLicence = antiqueLicenceTextBox.Text;

            //            using (transaction = conn.BeginTransaction())
            //            {
            //                if (typeTextBox.Text == "法人")
            //                {
            //                    string SQL_STR = @"update client_m_corporate set antique_license ='" + AntiqueLicence + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //                else if (typeTextBox.Text == "個人")
            //                {
            //                    string SQL_STR = @"update client_m_individual set antique_license ='" + AntiqueLicence + "'  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
            //                    cmd = new NpgsqlCommand(SQL_STR, conn);
            //                    cmd.ExecuteReader();
            //                }
            //            }
            //        }

            //    }
            //    #endregion
            //    #region "再登録"
            //    if (regist1 >= 1 || data == "S")
            //    {                
            //        NpgsqlDataAdapter adapterStatement;
            //        DataTable StatementDt = new DataTable();
            //        string str_sql_re = "select * from statement_calc_data where document_number = '" + DocumentNumber + "';";
            //        adapterStatement = new NpgsqlDataAdapter(str_sql_re, conn);
            //        adapterStatement.Fill(StatementDt);
            //        Re = StatementDt.Rows.Count;
            //    }
            //    else
            //    {
            //        Re = 0;
            //    }

            //    if (regist1 != 0 && string.IsNullOrEmpty(textBox1.Text))
            //    {
            //        MessageBox.Show("理由を記入して下さい。");
            //        return;
            //    }
            //    else { }
            //    #endregion
            //    if (Re == 0)
            //    {
            //        DateTime dat1 = DateTime.Now;
            //        DateTime dtToday = dat1.Date;
            //        string c = dtToday.ToString("yyyy年MM月dd日");
            //        sql_str = "Insert into statement_data (antique_number, id_number, staff_code, total_weight, total_amount, sub_total, tax_amount, total, delivery_method, payment_method, settlement_date, delivery_date, document_number, company_name, shop_name, name, name, type, birthday, occupation, address, assessment_date) VALUES ('" + AntiqueNumber + "','" + ID_Number + "' , '" + staff_id + "' , '" + TotalWeight + "' ,  '" + Amount + "' , '" + SubTotal + "', '" + TaxAmount + "' , '" + Total + "' , '" + DeliveryMethod + "' , '" + PaymentMethod + "' , '" + SettlementDate + "' , '" + DeliveryDate + "', '" + DocumentNumber + "','" + CompanyName + "','" + ShopName + "','" + StaffName + "','" + Name + "','" + TYPE + "','" + Birthday + "','" + Work + "', '" + address + "','" + c + "');";
            //        sql_str3 = "Insert into statement_data_revisions (document_number, antique_number, id_number, staff_code, total_weight, total_amount, sub_total, tax_amount, total, settlement_date, delivery_date, delivery_method, payment_method, registration_date, insert_name ) VALUES ('" + DocumentNumber +  "','" + AntiqueNumber + "','" + ID_Number + "' , '" + staff_id + "' , '" + TotalWeight + "' ,  '" + Amount + "' , '" + SubTotal + "', '" + TaxAmount + "' , '" + Total + "' , '" + SettlementDate + "','" + DeliveryDate  +  "','" + DeliveryMethod + "' , '" + PaymentMethod + "' , '"  + c + "' , "  +  staff_id +  ");";
            //    }
            //    else
            //    {
            //        DateTime dat1 = DateTime.Now;
            //        DateTime dtToday = dat1.Date;
            //        string c = dtToday.ToString("yyyy年MM月dd日");
            //        sql_str = "UPDATE statement_data SET antique_number = " + AntiqueNumber + ", id_number = "+ ID_Number + ", staff_code = " + staff_id + ", total_weight = " + TotalWeight + " , total_amount = " + Amount + ", sub_total = " + SubTotal + ", tax_amount = " + TaxAmount + ", total = " + Total + ", delivery_method = '" + DeliveryMethod + "', payment_method = '" + PaymentMethod + "', settlement_date = '" + SettlementDate + "', delivery_date = '" + DeliveryDate + "',  company_name = '" + CompanyName + "', shop_name = '" + ShopName + "', name = '" + StaffName + "', name = '" + Name  + "', type = " + TYPE + ", birthday = '" + Birthday + "', occupation = '" + Work  + "', address = '" + address + "', assessment_date = '" + c + "' , reason = '" + Reason + "' Where document_number = '" + DocumentNumber + "';";
            //    }


            //    conn.Close();
            //    #region "1行目" 
            //    int record = 1;     //行数
            //    //int mainCategory = mainCategoryCode0;
            //    //int item = itemCode0;
            //   #region "大分類コード"
            //   DataTable maindt = new DataTable();

            //   //conn.ConnectionString = @"Server = 192.168.152.168; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            //   string sql_main = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox0.Text + "';";
            //   adapter = new NpgsqlDataAdapter(sql_main, conn);
            //   adapter.Fill(maindt);
            //   DataRow dataRow;
            //   dataRow = maindt.Rows[0];
            //   int mainCategory = (int)dataRow["main_category_code"];
            //   #endregion
            //   #region "品名コード"
            //   DataTable itemdt = new DataTable();

            //   //conn.ConnectionString = @"Server = 192.168.152.168; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            //   string sql_item = "select * from item_m where item_name = '" + itemComboBox0.Text + "';";
            //   adapter = new NpgsqlDataAdapter(sql_item, conn);
            //   adapter.Fill(itemdt);
            //   DataRow dataRow1;
            //   dataRow1 = itemdt.Rows[0];
            //   int item = (int)dataRow1["item_code"];
            //   #endregion

            //    string Detail = itemDetail0.Text;
            //    decimal Weight = decimal.Parse(weightTextBox0.Text);
            //    int Count = int.Parse(countTextBox0.Text);
            //    decimal UnitPrice = decimal.Parse(unitPriceTextBox0.Text);
            //    decimal amount = money0;
            //    string Remarks = remarks0.Text;



            //    DataTable dt2 = new DataTable();
            //    DataTable revisiondt2 = new DataTable();
            //    if (regist1 >= 1 || data == "S")
            //    {
            //        sql_str2 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "', reason = '" + Reason + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";
            //    }
            //    else
            //    {
            //        DateTime dat1 = DateTime.Now;
            //        DateTime dtToday = dat1.Date;
            //        string c = dtToday.ToString("yyyy年MM月dd日");
            //        sql_str2 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail )VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "');";
            //        sql_str4 = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "',"  + staff_id + ", '" + c + "','" + Detail + "');";
            //    }


            //    adapter = new NpgsqlDataAdapter(sql_str, conn);
            //    adapter.Fill(dt);

            //    adapter = new NpgsqlDataAdapter(sql_str2, conn);
            //    adapter.Fill(dt2);
            //    if (Re == 0)
            //    {
            //        adapter = new NpgsqlDataAdapter(sql_str3, conn);
            //        adapter.Fill(revisiondt);

            //        adapter = new NpgsqlDataAdapter(sql_str4, conn);
            //        adapter.Fill(revisiondt2);
            //    }
            //    else { }

            //    #endregion
            //    #region "一時コメントアウト"
            //    /*
            //    #region "2行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox1.Text) && !(unitPriceTextBox1.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 2)
            //        {
            //            record = 2;
            //            mainCategory = mainCategoryCode1;
            //            item = itemCode1;
            //            Detail = itemDetail1.Text;
            //            Weight = decimal.Parse(weightTextBox1.Text);
            //            Count = int.Parse(countTextBox1.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox1.Text);
            //            amount = money1;
            //            Remarks = remarks1.Text;

            //            string sql_str4 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str4, conn);
            //            adapter.Fill(dt4);
            //        }
            //        else
            //        {
            //            record = 2;
            //            mainCategory = mainCategoryCode1;
            //            item = itemCode1;
            //            Detail = itemDetail1.Text;
            //            Weight = decimal.Parse(weightTextBox1.Text);
            //            Count = int.Parse(countTextBox1.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox1.Text);
            //            amount = money1;
            //            Remarks = remarks1.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str4 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str4, conn);
            //            adapter.Fill(dt4);

            //            if (Re == 0)
            //            {
            //                string sql_str4_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str4_re, conn);
            //                adapter.Fill(redt4);
            //            }
            //            else { }
            //        }

            //    }
            //    #endregion
            //    #region "3行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox2.Text) && !(unitPriceTextBox2.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 3)
            //        {
            //            record = 3;
            //            mainCategory = mainCategoryCode2;
            //            item = itemCode2;
            //            Detail = itemDetail2.Text;
            //            Weight = decimal.Parse(weightTextBox2.Text);
            //            Count = int.Parse(countTextBox2.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox2.Text);
            //            amount = money2;
            //            Remarks = remarks2.Text;

            //            string sql_str5 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str5, conn);
            //            adapter.Fill(dt5);
            //        }
            //        else
            //        {
            //            record = 3;
            //            mainCategory = mainCategoryCode2;
            //            item = itemCode2;
            //            Detail = itemDetail2.Text;
            //            Weight = decimal.Parse(weightTextBox2.Text);
            //            Count = int.Parse(countTextBox2.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox2.Text);
            //            amount = money2;
            //            Remarks = remarks2.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str5 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str5, conn);
            //            adapter.Fill(dt5);

            //            if (Re == 0)
            //            {
            //                string sql_str5_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str5_re, conn);
            //                adapter.Fill(redt5);
            //            }
            //            else { }
            //        }

            //    }
            //    #endregion
            //    #region "4行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox3.Text) && !(unitPriceTextBox3.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 4)
            //        {
            //            record = 4;
            //            mainCategory = mainCategoryCode3;
            //            item = itemCode3;
            //            Detail = itemDetail3.Text;
            //            Weight = decimal.Parse(weightTextBox3.Text);
            //            Count = int.Parse(countTextBox3.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox3.Text);
            //            amount = money3;
            //            Remarks = remarks3.Text;

            //            string sql_str6 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str6, conn);
            //            adapter.Fill(dt6);
            //        }
            //        else
            //        {
            //            record = 4;
            //            mainCategory = mainCategoryCode3;
            //            item = itemCode3;
            //            Detail = itemDetail3.Text;
            //            Weight = decimal.Parse(weightTextBox3.Text);
            //            Count = int.Parse(countTextBox3.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox3.Text);
            //            amount = money3;
            //            Remarks = remarks3.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str6 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str6, conn);
            //            adapter.Fill(dt6);

            //            if (Re == 0)
            //            {
            //                string sql_str6_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str6_re, conn);
            //                adapter.Fill(redt6);
            //            }
            //            else { }                    
            //        }

            //    }
            //    #endregion
            //    #region "5行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox4.Text) && !(unitPriceTextBox4.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re != 0 && Re >= 5)
            //        {
            //            record = 5;
            //            mainCategory = mainCategoryCode4;
            //            item = itemCode4;
            //            Detail = itemDetail4.Text;
            //            Weight = decimal.Parse(weightTextBox4.Text);
            //            Count = int.Parse(countTextBox4.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox4.Text);
            //            amount = money4;
            //            Remarks = remarks4.Text;

            //            string sql_str7 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str7, conn);
            //            adapter.Fill(dt7);
            //        }
            //        else
            //        {
            //            record = 5;
            //            mainCategory = mainCategoryCode4;
            //            item = itemCode4;
            //            Detail = itemDetail4.Text;
            //            Weight = decimal.Parse(weightTextBox4.Text);
            //            Count = int.Parse(countTextBox4.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox4.Text);
            //            amount = money4;
            //            Remarks = remarks4.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str7 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str7, conn);
            //            adapter.Fill(dt7);

            //            if (Re == 0)
            //            {
            //                string sql_str7_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str7_re, conn);
            //                adapter.Fill(redt7);
            //            }
            //            else { }                   
            //        }

            //    }
            //    #endregion
            //    #region "6行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox5.Text) && !(unitPriceTextBox5.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 6)
            //        {
            //            record = 6;
            //            mainCategory = mainCategoryCode5;
            //            item = itemCode5;
            //            Detail = itemDetail5.Text;
            //            Weight = decimal.Parse(weightTextBox5.Text);
            //            Count = int.Parse(countTextBox5.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox5.Text);
            //            amount = money5;
            //            Remarks = remarks5.Text;

            //            string sql_str8 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str8, conn);
            //            adapter.Fill(dt8);
            //        }
            //        else
            //        {
            //            record = 6;
            //            mainCategory = mainCategoryCode5;
            //            item = itemCode5;
            //            Detail = itemDetail5.Text;
            //            Weight = decimal.Parse(weightTextBox5.Text);
            //            Count = int.Parse(countTextBox5.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox5.Text);
            //            amount = money5;
            //            Remarks = remarks5.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str8 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str8, conn);
            //            adapter.Fill(dt8);

            //            if (Re == 0)
            //            {
            //                string sql_str8_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str8_re, conn);
            //                adapter.Fill(redt8);
            //            }
            //            else { }                    
            //        }                
            //    }
            //    #endregion
            //    #region "7行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox6.Text) && !(unitPriceTextBox6.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 7)
            //        {
            //            record = 7;
            //            mainCategory = mainCategoryCode6;
            //            item = itemCode6;
            //            Detail = itemDetail6.Text;
            //            Weight = decimal.Parse(weightTextBox6.Text);
            //            Count = int.Parse(countTextBox6.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox6.Text);
            //            amount = money6;
            //            Remarks = remarks6.Text;

            //            string sql_str9 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str9, conn);
            //            adapter.Fill(dt9);
            //        }
            //        else
            //        {
            //            record = 7;
            //            mainCategory = mainCategoryCode6;
            //            item = itemCode6;
            //            Detail = itemDetail6.Text;
            //            Weight = decimal.Parse(weightTextBox6.Text);
            //            Count = int.Parse(countTextBox6.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox6.Text);
            //            amount = money6;
            //            Remarks = remarks6.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str9 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str9, conn);
            //            adapter.Fill(dt9);

            //            if (Re == 0)
            //            {
            //                string sql_str9_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str9_re, conn);
            //                adapter.Fill(redt9);
            //            }
            //            else { }                    
            //        }

            //    }
            //    #endregion
            //    #region "8行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox7.Text) && !(unitPriceTextBox7.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 8)
            //        {
            //            record = 8;
            //            mainCategory = mainCategoryCode7;
            //            item = itemCode7;
            //            Detail = itemDetail7.Text;
            //            Weight = decimal.Parse(weightTextBox7.Text);
            //            Count = int.Parse(countTextBox7.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox7.Text);
            //            amount = money7;
            //            Remarks = remarks7.Text;

            //            string sql_str10 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str10, conn);
            //            adapter.Fill(dt10);
            //        }
            //        else
            //        {
            //            record = 8;
            //            mainCategory = mainCategoryCode7;
            //            item = itemCode7;
            //            Detail = itemDetail7.Text;
            //            Weight = decimal.Parse(weightTextBox7.Text);
            //            Count = int.Parse(countTextBox7.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox7.Text);
            //            amount = money7;
            //            Remarks = remarks7.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str10 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str10, conn);
            //            adapter.Fill(dt10);

            //            if (Re == 0)
            //            {
            //                string sql_str10_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str10_re, conn);
            //                adapter.Fill(redt10);
            //            }
            //            else { }                    
            //        }

            //    }
            //    #endregion
            //    #region "9行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox8.Text) && !(unitPriceTextBox8.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 9)
            //        {
            //            record = 9;
            //            mainCategory = mainCategoryCode8;
            //            item = itemCode8;
            //            Detail = itemDetail8.Text;
            //            Weight = decimal.Parse(weightTextBox8.Text);
            //            Count = int.Parse(countTextBox8.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox8.Text);
            //            amount = money8;
            //            Remarks = remarks8.Text;

            //            string sql_str11 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str11, conn);
            //            adapter.Fill(dt11);
            //        }
            //        else
            //        {
            //            record = 9;
            //            mainCategory = mainCategoryCode8;
            //            item = itemCode8;
            //            Detail = itemDetail8.Text;
            //            Weight = decimal.Parse(weightTextBox8.Text);
            //            Count = int.Parse(countTextBox8.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox8.Text);
            //            amount = money8;
            //            Remarks = remarks8.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str11 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str11, conn);
            //            adapter.Fill(dt11);

            //            if (Re == 0)
            //            {
            //                string sql_str11_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str11_re, conn);
            //                adapter.Fill(redt11);
            //            }
            //            else { }

            //        }               
            //    }
            //    #endregion
            //    #region "10行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox9.Text) && !(unitPriceTextBox9.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 10)
            //        {
            //            record = 10;
            //            mainCategory = mainCategoryCode9;
            //            item = itemCode9;
            //            Detail = itemDetail9.Text;
            //            Weight = decimal.Parse(weightTextBox9.Text);
            //            Count = int.Parse(countTextBox9.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox9.Text);
            //            amount = money9;
            //            Remarks = remarks9.Text;

            //            string sql_str12 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str12, conn);
            //            adapter.Fill(dt12);
            //        }
            //        else
            //        {
            //            record = 10;
            //            mainCategory = mainCategoryCode9;
            //            item = itemCode9;
            //            Detail = itemDetail9.Text;
            //            Weight = decimal.Parse(weightTextBox9.Text);
            //            Count = int.Parse(countTextBox9.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox9.Text);
            //            amount = money9;
            //            Remarks = remarks9.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str12 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "','" + Detail + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str12, conn);
            //            adapter.Fill(dt12);

            //            if (Re == 0)
            //            {
            //                string sql_str12_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str12_re, conn);
            //                adapter.Fill(redt12);
            //            }
            //            else { }                    
            //        }                
            //    }
            //    #endregion
            //    #region "11行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox10.Text) && !(unitPriceTextBox10.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 11)
            //        {
            //            record = 11;
            //            mainCategory = mainCategoryCode10;
            //            item = itemCode10;
            //            Detail = itemDetail10.Text;
            //            Weight = decimal.Parse(weightTextBox10.Text);
            //            Count = int.Parse(countTextBox10.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox10.Text);
            //            amount = money10;
            //            Remarks = remarks10.Text;

            //            string sql_str13 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str13, conn);
            //            adapter.Fill(dt13);
            //        }
            //        else
            //        {
            //            record = 11;
            //            mainCategory = mainCategoryCode10;
            //            item = itemCode10;
            //            Detail = itemDetail10.Text;
            //            Weight = decimal.Parse(weightTextBox10.Text);
            //            Count = int.Parse(countTextBox10.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox10.Text);
            //            amount = money10;
            //            Remarks = remarks10.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str13 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str13, conn);
            //            adapter.Fill(dt13);

            //            if (Re == 0)
            //            {
            //                string sql_str13_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str13_re, conn);
            //                adapter.Fill(redt13);
            //            }
            //            else { }                    
            //        }                
            //    }
            //    #endregion
            //    #region "12行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox11.Text) && !(unitPriceTextBox11.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 12)
            //        {
            //            record = 12;
            //            mainCategory = mainCategoryCode11;
            //            item = itemCode11;
            //            Detail = itemDetail11.Text;
            //            Weight = decimal.Parse(weightTextBox11.Text);
            //            Count = int.Parse(countTextBox11.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox11.Text);
            //            amount = money11;
            //            Remarks = remarks11.Text;

            //            string sql_str14 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";

            //            adapter = new NpgsqlDataAdapter(sql_str14, conn);
            //            adapter.Fill(dt14);
            //        }
            //        else
            //        {
            //            record = 12;
            //            mainCategory = mainCategoryCode11;
            //            item = itemCode11;
            //            Detail = itemDetail11.Text;
            //            Weight = decimal.Parse(weightTextBox11.Text);
            //            Count = int.Parse(countTextBox11.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox11.Text);
            //            amount = money11;
            //            Remarks = remarks11.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str14 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

            //            adapter = new NpgsqlDataAdapter(sql_str14, conn);
            //            adapter.Fill(dt14);

            //            if (Re == 0)
            //            {
            //                string sql_str14_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str14_re, conn);
            //                adapter.Fill(redt14);
            //            }
            //            else { }                    
            //        }                
            //    }
            //    #endregion
            //    #region "13行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox12.Text) && !(unitPriceTextBox12.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 13)
            //        {
            //            record = 13;
            //            mainCategory = mainCategoryCode12;
            //            item = itemCode12;
            //            Detail = itemDetail12.Text;
            //            Weight = decimal.Parse(weightTextBox12.Text);
            //            Count = int.Parse(countTextBox12.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox12.Text);
            //            amount = money12;
            //            Remarks = remarks12.Text;

            //            string sql_str15 = "UPDATE statement_calc_data SET  main_category_code = " + mainCategory + ", item_code = " + item + ",weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + ", amount = " + amount + ", remarks = '" + Remarks + "', detail = '" + Detail + "' where document_number = '" + DocumentNumber + "' and record_number = " + record + "; ";
            //            adapter = new NpgsqlDataAdapter(sql_str15, conn);
            //            adapter.Fill(dt15);
            //        }
            //        else
            //        {
            //            record = 13;
            //            mainCategory = mainCategoryCode12;
            //            item = itemCode12;
            //            Detail = itemDetail12.Text;
            //            Weight = decimal.Parse(weightTextBox12.Text);
            //            Count = int.Parse(countTextBox12.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox12.Text);
            //            amount = money12;
            //            Remarks = remarks12.Text;
            //            DateTime dat1 = DateTime.Now;
            //            DateTime dtToday = dat1.Date;
            //            string c = dtToday.ToString("yyyy年MM月dd日");

            //            string sql_str15 = "Insert into statement_calc_data (main_category_code, item_code, weight, count, unit_price, amount, remarks, document_number, record_number, detail ) VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";
            //            adapter = new NpgsqlDataAdapter(sql_str15, conn);
            //            adapter.Fill(dt15);

            //            if (Re == 0)
            //            {
            //                string sql_str15_re = "Insert into statement_calc_data_revisions VALUES ( '" + DocumentNumber + "'," + record + "," + mainCategory + "," + item + ", " + Weight + "," + Count + "," + UnitPrice + "," + amount + ", '" + Remarks + "'," + staff_id + ", '" + c + "','" + Detail + "');";
            //                adapter = new NpgsqlDataAdapter(sql_str15_re, conn);
            //                adapter.Fill(redt15);
            //            }
            //            else { }
            //        }                
            //    }
            //    #endregion
            //    */
            //    #endregion
            //    conn.Close();
            //    MessageBox.Show("登録しました。");
            //    this.previewButton.Enabled = true;
            //    this.RecordListButton.Enabled = true;
            //    this.label9.Visible = true;
            //    this.textBox1.Visible = true;
            //    if (access_auth == "C")
            //    {
            //        this.addButton.Enabled = false;
            //        this.label9.Visible = false;
            //        this.textBox1.Visible = false;
            //    }
            //    regist1++;
            #endregion
        }
        #endregion

        #region"一時コメントアウト（計算書、買取販売や成績入力から戻ってきたときの以前の処理）"
        //#region "計算書　フォーカス時初期状態ならnull"
        //private void unitPriceTextBox0_Enter(object sender, EventArgs e)
        //{
        //    if (data == "S" || Grade != 0)
        //    {

        //    }
        //    else
        //    {
        //        /*if (unitPriceTextBox0.Text.ToString() == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox0.Text = "";
        //        }*/
        //    }                        
        //}        
        //#endregion

        //#region"計算書　単価３桁区切り＋フォーカスが外れた時"
        //private void unitPriceTextBox0_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力画面"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox0.Text))
        //        {
        //            unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
        //            if (!string.IsNullOrEmpty(weightTextBox0.Text) && !(countTextBox0.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money0 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox0.ReadOnly = true;
        //            }
        //            else if (!(weightTextBox0.Text == "0") && !string.IsNullOrEmpty(countTextBox0.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money0 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox0.ReadOnly = true;
        //            }
        //            else { }
        //        }
        //    }
        //    #endregion
        //    //else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox0.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox0.Text, @"^[a-zA-Z]+$"))
        //    //{
        //    //    MessageBox.Show("正しく入力して下さい。");
        //    //    return;
        //    //}
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
        //        {
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox0.Text))
        //        {
        //            unitPrice = int.Parse(unitPriceTextBox0.Text);
        //            unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
        //            if (!string.IsNullOrEmpty(weightTextBox0.Text) && string.IsNullOrEmpty(countTextBox0.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
        //                //weightTextBox0.ReadOnly = true;
        //            }
        //            else if (string.IsNullOrEmpty(weightTextBox0.Text) && !string.IsNullOrEmpty(countTextBox0.Text))
        //            {
        //                sub = decimal.Parse(countTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
        //                //countTextBox0.ReadOnly = true;
        //            }
        //            else if(string.IsNullOrEmpty(countTextBox0.Text)&&string.IsNullOrEmpty(weightTextBox0.Text))
        //            {
        //                return;
        //            }
        //            sub1 = sub + sub * Tax / 100;
        //            money0 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //            moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //        }

        //    }

        //}

        //#region "一時コメントアウト"
        ///*
        //#region "2行目"
        //private void unitPriceTextBox1_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox1.Text))
        //        {
        //            unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox2.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox1.Text) && !(countTextBox1.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox1.ReadOnly = true;
        //                money1 = sub;
        //            }
        //            else if (!(weightTextBox1.Text == "0") && !string.IsNullOrEmpty(countTextBox1.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox1.ReadOnly = true;
        //                money1 = sub;
        //            }
        //            else { }

        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox0.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox0.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox1.Text))
        //        {
        //            unitPriceTextBox1.Text = "単価 -> 重量 or 数量";

        //        }
        //        else if(!string.IsNullOrEmpty(unitPriceTextBox2.Text))
        //        {
        //            unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox2.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox1.Text) && !(countTextBox1.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox1.ReadOnly = true;
        //                money1 = sub;
        //            }
        //            else if (!(weightTextBox1.Text == "0") && !string.IsNullOrEmpty(countTextBox1.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox1.ReadOnly = true;
        //                money1 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox2.ReadOnly = false;
        //            unitPriceTextBox2.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "3行目"
        //private void unitPriceTextBox2_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox2.Text))
        //        {
        //            unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox3.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox2.Text) && !(countTextBox2.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox2.ReadOnly = true;
        //                money2 = sub;
        //            }
        //            else if (!(weightTextBox2.Text == "0") && !string.IsNullOrEmpty(countTextBox2.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox2.ReadOnly = true;
        //                money2 = sub;
        //            }
        //            else { }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox1.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox1.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox2.Text))
        //        {
        //            unitPriceTextBox2.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox3.Text))
        //        {
        //            unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox3.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox2.Text) && !(countTextBox2.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox2.ReadOnly = true;
        //                money2 = sub;
        //            }
        //            else if (!(weightTextBox2.Text == "0") && !string.IsNullOrEmpty(countTextBox2.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox2.ReadOnly = true;
        //                money2 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox3.ReadOnly = false;
        //            unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }

        //}
        //#endregion
        //#region "4行目"
        //private void unitPriceTextBox3_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox3.Text))
        //        {
        //            unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox4.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox3.Text) && !(countTextBox3.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox3.Text) * decimal.Parse(unitPriceTextBox03.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox3.ReadOnly = true;
        //                money3 = sub;
        //            }
        //            else if (!(weightTextBox3.Text == "0") && !string.IsNullOrEmpty(countTextBox3.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox3.ReadOnly = true;
        //                money3 = sub;
        //            }
        //            else { }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox2.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox2.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox3.Text))
        //        {
        //            unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox4.Text))
        //        {
        //            unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox4.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox3.Text) && !(countTextBox3.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox3.Text) * decimal.Parse(unitPriceTextBox03.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox3.ReadOnly = true;
        //                money3 = sub;
        //            }
        //            else if (!(weightTextBox3.Text == "0") && !string.IsNullOrEmpty(countTextBox3.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox3.ReadOnly = true;
        //                money3 = sub;
        //            }
        //            else
        //            {

        //            }

        //        }
        //        else
        //        {
        //            unitPriceTextBox4.ReadOnly = false;
        //            unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "5行目"
        //private void unitPriceTextBox4_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or　成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox4.Text))
        //        {
        //            unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox5.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox4.Text) && !(countTextBox4.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money4 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox4.ReadOnly = true;
        //                money4 = sub;
        //            }
        //            else if (!(weightTextBox4.Text == "0") && !string.IsNullOrEmpty(countTextBox4.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
        //                moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox4.ReadOnly = true;
        //                money4 = sub;
        //            }
        //            else { }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox3.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox3.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox4.Text))
        //        {
        //            unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox5.Text))
        //        {
        //            unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox5.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox4.Text) && !(countTextBox4.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money4 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox4.ReadOnly = true;
        //                money4 = sub;
        //            }
        //            else if (!(weightTextBox4.Text == "0") && !string.IsNullOrEmpty(countTextBox4.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
        //                moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox4.ReadOnly = true;
        //                money4 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox5.ReadOnly = false;
        //            unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "6行目"
        //private void unitPriceTextBox5_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox5.Text))
        //        {
        //            unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox6.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox5.Text) && !(countTextBox5.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
        //                moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox5.ReadOnly = true;
        //                money5 = sub;
        //            }
        //            else if (!(weightTextBox5.Text == "0") && !string.IsNullOrEmpty(countTextBox5.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money5 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox5.ReadOnly = true;
        //                money5 = sub;
        //            }
        //            else { }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox4.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox4.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox5.Text))
        //        {
        //            unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox6.Text))
        //        {
        //            unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox6.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox5.Text) && !(countTextBox5.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
        //                moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox5.ReadOnly = true;
        //                money5 = sub;
        //            }
        //            else if (!(weightTextBox5.Text == "0") && !string.IsNullOrEmpty(countTextBox5.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money5 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox5.ReadOnly = true;
        //                money5 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox6.ReadOnly = false;
        //            unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "7行目"
        //private void unitPriceTextBox6_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or　成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox6.Text))
        //        {
        //            unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox7.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox6.Text) && !(countTextBox6.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox6.ReadOnly = true;
        //                money6 = sub;
        //            }
        //            else if (!(weightTextBox6.Text == "0") && !string.IsNullOrEmpty(countTextBox6.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox6.ReadOnly = true;
        //                money6 = sub;
        //            }
        //            else { }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox5.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox5.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox6.Text))
        //        {
        //            unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox7.Text))
        //        {
        //            unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox7.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox6.Text) && !(countTextBox6.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox6.ReadOnly = true;
        //                money6 = sub;
        //            }
        //            else if (!(weightTextBox6.Text == "0") && !string.IsNullOrEmpty(countTextBox6.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox6.ReadOnly = true;
        //                money6 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox7.ReadOnly = false;
        //            unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "8行目"
        //private void unitPriceTextBox7_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox7.Text))
        //        {
        //            unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox8.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox7.Text) && !(countTextBox7.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox7.ReadOnly = true;
        //                money7 = sub;
        //            }
        //            else if (!(weightTextBox7.Text == "0") && !string.IsNullOrEmpty(countTextBox7.Text))
        //            {
        //                sub07 = decimal.Parse(weightTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox7.ReadOnly = true;
        //                money7 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox6.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox6.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox7.Text))
        //        {
        //            unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox8.Text))
        //        {
        //            unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox8.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox7.Text) && !(countTextBox7.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox7.ReadOnly = true;
        //                money7 = sub;
        //            }
        //            else if (!(weightTextBox7.Text == "0") && !string.IsNullOrEmpty(countTextBox7.Text))
        //            {
        //                sub07 = decimal.Parse(weightTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox7.ReadOnly = true;
        //                money7 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox8.ReadOnly = false;
        //            unitPriceTextBox8.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "9行目"
        //private void unitPriceTextBox8_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or　成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox8.Text))
        //        {
        //            unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox9.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox8.Text) && !(countTextBox8.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
        //                moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox8.ReadOnly = true;
        //                money8 = sub;
        //            }
        //            else if (!(weightTextBox8.Text == "0") && !string.IsNullOrEmpty(countTextBox8.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money8 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox8.ReadOnly = true;
        //                money8 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox7.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox7.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox8.Text))
        //        {
        //            unitPriceTextBox8.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox9.Text))
        //        {
        //            unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox9.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox8.Text) && !(countTextBox8.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
        //                moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox8.ReadOnly = true;
        //                money8 = sub;
        //            }
        //            else if (!(weightTextBox8.Text == "0") && !string.IsNullOrEmpty(countTextBox8.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money8 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox8.ReadOnly = true;
        //                money8 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox9.ReadOnly = false;
        //            unitPriceTextBox9.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "10行目"
        //private void unitPriceTextBox9_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox9.Text))
        //        {
        //            unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox10.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox9.Text) && !(countTextBox9.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox9.ReadOnly = true;
        //                money9 = sub;
        //            }
        //            else if (!(weightTextBox9.Text == "0") && !string.IsNullOrEmpty(countTextBox9.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox9.ReadOnly = true;
        //                money9 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox8.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox8.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox9.Text))
        //        {
        //            unitPriceTextBox9.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox10.Text))
        //        {
        //            unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox10.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox9.Text) && !(countTextBox9.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox9.ReadOnly = true;
        //                money9 = sub;
        //            }
        //            else if (!(weightTextBox9.Text == "0") && !string.IsNullOrEmpty(countTextBox9.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox9.ReadOnly = true;
        //                money9 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox10.ReadOnly = false;
        //            unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "11行目"
        //private void unitPriceTextBox10_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox10.Text))
        //        {
        //            unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox11.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox10.Text) && !(countTextBox010.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox10.ReadOnly = true;
        //                money10 = sub;
        //            }
        //            else if (!(weightTextBox10.Text == "0") && !string.IsNullOrEmpty(countTextBox10.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox10.ReadOnly = true;
        //                money10 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox9.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox9.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox10.Text))
        //        {
        //            unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox11.Text))
        //        {
        //            unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox11.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox10.Text) && !(countTextBox010.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox10.ReadOnly = true;
        //                money10 = sub;
        //            }
        //            else if (!(weightTextBox10.Text == "0") && !string.IsNullOrEmpty(countTextBox10.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox10.ReadOnly = true;
        //                money10 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox11.ReadOnly = false;
        //            unitPriceTextBox11.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "12行目"
        //private void unitPriceTextBox11_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox11.Text))
        //        {
        //            unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox12.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox11.Text) && !(countTextBox11.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox11.ReadOnly = true;
        //                money11 = sub;
        //            }
        //            else if (!(weightTextBox11.Text == "0") && !string.IsNullOrEmpty(countTextBox11.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox11.ReadOnly = true;
        //                money11 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox10.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox10.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox11.Text))
        //        {
        //            unitPriceTextBox11.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox12.Text))
        //        {
        //            unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox12.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox11.Text) && !(countTextBox11.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox11.ReadOnly = true;
        //                money11 = sub;
        //            }
        //            else if (!(weightTextBox11.Text == "0") && !string.IsNullOrEmpty(countTextBox11.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
        //                sub1 = sub + sub * Tax / 100;
        //                money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox11.ReadOnly = true;
        //                money11 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox12.ReadOnly = false;
        //            unitPriceTextBox12.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "13行目"
        //private void unitPriceTextBox12_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売　or　成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox12.Text))
        //        {

        //            if (!string.IsNullOrEmpty(weightTextBox12.Text) && !(countTextBox12.Text == "0"))
        //            {
        //                sub = decimal.Parse(countTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
        //                unitPriceTextBox12.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox12.Text, System.Globalization.NumberStyles.Number));
        //                sub1 = sub + sub * Tax / 100;
        //                money12 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox12.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                weightTextBox12.ReadOnly = true;
        //                money12 = sub;
        //            }
        //            else if (!(weightTextBox12.Text == "0") && !string.IsNullOrEmpty(countTextBox12.Text))
        //            {
        //                sub = decimal.Parse(weightTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
        //                unitPriceTextBox12.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox12.Text, System.Globalization.NumberStyles.Number));
        //                sub1 = sub + sub * Tax / 100;
        //                money12 = Math.Round(sub, MidpointRounding.AwayFromZero);
        //                moneyTextBox12.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        //                countTextBox12.ReadOnly = true;
        //                money12 = sub;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox11.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox11.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox12.Text))
        //        {
        //            unitPriceTextBox12.Text = "単価 -> 重量 or 数量";
        //        }
        //        else
        //        {
        //            unitPriceTextBox12.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox12.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //*/
        //#endregion
        //#endregion

        //#region"計算書　金額が入力されたら次の単価が入力可＋総重量 or 総数計算、自動計算"
        //#region "1行目"
        //private void moneyTextBox0_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売 or 成績入力"
        //    if (data == "S" || Grade != 0)
        //    {
        //        if (!string.IsNullOrEmpty(weightTextBox0.Text) && string.IsNullOrEmpty(countTextBox0.Text))
        //        {
        //            countTextBox0.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox0.Text) && string.IsNullOrEmpty(weightTextBox0.Text))
        //        {
        //            weightTextBox0.Text = 0.ToString();
        //        }
        //        countsum0 = int.Parse(countTextBox0.Text);
        //        weisum0 = decimal.Parse(weightTextBox0.Text);
        //        subSum0 = money0;//計算書は税込み
        //        #region"一時コメントアウト"
        //        //#region "数量の場合わけ"
        //        //if (countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        //        //{
        //        //    countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        //        //}
        //        //else
        //        //{
        //        //    countsum = countsum0;
        //        //}
        //        //#endregion
        //        //#region "重量の場合わけ"
        //        //if (weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        //        //{
        //        //    weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        //        //}
        //        //else
        //        //{
        //        //    weisum = weisum0;
        //        //}
        //        //#endregion

        //        //#region "金額合計の場合わけ"
        //        //if (subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        //        //{
        //        //    subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        //        //}
        //        //else
        //        //{
        //        //    subSum = subSum0;
        //        //}
        //        //#endregion
        //        #endregion

        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        //totalCount.Text = string.Format("{0:#,0}", countsum);
        //        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount0.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        //        if (subSum >= 2000000)
        //        {
        //            groupBox1.Show();
        //            groupBox1.BackColor = Color.OrangeRed;
        //        }
        //        else
        //        {
        //            groupBox1.Hide();
        //        }
        //    }
        //    #endregion
        //    else
        //    {
        //        #region"コメントアウト"
        //        ////重量×単価
        //        //if (!string.IsNullOrEmpty(weightTextBox0.Text) && string.IsNullOrEmpty(countTextBox0.Text))
        //        //{
        //        //    countTextBox0.Text = 0.ToString();
        //        //}
        //        ////数量×単価
        //        //else if (!string.IsNullOrEmpty(countTextBox0.Text) && string.IsNullOrEmpty(weightTextBox0.Text))
        //        //{
        //        //    weightTextBox0.Text = 0.ToString();
        //        //}
        //        //else
        //        //{

        //        //}
        //        //countsum0 = int.Parse(countTextBox0.Text);
        //        //weisum0 = decimal.Parse(weightTextBox0.Text);
        //        #endregion

        //        subSum0 = money0;//計算書は税込み

        //        #region"一時コメントアウト"
        //        #region "数量の場合わけ"
        //        if (countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        //        {
        //            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        //        }
        //        else
        //        {
        //            countsum = countsum0;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        //        {
        //            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        //        }
        //        else
        //        {
        //            weisum = weisum0;
        //        }
        //        #endregion

        //        #region "金額合計の場合わけ"
        //        if (subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        //        {
        //            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        //        }
        //        else
        //        {
        //            subSum = subSum0;
        //        }
        //        #endregion
        //        #endregion

        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        //totalCount.Text = string.Format("{0:#,0}", countsum);
        //        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount0.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        //        if (subSum >= 2000000)
        //        {
        //            groupBox1.Show();
        //            groupBox1.BackColor = Color.OrangeRed;
        //        }
        //        else
        //        {
        //            groupBox1.Hide();
        //        }
        //    }

        //}
        //#endregion

        ////#region "2行目"
        ////private void moneyTextBox1_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or　成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        if (!string.IsNullOrEmpty(weightTextBox1.Text) && string.IsNullOrEmpty(countTextBox1.Text))
        ////        {
        ////            countTextBox1.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox1.Text) && string.IsNullOrEmpty(weightTextBox1.Text))
        ////        {
        ////            weightTextBox1.Text = 0.ToString();
        ////        }
        ////        countsum1 = int.Parse(countTextBox1.Text);
        ////        weisum1 = decimal.Parse(weightTextBox1.Text);
        ////        subSum1 = money1;
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum1;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum1;
        ////        }
        ////        #endregion
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum1;
        ////        }
        ////        #endregion
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));


        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox1.Text) && string.IsNullOrEmpty(countTextBox1.Text))
        ////        {
        ////            countTextBox1.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox1.Text) && string.IsNullOrEmpty(weightTextBox1.Text))
        ////        {
        ////            weightTextBox1.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum1 = int.Parse(countTextBox1.Text);
        ////        weisum1 = decimal.Parse(weightTextBox1.Text);
        ////        subSum1 = money1;
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum1;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum1;
        ////        }
        ////        #endregion
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum1;
        ////        }
        ////        #endregion
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));


        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }

        ////}
        ////#endregion
        ////#region "3行目"
        ////private void moneyTextBox2_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or　成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox2.Text) && string.IsNullOrEmpty(countTextBox2.Text))
        ////        {
        ////            countTextBox2.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox2.Text) && string.IsNullOrEmpty(weightTextBox2.Text))
        ////        {
        ////            weightTextBox2.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum2 = int.Parse(countTextBox2.Text);
        ////        weisum2 = decimal.Parse(weightTextBox2.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum2;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum2;
        ////        }
        ////        #endregion
        ////        subSum2 = money2;
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum2;
        ////        }
        ////        #endregion
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox2.Text) && string.IsNullOrEmpty(countTextBox2.Text))
        ////        {
        ////            countTextBox2.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox2.Text) && string.IsNullOrEmpty(weightTextBox2.Text))
        ////        {
        ////            weightTextBox2.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum2 = int.Parse(countTextBox2.Text);
        ////        weisum2 = decimal.Parse(weightTextBox2.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum2;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum2;
        ////        }
        ////        #endregion
        ////        subSum2 = money2;
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum2;
        ////        }
        ////        #endregion
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }

        ////}
        ////#endregion
        ////#region "4行目"
        ////private void moneyTextBox3_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or 成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox3.Text) && string.IsNullOrEmpty(countTextBox3.Text))
        ////        {
        ////            countTextBox3.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox3.Text) && string.IsNullOrEmpty(weightTextBox3.Text))
        ////        {
        ////            weightTextBox3.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum3 = int.Parse(countTextBox3.Text);
        ////        weisum3 = decimal.Parse(weightTextBox3.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum3;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum3;
        ////        }
        ////        #endregion
        ////        subSum3 = money3;     //増やしていく
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum3;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox3.Text) && string.IsNullOrEmpty(countTextBox3.Text))
        ////        {
        ////            countTextBox3.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox3.Text) && string.IsNullOrEmpty(weightTextBox3.Text))
        ////        {
        ////            weightTextBox3.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum3 = int.Parse(countTextBox3.Text);
        ////        weisum3 = decimal.Parse(weightTextBox3.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum3;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum3;
        ////        }
        ////        #endregion
        ////        subSum3 = money3;     //増やしていく
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum3;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }

        ////}
        ////#endregion
        ////#region "5行目"
        ////private void moneyTextBox4_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or　成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox4.Text) && string.IsNullOrEmpty(countTextBox4.Text))
        ////        {
        ////            countTextBox4.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox4.Text) && string.IsNullOrEmpty(weightTextBox4.Text))
        ////        {
        ////            weightTextBox4.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum4 = int.Parse(countTextBox4.Text);
        ////        weisum4 = decimal.Parse(weightTextBox4.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum4;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum4;
        ////        }
        ////        #endregion
        ////        subSum4 = money4;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum4;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox4.Text) && string.IsNullOrEmpty(countTextBox4.Text))
        ////        {
        ////            countTextBox4.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox4.Text) && string.IsNullOrEmpty(weightTextBox4.Text))
        ////        {
        ////            weightTextBox4.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum4 = int.Parse(countTextBox4.Text);
        ////        weisum4 = decimal.Parse(weightTextBox4.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum4;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum4;
        ////        }
        ////        #endregion
        ////        subSum4 = money4;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum4;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////}
        ////#endregion
        ////#region "6行目"
        ////private void moneyTextBox5_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or　成績入力"
        ////    if (data == "S")
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox5.Text) && string.IsNullOrEmpty(countTextBox5.Text))
        ////        {
        ////            countTextBox5.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox5.Text) && string.IsNullOrEmpty(weightTextBox5.Text))
        ////        {
        ////            weightTextBox5.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum5 = int.Parse(countTextBox5.Text);
        ////        weisum5 = decimal.Parse(weightTextBox5.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum5;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum5;
        ////        }
        ////        #endregion
        ////        subSum5 = money5;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum5;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox5.Text) && string.IsNullOrEmpty(countTextBox5.Text))
        ////        {
        ////            countTextBox5.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox5.Text) && string.IsNullOrEmpty(weightTextBox5.Text))
        ////        {
        ////            weightTextBox5.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum5 = int.Parse(countTextBox5.Text);
        ////        weisum5 = decimal.Parse(weightTextBox5.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum5;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum5;
        ////        }
        ////        #endregion
        ////        subSum5 = money5;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum5;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }

        ////}
        ////#endregion
        ////#region "7行目"
        ////private void moneyTextBox6_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or　成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox6.Text) && string.IsNullOrEmpty(countTextBox6.Text))
        ////        {
        ////            countTextBox6.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox6.Text) && string.IsNullOrEmpty(weightTextBox6.Text))
        ////        {
        ////            weightTextBox6.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum6 = int.Parse(countTextBox6.Text);
        ////        weisum6 = decimal.Parse(weightTextBox6.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum6;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum6;
        ////        }
        ////        #endregion
        ////        subSum6 = money6;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum6;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox6.Text) && string.IsNullOrEmpty(countTextBox6.Text))
        ////        {
        ////            countTextBox6.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox6.Text) && string.IsNullOrEmpty(weightTextBox6.Text))
        ////        {
        ////            weightTextBox6.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum6 = int.Parse(countTextBox6.Text);
        ////        weisum6 = decimal.Parse(weightTextBox6.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum6;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum6;
        ////        }
        ////        #endregion
        ////        subSum6 = money6;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum6;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }

        ////}
        ////#endregion
        ////#region "8行目"
        ////private void moneyTextBox7_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or 成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox7.Text) && string.IsNullOrEmpty(countTextBox7.Text))
        ////        {
        ////            countTextBox7.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox7.Text) && string.IsNullOrEmpty(weightTextBox7.Text))
        ////        {
        ////            weightTextBox7.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum7 = int.Parse(countTextBox7.Text);
        ////        weisum7 = decimal.Parse(weightTextBox7.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum7;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum7;
        ////        }
        ////        #endregion
        ////        subSum7 = money7;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum7;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox7.Text) && string.IsNullOrEmpty(countTextBox7.Text))
        ////        {
        ////            countTextBox7.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox7.Text) && string.IsNullOrEmpty(weightTextBox7.Text))
        ////        {
        ////            weightTextBox7.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum7 = int.Parse(countTextBox7.Text);
        ////        weisum7 = decimal.Parse(weightTextBox7.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum7;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum7;
        ////        }
        ////        #endregion
        ////        subSum7 = money7;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum7;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////}
        ////#endregion
        ////#region "9行目"
        ////private void moneyTextBox8_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or　成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox8.Text) && string.IsNullOrEmpty(countTextBox8.Text))
        ////        {
        ////            countTextBox8.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox8.Text) && string.IsNullOrEmpty(weightTextBox8.Text))
        ////        {
        ////            weightTextBox8.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum8 = int.Parse(countTextBox8.Text);
        ////        weisum8 = decimal.Parse(weightTextBox8.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum8;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum8;
        ////        }
        ////        #endregion
        ////        subSum8 = money8;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum8;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox8.Text) && string.IsNullOrEmpty(countTextBox8.Text))
        ////        {
        ////            countTextBox8.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox8.Text) && string.IsNullOrEmpty(weightTextBox8.Text))
        ////        {
        ////            weightTextBox8.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum8 = int.Parse(countTextBox8.Text);
        ////        weisum8 = decimal.Parse(weightTextBox8.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum8;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum8;
        ////        }
        ////        #endregion
        ////        subSum8 = money8;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum8;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////}
        ////#endregion
        ////#region "10行目"
        ////private void moneyTextBox9_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or　成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox9.Text) && string.IsNullOrEmpty(countTextBox9.Text))
        ////        {
        ////            countTextBox9.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox9.Text) && string.IsNullOrEmpty(weightTextBox9.Text))
        ////        {
        ////            weightTextBox9.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum9 = int.Parse(countTextBox9.Text);
        ////        weisum9 = decimal.Parse(weightTextBox9.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum9;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum9;
        ////        }
        ////        #endregion
        ////        subSum9 = money9;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum9;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox9.Text) && string.IsNullOrEmpty(countTextBox9.Text))
        ////        {
        ////            countTextBox9.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox9.Text) && string.IsNullOrEmpty(weightTextBox9.Text))
        ////        {
        ////            weightTextBox9.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum9 = int.Parse(countTextBox9.Text);
        ////        weisum9 = decimal.Parse(weightTextBox9.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum10 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum9;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum10 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum9;
        ////        }
        ////        #endregion
        ////        subSum9 = money9;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum10 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum9;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }

        ////}
        ////#endregion
        ////#region "11行目"
        ////private void moneyTextBox10_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or 成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox10.Text) && string.IsNullOrEmpty(countTextBox10.Text))
        ////        {
        ////            countTextBox10.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox10.Text) && string.IsNullOrEmpty(weightTextBox10.Text))
        ////        {
        ////            weightTextBox10.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum10 = int.Parse(countTextBox10.Text);
        ////        weisum10 = decimal.Parse(weightTextBox10.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum10;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum10;
        ////        }
        ////        #endregion
        ////        subSum10 = money10;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum10;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox10.Text) && string.IsNullOrEmpty(countTextBox10.Text))
        ////        {
        ////            countTextBox10.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox10.Text) && string.IsNullOrEmpty(weightTextBox10.Text))
        ////        {
        ////            weightTextBox10.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum10 = int.Parse(countTextBox10.Text);
        ////        weisum10 = decimal.Parse(weightTextBox10.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum11 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum10;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum11 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum10;
        ////        }
        ////        #endregion
        ////        subSum10 = money10;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum11 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum10;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////}
        ////#endregion
        ////#region "12行目"
        ////private void moneyTextBox11_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or 成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox11.Text) && string.IsNullOrEmpty(countTextBox11.Text))
        ////        {
        ////            countTextBox11.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox11.Text) && string.IsNullOrEmpty(weightTextBox11.Text))
        ////        {
        ////            weightTextBox11.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }

        ////        countsum11 = int.Parse(countTextBox11.Text);
        ////        weisum11 = decimal.Parse(weightTextBox11.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum11;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum11;
        ////        }
        ////        #endregion
        ////        subSum11 = money11;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum11;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox11.Text) && string.IsNullOrEmpty(countTextBox11.Text))
        ////        {
        ////            countTextBox11.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox11.Text) && string.IsNullOrEmpty(weightTextBox11.Text))
        ////        {
        ////            weightTextBox11.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum11 = int.Parse(countTextBox11.Text);
        ////        weisum11 = decimal.Parse(weightTextBox11.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum12 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum11;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum12 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum11;
        ////        }
        ////        #endregion
        ////        subSum11 = money11;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum12 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum11;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }

        ////}
        ////#endregion
        ////#region "13行目"
        ////private void moneyTextBox12_TextChanged(object sender, EventArgs e)
        ////{
        ////    #region "買取販売 or　成績入力"
        ////    if (data == "S" || Grade != 0)
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox12.Text) && string.IsNullOrEmpty(countTextBox12.Text))
        ////        {
        ////            countTextBox12.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox12.Text) && string.IsNullOrEmpty(weightTextBox12.Text))
        ////        {
        ////            weightTextBox12.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum12 = int.Parse(countTextBox12.Text);
        ////        weisum12 = decimal.Parse(weightTextBox12.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum12;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum12;
        ////        }
        ////        #endregion
        ////        subSum12 = money12;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum12;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }
        ////    #endregion
        ////    else
        ////    {
        ////        //重量×単価
        ////        if (!string.IsNullOrEmpty(weightTextBox12.Text) && string.IsNullOrEmpty(countTextBox12.Text))
        ////        {
        ////            countTextBox12.Text = 0.ToString();
        ////        }
        ////        //数量×単価
        ////        else if (!string.IsNullOrEmpty(countTextBox12.Text) && string.IsNullOrEmpty(weightTextBox12.Text))
        ////        {
        ////            weightTextBox12.Text = 0.ToString();
        ////        }
        ////        else
        ////        {

        ////        }
        ////        countsum12 = int.Parse(countTextBox12.Text);
        ////        weisum12 = decimal.Parse(weightTextBox12.Text);
        ////        #region "数量の場合わけ"
        ////        if (countsum0 != 0 || countsum1 != 0 || countsum2 != 0 || countsum3 != 0 || countsum4 != 0 || countsum5 != 0 || countsum6 != 0 || countsum7 != 0 || countsum8 != 0 || countsum9 != 0 || countsum10 != 0 || countsum11 != 0)
        ////        {
        ////            countsum = countsum0 + countsum1 + countsum2 + countsum3 + countsum4 + countsum5 + countsum6 + countsum7 + countsum8 + countsum9 + countsum10 + countsum11 + countsum12;
        ////        }
        ////        else
        ////        {
        ////            countsum = countsum12;
        ////        }
        ////        #endregion
        ////        #region "重量の場合わけ"
        ////        if (weisum0 != 0 || weisum1 != 0 || weisum2 != 0 || weisum3 != 0 || weisum4 != 0 || weisum5 != 0 || weisum6 != 0 || weisum7 != 0 || weisum8 != 0 || weisum9 != 0 || weisum10 != 0 || weisum11 != 0)
        ////        {
        ////            weisum = weisum0 + weisum1 + weisum2 + weisum3 + weisum4 + weisum5 + weisum6 + weisum7 + weisum8 + weisum9 + weisum10 + weisum11 + weisum12;
        ////        }
        ////        else
        ////        {
        ////            weisum = weisum12;
        ////        }
        ////        #endregion
        ////        subSum12 = money12;
        ////        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        ////        #region "金額合計の場合わけ"
        ////        if (subSum0 != 0 || subSum1 != 0 || subSum2 != 0 || subSum3 != 0 || subSum4 != 0 || subSum5 != 0 || subSum6 != 0 || subSum7 != 0 || subSum8 != 0 || subSum9 != 0 || subSum10 != 0 || subSum11 != 0)
        ////        {
        ////            subSum = subSum0 + subSum1 + subSum2 + subSum3 + subSum4 + subSum5 + subSum6 + subSum7 + subSum8 + subSum9 + subSum10 + subSum11 + subSum12;
        ////        }
        ////        else
        ////        {
        ////            subSum = subSum12;
        ////        }
        ////        #endregion
        ////        totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        ////        totalCount.Text = string.Format("{0:#,0}", countsum);
        ////        subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        ////        taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        ////        sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

        ////        if (subSum >= 2000000)
        ////        {
        ////            groupBox1.Show();
        ////            groupBox1.BackColor = Color.OrangeRed;
        ////        }
        ////        else
        ////        {
        ////            groupBox1.Hide();
        ////        }
        ////    }

        ////}
        ////#endregion
        ////*/
        ////#endregion
        //#endregion

        //#region　"納品書　単価を入力したら重量、数値入力可"
        //#region "1行目"
        //private void unitPriceTextBox00_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox00.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox00.Text))
        //    {
        //        weightTextBox00.ReadOnly = true;
        //        countTextBox00.ReadOnly = true;
        //        unitPriceTextBox01.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox00.Text) && !(unitPriceTextBox00.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox00.ReadOnly = false;
        //        countTextBox00.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "2行目"
        //private void unitPriceTextBox01_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox01.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox01.Text))
        //    {
        //        weightTextBox01.ReadOnly = true;
        //        countTextBox01.ReadOnly = true;
        //        unitPriceTextBox02.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox01.Text) && !(unitPriceTextBox01.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox01.ReadOnly = false;
        //        countTextBox01.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "3行目"
        //private void unitPriceTextBox02_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox02.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox02.Text))
        //    {
        //        weightTextBox02.ReadOnly = true;
        //        countTextBox02.ReadOnly = true;
        //        unitPriceTextBox03.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox02.Text) && !(unitPriceTextBox02.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox02.ReadOnly = false;
        //        countTextBox02.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "4行目"
        //private void unitPriceTextBox03_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox03.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox03.Text))
        //    {
        //        weightTextBox03.ReadOnly = true;
        //        countTextBox03.ReadOnly = true;
        //        unitPriceTextBox04.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox03.Text) && !(unitPriceTextBox03.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox03.ReadOnly = false;
        //        countTextBox03.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "5行目"
        //private void unitPriceTextBox04_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox04.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox04.Text))
        //    {
        //        weightTextBox04.ReadOnly = true;
        //        countTextBox04.ReadOnly = true;
        //        unitPriceTextBox05.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox04.Text) && !(unitPriceTextBox04.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox04.ReadOnly = false;
        //        countTextBox04.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "6行目"
        //private void unitPriceTextBox05_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox05.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox05.Text))
        //    {
        //        weightTextBox05.ReadOnly = true;
        //        countTextBox05.ReadOnly = true;
        //        unitPriceTextBox06.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox05.Text) && !(unitPriceTextBox05.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox05.ReadOnly = false;
        //        countTextBox05.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "7行目"
        //private void unitPriceTextBox06_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox06.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox06.Text))
        //    {
        //        weightTextBox06.ReadOnly = true;
        //        countTextBox06.ReadOnly = true;
        //        unitPriceTextBox07.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox06.Text) && !(unitPriceTextBox06.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox06.ReadOnly = false;
        //        countTextBox06.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "8行目"
        //private void unitPriceTextBox07_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox07.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox07.Text))
        //    {
        //        weightTextBox07.ReadOnly = true;
        //        countTextBox07.ReadOnly = true;
        //        unitPriceTextBox08.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox07.Text) && !(unitPriceTextBox07.Text.ToString() == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox07.ReadOnly = false;
        //        countTextBox07.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "9行目"
        //private void unitPriceTextBox08_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox08.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox08.Text))
        //    {
        //        weightTextBox08.ReadOnly = true;
        //        countTextBox08.ReadOnly = true;
        //        unitPriceTextBox09.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox08.Text) && !(unitPriceTextBox08.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox08.ReadOnly = false;
        //        countTextBox08.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "10行目"
        //private void unitPriceTextBox09_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox09.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox09.Text))
        //    {
        //        weightTextBox09.ReadOnly = true;
        //        countTextBox09.ReadOnly = true;
        //        unitPriceTextBox010.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox09.Text) && !(unitPriceTextBox09.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox09.ReadOnly = false;
        //        countTextBox09.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "11行目"
        //private void unitPriceTextBox010_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox010.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox010.Text))
        //    {
        //        weightTextBox010.ReadOnly = true;
        //        countTextBox010.ReadOnly = true;
        //        unitPriceTextBox011.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox010.Text) && !(unitPriceTextBox010.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox010.ReadOnly = false;
        //        countTextBox010.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "12行目"
        //private void unitPriceTextBox011_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox011.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox011.Text))
        //    {
        //        weightTextBox011.ReadOnly = true;
        //        countTextBox011.ReadOnly = true;
        //        unitPriceTextBox012.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox011.Text) && !(unitPriceTextBox011.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox011.ReadOnly = false;
        //        countTextBox011.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#region "13行目"
        //private void unitPriceTextBox012_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(unitPriceTextBox012.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (string.IsNullOrEmpty(unitPriceTextBox012.Text))
        //    {
        //        weightTextBox012.ReadOnly = true;
        //        countTextBox012.ReadOnly = true;
        //    }
        //    else if (!string.IsNullOrEmpty(unitPriceTextBox012.Text) && !(unitPriceTextBox012.Text.ToString() == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
        //    {
        //        weightTextBox012.ReadOnly = false;
        //        countTextBox012.ReadOnly = false;
        //    }
        //}
        //#endregion
        //#endregion

        //#region"納品書　フォーカス時初期状態ならnull"
        //#region "1行目"
        //private void unitPriceTextBox00_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (unitPriceTextBox00.Text.ToString() == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox00.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "2行目"
        //private void unitPriceTextBox01_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox01.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox01.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox01.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "3行目"
        //private void unitPriceTextBox02_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox02.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox01.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox01.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox02.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox02.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "4行目"
        //private void unitPriceTextBox03_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox03.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox02.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox02.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox03.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox03.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "5行目"
        //private void unitPriceTextBox04_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox04.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox03.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox03.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox04.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox04.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "6行目"
        //private void unitPriceTextBox05_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox05.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox04.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox04.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox05.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox05.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "7行目"
        //private void unitPriceTextBox06_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox06.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox05.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox05.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox06.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox06.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "8行目"
        //private void unitPriceTextBox07_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox07.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox06.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox06.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox07.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox07.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "9行目"
        //private void unitPriceTextBox08_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox08.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox07.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox07.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox08.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox08.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "10行目"
        //private void unitPriceTextBox09_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox09.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox08.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox08.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox09.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox09.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "11行目"
        //private void unitPriceTextBox010_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox010.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox09.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox09.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox010.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox010.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "12行目"
        //private void unitPriceTextBox011_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox011.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox010.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox010.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox011.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox011.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#region "13行目"
        //private void unitPriceTextBox012_Enter(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        unitPriceTextBox012.ReadOnly = false;
        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox011.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox011.Text, @"^[a-zA-Z]+$"))
        //        {

        //        }
        //        else if (unitPriceTextBox012.Text == "単価 -> 重量 or 数量")
        //        {
        //            unitPriceTextBox012.Text = "";
        //        }
        //    }
        //}
        //#endregion
        //#endregion

        //#region"納品書　数量を入力したら重量を入力不可"

        //#region "1行目"
        //private void countTextBox00_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox00.Text))
        //        {
        //            weightTextBox00.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox00.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "2行目"
        //private void countTextBox01_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox01.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox01.Text))
        //        {
        //            weightTextBox01.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox01.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "3行目"
        //private void countTextBox02_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox02.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox02.Text))
        //        {
        //            weightTextBox02.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox02.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "4行目"
        //private void countTextBox03_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox03.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox03.Text))
        //        {
        //            weightTextBox03.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox03.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "5行目"
        //private void countTextBox04_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox04.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox04.Text))
        //        {
        //            weightTextBox04.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox04.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "6行目"
        //private void countTextBox05_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox05.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox05.Text))
        //        {
        //            weightTextBox05.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox05.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "7行目"
        //private void countTextBox06_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox06.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox06.Text))
        //        {
        //            weightTextBox06.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox06.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "8行目"
        //private void countTextBox07_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox07.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox07.Text))
        //        {
        //            weightTextBox07.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox07.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "9行目"
        //private void countTextBox08_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox08.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox08.Text))
        //        {
        //            weightTextBox08.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox08.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "10行目"
        //private void countTextBox09_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox09.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox09.Text))
        //        {
        //            weightTextBox09.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox09.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "11行目"
        //private void countTextBox010_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox010.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox010.Text))
        //        {
        //            weightTextBox010.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox010.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "12行目"
        //private void countTextBox011_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox011.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox011.Text))
        //        {
        //            weightTextBox011.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox011.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#region "13行目"
        //private void countTextBox012_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox012.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox012.Text))
        //        {
        //            weightTextBox012.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox012.ReadOnly = false;
        //        }
        //    }
        //}
        //#endregion
        //#endregion
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
                    subTotal2.Text = (SUB - taxAmount).ToString("n0");
                    taxAmount2.Text = taxAmount.ToString("c0");
                }
                else        //税込み選択
                {
                    taxAmount2.Text = "";
                }
            }
        }
        #endregion

        #region"一時コメントアウト（納品書、買取販売からきたとき）"
        //#region"納品書　単価３桁区切り＋フォーカスが外れた時"
        //#region "1行目"
        //private void unitPriceTextBox00_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox00.Text))
        //        {
        //            unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox01.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox00.Text) && !(countTextBox00.Text == "0"))
        //            {
        //                sub00 = decimal.Parse(countTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
        //                sub10 = sub00 + sub00 * Tax / 100;
        //                money0 = Math.Round(sub00, MidpointRounding.AwayFromZero);
        //                moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub10, MidpointRounding.AwayFromZero));
        //                weightTextBox00.ReadOnly = true;
        //            }
        //            else if (!(weightTextBox00.Text == "0") && !string.IsNullOrEmpty(countTextBox00.Text))
        //            {
        //                sub00 = decimal.Parse(weightTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
        //                sub10 = sub00 + sub00 * Tax / 100;
        //                money0 = Math.Round(sub00, MidpointRounding.AwayFromZero);
        //                moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub10, MidpointRounding.AwayFromZero));
        //                countTextBox00.ReadOnly = true;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox00.Text))
        //        {
        //            unitPriceTextBox00.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox01.Text))
        //        {
        //            unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox01.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox00.Text) && !(countTextBox00.Text == "0"))
        //            {
        //                sub00 = decimal.Parse(countTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
        //                sub10 = sub00 + sub00 * Tax / 100;
        //                money0 = Math.Round(sub00, MidpointRounding.AwayFromZero);
        //                moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub10, MidpointRounding.AwayFromZero));
        //                weightTextBox00.ReadOnly = true;
        //            }
        //            else if (!(weightTextBox00.Text == "0") && !string.IsNullOrEmpty(countTextBox00.Text))
        //            {
        //                sub00 = decimal.Parse(weightTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
        //                sub10 = sub00 + sub00 * Tax / 100;
        //                money0 = Math.Round(sub00, MidpointRounding.AwayFromZero);
        //                moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub10, MidpointRounding.AwayFromZero));
        //                countTextBox00.ReadOnly = true;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox01.ReadOnly = false;
        //            unitPriceTextBox01.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "2行目"
        //private void unitPriceTextBox01_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox01.Text))
        //        {
        //            unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox02.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox01.Text) && !(countTextBox01.Text == "0"))
        //            {
        //                sub01 = decimal.Parse(countTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
        //                sub11 = sub01 + sub01 * Tax / 100;
        //                money1 = Math.Round(sub01, MidpointRounding.AwayFromZero);
        //                moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub11, MidpointRounding.AwayFromZero));
        //                weightTextBox01.ReadOnly = true;
        //                money1 = sub01;
        //            }
        //            else if (!(weightTextBox01.Text == "0") && !string.IsNullOrEmpty(countTextBox01.Text))
        //            {
        //                sub01 = decimal.Parse(weightTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
        //                sub11 = sub01 + sub01 * Tax / 100;
        //                money1 = Math.Round(sub01, MidpointRounding.AwayFromZero);
        //                moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub11, MidpointRounding.AwayFromZero));
        //                countTextBox01.ReadOnly = true;
        //                money1 = sub01;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
        //    {
        //        //MessageBox.Show("正しく入力して下さい。");
        //        //return;
        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox01.Text))
        //        {
        //            unitPriceTextBox01.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox02.Text))
        //        {
        //            unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox02.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox01.Text) && !(countTextBox01.Text == "0"))
        //            {
        //                sub01 = decimal.Parse(countTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
        //                sub11 = sub01 + sub01 * Tax / 100;
        //                money1 = Math.Round(sub01, MidpointRounding.AwayFromZero);
        //                moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub11, MidpointRounding.AwayFromZero));
        //                weightTextBox01.ReadOnly = true;
        //                money1 = sub01;
        //            }
        //            else if (!(weightTextBox01.Text == "0") && !string.IsNullOrEmpty(countTextBox01.Text))
        //            {
        //                sub01 = decimal.Parse(weightTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
        //                sub11 = sub01 + sub01 * Tax / 100;
        //                money1 = Math.Round(sub01, MidpointRounding.AwayFromZero);
        //                moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub11, MidpointRounding.AwayFromZero));
        //                countTextBox01.ReadOnly = true;
        //                money1 = sub01;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox02.ReadOnly = false;
        //            unitPriceTextBox02.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "3行目"
        //private void unitPriceTextBox02_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox02.Text))
        //        {
        //            unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox03.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox02.Text) && !(countTextBox02.Text == "0"))
        //            {
        //                sub02 = decimal.Parse(countTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
        //                sub12 = sub02 + sub02 * Tax / 100;
        //                money2 = Math.Round(sub02, MidpointRounding.AwayFromZero);
        //                moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub12, MidpointRounding.AwayFromZero));
        //                weightTextBox02.ReadOnly = true;
        //                money2 = sub02;
        //            }
        //            else if (!(weightTextBox02.Text == "0") && !string.IsNullOrEmpty(countTextBox02.Text))
        //            {
        //                sub02 = decimal.Parse(weightTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
        //                sub12 = sub02 + sub02 * Tax / 100;
        //                money2 = Math.Round(sub02, MidpointRounding.AwayFromZero);
        //                moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub12, MidpointRounding.AwayFromZero));
        //                countTextBox02.ReadOnly = true;
        //                money2 = sub02;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox01.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox01.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox02.Text))
        //        {
        //            unitPriceTextBox02.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox03.Text))
        //        {
        //            unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox03.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox02.Text) && !(countTextBox02.Text == "0"))
        //            {
        //                sub02 = decimal.Parse(countTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
        //                sub12 = sub02 + sub02 * Tax / 100;
        //                money2 = Math.Round(sub02, MidpointRounding.AwayFromZero);
        //                moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub12, MidpointRounding.AwayFromZero));
        //                weightTextBox02.ReadOnly = true;
        //                money2 = sub02;
        //            }
        //            else if (!(weightTextBox02.Text == "0") && !string.IsNullOrEmpty(countTextBox02.Text))
        //            {
        //                sub02 = decimal.Parse(weightTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
        //                sub12 = sub02 + sub02 * Tax / 100;
        //                money2 = Math.Round(sub02, MidpointRounding.AwayFromZero);
        //                moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub12, MidpointRounding.AwayFromZero));
        //                countTextBox02.ReadOnly = true;
        //                money2 = sub02;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox03.ReadOnly = false;
        //            unitPriceTextBox03.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "4行目"
        //private void unitPriceTextBox03_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox03.Text))
        //        {
        //            unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox04.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox03.Text) && !(countTextBox03.Text == "0"))
        //            {
        //                sub03 = decimal.Parse(countTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
        //                sub13 = sub03 + sub03 * Tax / 100;
        //                money3 = Math.Round(sub03, MidpointRounding.AwayFromZero);
        //                moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub13, MidpointRounding.AwayFromZero));
        //                weightTextBox03.ReadOnly = true;
        //                money3 = sub03;
        //            }
        //            else if (!(weightTextBox03.Text == "0") && !string.IsNullOrEmpty(countTextBox03.Text))
        //            {
        //                sub03 = decimal.Parse(weightTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
        //                sub13 = sub03 + sub03 * Tax / 100;
        //                money3 = Math.Round(sub03, MidpointRounding.AwayFromZero);
        //                moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub13, MidpointRounding.AwayFromZero));
        //                countTextBox03.ReadOnly = true;
        //                money3 = sub03;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox02.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox02.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox03.Text))
        //        {
        //            unitPriceTextBox03.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox04.Text))
        //        {
        //            unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox04.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox03.Text) && !(countTextBox03.Text == "0"))
        //            {
        //                sub03 = decimal.Parse(countTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
        //                sub13 = sub03 + sub03 * Tax / 100;
        //                money3 = Math.Round(sub03, MidpointRounding.AwayFromZero);
        //                moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub13, MidpointRounding.AwayFromZero));
        //                weightTextBox03.ReadOnly = true;
        //                money3 = sub03;
        //            }
        //            else if (!(weightTextBox03.Text == "0") && !string.IsNullOrEmpty(countTextBox03.Text))
        //            {
        //                sub03 = decimal.Parse(weightTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
        //                sub13 = sub03 + sub03 * Tax / 100;
        //                money3 = Math.Round(sub03, MidpointRounding.AwayFromZero);
        //                moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub13, MidpointRounding.AwayFromZero));
        //                countTextBox03.ReadOnly = true;
        //                money3 = sub03;
        //            }
        //            else
        //            {

        //            }

        //        }
        //        else
        //        {
        //            unitPriceTextBox04.ReadOnly = false;
        //            unitPriceTextBox04.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "5行目"
        //private void unitPriceTextBox04_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox04.Text))
        //        {
        //            unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox05.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox04.Text) && !(countTextBox04.Text == "0"))
        //            {
        //                sub04 = decimal.Parse(countTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
        //                sub14 = sub04 + sub04 * Tax / 100;
        //                money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
        //                moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub14, MidpointRounding.AwayFromZero));
        //                weightTextBox04.ReadOnly = true;
        //                money4 = sub04;
        //            }
        //            else if (!(weightTextBox04.Text == "0") && !string.IsNullOrEmpty(countTextBox04.Text))
        //            {
        //                sub04 = decimal.Parse(weightTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
        //                sub14 = sub04 + sub04 * Tax / 100;
        //                money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
        //                moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub14, MidpointRounding.AwayFromZero));
        //                countTextBox04.ReadOnly = true;
        //                money4 = sub04;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox03.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox03.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox04.Text))
        //        {
        //            unitPriceTextBox04.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox05.Text))
        //        {
        //            unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox05.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox04.Text) && !(countTextBox04.Text == "0"))
        //            {
        //                sub04 = decimal.Parse(countTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
        //                sub14 = sub04 + sub04 * Tax / 100;
        //                money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
        //                moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub14, MidpointRounding.AwayFromZero));
        //                weightTextBox04.ReadOnly = true;
        //                money4 = sub04;
        //            }
        //            else if (!(weightTextBox04.Text == "0") && !string.IsNullOrEmpty(countTextBox04.Text))
        //            {
        //                sub04 = decimal.Parse(weightTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
        //                sub14 = sub04 + sub04 * Tax / 100;
        //                money4 = Math.Round(sub04, MidpointRounding.AwayFromZero);
        //                moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub14, MidpointRounding.AwayFromZero));
        //                countTextBox04.ReadOnly = true;
        //                money4 = sub04;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox05.ReadOnly = false;
        //            unitPriceTextBox05.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "6行目"
        //private void unitPriceTextBox05_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox05.Text))
        //        {
        //            unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox06.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox05.Text) && !(countTextBox05.Text == "0"))
        //            {
        //                sub05 = decimal.Parse(countTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
        //                sub15 = sub05 + sub05 * Tax / 100;
        //                money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
        //                moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub15, MidpointRounding.AwayFromZero));
        //                weightTextBox05.ReadOnly = true;
        //                money5 = sub05;
        //            }
        //            else if (!(weightTextBox05.Text == "0") && !string.IsNullOrEmpty(countTextBox05.Text))
        //            {
        //                sub05 = decimal.Parse(weightTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
        //                sub15 = sub05 + sub05 * Tax / 100;
        //                money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
        //                moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub15, MidpointRounding.AwayFromZero));
        //                countTextBox05.ReadOnly = true;
        //                money5 = sub05;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox04.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox04.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox05.Text))
        //        {
        //            unitPriceTextBox05.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox06.Text))
        //        {
        //            unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox06.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox05.Text) && !(countTextBox05.Text == "0"))
        //            {
        //                sub05 = decimal.Parse(countTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
        //                sub15 = sub05 + sub05 * Tax / 100;
        //                money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
        //                moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub15, MidpointRounding.AwayFromZero));
        //                weightTextBox05.ReadOnly = true;
        //                money5 = sub05;
        //            }
        //            else if (!(weightTextBox05.Text == "0") && !string.IsNullOrEmpty(countTextBox05.Text))
        //            {
        //                sub05 = decimal.Parse(weightTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
        //                sub15 = sub05 + sub05 * Tax / 100;
        //                money5 = Math.Round(sub05, MidpointRounding.AwayFromZero);
        //                moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub15, MidpointRounding.AwayFromZero));
        //                countTextBox05.ReadOnly = true;
        //                money5 = sub05;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox06.ReadOnly = false;
        //            unitPriceTextBox06.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "7行目"
        //private void unitPriceTextBox06_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox06.Text))
        //        {
        //            unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox07.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox06.Text) && !(countTextBox06.Text == "0"))
        //            {
        //                sub06 = decimal.Parse(countTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
        //                sub16 = sub06 + sub06 * Tax / 100;
        //                money6 = Math.Round(sub06, MidpointRounding.AwayFromZero);
        //                moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub16, MidpointRounding.AwayFromZero));
        //                weightTextBox06.ReadOnly = true;
        //                money6 = sub06;
        //            }
        //            else if (!(weightTextBox06.Text == "0") && !string.IsNullOrEmpty(countTextBox06.Text))
        //            {
        //                sub06 = decimal.Parse(weightTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
        //                sub16 = sub06 + sub06 * Tax / 100;
        //                money6 = Math.Round(sub06, MidpointRounding.AwayFromZero);
        //                moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub16, MidpointRounding.AwayFromZero));
        //                countTextBox06.ReadOnly = true;
        //                money6 = sub06;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox05.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox05.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox06.Text))
        //        {
        //            unitPriceTextBox06.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox07.Text))
        //        {
        //            unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox07.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox06.Text) && !(countTextBox06.Text == "0"))
        //            {
        //                sub06 = decimal.Parse(countTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
        //                sub16 = sub06 + sub06 * Tax / 100;
        //                money6 = Math.Round(sub06, MidpointRounding.AwayFromZero);
        //                moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub16, MidpointRounding.AwayFromZero));
        //                weightTextBox06.ReadOnly = true;
        //                money6 = sub06;
        //            }
        //            else if (!(weightTextBox06.Text == "0") && !string.IsNullOrEmpty(countTextBox06.Text))
        //            {
        //                sub06 = decimal.Parse(weightTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
        //                sub16 = sub06 + sub06 * Tax / 100;
        //                money6 = Math.Round(sub06, MidpointRounding.AwayFromZero);
        //                moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub16, MidpointRounding.AwayFromZero));
        //                countTextBox06.ReadOnly = true;
        //                money6 = sub06;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox07.ReadOnly = false;
        //            unitPriceTextBox07.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "8行目"
        //private void unitPriceTextBox07_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox07.Text))
        //        {
        //            unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox08.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox07.Text) && !(countTextBox07.Text == "0"))
        //            {
        //                sub07 = decimal.Parse(countTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
        //                sub17 = sub07 + sub07 * Tax / 100;
        //                money7 = Math.Round(sub07, MidpointRounding.AwayFromZero);
        //                moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub17, MidpointRounding.AwayFromZero));
        //                weightTextBox07.ReadOnly = true;
        //                money7 = sub07;
        //            }
        //            else if (!(weightTextBox07.Text == "0") && !string.IsNullOrEmpty(countTextBox07.Text))
        //            {
        //                sub07 = decimal.Parse(weightTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
        //                sub17 = sub07 + sub07 * Tax / 100;
        //                money7 = Math.Round(sub07, MidpointRounding.AwayFromZero);
        //                moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub17, MidpointRounding.AwayFromZero));
        //                countTextBox07.ReadOnly = true;
        //                money7 = sub07;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox06.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox06.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox07.Text))
        //        {
        //            unitPriceTextBox07.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox08.Text))
        //        {
        //            unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox08.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox07.Text) && !(countTextBox07.Text == "0"))
        //            {
        //                sub07 = decimal.Parse(countTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
        //                sub17 = sub07 + sub07 * Tax / 100;
        //                money7 = Math.Round(sub07, MidpointRounding.AwayFromZero);
        //                moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub17, MidpointRounding.AwayFromZero));
        //                weightTextBox07.ReadOnly = true;
        //                money7 = sub07;
        //            }
        //            else if (!(weightTextBox07.Text == "0") && !string.IsNullOrEmpty(countTextBox07.Text))
        //            {
        //                sub07 = decimal.Parse(weightTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
        //                sub17 = sub07 + sub07 * Tax / 100;
        //                money7 = Math.Round(sub07, MidpointRounding.AwayFromZero);
        //                moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub17, MidpointRounding.AwayFromZero));
        //                countTextBox07.ReadOnly = true;
        //                money7 = sub07;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox08.ReadOnly = false;
        //            unitPriceTextBox08.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "9行目"
        //private void unitPriceTextBox08_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox08.Text))
        //        {
        //            unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox09.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox08.Text) && !(countTextBox08.Text == "0"))
        //            {
        //                sub08 = decimal.Parse(countTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
        //                sub18 = sub08 + sub08 * Tax / 100;
        //                money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
        //                moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub18, MidpointRounding.AwayFromZero));
        //                weightTextBox08.ReadOnly = true;
        //                money8 = sub08;
        //            }
        //            else if (!(weightTextBox08.Text == "0") && !string.IsNullOrEmpty(countTextBox08.Text))
        //            {
        //                sub08 = decimal.Parse(weightTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
        //                sub18 = sub08 + sub08 * Tax / 100;
        //                money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
        //                moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub18, MidpointRounding.AwayFromZero));
        //                countTextBox08.ReadOnly = true;
        //                money8 = sub08;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox07.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox07.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox08.Text))
        //        {
        //            unitPriceTextBox08.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox09.Text))
        //        {
        //            unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox09.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox08.Text) && !(countTextBox08.Text == "0"))
        //            {
        //                sub08 = decimal.Parse(countTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
        //                sub18 = sub08 + sub08 * Tax / 100;
        //                money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
        //                moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub18, MidpointRounding.AwayFromZero));
        //                weightTextBox08.ReadOnly = true;
        //                money8 = sub08;
        //            }
        //            else if (!(weightTextBox08.Text == "0") && !string.IsNullOrEmpty(countTextBox08.Text))
        //            {
        //                sub08 = decimal.Parse(weightTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
        //                sub18 = sub08 + sub08 * Tax / 100;
        //                money8 = Math.Round(sub08, MidpointRounding.AwayFromZero);
        //                moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub18, MidpointRounding.AwayFromZero));
        //                countTextBox08.ReadOnly = true;
        //                money8 = sub08;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox09.ReadOnly = false;
        //            unitPriceTextBox09.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "10行目"
        //private void unitPriceTextBox09_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox09.Text))
        //        {
        //            unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox010.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox09.Text) && !(countTextBox09.Text == "0"))
        //            {
        //                sub09 = decimal.Parse(countTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
        //                sub19 = sub09 + sub09 * Tax / 100;
        //                money9 = Math.Round(sub09, MidpointRounding.AwayFromZero);
        //                moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub19, MidpointRounding.AwayFromZero));
        //                weightTextBox09.ReadOnly = true;
        //                money9 = sub09;
        //            }
        //            else if (!(weightTextBox09.Text == "0") && !string.IsNullOrEmpty(countTextBox09.Text))
        //            {
        //                sub09 = decimal.Parse(weightTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
        //                sub19 = sub09 + sub09 * Tax / 100;
        //                money9 = Math.Round(sub09, MidpointRounding.AwayFromZero);
        //                moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub19, MidpointRounding.AwayFromZero));
        //                countTextBox09.ReadOnly = true;
        //                money9 = sub09;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox08.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox08.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox09.Text))
        //        {
        //            unitPriceTextBox09.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox010.Text))
        //        {
        //            unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox010.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox09.Text) && !(countTextBox09.Text == "0"))
        //            {
        //                sub09 = decimal.Parse(countTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
        //                sub19 = sub09 + sub09 * Tax / 100;
        //                money9 = Math.Round(sub09, MidpointRounding.AwayFromZero);
        //                moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub19, MidpointRounding.AwayFromZero));
        //                weightTextBox09.ReadOnly = true;
        //                money9 = sub09;
        //            }
        //            else if (!(weightTextBox09.Text == "0") && !string.IsNullOrEmpty(countTextBox09.Text))
        //            {
        //                sub09 = decimal.Parse(weightTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
        //                sub19 = sub09 + sub09 * Tax / 100;
        //                money9 = Math.Round(sub09, MidpointRounding.AwayFromZero);
        //                moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub19, MidpointRounding.AwayFromZero));
        //                countTextBox09.ReadOnly = true;
        //                money9 = sub09;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox010.ReadOnly = false;
        //            unitPriceTextBox010.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "11行目"
        //private void unitPriceTextBox010_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox010.Text))
        //        {
        //            unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox011.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox010.Text) && !(countTextBox010.Text == "0"))
        //            {
        //                sub010 = decimal.Parse(countTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
        //                sub110 = sub010 + sub010 * Tax / 100;
        //                money10 = Math.Round(sub010, MidpointRounding.AwayFromZero);
        //                moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub110, MidpointRounding.AwayFromZero));
        //                weightTextBox010.ReadOnly = true;
        //                money10 = sub010;
        //            }
        //            else if (!(weightTextBox010.Text == "0") && !string.IsNullOrEmpty(countTextBox010.Text))
        //            {
        //                sub010 = decimal.Parse(weightTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
        //                sub110 = sub010 + sub010 * Tax / 100;
        //                money10 = Math.Round(sub010, MidpointRounding.AwayFromZero);
        //                moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub110, MidpointRounding.AwayFromZero));
        //                countTextBox010.ReadOnly = true;
        //                money10 = sub010;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox09.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox09.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox010.Text))
        //        {
        //            unitPriceTextBox010.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox011.Text))
        //        {
        //            unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox011.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox010.Text) && !(countTextBox010.Text == "0"))
        //            {
        //                sub010 = decimal.Parse(countTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
        //                sub110 = sub010 + sub010 * Tax / 100;
        //                money10 = Math.Round(sub010, MidpointRounding.AwayFromZero);
        //                moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub110, MidpointRounding.AwayFromZero));
        //                weightTextBox010.ReadOnly = true;
        //                money10 = sub010;
        //            }
        //            else if (!(weightTextBox010.Text == "0") && !string.IsNullOrEmpty(countTextBox010.Text))
        //            {
        //                sub010 = decimal.Parse(weightTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
        //                sub110 = sub010 + sub010 * Tax / 100;
        //                money10 = Math.Round(sub010, MidpointRounding.AwayFromZero);
        //                moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub110, MidpointRounding.AwayFromZero));
        //                countTextBox010.ReadOnly = true;
        //                money10 = sub010;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox011.ReadOnly = false;
        //            unitPriceTextBox011.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "12行目"
        //private void unitPriceTextBox011_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox011.Text))
        //        {
        //            unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox012.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox011.Text) && !(countTextBox011.Text == "0"))
        //            {
        //                sub011 = decimal.Parse(countTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
        //                sub111 = sub011 + sub011 * Tax / 100;
        //                money11 = Math.Round(sub011, MidpointRounding.AwayFromZero);
        //                moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub111, MidpointRounding.AwayFromZero));
        //                weightTextBox011.ReadOnly = true;
        //                money11 = sub011;
        //            }
        //            else if (!(weightTextBox011.Text == "0") && !string.IsNullOrEmpty(countTextBox011.Text))
        //            {
        //                sub011 = decimal.Parse(weightTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
        //                sub111 = sub011 + sub011 * Tax / 100;
        //                money11 = Math.Round(sub011, MidpointRounding.AwayFromZero);
        //                moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub111, MidpointRounding.AwayFromZero));
        //                countTextBox011.ReadOnly = true;
        //                money11 = sub011;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox010.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox010.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox011.Text))
        //        {
        //            unitPriceTextBox011.Text = "単価 -> 重量 or 数量";
        //        }
        //        else if (!string.IsNullOrEmpty(unitPriceTextBox012.Text))
        //        {
        //            unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
        //            unitPriceTextBox012.ReadOnly = false;
        //            if (!string.IsNullOrEmpty(weightTextBox011.Text) && !(countTextBox011.Text == "0"))
        //            {
        //                sub011 = decimal.Parse(countTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
        //                sub111 = sub011 + sub011 * Tax / 100;
        //                money11 = Math.Round(sub011, MidpointRounding.AwayFromZero);
        //                moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub111, MidpointRounding.AwayFromZero));
        //                weightTextBox011.ReadOnly = true;
        //                money11 = sub011;
        //            }
        //            else if (!(weightTextBox011.Text == "0") && !string.IsNullOrEmpty(countTextBox011.Text))
        //            {
        //                sub011 = decimal.Parse(weightTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
        //                sub111 = sub011 + sub011 * Tax / 100;
        //                money11 = Math.Round(sub011, MidpointRounding.AwayFromZero);
        //                moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub111, MidpointRounding.AwayFromZero));
        //                countTextBox011.ReadOnly = true;
        //                money11 = sub011;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else
        //        {
        //            unitPriceTextBox012.ReadOnly = false;
        //            unitPriceTextBox012.Text = "単価 -> 重量 or 数量";
        //            unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#region "13行目"
        //private void unitPriceTextBox012_Leave(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (!string.IsNullOrEmpty(unitPriceTextBox012.Text))
        //        {
        //            if (!string.IsNullOrEmpty(weightTextBox012.Text) && !(countTextBox012.Text == "0"))
        //            {
        //                sub012 = decimal.Parse(countTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
        //                sub112 = sub012 + sub012 * Tax / 100;
        //                unitPriceTextBox012.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox012.Text, System.Globalization.NumberStyles.Number));
        //                money12 = Math.Round(sub012, MidpointRounding.AwayFromZero);
        //                moneyTextBox012.Text = string.Format("{0:C}", Math.Round(sub112, MidpointRounding.AwayFromZero));
        //                weightTextBox012.ReadOnly = true;
        //                money12 = sub012;
        //            }
        //            else if (!(weightTextBox012.Text == "0") && !string.IsNullOrEmpty(countTextBox012.Text))
        //            {
        //                sub012 = decimal.Parse(weightTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
        //                sub112 = sub012 + sub012 * Tax / 100;
        //                unitPriceTextBox012.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox012.Text, System.Globalization.NumberStyles.Number));
        //                money12 = Math.Round(sub012, MidpointRounding.AwayFromZero);
        //                moneyTextBox012.Text = string.Format("{0:C}", Math.Round(sub112, MidpointRounding.AwayFromZero));
        //                countTextBox012.ReadOnly = true;
        //                money12 = sub012;
        //            }
        //            else
        //            {

        //            }
        //        }
        //    }
        //    #endregion
        //    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox011.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox011.Text, @"^[a-zA-Z]+$"))
        //    {

        //    }
        //    else
        //    {
        //        if (string.IsNullOrEmpty(unitPriceTextBox012.Text))
        //        {
        //            unitPriceTextBox012.Text = "単価 -> 重量 or 数量";
        //        }
        //        else
        //        {
        //            unitPriceTextBox012.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox012.Text, System.Globalization.NumberStyles.Number));
        //        }
        //    }
        //}
        //#endregion
        //#endregion

        //#region"一時コメントアウト"
        ////#region"計算書　重量×単価"
        ////#region "1行目"
        ////private void weightTextBox0_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox0.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }else if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
        ////    {
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox0.Text) && !(weightTextBox0.Text == "0"))
        ////    {
        ////        int j = weightTextBox0.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight0 = Math.Round(decimal.Parse(weightTextBox0.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox0.Text = weight0.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight0 * decimal.Parse(unitPriceTextBox0.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
        ////        }
        ////        //countTextBox0.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox0.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money0 = Math.Round(sub, MidpointRounding.AwayFromZero);       //計算書は税込み
        ////    moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion

        ////#region "一時コメントアウト"
        /////*
        ////#region "2行目"
        ////private void weightTextBox1_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox1.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox1.Text) && !(weightTextBox1.Text == "0"))
        ////    {
        ////        int j = weightTextBox1.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight1 = Math.Round(decimal.Parse(weightTextBox1.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox1.Text = weight1.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight1 * decimal.Parse(unitPriceTextBox1.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
        ////        }
        ////        countTextBox1.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox1.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "3行目"
        ////private void weightTextBox2_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox2.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox2.Text) && !(weightTextBox2.Text == "0"))
        ////    {
        ////        int j = weightTextBox2.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight2 = Math.Round(decimal.Parse(weightTextBox2.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox2.Text = weight2.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight2 * decimal.Parse(unitPriceTextBox2.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
        ////        }
        ////        countTextBox2.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox2.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "4行目"
        ////private void weightTextBox3_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox3.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox3.Text) && !(weightTextBox3.Text == "0"))
        ////    {
        ////        int j = weightTextBox3.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight3 = Math.Round(decimal.Parse(weightTextBox3.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox3.Text = weight3.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight3 * decimal.Parse(unitPriceTextBox3.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
        ////        }
        ////        countTextBox3.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox3.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "5行目"
        ////private void weightTextBox4_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox4.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox4.Text) && !(weightTextBox4.Text == "0"))
        ////    {
        ////        int j = weightTextBox4.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight4 = Math.Round(decimal.Parse(weightTextBox4.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox4.Text = weight4.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight4 * decimal.Parse(unitPriceTextBox4.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
        ////        }
        ////        countTextBox4.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox4.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money4 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "6行目"
        ////private void weightTextBox5_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox5.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox5.Text) && !(weightTextBox5.Text == "0"))
        ////    {
        ////        int j = weightTextBox5.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight5 = Math.Round(decimal.Parse(weightTextBox5.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox5.Text = weight5.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight5 * decimal.Parse(unitPriceTextBox5.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
        ////        }
        ////        countTextBox5.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox5.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money5 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "7行目"
        ////private void weightTextBox6_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox6.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox6.Text) && !(weightTextBox6.Text == "0"))
        ////    {
        ////        int j = weightTextBox6.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight6 = Math.Round(decimal.Parse(weightTextBox6.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox6.Text = weight6.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight6 * decimal.Parse(unitPriceTextBox6.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
        ////        }
        ////        countTextBox6.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox6.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "8行目"
        ////private void weightTextBox7_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox7.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox7.Text) && !(weightTextBox7.Text == "0"))
        ////    {
        ////        int j = weightTextBox7.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight7 = Math.Round(decimal.Parse(weightTextBox7.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox7.Text = weight7.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight7 * decimal.Parse(unitPriceTextBox7.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
        ////        }
        ////        countTextBox7.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox7.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "9行目"
        ////private void weightTextBox8_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox8.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox8.Text) && !(weightTextBox8.Text == "0"))
        ////    {
        ////        int j = weightTextBox8.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight8 = Math.Round(decimal.Parse(weightTextBox8.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox8.Text = weight8.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight8 * decimal.Parse(unitPriceTextBox8.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
        ////        }
        ////        countTextBox8.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox8.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money8 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "10行目"
        ////private void weightTextBox9_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox9.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox9.Text) && !(weightTextBox9.Text == "0"))
        ////    {
        ////        int j = weightTextBox9.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight9 = Math.Round(decimal.Parse(weightTextBox9.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox9.Text = weight9.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight9 * decimal.Parse(unitPriceTextBox9.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
        ////        }
        ////        countTextBox9.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox9.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "11行目"
        ////private void weightTextBox10_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox10.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox10.Text) && !(weightTextBox10.Text == "0"))
        ////    {
        ////        int j = weightTextBox10.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight10 = Math.Round(decimal.Parse(weightTextBox10.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox10.Text = weight10.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight10 * decimal.Parse(unitPriceTextBox10.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
        ////        }
        ////        countTextBox10.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox10.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "12行目"
        ////private void weightTextBox11_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox11.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox11.Text) && !(weightTextBox11.Text == "0"))
        ////    {
        ////        int j = weightTextBox11.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight11 = Math.Round(decimal.Parse(weightTextBox11.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox11.Text = weight11.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight11 * decimal.Parse(unitPriceTextBox11.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
        ////        }
        ////        countTextBox11.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox11.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////#region "13行目"
        ////private void weightTextBox12_Leave(object sender, EventArgs e)
        ////{
        ////    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox12.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(weightTextBox12.Text) && !(weightTextBox12.Text == "0"))
        ////    {
        ////        int j = weightTextBox12.Text.IndexOf(".");
        ////        if (j > -1)     //小数点あり
        ////        {
        ////            //重量の四捨五入
        ////            decimal weight12 = Math.Round(decimal.Parse(weightTextBox12.Text), 1, MidpointRounding.AwayFromZero);
        ////            weightTextBox12.Text = weight12.ToString();

        ////            //重量×単価の切捨て
        ////            sub = Math.Floor(weight12 * decimal.Parse(unitPriceTextBox12.Text));
        ////        }
        ////        else        //小数点なし
        ////        {
        ////            sub = decimal.Parse(weightTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
        ////        }
        ////        countTextBox12.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        countTextBox12.ReadOnly = false;
        ////    }
        ////    //sub += sub * Tax / 100;
        ////    money12 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////    moneyTextBox12.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////}
        ////#endregion
        ////*/
        ////#endregion
        ////#endregion

        ////#region"計算書　数量×単価"
        ////#region "1行目"
        ////private void countTextBox0_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox0.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox0.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox0.Text) && !(countTextBox0.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
        ////        //sub += sub * Tax / 100;
        ////        money0 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox0.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        //weightTextBox0.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox0.ReadOnly = false;
        ////    }
        ////}
        ////#endregion

        ////#region "一時コメントアウト"
        /////*
        ////#region "2行目"
        ////private void countTextBox1_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox1.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox1.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox1.Text) && !(countTextBox1.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
        ////        //sub += sub * Tax / 100;
        ////        money1 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox1.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox1.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox1.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "3行目"
        ////private void countTextBox2_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox2.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox2.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox2.Text) && !(countTextBox2.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
        ////        //sub += sub * Tax / 100;
        ////        money2 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox2.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox2.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox2.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "4行目"
        ////private void countTextBox3_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox3.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox3.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox3.Text) && !(countTextBox3.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
        ////        //sub += sub * Tax / 100;
        ////        money3 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox3.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox3.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox3.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "5行目"
        ////private void countTextBox4_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox4.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox4.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox4.Text) && !(countTextBox4.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
        ////        //sub += sub * Tax / 100;
        ////        money4 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox4.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox4.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox4.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "6行目"
        ////private void countTextBox5_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox5.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox5.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox5.Text) && !(countTextBox5.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
        ////        //sub += sub * Tax / 100;
        ////        money5 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox5.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox5.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox5.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "7行目"
        ////private void countTextBox6_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox6.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox6.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox6.Text) && !(countTextBox6.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
        ////        //sub += sub * Tax / 100;
        ////        money6 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox6.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox6.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox6.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "8行目"
        ////private void countTextBox7_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox7.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox7.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox7.Text) && !(countTextBox7.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
        ////        //sub += sub * Tax / 100;
        ////        money7 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox7.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox7.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox7.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "9行目"
        ////private void countTextBox8_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox8.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox8.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox8.Text) && !(countTextBox8.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
        ////        //sub += sub * Tax / 100;
        ////        money8 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox8.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox8.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox8.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "10行目"
        ////private void countTextBox9_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox9.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox9.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox9.Text) && !(countTextBox9.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
        ////        //sub += sub * Tax / 100;
        ////        money9 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox9.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox9.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox9.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "11行目"
        ////private void countTextBox10_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox10.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox10.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox10.Text) && !(countTextBox10.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
        ////        //sub += sub * Tax / 100;
        ////        money10 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox10.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox10.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox10.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "12行目"
        ////private void countTextBox11_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox11.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox11.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    if (!string.IsNullOrEmpty(countTextBox11.Text) && !(countTextBox11.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
        ////        //sub += sub * Tax / 100;
        ////        money11 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox11.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox11.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox11.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////#region "13行目"
        ////private void countTextBox12_Leave(object sender, EventArgs e)
        ////{
        ////    int st = countTextBox12.Text.IndexOf(".");
        ////    if (st > -1)
        ////    {
        ////        MessageBox.Show("整数を入力して下さい。");
        ////        return;
        ////    }
        ////    else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox12.Text, @"^[a-zA-Z]+$"))
        ////    {
        ////        MessageBox.Show("正しく入力して下さい。");
        ////        return;
        ////    }
        ////    else if (!string.IsNullOrEmpty(countTextBox12.Text) && !(countTextBox12.Text == "0"))
        ////    {
        ////        sub = decimal.Parse(countTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
        ////        //sub += sub * Tax / 100;
        ////        money12 = Math.Round(sub, MidpointRounding.AwayFromZero);
        ////        moneyTextBox12.Text = string.Format("{0:C}", Math.Round(sub, MidpointRounding.AwayFromZero));
        ////        weightTextBox12.ReadOnly = true;
        ////    }
        ////    else
        ////    {
        ////        weightTextBox12.ReadOnly = false;
        ////    }
        ////}
        ////#endregion
        ////*/
        ////#endregion
        ////#endregion
        //#endregion

        //#region "納品書　重量×単価"
        //#region "重量1行目"
        //private void weightTextBox00_Leave(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (!string.IsNullOrEmpty(weightTextBox00.Text) && !(weightTextBox00.Text == "0"))
        //    {
        //        int j = weightTextBox00.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight0 = Math.Round(decimal.Parse(weightTextBox00.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox00.Text = weight0.ToString();

        //            //重量×単価の切捨て
        //            sub00 = Math.Floor(weight0 * decimal.Parse(unitPriceTextBox00.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub00 = decimal.Parse(weightTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
        //        }
        //        countTextBox00.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox00.ReadOnly = false;
        //    }
        //    money0 = sub00;
        //    sub10 = sub00 + sub00 * Tax / 100;
        //    moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub00, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量2行目"
        //private void weightTextBox01_Leave(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox01.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (!string.IsNullOrEmpty(weightTextBox01.Text) && !(weightTextBox01.Text == "0"))
        //    {
        //        int j = weightTextBox01.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight1 = Math.Round(decimal.Parse(weightTextBox01.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox01.Text = weight1.ToString();

        //            //重量×単価の切捨て
        //            sub01 = Math.Floor(weight1 * decimal.Parse(unitPriceTextBox01.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub01 = decimal.Parse(weightTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
        //        }
        //        countTextBox01.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox01.ReadOnly = false;
        //    }
        //    money1 = sub01;
        //    sub11 = sub01 + sub01 * Tax / 100;
        //    moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub01, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量3行目"
        //private void weightTextBox02_Leave(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox02.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (!string.IsNullOrEmpty(weightTextBox02.Text) && !(weightTextBox02.Text == "0"))
        //    {
        //        int j = weightTextBox02.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight2 = Math.Round(decimal.Parse(weightTextBox02.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox02.Text = weight2.ToString();

        //            //重量×単価の切捨て
        //            sub02 = Math.Floor(weight2 * decimal.Parse(unitPriceTextBox02.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub02 = decimal.Parse(weightTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
        //        }
        //        countTextBox02.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox02.ReadOnly = false;
        //    }
        //    money2 = sub02;
        //    sub12 = sub02 + sub02 * Tax / 100;
        //    moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub02, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量4行目"
        //private void weightTextBox03_Leave(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(weightTextBox03.Text, @"^[a-zA-Z]+$"))
        //    {
        //        MessageBox.Show("正しく入力して下さい。");
        //        return;
        //    }
        //    else if (!string.IsNullOrEmpty(weightTextBox03.Text) && !(weightTextBox03.Text == "0"))
        //    {
        //        int j = weightTextBox03.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight3 = Math.Round(decimal.Parse(weightTextBox03.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox03.Text = weight3.ToString();

        //            //重量×単価の切捨て
        //            sub03 = Math.Floor(weight3 * decimal.Parse(unitPriceTextBox03.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub03 = decimal.Parse(weightTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
        //        }
        //        countTextBox03.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox03.ReadOnly = false;
        //    }
        //    money3 = sub03;
        //    sub13 = sub03 + sub03 * Tax / 100;
        //    moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub03, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量5行目"
        //private void weightTextBox04_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox04.Text) && !(weightTextBox04.Text == "0"))
        //    {
        //        int j = weightTextBox04.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight4 = Math.Round(decimal.Parse(weightTextBox04.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox04.Text = weight4.ToString();

        //            //重量×単価の切捨て
        //            sub04 = Math.Floor(weight4 * decimal.Parse(unitPriceTextBox04.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub04 = decimal.Parse(weightTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
        //        }
        //        countTextBox04.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox04.ReadOnly = false;
        //    }
        //    money4 = sub04;
        //    sub14 = sub04 + sub04 * Tax / 100;
        //    moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub04, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量6行目"
        //private void weightTextBox05_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox05.Text) && !(weightTextBox05.Text == "0"))
        //    {
        //        int j = weightTextBox05.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight5 = Math.Round(decimal.Parse(weightTextBox05.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox05.Text = weight5.ToString();

        //            //重量×単価の切捨て
        //            sub05 = Math.Floor(weight5 * decimal.Parse(unitPriceTextBox05.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub05 = decimal.Parse(weightTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
        //        }
        //        countTextBox05.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox05.ReadOnly = false;
        //    }
        //    money5 = sub05;
        //    sub15 = sub05 + sub05 * Tax / 100;
        //    moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub05, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量7行目"
        //private void weightTextBox06_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox06.Text) && !(weightTextBox06.Text == "0"))
        //    {
        //        int j = weightTextBox06.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight6 = Math.Round(decimal.Parse(weightTextBox06.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox06.Text = weight6.ToString();

        //            //重量×単価の切捨て
        //            sub06 = Math.Floor(weight6 * decimal.Parse(unitPriceTextBox06.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub06 = decimal.Parse(weightTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
        //        }
        //        countTextBox06.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox06.ReadOnly = false;
        //    }
        //    money6 = sub06;
        //    sub16 = sub06 + sub06 * Tax / 100;
        //    moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub06, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量8行目"
        //private void weightTextBox07_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox07.Text) && !(weightTextBox07.Text == "0"))
        //    {
        //        int j = weightTextBox07.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight7 = Math.Round(decimal.Parse(weightTextBox07.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox07.Text = weight7.ToString();

        //            //重量×単価の切捨て
        //            sub07 = Math.Floor(weight7 * decimal.Parse(unitPriceTextBox07.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub07 = decimal.Parse(weightTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
        //        }
        //        countTextBox07.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox07.ReadOnly = false;
        //    }
        //    money7 = sub07;
        //    sub17 = sub07 + sub07 * Tax / 100;
        //    moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub07, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量9行目"
        //private void weightTextBox08_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox08.Text) && !(weightTextBox08.Text == "0"))
        //    {
        //        int j = weightTextBox08.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight8 = Math.Round(decimal.Parse(weightTextBox08.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox08.Text = weight8.ToString();

        //            //重量×単価の切捨て
        //            sub08 = Math.Floor(weight8 * decimal.Parse(unitPriceTextBox08.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub08 = decimal.Parse(weightTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
        //        }
        //        countTextBox08.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox08.ReadOnly = false;
        //    }
        //    money8 = sub08;
        //    sub18 = sub08 + sub08 * Tax / 100;
        //    moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub08, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量10行目"
        //private void weightTextBox09_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox09.Text) && !(weightTextBox09.Text == "0"))
        //    {
        //        int j = weightTextBox09.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight9 = Math.Round(decimal.Parse(weightTextBox09.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox09.Text = weight9.ToString();

        //            //重量×単価の切捨て
        //            sub09 = Math.Floor(weight9 * decimal.Parse(unitPriceTextBox09.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub09 = decimal.Parse(weightTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
        //        }
        //        countTextBox09.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox09.ReadOnly = false;
        //    }
        //    money9 = sub09;
        //    sub19 = sub09 + sub09 * Tax / 100;
        //    moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub09, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量11行目"
        //private void weightTextBox010_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox010.Text) && !(weightTextBox010.Text == "0"))
        //    {
        //        int j = weightTextBox010.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight10 = Math.Round(decimal.Parse(weightTextBox010.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox010.Text = weight10.ToString();

        //            //重量×単価の切捨て
        //            sub010 = Math.Floor(weight10 * decimal.Parse(unitPriceTextBox010.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub010 = decimal.Parse(weightTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
        //        }
        //        countTextBox010.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox010.ReadOnly = false;
        //    }
        //    money10 = sub010;
        //    sub110 = sub010 + sub010 * Tax / 100;
        //    moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub010, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量12行目"
        //private void weightTextBox011_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox011.Text) && !(weightTextBox011.Text == "0"))
        //    {
        //        int j = weightTextBox011.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight11 = Math.Round(decimal.Parse(weightTextBox011.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox011.Text = weight11.ToString();

        //            //重量×単価の切捨て
        //            sub011 = Math.Floor(weight11 * decimal.Parse(unitPriceTextBox011.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub011 = decimal.Parse(weightTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
        //        }
        //        countTextBox011.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox011.ReadOnly = false;
        //    }
        //    money11 = sub011;
        //    sub111 = sub011 + sub011 * Tax / 100;
        //    moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub011, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "重量13行目"
        //private void weightTextBox012_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(weightTextBox012.Text) && !(weightTextBox012.Text == "0"))
        //    {
        //        int j = weightTextBox012.Text.IndexOf(".");
        //        if (j > -1)     //小数点あり
        //        {
        //            //重量の四捨五入
        //            decimal weight12 = Math.Round(decimal.Parse(weightTextBox012.Text), 1, MidpointRounding.AwayFromZero);
        //            weightTextBox012.Text = weight12.ToString();

        //            //重量×単価の切捨て
        //            sub012 = Math.Floor(weight12 * decimal.Parse(unitPriceTextBox012.Text));
        //        }
        //        else        //小数点なし
        //        {
        //            sub012 = decimal.Parse(weightTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
        //        }
        //        countTextBox012.ReadOnly = true;
        //    }
        //    else
        //    {
        //        countTextBox012.ReadOnly = false;
        //    }
        //    money12 = sub012;
        //    sub112 = sub012 + sub012 * Tax / 100;
        //    moneyTextBox012.Text = string.Format("{0:C}", Math.Round(sub012, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#endregion

        //#region　"納品書　数量×単価"
        //#region "数量1行目"
        //private void countTextBox00_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox00.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox00.Text) && !(countTextBox00.Text == "0"))
        //        {
        //            sub00 = decimal.Parse(countTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
        //            weightTextBox00.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox00.ReadOnly = false;
        //        }
        //        money0 = sub00;
        //        sub10 = sub00 + sub00 * Tax / 100;
        //        moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub00, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量2行目"
        //private void countTextBox01_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox01.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox01.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox01.Text) && !(countTextBox01.Text == "0"))
        //        {
        //            sub01 = decimal.Parse(countTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
        //            weightTextBox01.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox01.ReadOnly = false;
        //        }
        //        money1 = sub01;
        //        sub11 = sub01 + sub01 * Tax / 100;
        //        moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub01, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量3行目"
        //private void countTextBox02_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox02.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox02.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox02.Text) && !(countTextBox02.Text == "0"))
        //        {
        //            sub02 = decimal.Parse(countTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
        //            weightTextBox02.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox02.ReadOnly = false;
        //        }
        //        money2 = sub02;
        //        sub12 = sub02 + sub02 * Tax / 100;
        //        moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub02, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量4行目"
        //private void countTextBox03_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox03.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox03.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox03.Text) && !(countTextBox03.Text == "0"))
        //        {
        //            sub03 = decimal.Parse(countTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
        //            weightTextBox03.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox03.ReadOnly = false;
        //        }
        //        money3 = sub03;
        //        sub13 = sub03 + sub03 * Tax / 100;
        //        moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub03, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量5行目"
        //private void countTextBox04_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox04.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox04.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox04.Text) && !(countTextBox04.Text == "0"))
        //        {
        //            sub04 = decimal.Parse(countTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
        //            weightTextBox04.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox04.ReadOnly = false;
        //        }
        //        money4 = sub04;
        //        sub14 = sub04 + sub04 * Tax / 100;
        //        moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub04, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量6行目"
        //private void countTextBox05_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox05.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox05.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox05.Text) && !(countTextBox05.Text == "0"))
        //        {
        //            sub05 = decimal.Parse(countTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
        //            weightTextBox05.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox05.ReadOnly = false;
        //        }
        //        money5 = sub05;
        //        sub15 = sub05 + sub05 * Tax / 100;
        //        moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub05, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量7行目"
        //private void countTextBox06_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox06.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox06.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox06.Text) && !(countTextBox06.Text == "0"))
        //        {
        //            sub06 = decimal.Parse(countTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
        //            weightTextBox06.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox06.ReadOnly = false;
        //        }
        //        money6 = sub06;
        //        sub16 = sub06 + sub06 * Tax / 100;
        //        moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub06, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量8行目"
        //private void countTextBox07_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox07.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox07.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox07.Text) && !(countTextBox07.Text == "0"))
        //        {
        //            sub07 = decimal.Parse(countTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
        //            weightTextBox07.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox07.ReadOnly = false;
        //        }
        //        money7 = sub07;
        //        sub17 = sub07 + sub07 * Tax / 100;
        //        moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub07, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量9行目"
        //private void countTextBox08_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox08.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox08.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox08.Text) && !(countTextBox08.Text == "0"))
        //        {
        //            sub08 = decimal.Parse(countTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
        //            weightTextBox08.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox08.ReadOnly = false;
        //        }
        //        money8 = sub08;
        //        sub18 = sub08 + sub08 * Tax / 100;
        //        moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub08, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量10行目"
        //private void countTextBox09_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox09.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox09.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox09.Text) && !(countTextBox09.Text == "0"))
        //        {
        //            sub09 = decimal.Parse(countTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
        //            weightTextBox09.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox09.ReadOnly = false;
        //        }
        //        money9 = sub09;
        //        sub19 = sub09 + sub09 * Tax / 100;
        //        moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub09, MidpointRounding.AwayFromZero));
        //    }

        //}
        //#endregion
        //#region "数量11行目"
        //private void countTextBox010_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox010.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox010.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox010.Text) && !(countTextBox010.Text == "0"))
        //        {
        //            sub010 = decimal.Parse(countTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
        //            weightTextBox010.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox010.ReadOnly = false;
        //        }
        //        money10 = sub010;
        //        sub110 = sub010 + sub010 * Tax / 100;
        //        moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub010, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量12行目"
        //private void countTextBox011_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox011.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox011.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox011.Text) && !(countTextBox011.Text == "0"))
        //        {
        //            sub011 = decimal.Parse(countTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
        //            weightTextBox011.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox011.ReadOnly = false;
        //        }
        //        money11 = sub011;
        //        sub111 = sub011 + sub011 * Tax / 100;
        //        moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub011, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "数量13行目"
        //private void countTextBox012_Leave(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {

        //    }
        //    else
        //    {
        //        int co = countTextBox012.Text.IndexOf(".");
        //        if (co > -1)
        //        {
        //            MessageBox.Show("整数を入力して下さい。");
        //            return;
        //        }
        //        else if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox012.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        else if (!string.IsNullOrEmpty(countTextBox012.Text) && !(countTextBox012.Text == "0"))
        //        {
        //            sub012 = decimal.Parse(countTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
        //            weightTextBox012.ReadOnly = true;
        //        }
        //        else
        //        {
        //            weightTextBox012.ReadOnly = false;
        //        }
        //        money12 = sub012;
        //        sub112 = sub012 + sub012 * Tax / 100;
        //        moneyTextBox012.Text = string.Format("{0:C}", Math.Round(sub012, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#endregion

        //#region"納品書　金額が入力されたら総重量 or 総数計算、自動計算"
        //#region "1行目"
        //private void moneyTextBox00_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        //重量×単価
        //        else if (!string.IsNullOrEmpty(weightTextBox00.Text) && string.IsNullOrEmpty(countTextBox00.Text))
        //        {
        //            countTextBox00.Text = 0.ToString();

        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox00.Text) && string.IsNullOrEmpty(weightTextBox00.Text))
        //        {
        //            weightTextBox00.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum00 = int.Parse(countTextBox00.Text);
        //        weisum00 = decimal.Parse(weightTextBox00.Text);
        //        subSum00 = money0;
        //        #region "金額合計の場合わけ"
        //        if (subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum00;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum00;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum00;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(countTextBox00.Text, @"^[a-zA-Z]+$") || System.Text.RegularExpressions.Regex.IsMatch(weightTextBox00.Text, @"^[a-zA-Z]+$"))
        //        {
        //            MessageBox.Show("正しく入力して下さい。");
        //            return;
        //        }
        //        //重量×単価
        //        else if (!string.IsNullOrEmpty(weightTextBox00.Text) && string.IsNullOrEmpty(countTextBox00.Text))
        //        {
        //            countTextBox00.Text = 0.ToString();

        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox00.Text) && string.IsNullOrEmpty(weightTextBox00.Text))
        //        {
        //            weightTextBox00.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        #region "顧客選択から戻ってきた時(コメントアウト)"
        //        /*if (amount00 != 0)
        //        {
        //            #region "日付呼び出し"
        //            DataTable upddt = new DataTable();
        //            string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
        //            adapter = new NpgsqlDataAdapter(str_sql_if, conn);
        //            adapter.Fill(upddt);
        //            DataRow dataRow2;
        //            dataRow2 = upddt.Rows[0];
        //            string date = dataRow2["upd_date"].ToString();
        //            #endregion

        //            DataTable dt10 = new DataTable();
        //            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 1 + " and upd_date = '" + date + "';";
        //            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
        //            adapter.Fill(dt10);
        //            DataRow dataRow1;
        //            dataRow1 = dt10.Rows[0];
        //            money0 = (decimal)dataRow1["amount"] / 1.1m;

        //        }*/
        //        #endregion
        //        countsum00 = int.Parse(countTextBox00.Text);
        //        weisum00 = decimal.Parse(weightTextBox00.Text);
        //        subSum00 = money0;
        //        #region "金額合計の場合わけ"
        //        if (subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum00;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum00;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum00;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "2行目"
        //private void moneyTextBox01_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox01.Text) && string.IsNullOrEmpty(countTextBox01.Text))
        //        {
        //            countTextBox01.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox01.Text) && string.IsNullOrEmpty(weightTextBox01.Text))
        //        {
        //            weightTextBox01.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum01 = int.Parse(countTextBox01.Text);
        //        weisum01 = decimal.Parse(weightTextBox01.Text);
        //        subSum01 = money1;       //納品書は税抜き
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum01;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum01;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum01;
        //        }
        //        #endregion


        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox01.Text) && string.IsNullOrEmpty(countTextBox01.Text))
        //        {
        //            countTextBox01.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox01.Text) && string.IsNullOrEmpty(weightTextBox01.Text))
        //        {
        //            weightTextBox01.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        #region "顧客選択から戻ってきた時(コメントアウト)"
        //        /*if (amount01 != 0)
        //        {
        //            #region "日付呼び出し"
        //            DataTable upddt = new DataTable();
        //            string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
        //            adapter = new NpgsqlDataAdapter(str_sql_if, conn);
        //            adapter.Fill(upddt);
        //            DataRow dataRow2;
        //            dataRow2 = upddt.Rows[0];
        //            string date = dataRow2["upd_date"].ToString();
        //            #endregion
        //            DataTable dt10 = new DataTable();
        //            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 2 + " and upd_date = '" + date + "';";
        //            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
        //            adapter.Fill(dt10);
        //            DataRow dataRow1;
        //            dataRow1 = dt10.Rows[0];
        //            money1 = (decimal)dataRow1["amount"] / 1.1m;
        //        }*/
        //        #endregion

        //        countsum01 = int.Parse(countTextBox01.Text);
        //        weisum01 = decimal.Parse(weightTextBox01.Text);
        //        subSum01 = money1;       //納品書は税抜き
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum01;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum01;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum01;
        //        }
        //        #endregion


        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }

        //}
        //#endregion
        //#region "3行目"
        //private void moneyTextBox02_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox02.Text) && string.IsNullOrEmpty(countTextBox02.Text))
        //        {
        //            countTextBox02.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox02.Text) && string.IsNullOrEmpty(weightTextBox02.Text))
        //        {
        //            weightTextBox02.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum02 = int.Parse(countTextBox02.Text);
        //        weisum02 = decimal.Parse(weightTextBox02.Text);
        //        subSum02 = money2;
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum02;
        //        }
        //        #endregion

        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum02;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum02;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox02.Text) && string.IsNullOrEmpty(countTextBox02.Text))
        //        {
        //            countTextBox02.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox02.Text) && string.IsNullOrEmpty(weightTextBox02.Text))
        //        {
        //            weightTextBox02.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        #region "顧客選択から戻ってきた時(コメントアウト)"
        //        /*if (amount02 != 0)
        //        {
        //            #region "日付呼び出し"
        //            DataTable upddt = new DataTable();
        //            string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
        //            adapter = new NpgsqlDataAdapter(str_sql_if, conn);
        //            adapter.Fill(upddt);
        //            DataRow dataRow2;
        //            dataRow2 = upddt.Rows[0];
        //            string date = dataRow2["upd_date"].ToString();
        //            #endregion
        //            DataTable dt10 = new DataTable();
        //            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 3 + " and upd_date = '" + date + "';";
        //            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
        //            adapter.Fill(dt10);
        //            DataRow dataRow1;
        //            dataRow1 = dt10.Rows[0];
        //            money2 = (decimal)dataRow1["amount"] / 1.1m;
        //        }*/
        //        #endregion
        //        countsum02 = int.Parse(countTextBox02.Text);
        //        weisum02 = decimal.Parse(weightTextBox02.Text);
        //        subSum02 = money2;
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum02;
        //        }
        //        #endregion

        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum02;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum02;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }

        //}
        //#endregion
        //#region "4行目"
        //private void moneyTextBox03_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox03.Text) && string.IsNullOrEmpty(countTextBox03.Text))
        //        {
        //            countTextBox03.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox03.Text) && string.IsNullOrEmpty(weightTextBox03.Text))
        //        {
        //            weightTextBox03.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum03 = int.Parse(countTextBox03.Text);
        //        weisum03 = decimal.Parse(weightTextBox03.Text);
        //        subSum03 = money3;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum03;
        //        }
        //        #endregion

        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum03;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum03;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox03.Text) && string.IsNullOrEmpty(countTextBox03.Text))
        //        {
        //            countTextBox03.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox03.Text) && string.IsNullOrEmpty(weightTextBox03.Text))
        //        {
        //            weightTextBox03.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        #region "顧客選択から戻ってきた時(コメントアウト)"
        //        /*if (amount03 != 0)
        //        {
        //            #region "日付呼び出し"
        //            DataTable upddt = new DataTable();
        //            string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
        //            adapter = new NpgsqlDataAdapter(str_sql_if, conn);
        //            adapter.Fill(upddt);
        //            DataRow dataRow2;
        //            dataRow2 = upddt.Rows[0];
        //            string date = dataRow2["upd_date"].ToString();
        //            #endregion
        //            DataTable dt10 = new DataTable();
        //            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 4 + " and upd_date = '" + date + "';";
        //            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
        //            adapter.Fill(dt10);
        //            DataRow dataRow1;
        //            dataRow1 = dt10.Rows[0];
        //            money3 = (decimal)dataRow1["amount"] / 1.1m;
        //        }*/
        //        #endregion
        //        countsum03 = int.Parse(countTextBox03.Text);
        //        weisum03 = decimal.Parse(weightTextBox03.Text);
        //        subSum03 = money3;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum03;
        //        }
        //        #endregion

        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum03;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum03;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "5行目"
        //private void moneyTextBox04_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox04.Text) && string.IsNullOrEmpty(countTextBox04.Text))
        //        {
        //            countTextBox04.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox04.Text) && string.IsNullOrEmpty(weightTextBox04.Text))
        //        {
        //            weightTextBox04.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum04 = int.Parse(countTextBox04.Text);
        //        weisum04 = decimal.Parse(weightTextBox04.Text);
        //        subSum04 = money4;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum04;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum04;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum04;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox04.Text) && string.IsNullOrEmpty(countTextBox04.Text))
        //        {
        //            countTextBox04.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox04.Text) && string.IsNullOrEmpty(weightTextBox04.Text))
        //        {
        //            weightTextBox04.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        countsum04 = int.Parse(countTextBox04.Text);
        //        weisum04 = decimal.Parse(weightTextBox04.Text);
        //        subSum04 = money4;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum04;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum04;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum04;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "6行目"
        //private void moneyTextBox05_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox05.Text) && string.IsNullOrEmpty(countTextBox05.Text))
        //        {
        //            countTextBox05.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox05.Text) && string.IsNullOrEmpty(weightTextBox05.Text))
        //        {
        //            weightTextBox05.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum05 = int.Parse(countTextBox05.Text);
        //        weisum05 = decimal.Parse(weightTextBox05.Text);
        //        subSum05 = money5;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum05;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum05;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum05;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox05.Text) && string.IsNullOrEmpty(countTextBox05.Text))
        //        {
        //            countTextBox05.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox05.Text) && string.IsNullOrEmpty(weightTextBox05.Text))
        //        {
        //            weightTextBox05.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        #region "顧客選択から戻ってきた時(コメントアウト)"
        //        /*if (amount05 != 0)
        //        {
        //            #region "日付呼び出し"
        //            DataTable upddt = new DataTable();
        //            string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
        //            adapter = new NpgsqlDataAdapter(str_sql_if, conn);
        //            adapter.Fill(upddt);
        //            DataRow dataRow2;
        //            dataRow2 = upddt.Rows[0];
        //            string date = dataRow2["upd_date"].ToString();
        //            #endregion
        //            DataTable dt10 = new DataTable();
        //            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 6 + " and upd_date = '" + date + "';";
        //            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
        //            adapter.Fill(dt10);
        //            DataRow dataRow1;
        //            dataRow1 = dt10.Rows[0];
        //            money5 = (decimal)dataRow1["amount"] / 1.1m;
        //        }*/
        //        #endregion
        //        countsum05 = int.Parse(countTextBox05.Text);
        //        weisum05 = decimal.Parse(weightTextBox05.Text);
        //        subSum05 = money5;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum05;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum05;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum05;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "7行目"
        //private void moneyTextBox06_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox06.Text) && string.IsNullOrEmpty(countTextBox06.Text))
        //        {
        //            countTextBox06.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox06.Text) && string.IsNullOrEmpty(weightTextBox06.Text))
        //        {
        //            weightTextBox06.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum06 = int.Parse(countTextBox06.Text);
        //        weisum06 = decimal.Parse(weightTextBox06.Text);
        //        subSum06 = money6;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum06;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum06;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum06;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox06.Text) && string.IsNullOrEmpty(countTextBox06.Text))
        //        {
        //            countTextBox06.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox06.Text) && string.IsNullOrEmpty(weightTextBox06.Text))
        //        {
        //            weightTextBox06.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        #region "顧客選択から戻ってきた時(コメントアウト)"
        //        /*if (amount06 != 0)
        //        {
        //            #region "日付呼び出し"
        //            DataTable upddt = new DataTable();
        //            string str_sql_if = "select * from delivery_calc_if where control_number = " + control + "ORDER BY upd_date DESC;";
        //            adapter = new NpgsqlDataAdapter(str_sql_if, conn);
        //            adapter.Fill(upddt);
        //            DataRow dataRow2;
        //            dataRow2 = upddt.Rows[0];
        //            string date = dataRow2["upd_date"].ToString();
        //            #endregion
        //            DataTable dt10 = new DataTable();
        //            string str_sql_if3 = "select * from delivery_calc_if where control_number = " + control + " and record_number = " + 7 + " and upd_date = '"  + date + "';";
        //            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
        //            adapter.Fill(dt10);
        //            DataRow dataRow1;
        //            dataRow1 = dt10.Rows[0];
        //            money6 = (decimal)dataRow1["amount"] / 1.1m;
        //        }*/
        //        #endregion
        //        countsum06 = int.Parse(countTextBox06.Text);
        //        weisum06 = decimal.Parse(weightTextBox06.Text);
        //        subSum06 = money6;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum06;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum06;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum06;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "8行目"
        //private void moneyTextBox07_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox07.Text) && string.IsNullOrEmpty(countTextBox07.Text))
        //        {
        //            countTextBox07.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox07.Text) && string.IsNullOrEmpty(weightTextBox07.Text))
        //        {
        //            weightTextBox07.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum07 = int.Parse(countTextBox07.Text);
        //        weisum07 = decimal.Parse(weightTextBox07.Text);
        //        subSum07 = money7;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum07;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum07;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum07;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {

        //    }
        //    //重量×単価
        //    if (!string.IsNullOrEmpty(weightTextBox07.Text) && string.IsNullOrEmpty(countTextBox07.Text))
        //    {
        //        countTextBox07.Text = 0.ToString();
        //    }
        //    //数量×単価
        //    else if (!string.IsNullOrEmpty(countTextBox07.Text) && string.IsNullOrEmpty(weightTextBox07.Text))
        //    {
        //        weightTextBox07.Text = 0.ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("数値を入力してください");
        //        return;
        //    }
        //    countsum07 = int.Parse(countTextBox07.Text);
        //    weisum07 = decimal.Parse(weightTextBox07.Text);
        //    subSum07 = money7;     //増やしていく
        //    #region "金額合計の場合わけ"
        //    if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //    {
        //        subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //    }
        //    else
        //    {
        //        subSum = subSum07;
        //    }
        //    #endregion
        //    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //    sum = subSum + TaxAmount;
        //    #region "数量の場合わけ"
        //    if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //    {
        //        countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //    }
        //    else
        //    {
        //        countsum = countsum07;
        //    }
        //    #endregion
        //    #region "重量の場合わけ"
        //    if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //    {
        //        weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //    }
        //    else
        //    {
        //        weisum = weisum07;
        //    }
        //    #endregion

        //    totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //    totalCount2.Text = string.Format("{0:#,0}", countsum);
        //    subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //    taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //    sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "9行目"
        //private void moneyTextBox08_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox08.Text) && string.IsNullOrEmpty(countTextBox08.Text))
        //        {
        //            countTextBox08.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox08.Text) && string.IsNullOrEmpty(weightTextBox08.Text))
        //        {
        //            weightTextBox08.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum08 = int.Parse(countTextBox08.Text);
        //        weisum08 = decimal.Parse(weightTextBox08.Text);
        //        subSum08 = money8;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum08;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum08;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum08;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {

        //    }
        //    //重量×単価
        //    if (!string.IsNullOrEmpty(weightTextBox08.Text) && string.IsNullOrEmpty(countTextBox08.Text))
        //    {
        //        countTextBox08.Text = 0.ToString();
        //    }
        //    //数量×単価
        //    else if (!string.IsNullOrEmpty(countTextBox08.Text) && string.IsNullOrEmpty(weightTextBox08.Text))
        //    {
        //        weightTextBox08.Text = 0.ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("数値を入力してください");
        //        return;
        //    }
        //    countsum08 = int.Parse(countTextBox08.Text);
        //    weisum08 = decimal.Parse(weightTextBox08.Text);
        //    subSum08 = money8;     //増やしていく
        //    #region "金額合計の場合わけ"
        //    if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //    {
        //        subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //    }
        //    else
        //    {
        //        subSum = subSum08;
        //    }
        //    #endregion
        //    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //    sum = subSum + TaxAmount;
        //    #region "数量の場合わけ"
        //    if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //    {
        //        countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //    }
        //    else
        //    {
        //        countsum = countsum08;
        //    }
        //    #endregion
        //    #region "重量の場合わけ"
        //    if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //    {
        //        weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //    }
        //    else
        //    {
        //        weisum = weisum08;
        //    }
        //    #endregion

        //    totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //    totalCount2.Text = string.Format("{0:#,0}", countsum);
        //    subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //    taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //    sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //}
        //#endregion
        //#region "10行目"
        //private void moneyTextBox09_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox09.Text) && string.IsNullOrEmpty(countTextBox09.Text))
        //        {
        //            countTextBox09.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox09.Text) && string.IsNullOrEmpty(weightTextBox09.Text))
        //        {
        //            weightTextBox09.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum09 = int.Parse(countTextBox09.Text);
        //        weisum09 = decimal.Parse(weightTextBox09.Text);
        //        subSum09 = money9;
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum09;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum09;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum09;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox09.Text) && string.IsNullOrEmpty(countTextBox09.Text))
        //        {
        //            countTextBox09.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox09.Text) && string.IsNullOrEmpty(weightTextBox09.Text))
        //        {
        //            weightTextBox09.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        countsum09 = int.Parse(countTextBox09.Text);
        //        weisum09 = decimal.Parse(weightTextBox09.Text);
        //        subSum09 = money9;
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum010 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum09;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum010 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum09;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum010 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum09;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "11行目"
        //private void moneyTextBox010_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox010.Text) && string.IsNullOrEmpty(countTextBox010.Text))
        //        {
        //            countTextBox010.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox010.Text) && string.IsNullOrEmpty(weightTextBox010.Text))
        //        {
        //            weightTextBox010.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum010 = int.Parse(countTextBox010.Text);
        //        weisum010 = decimal.Parse(weightTextBox010.Text);
        //        subSum010 = money10;
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum010;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum010;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum010;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox010.Text) && string.IsNullOrEmpty(countTextBox010.Text))
        //        {
        //            countTextBox010.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox010.Text) && string.IsNullOrEmpty(weightTextBox010.Text))
        //        {
        //            weightTextBox010.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        countsum010 = int.Parse(countTextBox010.Text);
        //        weisum010 = decimal.Parse(weightTextBox010.Text);
        //        subSum010 = money10;
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum011 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum010;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum011 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum010;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum011 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum010;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "12行目"
        //private void moneyTextBox011_TextChanged(object sender, EventArgs e)
        //{
        //    #region "買取販売"
        //    if (data == "D")
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox011.Text) && string.IsNullOrEmpty(countTextBox011.Text))
        //        {
        //            countTextBox011.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox011.Text) && string.IsNullOrEmpty(weightTextBox011.Text))
        //        {
        //            weightTextBox011.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum011 = int.Parse(countTextBox011.Text);
        //        weisum011 = decimal.Parse(weightTextBox011.Text);
        //        subSum011 = money11;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum011;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum011;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum011;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox011.Text) && string.IsNullOrEmpty(countTextBox011.Text))
        //        {
        //            countTextBox011.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox011.Text) && string.IsNullOrEmpty(weightTextBox011.Text))
        //        {
        //            weightTextBox011.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        countsum011 = int.Parse(countTextBox011.Text);
        //        weisum011 = decimal.Parse(weightTextBox011.Text);
        //        subSum011 = money11;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum012 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum011;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum012 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum011;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum012 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum011;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#region "13行目"
        //private void moneyTextBox012_TextChanged(object sender, EventArgs e)
        //{
        //    if (data == "D")
        //    {
        //        #region "買取販売"
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox012.Text) && string.IsNullOrEmpty(countTextBox012.Text))
        //        {
        //            countTextBox012.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox012.Text) && string.IsNullOrEmpty(weightTextBox012.Text))
        //        {
        //            weightTextBox012.Text = 0.ToString();
        //        }
        //        else
        //        {

        //        }
        //        countsum012 = int.Parse(countTextBox012.Text);
        //        weisum012 = decimal.Parse(weightTextBox012.Text);
        //        subSum012 = money12;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum012;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 110, MidpointRounding.AwayFromZero);
        //        //sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum012;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum012;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
        //    }
        //    #endregion
        //    else
        //    {
        //        //重量×単価
        //        if (!string.IsNullOrEmpty(weightTextBox012.Text) && string.IsNullOrEmpty(countTextBox012.Text))
        //        {
        //            countTextBox012.Text = 0.ToString();
        //        }
        //        //数量×単価
        //        else if (!string.IsNullOrEmpty(countTextBox012.Text) && string.IsNullOrEmpty(weightTextBox012.Text))
        //        {
        //            weightTextBox012.Text = 0.ToString();
        //        }
        //        else
        //        {
        //            MessageBox.Show("数値を入力してください");
        //            return;
        //        }
        //        countsum012 = int.Parse(countTextBox012.Text);
        //        weisum012 = decimal.Parse(weightTextBox012.Text);
        //        subSum012 = money12;     //増やしていく
        //        #region "金額合計の場合わけ"
        //        if (subSum00 != 0 || subSum01 != 0 || subSum02 != 0 || subSum03 != 0 || subSum04 != 0 || subSum05 != 0 || subSum06 != 0 || subSum07 != 0 || subSum08 != 0 || subSum09 != 0 || subSum010 != 0 || subSum011 != 0)
        //        {
        //            subSum = subSum00 + subSum01 + subSum02 + subSum03 + subSum04 + subSum05 + subSum06 + subSum07 + subSum08 + subSum09 + subSum010 + subSum011 + subSum012;
        //        }
        //        else
        //        {
        //            subSum = subSum012;
        //        }
        //        #endregion
        //        TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
        //        sum = subSum + TaxAmount;
        //        #region "数量の場合わけ"
        //        if (countsum00 != 0 || countsum01 != 0 || countsum02 != 0 || countsum03 != 0 || countsum04 != 0 || countsum05 != 0 || countsum06 != 0 || countsum07 != 0 || countsum08 != 0 || countsum09 != 0 || countsum010 != 0 || countsum011 != 0)
        //        {
        //            countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
        //        }
        //        else
        //        {
        //            countsum = countsum012;
        //        }
        //        #endregion
        //        #region "重量の場合わけ"
        //        if (weisum00 != 0 || weisum01 != 0 || weisum02 != 0 || weisum03 != 0 || weisum04 != 0 || weisum05 != 0 || weisum06 != 0 || weisum07 != 0 || weisum08 != 0 || weisum09 != 0 || weisum010 != 0 || weisum011 != 0)
        //        {
        //            weisum = weisum00 + weisum01 + weisum02 + weisum03 + weisum04 + weisum05 + weisum06 + weisum07 + weisum08 + weisum09 + weisum010 + weisum011 + weisum012;
        //        }
        //        else
        //        {
        //            weisum = weisum012;
        //        }
        //        #endregion

        //        totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
        //        totalCount2.Text = string.Format("{0:#,0}", countsum);
        //        subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
        //        taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
        //        sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        //    }
        //}
        //#endregion
        //#endregion

        #endregion

        #region"右上の×で戻る"
        private void Statement_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (data == "S" || data == "D")
            {
                //this.Close();
            }
            else if (screan)
            {
                MainMenu mainMenu = new MainMenu(topMenu, staff_id, pass, access_auth);
                mainMenu.Show();
            }
        }
        #endregion

        #region"納品書　登録ボタン"
        private void Register_Click(object sender, EventArgs e)
        {
            #region"確認事項"
            if (string.IsNullOrEmpty(name.Text))
            {
                MessageBox.Show("宛名を入力して下さい。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (titleComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("敬称を選んで下さい。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (typeComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("種別を選んで下さい。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (paymentMethodComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("お支払方法を選んで下さい。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (paymentMethodComboBox.Text == "振込")
            {
                if (string.IsNullOrEmpty(PayeeTextBox1.Text))
                {
                    MessageBox.Show("振込先を入力して下さい。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(typeTextBox2.Text))
            {
                MessageBox.Show("顧客選択をしてください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dialog = MessageBox.Show("納品書の登録をしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.No)
            {
                return;
            }

            #endregion

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            conn.Open();

            #region"納品書枠外"
            //管理番号は Number、区分（法人 or 個人は type）、顧客番号は ClientCode、担当者コードは staff_id、小計・合計は sum、消費税額は TaxAmount、納品日は DeliveryDate、
            //決済日は SettlementDate、支払方法は SettlementMethod、総数は TotalCount、総重量は TotalWeight、消費税率は Tax

            sum = SUB;                                      //小計・合計
            string vatType = comboBox11.Text;               //消費税区分
            if (!string.IsNullOrEmpty(taxAmount2.Text))
            {
                TaxAmount = decimal.Parse(taxAmount2.Text);     //消費税額
            }

            string Name = name.Text;
            string honorificTitle = titleComboBox.Text;
            string deliveryOrbill = typeComboBox.Text;
            string orderDate = orderDateTimePicker.Text;
            DeliveryDate = DeliveryDateTimePicker.Text;
            SettlementDate = SettlementDateTimePicker.Text;
            DateTime settlementDate = DateTime.Parse(SettlementDate);
            string SeaalPrint = "";
            if (sealY.Checked)
            {
                SeaalPrint = "する";
            }
            if (sealN.Checked)
            {
                SeaalPrint = "しない";
            }
            SettlementMethod = paymentMethodComboBox.Text;
            string accountPayable = PayeeTextBox1.Text;
            string Coin = CoinComboBox.Text;

            if (!string.IsNullOrEmpty(totalCountTextBox1.Text))
            {
                TotalCount = decimal.Parse(totalCountTextBox1.Text);
            }

            if (!string.IsNullOrEmpty(totalWeightTextBox1.Text))
            {
                TotalWeight = decimal.Parse(totalWeightTextBox1.Text);
            }

            deliveryRemarks = RemarkRegister.Text;

            if (!string.IsNullOrEmpty(antiqueLicenceTextBox2.Text))
            {
                AntiqueLicence = antiqueLicenceTextBox2.Text;

                using (transaction = conn.BeginTransaction())
                {
                    if (type == 0)
                    {
                        string SQL_STR = @"update client_m set antique_license ='" + AntiqueLicence + "' where code = '" + ClientCode + "';";
                        cmd = new NpgsqlCommand(SQL_STR, conn);
                        cmd.ExecuteReader();
                    }
                    else if (type == 1)
                    {
                        string SQL_STR = @"update client_m set antique_license ='" + AntiqueLicence + "'  where code = '" + ClientCode + "';";
                        cmd = new NpgsqlCommand(SQL_STR, conn);
                        cmd.ExecuteReader();
                    }
                }
            }

            if (remarks1 != clientRemarksTextBox2.Text)
            {
                using (transaction = conn.BeginTransaction())
                {
                    string SQL_STR = @"update client_m set remarks = '" + remarks1 + "'where code = '" + ClientCode + "';";
                    cmd = new NpgsqlCommand(SQL_STR, conn);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }

            using (transaction = conn.BeginTransaction())
            {
                Sql = "update delivery_m set (control_number, code, sub_total, vat, vat_rate, vat_amount, total, honorific_title, type, order_date," +
                    " delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, total_count, total_weight, types1, remark, name)" +
                    " = ('" + Number + "','" + ClientCode + "','" + sum + "','" + vatType + "','" + Tax + "','" + TaxAmount + "','" + sum + "','" + honorificTitle + "','" + deliveryOrbill + "','" + orderDate + "','" + DeliveryDate + "'," +
                    "'" + settlementDate + "','" + SeaalPrint + "','" + SettlementMethod + "','" + accountPayable + "','" + Coin + "','" + TotalCount + "','" + TotalWeight + "','" + type + "', '" + deliveryRemarks + "', '" + Name + "')" +
                    "where control_number = '" + Number + "';";
                cmd = new NpgsqlCommand(Sql, conn);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            #endregion

            #region"納品書表"
            int index = dataGridView2.Rows.Count;
            #region"納品書　表のデータ　初回登録"
            if (index == 2)
            {
                index--;
            }

            if (dataGridView2.Rows[index - 1].IsNewRow)
            {
                index--;
            }

            for (int i = 0; i < index; i++)
            {
                #region"値の初期化"
                ItemDetail = "";
                Weight = 0;
                UnitPrice = 0;
                COUNT = 0;
                Money = 0;
                Remark = "";
                #endregion

                mainCategoryCode = int.Parse(dataGridView2[1, i].Value.ToString());

                itemCode = int.Parse(dataGridView2[2, i].Value.ToString());

                if (dataGridView2.Rows[i].Cells[3].Value != null && dataGridView2.Rows[i].Cells[3].Value.ToString() != "")
                {
                    ItemDetail = dataGridView2[3, i].Value.ToString();
                }

                if (dataGridView2.Rows[i].Cells[4].Value != null && dataGridView2.Rows[i].Cells[4].Value.ToString() != "")
                {
                    Weight = decimal.Parse(dataGridView2[4, i].Value.ToString());
                }

                if (dataGridView2.Rows[i].Cells[5].Value != null && dataGridView2.Rows[i].Cells[5].Value.ToString() != "")
                {
                    UnitPrice = decimal.Parse(dataGridView2[5, i].Value.ToString());
                }

                if (dataGridView2.Rows[i].Cells[6].Value != null && dataGridView2.Rows[i].Cells[6].Value.ToString() != "")
                {
                    COUNT = int.Parse(dataGridView2[6, i].Value.ToString());
                }

                if (dataGridView2.Rows[i].Cells[7].Value != null && dataGridView2.Rows[i].Cells[7].Value.ToString() != "")
                {
                    Money = decimal.Parse(dataGridView2[7, i].Value.ToString().Substring(1));
                }

                if (dataGridView2.Rows[i].Cells[8].Value != null && dataGridView2.Rows[i].Cells[8].Value.ToString() != "")
                {
                    Remark = dataGridView2[8, i].Value.ToString();
                }

                record = i + 1;

                Sql = "insert into delivery_calc (main_category_code, item_code, weight, count, unit_price, amount, remarks, control_number, detail, record_number)" +
                    " values ('" + MainCategoryCode + "','" + itemCode + "','" + Weight + "','" + COUNT + "','" + UnitPrice + "','" + Money + "','" + Remark + "'," +
                    "'" + documentNumberTextBox2.Text + "', '" + ItemDetail + "','" + record + "');";
                cmd = new NpgsqlCommand(Sql, conn);
                cmd.ExecuteNonQuery();
            }
            #endregion
            #endregion

            conn.Close();
            MessageBox.Show("納品書の登録をしました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Register.Enabled = false;
            #region"一時コメントアウト"
            #region "ありうるミス"
            //if (string.IsNullOrEmpty(name.Text))
            //{
            //    MessageBox.Show("宛名を入力して下さい。");
            //    return;
            //}

            //if (titleComboBox.SelectedIndex == -1)
            //{
            //    MessageBox.Show("敬称を選んで下さい。");
            //    return;
            //}
            //if (typeComboBox.SelectedIndex == -1)
            //{
            //    MessageBox.Show("種別を選んで下さい。");
            //    return;
            //}
            //if (paymentMethodComboBox.SelectedIndex == -1)
            //{
            //    MessageBox.Show("お支払方法を選んで下さい。");
            //    return;
            //}
            //if (paymentMethodComboBox.Text == "振込")
            //{
            //    if (string.IsNullOrEmpty(PayeeTextBox1.Text))
            //    {
            //        MessageBox.Show("振込先を入力して下さい。");
            //        return;
            //    }
            //}
            #endregion
            //DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            //if (dr == DialogResult.No)
            //{
            //    return;
            //}
            //if (regist >= 1 && access_auth != "C" || data == "D" && access_auth != "C")
            //{
            //    int ControlNumber = number;

            //    decimal TotalWeight = weisum;
            //    int Amount = countsum;
            //    decimal SubTotal = subSum + TaxAmount;
            //    decimal Total = sum;
            //    //vat（税込み税抜きどちらか）, vat_rate（税率）
            //    string vat = this.comboBox11.SelectedItem.ToString();
            //    int vat_rate = (int)Tax;

            //    string seaal = "";
            //    //印鑑印刷
            //    if (sealY.Checked == true)
            //    {
            //        seaal = "する";
            //    }
            //    if (sealN.Checked == true)
            //    {
            //        seaal = "しない";
            //    }
            //    #region "再登録用に繋げる"
            //    NpgsqlConnection connDelivery = new NpgsqlConnection();
            //    PostgreSQL postgre = new PostgreSQL();
            //    connDelivery = postgre.connection();
            //    NpgsqlDataAdapter adapterDelivery;
            //    DataTable DeliveryDt = new DataTable();
            //    string str_sql_re = "select * from delivery_calc where control_number = " + number + ";";
            //    adapterDelivery = new NpgsqlDataAdapter(str_sql_re, connDelivery);
            //    adapterDelivery.Fill(DeliveryDt);
            //    int Re = DeliveryDt.Rows.Count;
            //    #endregion
            //    string OrderDate = orderDateTimePicker.Text;
            //    string DeliveryDate = DeliveryDateTimePicker.Text;
            //    string SettlementDate = SettlementDateTimePicker.Text;
            //    string PaymentMethod = paymentMethodComboBox.SelectedItem.ToString();
            //    string Name = name.Text;                                           //宛名
            //    string Title = titleComboBox.SelectedItem.ToString();              //敬称
            //    string Type = typeComboBox.SelectedItem.ToString();                //納品書or請求書
            //    string payee = PayeeTextBox1.Text;                                 //振り込み先
            //    string coin = CoinComboBox.SelectedItem.ToString();                //通貨
            //    string remark = RemarkRegister.Text;
            //    string Reason = this.textBox2.Text;
            //    DateTime dat1 = DateTime.Now;
            //    DateTime dtToday = dat1.Date;
            //    string c = dtToday.ToString("yyyy年MM月dd日");

            //    #region "法人"
            //    if (type == 0)
            //    {
            //        NpgsqlConnection connA = new NpgsqlConnection();
            //        connA = postgre.connection();

            //        //connA.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            //        NpgsqlDataAdapter adapterA;
            //        DataTable clientDt = new DataTable();
            //        string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and name = '" + client_staff_name + "' and address = '" + address + "';";
            //        adapterA = new NpgsqlDataAdapter(str_sql_corporate, connA);
            //        adapterA.Fill(clientDt);

            //        DataRow row2;
            //        row2 = clientDt.Rows[0];
            //        int antique = (int)row2["antique_number"];

            //        NpgsqlConnection conn0 = new NpgsqlConnection();
            //        NpgsqlDataAdapter adapter0;
            //        conn0 = postgre.connection();
            //        //conn0.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        DataTable dt = new DataTable();
            //        string sql_str = "UPDATE delivery_m SET staff_code = " + staff_id + ", sub_total = '" + SubTotal + "', vat = '" + vat + "', vat_rate = '" + vat_rate + "', vat_amount = '" + TaxAmount + "', total = '" + Total + "', name = '" + Name + "', honorific_title = '" + Title + "', type = '" + Type + "', order_date = '" + OrderDate + "', delivery_date = '" + DeliveryDate + "', settlement_date = '" + SettlementDate + "', seaal_print = '" + seaal + "', payment_method = '" + PaymentMethod + "', account_payble = '" + payee + "', currency = '" + coin + "', remarks2 = '" + remark + "', total_count = '" + Amount + "', total_weight = '" + TotalWeight + "', types1 = 0, reason = '" + Reason + "', registration_date = '" + c + "' where control_number = " + ControlNumber + " and antique_number = " + antique + ";";
            //        adapter0 = new NpgsqlDataAdapter(sql_str, conn0);
            //        adapter0.Fill(dt);

            //    }
            //    #endregion
            //    #region "個人"
            //    if (type == 1)
            //    {
            //        NpgsqlConnection connI = new NpgsqlConnection();
            //        connI = postgre.connection();
            //        //connI.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        NpgsqlDataAdapter adapterI;
            //        DataTable clientDt = new DataTable();
            //        string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
            //        adapterI = new NpgsqlDataAdapter(str_sql_individual, connI);
            //        adapterI.Fill(clientDt);

            //        DataRow row2;
            //        row2 = clientDt.Rows[0];
            //        int ID = (int)row2["id_number"];

            //        NpgsqlConnection conn1 = new NpgsqlConnection();
            //        NpgsqlDataAdapter adapter1;
            //        conn1 = postgre.connection();
            //        //conn1.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        DataTable dt = new DataTable();
            //        string sql_str = "UPDATE delivery_m SET staff_code = " + staff_id + ", sub_total = '" + SubTotal + "', vat = '" + vat + "', vat_rate = '" + vat_rate + "', vat_amount = '" + TaxAmount + "', total = '" + Total + "', name  = '" + Name + "', honorific_title = '" + Title + "', type = '" + Type + "', order_date '" + OrderDate + "', delivery_date = '" + DeliveryDate + "', settlement_date = '" + SettlementDate + "', seaal_print = '" + seaal + "', payment_method = '" + PaymentMethod + "', account_payble  = '" + payee + "', currency = '" + coin + "', remarks2 = '" + remark + "', total_count = '" + Amount + "', total_weight = '" + TotalWeight + "', types1 = 1, reason = '" + Reason + "', registration_date = '" + c + "' where control_number = " + ControlNumber + " and id_number = " + ID + ";";
            //        adapter1 = new NpgsqlDataAdapter(sql_str, conn1);
            //        adapter1.Fill(dt);
            //    }
            //    #endregion

            //    /*NpgsqlConnection conn = new NpgsqlConnection();
            //    NpgsqlDataAdapter adapter;

            //    DataTable dt = new DataTable();
            //    string sql_str = "Insert into delivery_m (control_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, remarks2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "','" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + type + ");";
            //    adapter = new NpgsqlDataAdapter(sql_str, conn);
            //    adapter.Fill(dt);*/
            //    #region "1行目"
            //    //管理番号：ControlNumber
            //    int record = 1;     //行数
            //                        //int mainCategory = mainCategoryCode00;
            //                        //int item = itemCode00;
            //    #region "大分類コード"
            //    DataTable maindt = new DataTable();
            //    conn = postgre.connection();
            //    //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //    string sql_main = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox00.Text + "';";
            //    adapter = new NpgsqlDataAdapter(sql_main, conn);
            //    adapter.Fill(maindt);
            //    DataRow dataRow;
            //    dataRow = maindt.Rows[0];
            //    int mainCategory = (int)dataRow["main_category_code"];
            //    #endregion
            //    #region "品名コード"
            //    DataTable itemdt = new DataTable();
            //    conn = postgre.connection();
            //    //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //    string sql_item = "select * from item_m where item_name = '" + itemComboBox00.Text + "';";
            //    adapter = new NpgsqlDataAdapter(sql_item, conn);
            //    adapter.Fill(itemdt);
            //    DataRow dataRow1;
            //    dataRow1 = itemdt.Rows[0];
            //    int item = (int)dataRow1["item_code"];
            //    #endregion
            //    string Detail = itemDetail00.Text;
            //    decimal Weight = decimal.Parse(weightTextBox00.Text);
            //    int Count = int.Parse(countTextBox00.Text);
            //    decimal UnitPrice = decimal.Parse(unitPriceTextBox00.Text);
            //    decimal amount = money0 + money0 * Tax / 100;
            //    string Remarks = remarks00.Text;

            //    DataTable dt2 = new DataTable();
            //    string sql_str2 = "UPDATE delivery_calc SET item_code = " + item + " ,weight = " + Weight + " , count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = '" + mainCategory + "',detail = '" + Detail + "' , reason = ' " + Reason + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //    conn = postgre.connection();
            //    //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //    conn.Open();
            //    adapter = new NpgsqlDataAdapter(sql_str2, conn);
            //    adapter.Fill(dt2);
            //    #endregion
            //    #region "2行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox01.Text) && !(unitPriceTextBox01.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 2)
            //        {
            //            record = 2;
            //            //mainCategory = mainCategoryCode01;
            //            //item = itemCode01;
            //            #region "大分類コード"
            //            DataTable main1dt = new DataTable();
            //            string sql_main1 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox01.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main1, conn);
            //            adapter.Fill(main1dt);
            //            DataRow dataRow2;
            //            dataRow2 = main1dt.Rows[0];
            //            int mainCategory1 = (int)dataRow2["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item1dt = new DataTable();
            //            string sql_item1 = "select * from item_m where item_name = '" + itemComboBox01.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item1, conn);
            //            adapter.Fill(item1dt);
            //            DataRow dataRow3;
            //            dataRow3 = item1dt.Rows[0];
            //            int item1 = (int)dataRow3["item_code"];
            //            #endregion
            //            Detail = itemDetail01.Text;
            //            Weight = decimal.Parse(weightTextBox01.Text);
            //            Count = int.Parse(countTextBox01.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox01.Text);
            //            amount = money1 + money1 * Tax / 100;  //decimal.Parse(moneyTextBox01.Text);
            //            Remarks = remarks01.Text;

            //            DataTable dt4 = new DataTable();
            //            string sql_str4 = "UPDATE delivery_calc SET item_code = " + item1 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory1 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str4, conn);
            //            adapter.Fill(dt4);
            //        }
            //        else
            //        {
            //            record = 2;
            //            //mainCategory = mainCategoryCode01;
            //            //item = itemCode01;
            //            #region "大分類コード"
            //            DataTable main1dt = new DataTable();
            //            string sql_main1 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox01.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main1, conn);
            //            adapter.Fill(main1dt);
            //            DataRow dataRow2;
            //            dataRow2 = main1dt.Rows[0];
            //            int mainCategory1 = (int)dataRow2["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item1dt = new DataTable();
            //            string sql_item1 = "select * from item_m where item_name = '" + itemComboBox01.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item1, conn);
            //            adapter.Fill(item1dt);
            //            DataRow dataRow3;
            //            dataRow3 = item1dt.Rows[0];
            //            int item1 = (int)dataRow3["item_code"];
            //            #endregion
            //            Detail = itemDetail01.Text;
            //            Weight = decimal.Parse(weightTextBox01.Text);
            //            Count = int.Parse(countTextBox01.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox01.Text);
            //            amount = money1 + money1 * Tax / 100;  //decimal.Parse(moneyTextBox01.Text);
            //            Remarks = remarks01.Text;

            //            DataTable dt4 = new DataTable();
            //            string sql_str4 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item1 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory1 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str4, conn);
            //            adapter.Fill(dt4);
            //        }

            //    }
            //    #endregion
            //    #region "3行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox02.Text) && !(unitPriceTextBox02.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 3)
            //        {
            //            record = 3;
            //            //mainCategory = mainCategoryCode02;
            //            //item = itemCode02;
            //            #region "大分類コード"
            //            DataTable main2dt = new DataTable();
            //            string sql_main2 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox02.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main2, conn);
            //            adapter.Fill(main2dt);
            //            DataRow dataRow4;
            //            dataRow4 = main2dt.Rows[0];
            //            int mainCategory2 = (int)dataRow4["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item1dt = new DataTable();
            //            string sql_item2 = "select * from item_m where item_name = '" + itemComboBox02.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item2, conn);
            //            adapter.Fill(item1dt);
            //            DataRow dataRow5;
            //            dataRow5 = item1dt.Rows[0];
            //            int item2 = (int)dataRow5["item_code"];
            //            #endregion
            //            Detail = itemDetail02.Text;
            //            Weight = decimal.Parse(weightTextBox02.Text);
            //            Count = int.Parse(countTextBox02.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox02.Text);
            //            amount = money2 + money2 * Tax / 100;  //decimal.Parse(moneyTextBox02.Text);
            //            Remarks = remarks02.Text;

            //            DataTable dt5 = new DataTable();
            //            string sql_str5 = "UPDATE delivery_calc SET item_code = " + item2 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory2 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str5, conn);
            //            adapter.Fill(dt5);
            //        }
            //        else
            //        {
            //            record = 3;
            //            //mainCategory = mainCategoryCode02;
            //            //item = itemCode02;
            //            #region "大分類コード"
            //            DataTable main2dt = new DataTable();
            //            string sql_main2 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox02.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main2, conn);
            //            adapter.Fill(main2dt);
            //            DataRow dataRow4;
            //            dataRow4 = main2dt.Rows[0];
            //            int mainCategory2 = (int)dataRow4["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item1dt = new DataTable();
            //            string sql_item2 = "select * from item_m where item_name = '" + itemComboBox02.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item2, conn);
            //            adapter.Fill(item1dt);
            //            DataRow dataRow5;
            //            dataRow5 = item1dt.Rows[0];
            //            int item2 = (int)dataRow5["item_code"];
            //            #endregion
            //            Detail = itemDetail02.Text;
            //            Weight = decimal.Parse(weightTextBox02.Text);
            //            Count = int.Parse(countTextBox02.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox02.Text);
            //            amount = money2 + money2 * Tax / 100;  //decimal.Parse(moneyTextBox02.Text);
            //            Remarks = remarks02.Text;

            //            DataTable dt5 = new DataTable();
            //            string sql_str5 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item2 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory2 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str5, conn);
            //            adapter.Fill(dt5);
            //        }

            //    }
            //    #endregion
            //    #region "4行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox03.Text) && !(unitPriceTextBox03.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 4)
            //        {
            //            record = 4;
            //            //mainCategory = mainCategoryCode03;
            //            //item = itemCode03;
            //            #region "大分類コード"
            //            DataTable main3dt = new DataTable();
            //            string sql_main3 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox03.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main3, conn);
            //            adapter.Fill(main3dt);
            //            DataRow dataRow6;
            //            dataRow6 = main3dt.Rows[0];
            //            int mainCategory3 = (int)dataRow6["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item3dt = new DataTable();
            //            string sql_item3 = "select * from item_m where item_name = '" + itemComboBox03.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item3, conn);
            //            adapter.Fill(item3dt);
            //            DataRow dataRow7;
            //            dataRow7 = item3dt.Rows[0];
            //            int item3 = (int)dataRow7["item_code"];
            //            #endregion
            //            Detail = itemDetail03.Text;
            //            Weight = decimal.Parse(weightTextBox03.Text);
            //            Count = int.Parse(countTextBox03.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox03.Text);
            //            amount = money3 + money3 * Tax / 100;  //decimal.Parse(moneyTextBox03.Text);
            //            Remarks = remarks03.Text;

            //            DataTable dt6 = new DataTable();
            //            string sql_str6 = "UPDATE delivery_calc SET item_code = " + item3 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory3 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str6, conn);
            //            adapter.Fill(dt6);
            //        }
            //        else
            //        {
            //            record = 4;
            //            //mainCategory = mainCategoryCode03;
            //            //item = itemCode03;
            //            #region "大分類コード"
            //            DataTable main3dt = new DataTable();
            //            string sql_main3 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox03.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main3, conn);
            //            adapter.Fill(main3dt);
            //            DataRow dataRow6;
            //            dataRow6 = main3dt.Rows[0];
            //            int mainCategory3 = (int)dataRow6["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item3dt = new DataTable();
            //            string sql_item3 = "select * from item_m where item_name = '" + itemComboBox03.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item3, conn);
            //            adapter.Fill(item3dt);
            //            DataRow dataRow7;
            //            dataRow7 = item3dt.Rows[0];
            //            int item3 = (int)dataRow7["item_code"];
            //            #endregion
            //            Detail = itemDetail03.Text;
            //            Weight = decimal.Parse(weightTextBox03.Text);
            //            Count = int.Parse(countTextBox03.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox03.Text);
            //            amount = money3 + money3 * Tax / 100;  //decimal.Parse(moneyTextBox03.Text);
            //            Remarks = remarks03.Text;

            //            DataTable dt6 = new DataTable();
            //            string sql_str6 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item3 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory3 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str6, conn);
            //            adapter.Fill(dt6);
            //        }

            //    }
            //    #endregion
            //    #region "5行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox04.Text) && !(unitPriceTextBox04.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 5)
            //        {
            //            record = 5;
            //            //mainCategory = mainCategoryCode04;
            //            //item = itemCode04;
            //            #region "大分類コード"
            //            DataTable main4dt = new DataTable();
            //            string sql_main4 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox04.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main4, conn);
            //            adapter.Fill(main4dt);
            //            DataRow dataRow8;
            //            dataRow8 = main4dt.Rows[0];
            //            int mainCategory4 = (int)dataRow8["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item4dt = new DataTable();
            //            string sql_item4 = "select * from item_m where item_name = '" + itemComboBox04.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item4, conn);
            //            adapter.Fill(item4dt);
            //            DataRow dataRow9;
            //            dataRow9 = item4dt.Rows[0];
            //            int item4 = (int)dataRow9["item_code"];
            //            #endregion
            //            Detail = itemDetail04.Text;
            //            Weight = decimal.Parse(weightTextBox04.Text);
            //            Count = int.Parse(countTextBox04.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox04.Text);
            //            amount = money4 + money4 * Tax / 100; //decimal.Parse(moneyTextBox04.Text);
            //            Remarks = remarks04.Text;

            //            DataTable dt7 = new DataTable();
            //            string sql_str7 = "UPDATE delivery_calc SET item_code = " + item4 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory4 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";
            //            adapter = new NpgsqlDataAdapter(sql_str7, conn);
            //            adapter.Fill(dt7);
            //        }
            //        else
            //        {
            //            record = 5;
            //            //mainCategory = mainCategoryCode04;
            //            //item = itemCode04;
            //            #region "大分類コード"
            //            DataTable main4dt = new DataTable();
            //            string sql_main4 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox04.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main4, conn);
            //            adapter.Fill(main4dt);
            //            DataRow dataRow8;
            //            dataRow8 = main4dt.Rows[0];
            //            int mainCategory4 = (int)dataRow8["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item4dt = new DataTable();
            //            string sql_item4 = "select * from item_m where item_name = '" + itemComboBox04.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item4, conn);
            //            adapter.Fill(item4dt);
            //            DataRow dataRow9;
            //            dataRow9 = item4dt.Rows[0];
            //            int item4 = (int)dataRow9["item_code"];
            //            #endregion
            //            Detail = itemDetail04.Text;
            //            Weight = decimal.Parse(weightTextBox04.Text);
            //            Count = int.Parse(countTextBox04.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox04.Text);
            //            amount = money4 + money4 * Tax / 100; //decimal.Parse(moneyTextBox04.Text);
            //            Remarks = remarks04.Text;

            //            DataTable dt7 = new DataTable();
            //            string sql_str7 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item4 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory4 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str7, conn);
            //            adapter.Fill(dt7);
            //        }

            //    }
            //    #endregion
            //    #region "6行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox05.Text) && !(unitPriceTextBox05.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 6)
            //        {
            //            record = 6;
            //            //mainCategory = mainCategoryCode05;
            //            //item = itemCode05;
            //            #region "大分類コード"
            //            DataTable main5dt = new DataTable();
            //            conn = postgre.connection();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main5 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox05.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main5, conn);
            //            adapter.Fill(main5dt);
            //            DataRow dataRow10;
            //            dataRow10 = main5dt.Rows[0];
            //            int mainCategory5 = (int)dataRow10["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item5dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item5 = "select * from item_m where item_name = '" + itemComboBox05.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item5, conn);
            //            adapter.Fill(item5dt);
            //            DataRow dataRow11;
            //            dataRow11 = item5dt.Rows[0];
            //            int item5 = (int)dataRow11["item_code"];
            //            #endregion
            //            Detail = itemDetail05.Text;
            //            Weight = decimal.Parse(weightTextBox05.Text);
            //            Count = int.Parse(countTextBox05.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox05.Text);
            //            amount = money5 + money5 * Tax / 100; //decimal.Parse(moneyTextBox05.Text);
            //            Remarks = remarks05.Text;

            //            DataTable dt8 = new DataTable();
            //            string sql_str8 = "UPDATE delivery_calc SET item_code = " + item5 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory5 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str8, conn);
            //            adapter.Fill(dt8);
            //        }
            //        else
            //        {
            //            record = 6;
            //            //mainCategory = mainCategoryCode05;
            //            //item = itemCode05;
            //            #region "大分類コード"
            //            DataTable main5dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main5 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox05.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main5, conn);
            //            adapter.Fill(main5dt);
            //            DataRow dataRow10;
            //            dataRow10 = main5dt.Rows[0];
            //            int mainCategory5 = (int)dataRow10["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item5dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item5 = "select * from item_m where item_name = '" + itemComboBox05.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item5, conn);
            //            adapter.Fill(item5dt);
            //            DataRow dataRow11;
            //            dataRow11 = item5dt.Rows[0];
            //            int item5 = (int)dataRow11["item_code"];
            //            #endregion
            //            Detail = itemDetail05.Text;
            //            Weight = decimal.Parse(weightTextBox05.Text);
            //            Count = int.Parse(countTextBox05.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox05.Text);
            //            amount = money5 + money5 * Tax / 100; //decimal.Parse(moneyTextBox05.Text);
            //            Remarks = remarks05.Text;

            //            DataTable dt8 = new DataTable();
            //            string sql_str8 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES  ( '" + ControlNumber + "' ,'" + record + "' , " + item5 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory5 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str8, conn);
            //            adapter.Fill(dt8);
            //        }

            //    }
            //    #endregion
            //    #region "7行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox06.Text) && !(unitPriceTextBox06.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 7)
            //        {
            //            record = 7;
            //            //mainCategory = mainCategoryCode06;
            //            //item = itemCode06;
            //            #region "大分類コード"
            //            DataTable main6dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main6 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox06.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main6, conn);
            //            adapter.Fill(main6dt);
            //            DataRow dataRow12;
            //            dataRow12 = main6dt.Rows[0];
            //            int mainCategory6 = (int)dataRow12["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item6dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item6 = "select * from item_m where item_name = '" + itemComboBox06.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item6, conn);
            //            adapter.Fill(item6dt);
            //            DataRow dataRow13;
            //            dataRow13 = item6dt.Rows[0];
            //            int item6 = (int)dataRow13["item_code"];
            //            #endregion
            //            Detail = itemDetail06.Text;
            //            Weight = decimal.Parse(weightTextBox06.Text);
            //            Count = int.Parse(countTextBox06.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox06.Text);
            //            amount = money6 + money6 * Tax / 100; //decimal.Parse(moneyTextBox06.Text);
            //            Remarks = remarks06.Text;

            //            DataTable dt9 = new DataTable();
            //            string sql_str9 = "UPDATE delivery_calc SET item_code = " + item6 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory6 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str9, conn);
            //            adapter.Fill(dt9);
            //        }
            //        else
            //        {
            //            record = 7;
            //            //mainCategory = mainCategoryCode06;
            //            //item = itemCode06;
            //            #region "大分類コード"
            //            DataTable main6dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main6 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox06.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main6, conn);
            //            adapter.Fill(main6dt);
            //            DataRow dataRow12;
            //            dataRow12 = main6dt.Rows[0];
            //            int mainCategory6 = (int)dataRow12["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item6dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item6 = "select * from item_m where item_name = '" + itemComboBox06.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item6, conn);
            //            adapter.Fill(item6dt);
            //            DataRow dataRow13;
            //            dataRow13 = item6dt.Rows[0];
            //            int item6 = (int)dataRow13["item_code"];
            //            #endregion
            //            Detail = itemDetail06.Text;
            //            Weight = decimal.Parse(weightTextBox06.Text);
            //            Count = int.Parse(countTextBox06.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox06.Text);
            //            amount = money6 + money6 * Tax / 100; //decimal.Parse(moneyTextBox06.Text);
            //            Remarks = remarks06.Text;

            //            DataTable dt9 = new DataTable();
            //            string sql_str9 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item6 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory6 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str9, conn);
            //            adapter.Fill(dt9);
            //        }

            //    }
            //    #endregion
            //    #region "8行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox07.Text) && !(unitPriceTextBox07.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 8)
            //        {
            //            record = 8;
            //            //mainCategory = mainCategoryCode07;
            //            //item = itemCode07;
            //            #region "大分類コード"
            //            DataTable main7dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main7 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox07.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main7, conn);
            //            adapter.Fill(main7dt);
            //            DataRow dataRow13;
            //            dataRow13 = main7dt.Rows[0];
            //            int mainCategory7 = (int)dataRow13["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item7dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item7 = "select * from item_m where item_name = '" + itemComboBox07.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item7, conn);
            //            adapter.Fill(item7dt);
            //            DataRow dataRow14;
            //            dataRow14 = item7dt.Rows[0];
            //            int item7 = (int)dataRow14["item_code"];
            //            #endregion
            //            Detail = itemDetail07.Text;
            //            Weight = decimal.Parse(weightTextBox07.Text);
            //            Count = int.Parse(countTextBox07.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox07.Text);
            //            amount = money7 + money7 * Tax / 100; //decimal.Parse(moneyTextBox07.Text);
            //            Remarks = remarks07.Text;

            //            DataTable dt10 = new DataTable();
            //            string sql_str10 = "UPDATE delivery_calc SET item_code = " + item7 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory7 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str10, conn);
            //            adapter.Fill(dt10);
            //        }
            //        else
            //        {
            //            record = 8;
            //            //mainCategory = mainCategoryCode07;
            //            //item = itemCode07;
            //            #region "大分類コード"
            //            DataTable main7dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main7 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox07.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main7, conn);
            //            adapter.Fill(main7dt);
            //            DataRow dataRow13;
            //            dataRow13 = main7dt.Rows[0];
            //            int mainCategory7 = (int)dataRow13["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item7dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item7 = "select * from item_m where item_name = '" + itemComboBox07.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item7, conn);
            //            adapter.Fill(item7dt);
            //            DataRow dataRow14;
            //            dataRow14 = item7dt.Rows[0];
            //            int item7 = (int)dataRow14["item_code"];
            //            #endregion
            //            Detail = itemDetail07.Text;
            //            Weight = decimal.Parse(weightTextBox07.Text);
            //            Count = int.Parse(countTextBox07.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox07.Text);
            //            amount = money7 + money7 * Tax / 100; //decimal.Parse(moneyTextBox07.Text);
            //            Remarks = remarks07.Text;

            //            DataTable dt10 = new DataTable();
            //            string sql_str10 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item7 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory7 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str10, conn);
            //            adapter.Fill(dt10);
            //        }

            //    }
            //    #endregion
            //    #region "9行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox08.Text) && !(unitPriceTextBox08.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 9)
            //        {
            //            record = 9;
            //            //mainCategory = mainCategoryCode08;
            //            //item = itemCode08;
            //            #region "大分類コード"
            //            DataTable main8dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main8 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox08.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main8, conn);
            //            adapter.Fill(main8dt);
            //            DataRow dataRow15;
            //            dataRow15 = main8dt.Rows[0];
            //            int mainCategory8 = (int)dataRow15["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item8dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item8 = "select * from item_m where item_name = '" + itemComboBox08.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item8, conn);
            //            adapter.Fill(item8dt);
            //            DataRow dataRow16;
            //            dataRow16 = item8dt.Rows[0];
            //            int item8 = (int)dataRow16["item_code"];
            //            #endregion
            //            Detail = itemDetail08.Text;
            //            Weight = decimal.Parse(weightTextBox08.Text);
            //            Count = int.Parse(countTextBox08.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox08.Text);
            //            amount = money8 + money8 * Tax / 100; //decimal.Parse(moneyTextBox08.Text);
            //            Remarks = remarks08.Text;

            //            DataTable dt11 = new DataTable();
            //            string sql_str11 = "UPDATE delivery_calc SET item_code = " + item8 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory8 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str11, conn);
            //            adapter.Fill(dt11);
            //        }
            //        else
            //        {
            //            record = 9;
            //            //mainCategory = mainCategoryCode08;
            //            //item = itemCode08;
            //            #region "大分類コード"
            //            DataTable main8dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main8 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox08.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main8, conn);
            //            adapter.Fill(main8dt);
            //            DataRow dataRow15;
            //            dataRow15 = main8dt.Rows[0];
            //            int mainCategory8 = (int)dataRow15["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item8dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item8 = "select * from item_m where item_name = '" + itemComboBox08.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item8, conn);
            //            adapter.Fill(item8dt);
            //            DataRow dataRow16;
            //            dataRow16 = item8dt.Rows[0];
            //            int item8 = (int)dataRow16["item_code"];
            //            #endregion
            //            Detail = itemDetail08.Text;
            //            Weight = decimal.Parse(weightTextBox08.Text);
            //            Count = int.Parse(countTextBox08.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox08.Text);
            //            amount = money8 + money8 * Tax / 100; //decimal.Parse(moneyTextBox08.Text);
            //            Remarks = remarks08.Text;

            //            DataTable dt11 = new DataTable();
            //            string sql_str11 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item8 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory8 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str11, conn);
            //            adapter.Fill(dt11);
            //        }

            //    }
            //    #endregion
            //    #region "10行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox09.Text) && !(unitPriceTextBox09.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 10)
            //        {
            //            record = 10;
            //            //mainCategory = mainCategoryCode09;
            //            //item = itemCode09;
            //            #region "大分類コード"
            //            DataTable main9dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main9 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox09.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main9, conn);
            //            adapter.Fill(main9dt);
            //            DataRow dataRow17;
            //            dataRow17 = main9dt.Rows[0];
            //            int mainCategory9 = (int)dataRow17["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item9dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item9 = "select * from item_m where item_name = '" + itemComboBox09.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item9, conn);
            //            adapter.Fill(item9dt);
            //            DataRow dataRow18;
            //            dataRow18 = item9dt.Rows[0];
            //            int item9 = (int)dataRow18["item_code"];
            //            #endregion
            //            Detail = itemDetail09.Text;
            //            Weight = decimal.Parse(weightTextBox09.Text);
            //            Count = int.Parse(countTextBox09.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox09.Text);
            //            amount = money9 + money9 * Tax / 100; //decimal.Parse(moneyTextBox09.Text);
            //            Remarks = remarks09.Text;

            //            DataTable dt12 = new DataTable();
            //            string sql_str12 = "UPDATE delivery_calc SET item_code = " + item9 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory9 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str12, conn);
            //            adapter.Fill(dt12);
            //        }
            //        else
            //        {
            //            record = 10;
            //            //mainCategory = mainCategoryCode09;
            //            //item = itemCode09;
            //            #region "大分類コード"
            //            DataTable main9dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main9 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox09.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main9, conn);
            //            adapter.Fill(main9dt);
            //            DataRow dataRow17;
            //            dataRow17 = main9dt.Rows[0];
            //            int mainCategory9 = (int)dataRow17["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item9dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item9 = "select * from item_m where item_name = '" + itemComboBox09.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item9, conn);
            //            adapter.Fill(item9dt);
            //            DataRow dataRow18;
            //            dataRow18 = item9dt.Rows[0];
            //            int item9 = (int)dataRow18["item_code"];
            //            #endregion
            //            Detail = itemDetail09.Text;
            //            Weight = decimal.Parse(weightTextBox09.Text);
            //            Count = int.Parse(countTextBox09.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox09.Text);
            //            amount = money9 + money9 * Tax / 100; //decimal.Parse(moneyTextBox09.Text);
            //            Remarks = remarks09.Text;

            //            DataTable dt12 = new DataTable();
            //            string sql_str12 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item9 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory9 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str12, conn);
            //            adapter.Fill(dt12);
            //        }

            //    }
            //    #endregion
            //    #region "11行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox010.Text) && !(unitPriceTextBox010.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 11)
            //        {
            //            record = 11;
            //            //mainCategory = mainCategoryCode010;
            //            //item = itemCode010;
            //            #region "大分類コード"
            //            DataTable main10dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main10 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox010.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main10, conn);
            //            adapter.Fill(main10dt);
            //            DataRow dataRow19;
            //            dataRow19 = main10dt.Rows[0];
            //            int mainCategory10 = (int)dataRow19["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item10dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item10 = "select * from item_m where item_name = '" + itemComboBox010.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item10, conn);
            //            adapter.Fill(item10dt);
            //            DataRow dataRow20;
            //            dataRow20 = item10dt.Rows[0];
            //            int item10 = (int)dataRow20["item_code"];
            //            #endregion
            //            Detail = itemDetail010.Text;
            //            Weight = decimal.Parse(weightTextBox010.Text);
            //            Count = int.Parse(countTextBox010.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox010.Text);
            //            amount = money10 + money10 * Tax / 100; //decimal.Parse(moneyTextBox010.Text);
            //            Remarks = remarks010.Text;

            //            DataTable dt13 = new DataTable();
            //            string sql_str13 = "UPDATE delivery_calc SET item_code = " + item10 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory10 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str13, conn);
            //            adapter.Fill(dt13);
            //        }
            //        else
            //        {
            //            record = 11;
            //            //mainCategory = mainCategoryCode010;
            //            //item = itemCode010;
            //            #region "大分類コード"
            //            DataTable main10dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main10 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox010.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main10, conn);
            //            adapter.Fill(main10dt);
            //            DataRow dataRow19;
            //            dataRow19 = main10dt.Rows[0];
            //            int mainCategory10 = (int)dataRow19["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item10dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item10 = "select * from item_m where item_name = '" + itemComboBox010.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item10, conn);
            //            adapter.Fill(item10dt);
            //            DataRow dataRow20;
            //            dataRow20 = item10dt.Rows[0];
            //            int item10 = (int)dataRow20["item_code"];
            //            #endregion
            //            Detail = itemDetail010.Text;
            //            Weight = decimal.Parse(weightTextBox010.Text);
            //            Count = int.Parse(countTextBox010.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox010.Text);
            //            amount = money10 + money10 * Tax / 100; //decimal.Parse(moneyTextBox010.Text);
            //            Remarks = remarks010.Text;

            //            DataTable dt13 = new DataTable();
            //            string sql_str13 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item10 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory10 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str13, conn);
            //            adapter.Fill(dt13);
            //        }

            //    }
            //    #endregion
            //    #region "12行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox011.Text) && !(unitPriceTextBox011.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 12)
            //        {
            //            record = 12;
            //            //mainCategory = mainCategoryCode011;
            //            //item = itemCode011;
            //            #region "大分類コード"
            //            DataTable main11dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main11 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox011.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main11, conn);
            //            adapter.Fill(main11dt);
            //            DataRow dataRow21;
            //            dataRow21 = main11dt.Rows[0];
            //            int mainCategory11 = (int)dataRow21["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item11dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item11 = "select * from item_m where item_name = '" + itemComboBox011.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item11, conn);
            //            adapter.Fill(item11dt);
            //            DataRow dataRow22;
            //            dataRow22 = item11dt.Rows[0];
            //            int item11 = (int)dataRow22["item_code"];
            //            #endregion
            //            Detail = itemDetail011.Text;
            //            Weight = decimal.Parse(weightTextBox011.Text);
            //            Count = int.Parse(countTextBox011.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox011.Text);
            //            amount = money11 + money11 * Tax / 100; //decimal.Parse(moneyTextBox011.Text);
            //            Remarks = remarks011.Text;

            //            DataTable dt14 = new DataTable();
            //            string sql_str14 = "UPDATE delivery_calc SET item_code = " + item11 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory11 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str14, conn);
            //            adapter.Fill(dt14);
            //        }
            //        else
            //        {
            //            record = 12;
            //            //mainCategory = mainCategoryCode011;
            //            //item = itemCode011;
            //            #region "大分類コード"
            //            DataTable main11dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main11 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox011.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main11, conn);
            //            adapter.Fill(main11dt);
            //            DataRow dataRow21;
            //            dataRow21 = main11dt.Rows[0];
            //            int mainCategory11 = (int)dataRow21["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item11dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item11 = "select * from item_m where item_name = '" + itemComboBox011.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item11, conn);
            //            adapter.Fill(item11dt);
            //            DataRow dataRow22;
            //            dataRow22 = item11dt.Rows[0];
            //            int item11 = (int)dataRow22["item_code"];
            //            #endregion
            //            Detail = itemDetail011.Text;
            //            Weight = decimal.Parse(weightTextBox011.Text);
            //            Count = int.Parse(countTextBox011.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox011.Text);
            //            amount = money11 + money11 * Tax / 100; //decimal.Parse(moneyTextBox011.Text);
            //            Remarks = remarks011.Text;

            //            DataTable dt14 = new DataTable();
            //            string sql_str14 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item11 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory11 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str14, conn);
            //            adapter.Fill(dt14);
            //        }

            //    }
            //    #endregion
            //    #region "13行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox012.Text) && !(unitPriceTextBox012.Text == "単価 -> 重量 or 数量"))
            //    {
            //        if (Re >= 13)
            //        {
            //            record = 13;
            //            //mainCategory = mainCategoryCode012;
            //            //item = itemCode012;
            //            #region "大分類コード"
            //            DataTable main12dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main12 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox012.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main12, conn);
            //            adapter.Fill(main12dt);
            //            DataRow dataRow23;
            //            dataRow23 = main12dt.Rows[0];
            //            int mainCategory12 = (int)dataRow23["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item12dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item12 = "select * from item_m where item_name = '" + itemComboBox012.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item12, conn);
            //            adapter.Fill(item12dt);
            //            DataRow dataRow24;
            //            dataRow24 = item12dt.Rows[0];
            //            int item12 = (int)dataRow24["item_code"];
            //            #endregion
            //            Detail = itemDetail012.Text;
            //            Weight = decimal.Parse(weightTextBox012.Text);
            //            Count = int.Parse(countTextBox012.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox012.Text);
            //            amount = money12 + money12 * Tax / 100; //decimal.Parse(moneyTextBox012.Text);
            //            Remarks = remarks012.Text;

            //            DataTable dt15 = new DataTable();
            //            string sql_str15 = "UPDATE delivery_calc SET item_code = " + item12 + " ,weight = " + Weight + " ,count = " + Count + " ,unit_price = " + UnitPrice + " ,amount = " + amount + " ,remarks = '" + Remarks + "',main_category_code = " + mainCategory12 + ",detail = '" + Detail + "' where control_number = " + ControlNumber + " and record_number = " + record + ";";

            //            adapter = new NpgsqlDataAdapter(sql_str15, conn);
            //            adapter.Fill(dt15);
            //        }
            //        else
            //        {
            //            record = 13;
            //            //mainCategory = mainCategoryCode012;
            //            //item = itemCode012;
            //            #region "大分類コード"
            //            DataTable main12dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_main12 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox012.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_main12, conn);
            //            adapter.Fill(main12dt);
            //            DataRow dataRow23;
            //            dataRow23 = main12dt.Rows[0];
            //            int mainCategory12 = (int)dataRow23["main_category_code"];
            //            #endregion
            //            #region "品名コード"
            //            DataTable item12dt = new DataTable();
            //            //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //            string sql_item12 = "select * from item_m where item_name = '" + itemComboBox012.Text + "';";
            //            adapter = new NpgsqlDataAdapter(sql_item12, conn);
            //            adapter.Fill(item12dt);
            //            DataRow dataRow24;
            //            dataRow24 = item12dt.Rows[0];
            //            int item12 = (int)dataRow24["item_code"];
            //            #endregion
            //            Detail = itemDetail012.Text;
            //            Weight = decimal.Parse(weightTextBox012.Text);
            //            Count = int.Parse(countTextBox012.Text);
            //            UnitPrice = decimal.Parse(unitPriceTextBox012.Text);
            //            amount = money12 + money12 * Tax / 100; //decimal.Parse(moneyTextBox012.Text);
            //            Remarks = remarks012.Text;

            //            DataTable dt15 = new DataTable();
            //            string sql_str15 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item12 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory12 + ",'" + Detail + "');";

            //            adapter = new NpgsqlDataAdapter(sql_str15, conn);
            //            adapter.Fill(dt15);
            //        }

            //    }
            //    #endregion
            //}
            //#region "新規登録"
            //else
            //{
            //    int ControlNumber = number;

            //    decimal TotalWeight = weisum;
            //    //countsum = countsum00 + countsum01 + countsum02 + countsum03 + countsum04 + countsum05 + countsum06 + countsum07 + countsum08 + countsum09 + countsum010 + countsum011 + countsum012;
            //    int Amount = countsum;
            //    //int Amount = int.Parse(totalCount2.Text);
            //    decimal SubTotal = subSum + TaxAmount;
            //    decimal Total = sum;
            //    //vat（税込み税抜きどちらか）, vat_rate（税率）
            //    string vat = this.comboBox11.SelectedItem.ToString();
            //    int vat_rate = (int)Tax;

            //    string seaal = "";
            //    //印鑑印刷
            //    if (sealY.Checked == true)
            //    {
            //        seaal = "する";
            //    }
            //    if (sealN.Checked == true)
            //    {
            //        seaal = "しない";
            //    }

            //    string OrderDate = orderDateTimePicker.Text;
            //    string DeliveryDate = DeliveryDateTimePicker.Text;
            //    string SettlementDate = SettlementDateTimePicker.Text;
            //    string PaymentMethod = paymentMethodComboBox.SelectedItem.ToString();
            //    string Name = name.Text;                                           //宛名
            //    string Title = titleComboBox.SelectedItem.ToString();              //敬称
            //    string Type = typeComboBox.SelectedItem.ToString();                //納品書or請求書
            //    string payee = PayeeTextBox1.Text;                                 //振り込み先
            //    string coin = CoinComboBox.SelectedItem.ToString();                //通貨
            //    string remark = RemarkRegister.Text;
            //    DateTime dat1 = DateTime.Now;
            //    DateTime dtToday = dat1.Date;
            //    string c = dtToday.ToString("yyyy年MM月dd日");

            //    PostgreSQL postgre = new PostgreSQL();

            //    #region "法人"
            //    if (type == 0)
            //    {
            //        NpgsqlConnection connA = new NpgsqlConnection();
            //        connA = postgre.connection();
            //        //connA.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        NpgsqlDataAdapter adapterA;
            //        DataTable clientDt = new DataTable();
            //        string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and name = '" + client_staff_name + "' and address = '" + address + "';";
            //        adapterA = new NpgsqlDataAdapter(str_sql_corporate, connA);
            //        adapterA.Fill(clientDt);

            //        DataRow row2;
            //        row2 = clientDt.Rows[0];
            //        int antique = (int)row2["antique_number"];
            //        #region "使用する"
            //        NpgsqlConnection conn0 = new NpgsqlConnection();
            //        conn0 = postgre.connection();
            //        NpgsqlDataAdapter adapter0;
            //        //conn0.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        DataTable dt = new DataTable();
            //        string sql_str = "Insert into delivery_m (control_number, antique_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, remarks2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "'," + antique + ",'" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + 0 + ");";
            //        adapter0 = new NpgsqlDataAdapter(sql_str, conn0);
            //        adapter0.Fill(dt);
            //        #endregion
            //        #region "履歴"
            //        DataTable dtre = new DataTable();
            //        string sql_str_re = "Insert into delivery_m_revisions (control_number, antique_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seal_print, payment_method, account_payable, currency, remarks2, registration_date, insert_name ) VALUES ( '" + ControlNumber + "'," + antique + ",'" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + c + "'," + staff_id + ");";
            //        adapter0 = new NpgsqlDataAdapter(sql_str_re, conn);
            //        adapter0.Fill(dtre);
            //        #endregion
            //    }
            //    #endregion
            //    #region "個人"
            //    if (type == 1)
            //    {
            //        NpgsqlConnection connI = new NpgsqlConnection();
            //        connI = postgre.connection();
            //        //connI.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        NpgsqlDataAdapter adapterI;
            //        DataTable clientDt = new DataTable();
            //        string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
            //        adapterI = new NpgsqlDataAdapter(str_sql_individual, connI);
            //        adapterI.Fill(clientDt);

            //        DataRow row2;
            //        row2 = clientDt.Rows[0];
            //        int ID = (int)row2["id_number"];
            //        #region "使用する"
            //        NpgsqlConnection conn1 = new NpgsqlConnection();
            //        NpgsqlDataAdapter adapter1;
            //        conn1 = postgre.connection();
            //        //conn1.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        DataTable dt = new DataTable();
            //        string sql_str = "Insert into delivery_m (control_number, id_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, remarks2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "','" + ID + "','" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + 1 + ");";
            //        adapter1 = new NpgsqlDataAdapter(sql_str, conn1);
            //        adapter1.Fill(dt);
            //        #endregion
            //        #region "履歴"
            //        DataTable dtre = new DataTable();
            //        string sql_str_re = "Insert into delivery_m_revisions (control_number, id_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seal_print, payment_method, account_payable, currency, remarks2, registration_date, insert_name ) VALUES ( '" + ControlNumber + "'," + ID + ",'" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + c + "'," + staff_id + ");";
            //        adapter1 = new NpgsqlDataAdapter(sql_str_re, conn);
            //        adapter1.Fill(dtre);
            //        #endregion
            //    }
            //    #endregion

            //    /*NpgsqlConnection conn = new NpgsqlConnection();
            //    NpgsqlDataAdapter adapter;

            //    DataTable dt = new DataTable();
            //    string sql_str = "Insert into delivery_m (control_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, honorific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payble, currency, remarks2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "','" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "', '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + type + ");";
            //    adapter = new NpgsqlDataAdapter(sql_str, conn);
            //    adapter.Fill(dt);*/

            //    #region "1行目"
            //    //管理番号：ControlNumber
            //    int record = 1;     //行数
            //                        //int mainCategory = mainCategoryCode00;
            //                        //int item = itemCode00;
            //    #region "大分類コード"
            //    DataTable maindt = new DataTable();

            //    conn = postgre.connection();
            //    //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //    string sql_main = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox00.Text + "';";
            //    adapter = new NpgsqlDataAdapter(sql_main, conn);
            //    adapter.Fill(maindt);
            //    DataRow dataRow;
            //    dataRow = maindt.Rows[0];
            //    int mainCategory = (int)dataRow["main_category_code"];
            //    #endregion
            //    #region "品名コード"
            //    DataTable itemdt = new DataTable();
            //    //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //    string sql_item = "select * from item_m where item_name = '" + itemComboBox00.Text + "';";
            //    adapter = new NpgsqlDataAdapter(sql_item, conn);
            //    adapter.Fill(itemdt);
            //    DataRow dataRow1;
            //    dataRow1 = itemdt.Rows[0];
            //    int item = (int)dataRow1["item_code"];
            //    #endregion
            //    string Detail = itemDetail00.Text;
            //    decimal Weight = decimal.Parse(weightTextBox00.Text);
            //    int Count = int.Parse(countTextBox00.Text);
            //    decimal UnitPrice = decimal.Parse(unitPriceTextBox00.Text);
            //    decimal amount = money0 + money0 * Tax / 100;
            //    string Remarks = remarks00.Text;

            //    DataTable dt2 = new DataTable();
            //    string sql_str2 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + mainCategory + "','" + Detail + "');";
            //    conn = postgre.connection();
            //    //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //    conn.Open();
            //    adapter = new NpgsqlDataAdapter(sql_str2, conn);
            //    adapter.Fill(dt2);
            //    #region "履歴"
            //    DataTable dt2re = new DataTable();
            //    string sql_str2_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

            //    adapter = new NpgsqlDataAdapter(sql_str2_re, conn);
            //    adapter.Fill(dt2re);
            //    #endregion
            //    #endregion
            //    #region "2行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox01.Text) && !(unitPriceTextBox01.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 2;
            //        //mainCategory = mainCategoryCode01;
            //        //item = itemCode01;
            //        #region "大分類コード"
            //        DataTable main1dt = new DataTable();
            //        string sql_main1 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox01.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main1, conn);
            //        adapter.Fill(main1dt);
            //        DataRow dataRow2;
            //        dataRow2 = main1dt.Rows[0];
            //        int mainCategory1 = (int)dataRow2["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item1dt = new DataTable();
            //        string sql_item1 = "select * from item_m where item_name = '" + itemComboBox01.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item1, conn);
            //        adapter.Fill(item1dt);
            //        DataRow dataRow3;
            //        dataRow3 = item1dt.Rows[0];
            //        int item1 = (int)dataRow3["item_code"];
            //        #endregion
            //        Detail = itemDetail01.Text;
            //        Weight = decimal.Parse(weightTextBox01.Text);
            //        Count = int.Parse(countTextBox01.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox01.Text);
            //        amount = money1 + money1 * Tax / 100;  //decimal.Parse(moneyTextBox01.Text);
            //        Remarks = remarks01.Text;

            //        DataTable dt4 = new DataTable();
            //        string sql_str4 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item1 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory1 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str4, conn);
            //        adapter.Fill(dt4);

            //        #region "履歴"
            //        DataTable dt4re = new DataTable();
            //        string sql_str4_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str4_re, conn);
            //        adapter.Fill(dt4re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "3行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox02.Text) && !(unitPriceTextBox02.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 3;
            //        //mainCategory = mainCategoryCode02;
            //        //item = itemCode02;
            //        #region "大分類コード"
            //        DataTable main2dt = new DataTable();
            //        string sql_main2 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox02.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main2, conn);
            //        adapter.Fill(main2dt);
            //        DataRow dataRow4;
            //        dataRow4 = main2dt.Rows[0];
            //        int mainCategory2 = (int)dataRow4["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item1dt = new DataTable();
            //        string sql_item2 = "select * from item_m where item_name = '" + itemComboBox02.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item2, conn);
            //        adapter.Fill(item1dt);
            //        DataRow dataRow5;
            //        dataRow5 = item1dt.Rows[0];
            //        int item2 = (int)dataRow5["item_code"];
            //        #endregion
            //        Detail = itemDetail02.Text;
            //        Weight = decimal.Parse(weightTextBox02.Text);
            //        Count = int.Parse(countTextBox02.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox02.Text);
            //        amount = money2 + money2 * Tax / 100;  //decimal.Parse(moneyTextBox02.Text);
            //        Remarks = remarks02.Text;

            //        DataTable dt5 = new DataTable();
            //        string sql_str5 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item2 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory2 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str5, conn);
            //        adapter.Fill(dt5);

            //        #region "履歴"
            //        DataTable dt5re = new DataTable();
            //        string sql_str5_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str5_re, conn);
            //        adapter.Fill(dt5re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "4行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox03.Text) && !(unitPriceTextBox03.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 4;
            //        //mainCategory = mainCategoryCode03;
            //        //item = itemCode03;
            //        #region "大分類コード"
            //        DataTable main3dt = new DataTable();
            //        string sql_main3 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox03.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main3, conn);
            //        adapter.Fill(main3dt);
            //        DataRow dataRow6;
            //        dataRow6 = main3dt.Rows[0];
            //        int mainCategory3 = (int)dataRow6["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item3dt = new DataTable();
            //        string sql_item3 = "select * from item_m where item_name = '" + itemComboBox03.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item3, conn);
            //        adapter.Fill(item3dt);
            //        DataRow dataRow7;
            //        dataRow7 = item3dt.Rows[0];
            //        int item3 = (int)dataRow7["item_code"];
            //        #endregion
            //        Detail = itemDetail03.Text;
            //        Weight = decimal.Parse(weightTextBox03.Text);
            //        Count = int.Parse(countTextBox03.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox03.Text);
            //        amount = money3 + money3 * Tax / 100;  //decimal.Parse(moneyTextBox03.Text);
            //        Remarks = remarks03.Text;

            //        DataTable dt6 = new DataTable();
            //        string sql_str6 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item3 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory3 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str6, conn);
            //        adapter.Fill(dt6);

            //        #region "履歴"
            //        DataTable dt6re = new DataTable();
            //        string sql_str6_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str6_re, conn);
            //        adapter.Fill(dt6re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "5行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox04.Text) && !(unitPriceTextBox04.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 5;
            //        //mainCategory = mainCategoryCode04;
            //        //item = itemCode04;
            //        #region "大分類コード"
            //        DataTable main4dt = new DataTable();
            //        string sql_main4 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox04.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main4, conn);
            //        adapter.Fill(main4dt);
            //        DataRow dataRow8;
            //        dataRow8 = main4dt.Rows[0];
            //        int mainCategory4 = (int)dataRow8["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item4dt = new DataTable();
            //        string sql_item4 = "select * from item_m where item_name = '" + itemComboBox04.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item4, conn);
            //        adapter.Fill(item4dt);
            //        DataRow dataRow9;
            //        dataRow9 = item4dt.Rows[0];
            //        int item4 = (int)dataRow9["item_code"];
            //        #endregion
            //        Detail = itemDetail04.Text;
            //        Weight = decimal.Parse(weightTextBox04.Text);
            //        Count = int.Parse(countTextBox04.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox04.Text);
            //        amount = money4 + money4 * Tax / 100; //decimal.Parse(moneyTextBox04.Text);
            //        Remarks = remarks04.Text;

            //        DataTable dt7 = new DataTable();
            //        string sql_str7 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item4 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory4 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str7, conn);
            //        adapter.Fill(dt7);
            //        #region "履歴"
            //        DataTable dt7re = new DataTable();
            //        string sql_str7_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str7_re, conn);
            //        adapter.Fill(dt7re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "6行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox05.Text) && !(unitPriceTextBox05.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 6;
            //        //mainCategory = mainCategoryCode05;
            //        //item = itemCode05;
            //        #region "大分類コード"
            //        DataTable main5dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_main5 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox05.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main5, conn);
            //        adapter.Fill(main5dt);
            //        DataRow dataRow10;
            //        dataRow10 = main5dt.Rows[0];
            //        int mainCategory5 = (int)dataRow10["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item5dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_item5 = "select * from item_m where item_name = '" + itemComboBox05.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item5, conn);
            //        adapter.Fill(item5dt);
            //        DataRow dataRow11;
            //        dataRow11 = item5dt.Rows[0];
            //        int item5 = (int)dataRow11["item_code"];
            //        #endregion
            //        Detail = itemDetail05.Text;
            //        Weight = decimal.Parse(weightTextBox05.Text);
            //        Count = int.Parse(countTextBox05.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox05.Text);
            //        amount = money5 + money5 * Tax / 100; //decimal.Parse(moneyTextBox05.Text);
            //        Remarks = remarks05.Text;

            //        DataTable dt8 = new DataTable();
            //        string sql_str8 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES  ( '" + ControlNumber + "' ,'" + record + "' , " + item5 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory5 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str8, conn);
            //        adapter.Fill(dt8);

            //        #region "履歴"
            //        DataTable dt8re = new DataTable();
            //        string sql_str8_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str8_re, conn);
            //        adapter.Fill(dt8re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "7行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox06.Text) && !(unitPriceTextBox06.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 7;
            //        //mainCategory = mainCategoryCode06;
            //        //item = itemCode06;
            //        #region "大分類コード"
            //        DataTable main6dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_main6 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox06.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main6, conn);
            //        adapter.Fill(main6dt);
            //        DataRow dataRow12;
            //        dataRow12 = main6dt.Rows[0];
            //        int mainCategory6 = (int)dataRow12["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item6dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_item6 = "select * from item_m where item_name = '" + itemComboBox06.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item6, conn);
            //        adapter.Fill(item6dt);
            //        DataRow dataRow13;
            //        dataRow13 = item6dt.Rows[0];
            //        int item6 = (int)dataRow13["item_code"];
            //        #endregion
            //        Detail = itemDetail06.Text;
            //        Weight = decimal.Parse(weightTextBox06.Text);
            //        Count = int.Parse(countTextBox06.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox06.Text);
            //        amount = money6 + money6 * Tax / 100; //decimal.Parse(moneyTextBox06.Text);
            //        Remarks = remarks06.Text;

            //        DataTable dt9 = new DataTable();
            //        string sql_str9 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item6 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory6 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str9, conn);
            //        adapter.Fill(dt9);

            //        #region "履歴"
            //        DataTable dt9re = new DataTable();
            //        string sql_str9_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str9_re, conn);
            //        adapter.Fill(dt9re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "8行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox07.Text) && !(unitPriceTextBox07.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 8;
            //        //mainCategory = mainCategoryCode07;
            //        //item = itemCode07;
            //        #region "大分類コード"
            //        DataTable main7dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_main7 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox07.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main7, conn);
            //        adapter.Fill(main7dt);
            //        DataRow dataRow13;
            //        dataRow13 = main7dt.Rows[0];
            //        int mainCategory7 = (int)dataRow13["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item7dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_item7 = "select * from item_m where item_name = '" + itemComboBox07.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item7, conn);
            //        adapter.Fill(item7dt);
            //        DataRow dataRow14;
            //        dataRow14 = item7dt.Rows[0];
            //        int item7 = (int)dataRow14["item_code"];
            //        #endregion
            //        Detail = itemDetail07.Text;
            //        Weight = decimal.Parse(weightTextBox07.Text);
            //        Count = int.Parse(countTextBox07.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox07.Text);
            //        amount = money7 + money7 * Tax / 100; //decimal.Parse(moneyTextBox07.Text);
            //        Remarks = remarks07.Text;

            //        DataTable dt10 = new DataTable();
            //        string sql_str10 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item7 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory7 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str10, conn);
            //        adapter.Fill(dt10);

            //        #region "履歴"
            //        DataTable dt10re = new DataTable();
            //        string sql_str10_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str10_re, conn);
            //        adapter.Fill(dt10re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "9行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox08.Text) && !(unitPriceTextBox08.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 9;
            //        //mainCategory = mainCategoryCode08;
            //        //item = itemCode08;
            //        #region "大分類コード"
            //        DataTable main8dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_main8 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox08.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main8, conn);
            //        adapter.Fill(main8dt);
            //        DataRow dataRow15;
            //        dataRow15 = main8dt.Rows[0];
            //        int mainCategory8 = (int)dataRow15["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item8dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_item8 = "select * from item_m where item_name = '" + itemComboBox08.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item8, conn);
            //        adapter.Fill(item8dt);
            //        DataRow dataRow16;
            //        dataRow16 = item8dt.Rows[0];
            //        int item8 = (int)dataRow16["item_code"];
            //        #endregion
            //        Detail = itemDetail08.Text;
            //        Weight = decimal.Parse(weightTextBox08.Text);
            //        Count = int.Parse(countTextBox08.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox08.Text);
            //        amount = money8 + money8 * Tax / 100; //decimal.Parse(moneyTextBox08.Text);
            //        Remarks = remarks08.Text;

            //        DataTable dt11 = new DataTable();
            //        string sql_str11 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item8 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory8 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str11, conn);
            //        adapter.Fill(dt11);

            //        #region "履歴"
            //        DataTable dt11re = new DataTable();
            //        string sql_str11_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str11_re, conn);
            //        adapter.Fill(dt11re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "10行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox09.Text) && !(unitPriceTextBox09.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 10;
            //        //mainCategory = mainCategoryCode09;
            //        //item = itemCode09;
            //        #region "大分類コード"
            //        DataTable main9dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_main9 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox09.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main9, conn);
            //        adapter.Fill(main9dt);
            //        DataRow dataRow17;
            //        dataRow17 = main9dt.Rows[0];
            //        int mainCategory9 = (int)dataRow17["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item9dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_item9 = "select * from item_m where item_name = '" + itemComboBox09.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item9, conn);
            //        adapter.Fill(item9dt);
            //        DataRow dataRow18;
            //        dataRow18 = item9dt.Rows[0];
            //        int item9 = (int)dataRow18["item_code"];
            //        #endregion
            //        Detail = itemDetail09.Text;
            //        Weight = decimal.Parse(weightTextBox09.Text);
            //        Count = int.Parse(countTextBox09.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox09.Text);
            //        amount = money9 + money9 * Tax / 100; //decimal.Parse(moneyTextBox09.Text);
            //        Remarks = remarks09.Text;

            //        DataTable dt12 = new DataTable();
            //        string sql_str12 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item9 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory9 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str12, conn);
            //        adapter.Fill(dt12);

            //        #region "履歴"
            //        DataTable dt12re = new DataTable();
            //        string sql_str12_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str12_re, conn);
            //        adapter.Fill(dt12re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "11行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox010.Text) && !(unitPriceTextBox010.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 11;
            //        //mainCategory = mainCategoryCode010;
            //        //item = itemCode010;
            //        #region "大分類コード"
            //        DataTable main10dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_main10 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox010.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main10, conn);
            //        adapter.Fill(main10dt);
            //        DataRow dataRow19;
            //        dataRow19 = main10dt.Rows[0];
            //        int mainCategory10 = (int)dataRow19["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item10dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_item10 = "select * from item_m where item_name = '" + itemComboBox010.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item10, conn);
            //        adapter.Fill(item10dt);
            //        DataRow dataRow20;
            //        dataRow20 = item10dt.Rows[0];
            //        int item10 = (int)dataRow20["item_code"];
            //        #endregion
            //        Detail = itemDetail010.Text;
            //        Weight = decimal.Parse(weightTextBox010.Text);
            //        Count = int.Parse(countTextBox010.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox010.Text);
            //        amount = money10 + money10 * Tax / 100; //decimal.Parse(moneyTextBox010.Text);
            //        Remarks = remarks010.Text;

            //        DataTable dt13 = new DataTable();
            //        string sql_str13 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item10 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory10 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str13, conn);
            //        adapter.Fill(dt13);

            //        #region "履歴"
            //        DataTable dt13re = new DataTable();
            //        string sql_str13_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str13_re, conn);
            //        adapter.Fill(dt13re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "12行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox011.Text) && !(unitPriceTextBox011.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 12;
            //        //mainCategory = mainCategoryCode011;
            //        //item = itemCode011;
            //        #region "大分類コード"
            //        DataTable main11dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_main11 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox011.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main11, conn);
            //        adapter.Fill(main11dt);
            //        DataRow dataRow21;
            //        dataRow21 = main11dt.Rows[0];
            //        int mainCategory11 = (int)dataRow21["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item11dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_item11 = "select * from item_m where item_name = '" + itemComboBox011.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item11, conn);
            //        adapter.Fill(item11dt);
            //        DataRow dataRow22;
            //        dataRow22 = item11dt.Rows[0];
            //        int item11 = (int)dataRow22["item_code"];
            //        #endregion
            //        Detail = itemDetail011.Text;
            //        Weight = decimal.Parse(weightTextBox011.Text);
            //        Count = int.Parse(countTextBox011.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox011.Text);
            //        amount = money11 + money11 * Tax / 100; //decimal.Parse(moneyTextBox011.Text);
            //        Remarks = remarks011.Text;

            //        DataTable dt14 = new DataTable();
            //        string sql_str14 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item11 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory11 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str14, conn);
            //        adapter.Fill(dt14);

            //        #region "履歴"
            //        DataTable dt14re = new DataTable();
            //        string sql_str14_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";


            //        adapter = new NpgsqlDataAdapter(sql_str14_re, conn);
            //        adapter.Fill(dt14re);
            //        #endregion
            //    }
            //    #endregion
            //    #region "13行目"
            //    if (!string.IsNullOrEmpty(unitPriceTextBox012.Text) && !(unitPriceTextBox012.Text == "単価 -> 重量 or 数量"))
            //    {
            //        record = 13;
            //        //mainCategory = mainCategoryCode012;
            //        //item = itemCode012;
            //        #region "大分類コード"
            //        DataTable main12dt = new DataTable();
            //        //conn.ConnectionString = @"/*Server*/ = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_main12 = "select * from main_category_m where main_category_name = '" + mainCategoryComboBox012.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_main12, conn);
            //        adapter.Fill(main12dt);
            //        DataRow dataRow23;
            //        dataRow23 = main12dt.Rows[0];
            //        int mainCategory12 = (int)dataRow23["main_category_code"];
            //        #endregion
            //        #region "品名コード"
            //        DataTable item12dt = new DataTable();
            //        //conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //        string sql_item12 = "select * from item_m where item_name = '" + itemComboBox012.Text + "';";
            //        adapter = new NpgsqlDataAdapter(sql_item12, conn);
            //        adapter.Fill(item12dt);
            //        DataRow dataRow24;
            //        dataRow24 = item12dt.Rows[0];
            //        int item12 = (int)dataRow24["item_code"];
            //        #endregion
            //        Detail = itemDetail012.Text;
            //        Weight = decimal.Parse(weightTextBox012.Text);
            //        Count = int.Parse(countTextBox012.Text);
            //        UnitPrice = decimal.Parse(unitPriceTextBox012.Text);
            //        amount = money12 + money12 * Tax / 100; //decimal.Parse(moneyTextBox012.Text);
            //        Remarks = remarks012.Text;

            //        DataTable dt15 = new DataTable();
            //        string sql_str15 = "Insert into delivery_calc (control_number, record_number, item_code, weight, count, unit_price, amount, remarks, main_category_code, detail ) VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item12 + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory12 + ",'" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str15, conn);
            //        adapter.Fill(dt15);

            //        #region "履歴"
            //        DataTable dt15re = new DataTable();
            //        string sql_str15_re = "Insert into delivery_calc_revisions VALUES ( " + ControlNumber + " ," + record + " , " + mainCategory + "," + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "','" + Detail + "');";

            //        adapter = new NpgsqlDataAdapter(sql_str15_re, conn);
            //        adapter.Fill(dt15re);
            //        #endregion
            //    }
            //    #endregion
            //}
            //#endregion
            //conn.Close();
            //MessageBox.Show("登録しました。");
            //this.DeliveryPreviewButton.Enabled = true;
            //if (access_auth != "C")
            //{
            //    this.textBox2.Visible = true;
            //    this.label10.Visible = true;
            //}

            //regist++;
            #endregion
        }

        #endregion

        #region"画像選択"
        private void financialButton_Click(object sender, EventArgs e)//ファイルを選択
        {
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    //PostgreSQL postgre = new PostgreSQL();
                    //conn = postgre.connection();

                    //conn.Open();

                    //using (transaction = conn.BeginTransaction())
                    //{
                    //    SQL = "update client_m set seal_certification = '" + sealCertificationTextBox.Text + "' where code = '" + ClientCode + "';";
                    //    cmd = new NpgsqlCommand(SQL, conn);
                    //    cmd.ExecuteNonQuery();
                    //    transaction.Commit();
                    //}
                    //conn.Close();
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
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    //PostgreSQL postgre = new PostgreSQL();
                    //conn = postgre.connection();

                    //conn.Open();

                    //using (transaction = conn.BeginTransaction())
                    //{
                    //    SQL = "update client_m set residence_card = '" + residenceCardTextBox.Text + "' where code = '" + ClientCode + "';";
                    //    cmd = new NpgsqlCommand(SQL, conn);
                    //    cmd.ExecuteNonQuery();
                    //    transaction.Commit();
                    //}
                    //conn.Close();

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
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    //PostgreSQL postgre = new PostgreSQL();
                    //conn = postgre.connection();

                    //conn.Open();

                    //using (transaction = conn.BeginTransaction())
                    //{
                    //    SQL = "update client_m set antique_license = '" + antiqueLicenceTextBox.Text + "' where code = '" + ClientCode + "';";
                    //    cmd = new NpgsqlCommand(SQL, conn);
                    //    cmd.ExecuteNonQuery();
                    //    transaction.Commit();
                    //}
                    //conn.Close();
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
                MessageBox.Show("顧客選択を先にしてください", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    //PostgreSQL postgre = new PostgreSQL();
                    //conn = postgre.connection();

                    //conn.Open();

                    //using (transaction = conn.BeginTransaction())
                    //{
                    //    SQL = "update client_m set antique_license = '" + antiqueLicenceTextBox2.Text + "' where code = '" + ClientCode + "';";
                    //    cmd = new NpgsqlCommand(SQL, conn);
                    //    cmd.ExecuteNonQuery();
                    //    transaction.Commit();
                    //}
                    //conn.Close();
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
                //residencePeriod.Visible = true;
                residencePerioddatetimepicker.Visible = true;
            }
        }

        #region"計算書　印刷プレビュー"
        private void previewButton_Click(object sender, EventArgs e)
        {
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            conn.Open();

            //計算書印刷プレビュー
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            //pd.Print();
            printPreviewDialog1.Document = pd;


            string sql = "";

            #region"顧客番号、受渡日、決済日、決済方法、合計金額、総数"
            sql = "select * from statement_data where document_number = '" + documentNumberTextBox.Text + "';";
            adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(statementTotalTable);

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
            sql = "select * from client_m where code = '" + ClientCode + "';";
            adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(clientTable);
            #endregion

            string sql_PreviewCount = "select count(*) from statement_calc_data where document_number = '" + documentNumberTextBox.Text + "';";
            cmd = new NpgsqlCommand(sql_PreviewCount, conn);
            PreviewRow = int.Parse(cmd.ExecuteScalar().ToString());

            string sql_StatementPreview = "select * from statement_calc_data A inner join item_m B on A.item_code = B.item_code" +
                " where document_number = '" + documentNumberTextBox.Text + "' order by A.record_number;";
            adapter = new NpgsqlDataAdapter(sql_StatementPreview, conn);
            adapter.Fill(table);

            conn.Close();
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
            DataRow row = statementTotalTable.Rows[0];
            string deliveryDate = DateTime.Parse(row["delivery_date"].ToString()).ToShortDateString();
            string settlementData = ((DateTime)row["settlement_date"]).ToString("yyyy/MM/dd");
            string settlementMethod = row["payment_method"].ToString();
            totalMoney = ((decimal)row["total"]).ToString("c0");
            totalWeight = ((decimal)row["total_weight"]).ToString("n1");
            totalCount = ((int)row["total_amount"]).ToString("n0");

            DataRow row1 = clientTable.Rows[0];
            int Type = (int)row1["type"];
            string companyName = row1["company_name"].ToString();
            string shopName = row1["shop_name"].ToString();
            string name = row1["name"].ToString();
            string tel = row1["phone_number"].ToString();
            string fax = row1["fax_number"].ToString();
            string Address = row1["address"].ToString();
            string birthday = row1["birthday"].ToString();
            string occupation = row1["occupation"].ToString();
            antiqueLicense = row1["antique_license"].ToString();
            #endregion

            #region"法人、個人事業主、一般で共通"
            //一番上
            e.Graphics.DrawString("計算書（お客様控え）", font1, brush, new PointF(280, 30));

            //伝票番号
            e.Graphics.DrawString(documentNumberTextBox.Text, font, brush, new PointF(680, 40));
            e.Graphics.DrawString(documentNumberTextBox.Text, font, brush, new PointF(680, 600));

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
            rowStatementPreview = table.Rows[0];

            //１行目の表の中身
            string ItemNamePreview = rowStatementPreview["item_name"].ToString();
            string ItemDetailPreview = rowStatementPreview["detail"].ToString();
            string WeightPreview = ((decimal)rowStatementPreview["weight"]).ToString("n1");
            string UnitPricePreview = ((decimal)rowStatementPreview["unit_price"]).ToString("n0");
            string CountPreview = ((int)rowStatementPreview["count"]).ToString("n0");
            string MoneyPreview = ((decimal)rowStatementPreview["amount"]).ToString("c0");
            string RemarkPreview = rowStatementPreview["remarks"].ToString();

            for (int i = 1; i <= PreviewRow; i++)
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
                        rowStatementPreview = table.Rows[1];

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
                        rowStatementPreview = table.Rows[2];

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
                        rowStatementPreview = table.Rows[3];

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
                        rowStatementPreview = table.Rows[4];

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
                        rowStatementPreview = table.Rows[5];

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
                        rowStatementPreview = table.Rows[6];

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
                        rowStatementPreview = table.Rows[7];

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
                        rowStatementPreview = table.Rows[8];

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
                        rowStatementPreview = table.Rows[9];

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
                        rowStatementPreview = table.Rows[10];

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
                        rowStatementPreview = table.Rows[11];

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
                        rowStatementPreview = table.Rows[12];

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

            if (totalWeight == "0.0")
            {
                totalWeight = "";
            }
            if(totalCount == "0")
            {
                totalCount = "";
            }

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

        }

        #endregion

        #region"計算書　成績入力画面"
        private void RecordListButton_Click(object sender, EventArgs e)
        {
            RecordList recordList = new RecordList(this, staff_id, client_staff_name, type, documentNumberTextBox.Text, Grade, AntiqueNumber, ID_Number, access_auth, pass, NameChange, CarryOver, MonthCatalog);
            screan = false;
            this.Close();
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
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region "納品書　顧客選択"
        private void Client_searchButton1_Click(object sender, EventArgs e)
        {
            client_search search2 = new client_search(this, staff_id, type, client_staff_name, address, total, number, document, access_auth, pass);
            Properties.Settings.Default.Save();
            //screan = false;
            //this.Close();
            this.data = search2.data;
            search2.ShowDialog();

            #region "これから選択する場合"
            if (count != 0)
            {
                if (type == 0)
                {
                    //顧客情報 法人
                    #region "計算書"
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m where invalid = 0 and type = 0 and code = '" + ClientCode + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    remarks1 = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    //ClientCode = (int)row2["code"];

                    label16.Text = "会社名";
                    label17.Text = "店舗名";
                    label18.Text = "担当者名・個人名";
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
                    label75.Text = "会社名";
                    label77.Text = "店舗名";
                    label76.Text = "担当者名・個人名";
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
                    string str_sql_individual = "select * from client_m where invalid = 0 and type = 1 and code = '" + ClientCode + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    remarks1 = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    //ClientCode = (int)row2["code"];

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
                    string str_sql_corporate = "select * from client_m where invalid = 0 and type = 0 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_corporate, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];
                    //int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    ClientCode = (int)row2["code"];
                    label16.Text = "会社名";
                    label17.Text = "店舗名";
                    label18.Text = "担当者名・個人名";
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
                    label75.Text = "会社名";
                    label77.Text = "店舗名";
                    label76.Text = "担当者名・個人名";
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
                    string str_sql_individual = "select * from client_m where invalid = 0 and type = 1 and name = '" + client_staff_name + "' and address = '" + address + "';";
                    adapter = new NpgsqlDataAdapter(str_sql_individual, conn);
                    adapter.Fill(clientDt);

                    DataRow row2;
                    row2 = clientDt.Rows[0];

                    string name = row2["name"].ToString();
                    string register_date = row2["registration_date"].ToString();
                    remarks = row2["remarks"].ToString();
                    string occupation = row2["occupation"].ToString();
                    string birthday = row2["birthday"].ToString();
                    string antique_license = row2["antique_license"].ToString();
                    ClientCode = (int)row2["code"];

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

        #region"datagridview"

        private DataGridViewComboBoxEditingControl dataGridViewComboBox1 = null;            //計算書の大分類
        private DataGridViewComboBoxEditingControl dataGridViewComboBox2 = null;            //納品書の大分類

        #region "データグリッドビュー行追加（計算書）"
        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1[1, code].Value = itemMainCategoryCode;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            
            //２行目追加時
            if (dataGridView1.CurrentCell.RowIndex == 0)
            {
                dt12.Clear();
                //dt12 = dt5.Copy();
                //品名検索
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt12);

                row = dt12.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 1].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[1].Cells[2];
                dataGridViewComboBoxCell.DataSource = dt12;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //３行目追加時
            if (dataGridView1.CurrentCell.RowIndex == 1)
            {
                dt13.Clear();

                //品名検索
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt13);

                row = dt13.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 2].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[2].Cells[2];
                dataGridViewComboBoxCell.DataSource = dt13;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //４行目追加
            if (dataGridView1.CurrentCell.RowIndex == 2)
            {
                dt14.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt14);

                row = dt14.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 3].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 3];
                dataGridViewComboBoxCell.DataSource = dt14;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //５行目追加
            if (dataGridView1.CurrentCell.RowIndex == 3)
            {
                dt15.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt15);

                row = dt15.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 4].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 4];
                dataGridViewComboBoxCell.DataSource = dt15;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //６行目追加
            if (dataGridView1.CurrentCell.RowIndex == 4)
            {
                dt16.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt16);

                row = dt16.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 5].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 5];
                dataGridViewComboBoxCell.DataSource = dt16;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //７行目追加
            if (dataGridView1.CurrentCell.RowIndex == 5)
            {
                dt17.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt17);

                row = dt17.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 6].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 6];
                dataGridViewComboBoxCell.DataSource = dt17;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //８行目追加
            if (dataGridView1.CurrentCell.RowIndex == 6)
            {
                dt18.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt18);

                row = dt18.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 7].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 7];
                dataGridViewComboBoxCell.DataSource = dt18;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //９行目追加
            if (dataGridView1.CurrentCell.RowIndex == 7)
            {
                dt19.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt19);

                row = dt19.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 8].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 8];
                dataGridViewComboBoxCell.DataSource = dt19;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１０行目追加
            if (dataGridView1.CurrentCell.RowIndex == 8)
            {
                dt20.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt20);

                row = dt20.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 9].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 9];
                dataGridViewComboBoxCell.DataSource = dt20;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１１行目追加
            if (dataGridView1.CurrentCell.RowIndex == 9)
            {
                dt21.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt21);

                row = dt21.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 10].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 10];
                dataGridViewComboBoxCell.DataSource = dt21;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１２行目追加
            if (dataGridView1.CurrentCell.RowIndex == 10)
            {
                dt22.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt22);

                row = dt22.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 11].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 11];
                dataGridViewComboBoxCell.DataSource = dt22;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１３行目追加
            if (dataGridView1.CurrentCell.RowIndex == 11)
            {
                dt23.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt23);

                row = dt23.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 12].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 12];
                dataGridViewComboBoxCell.DataSource = dt23;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            code++;

            conn.Close();
        }
        #endregion

        private void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            #region"コンボボックスの処理"
            //表示されているコントロールがDataGridViewComboBoxEditingControlか調べる
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                DataGridView dgv = (DataGridView)sender;

                //該当する列か調べる
                if (dgv.CurrentCell.OwningColumn.Name == "MainCategoryColumn")
                {
                    //編集のために表示されているコントロールを取得
                    this.dataGridViewComboBox1 = (DataGridViewComboBoxEditingControl)e.Control;
                    //SelectedIndexChangedイベントハンドラを追加
                    this.dataGridViewComboBox1.SelectedIndexChanged += new EventHandler(dataGridViewComboBox1_SelectedIndexChanged);
                }
            }
            #endregion

            #region"重量・単価・数量の入力制御"
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                dataGridView = (DataGridView)sender;

                //編集のために表示されているコントローラを取得
                DataGridViewTextBoxEditingControl control = (DataGridViewTextBoxEditingControl)e.Control;

                //イベントハンドラを削除
                control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);
                control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPressIntOnly);

                //該当する列か調べる
                if (dataGridView.CurrentCell.OwningColumn.Name == "WeightColumn")
                {
                    control.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
                }

                if (dataGridView.CurrentCell.OwningColumn.Name == "CountColumn" || dataGridView.CurrentCell.OwningColumn.Name == "UnitPriceColumn")
                {
                    control.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPressIntOnly);
                }
            }
            #endregion
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //SelectedIndexChangedイベントハンドラを削除
            if (this.dataGridViewComboBox1 != null)
            {
                this.dataGridViewComboBox1.SelectedIndexChanged -= new EventHandler(dataGridViewComboBox1_SelectedIndexChanged);
                this.dataGridViewComboBox1 = null;
            }
        }

        private void dataGridViewComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //１行目
            if (dataGridView1.CurrentCell.RowIndex == 0)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt5.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt5);

                row = dt5.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2,0].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2,0];
                dataGridViewComboBoxCell.DataSource = dt5;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //２行目
            if (dataGridView1.CurrentCell.RowIndex == 1)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt12.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt12);

                row = dt12.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 1].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 1];
                dataGridViewComboBoxCell.DataSource = dt12;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //３行目
            if (dataGridView1.CurrentCell.RowIndex == 2)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt13.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt13);

                row = dt13.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 2].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 2];
                dataGridViewComboBoxCell.DataSource = dt13;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //４行目
            if (dataGridView1.CurrentCell.RowIndex == 3)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt14.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt14);

                row = dt14.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 3].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 3];
                dataGridViewComboBoxCell.DataSource = dt14;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //５行目
            if (dataGridView1.CurrentCell.RowIndex == 4)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt15.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt15);

                row = dt15.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 4].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 4];
                dataGridViewComboBoxCell.DataSource = dt15;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //６行目
            if (dataGridView1.CurrentCell.RowIndex == 5)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt16.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt16);

                row = dt16.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 5].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 5];
                dataGridViewComboBoxCell.DataSource = dt16;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //７行目
            if (dataGridView1.CurrentCell.RowIndex == 6)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt17.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt17);

                row = dt17.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 6].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 6];
                dataGridViewComboBoxCell.DataSource = dt17;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //８行目
            if (dataGridView1.CurrentCell.RowIndex == 7)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt18.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt18);

                row = dt18.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 7].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 7];
                dataGridViewComboBoxCell.DataSource = dt18;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //９行目
            if (dataGridView1.CurrentCell.RowIndex == 8)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt19.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt19);

                row = dt19.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 8].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 8];
                dataGridViewComboBoxCell.DataSource = dt19;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１０行目
            if (dataGridView1.CurrentCell.RowIndex == 9)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt20.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt20);

                row = dt20.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 9].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 9];
                dataGridViewComboBoxCell.DataSource = dt20;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１１行目
            if (dataGridView1.CurrentCell.RowIndex == 10)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt21.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt21);

                row = dt21.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 10].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 10];
                dataGridViewComboBoxCell.DataSource = dt21;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１２行目
            if (dataGridView1.CurrentCell.RowIndex == 11)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt22.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt22);

                row = dt22.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 11].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 11];
                dataGridViewComboBoxCell.DataSource = dt22;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１３行目
            if (dataGridView1.CurrentCell.RowIndex == 12)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt23.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt23);

                row = dt23.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView1[2, 12].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView1[2, 12];
                dataGridViewComboBoxCell.DataSource = dt23;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            conn.Close();
        }

        private void dataGridViewComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            //１行目
            if (dataGridView2.CurrentCell.RowIndex == 0)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt7.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt7);

                row = dt7.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 0].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 0];
                dataGridViewComboBoxCell.DataSource = dt7;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //２行目
            if (dataGridView2.CurrentCell.RowIndex == 1)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt32.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt32);

                row = dt32.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 1].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 1];
                dataGridViewComboBoxCell.DataSource = dt32;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //３行目
            if (dataGridView2.CurrentCell.RowIndex == 2)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt33.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt33);

                row = dt33.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 2].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 2];
                dataGridViewComboBoxCell.DataSource = dt33;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //４行目
            if (dataGridView2.CurrentCell.RowIndex == 3)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt34.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt34);

                row = dt34.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 3].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 3];
                dataGridViewComboBoxCell.DataSource = dt34;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //５行目
            if (dataGridView2.CurrentCell.RowIndex == 4)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt35.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt35);

                row = dt35.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 4].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 4];
                dataGridViewComboBoxCell.DataSource = dt35;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //６行目
            if (dataGridView2.CurrentCell.RowIndex == 5)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt36.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt36);

                row = dt36.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 5].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 5];
                dataGridViewComboBoxCell.DataSource = dt36;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //７行目
            if (dataGridView2.CurrentCell.RowIndex == 6)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt37.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt37);

                row = dt37.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 6].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 6];
                dataGridViewComboBoxCell.DataSource = dt37;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //８行目
            if (dataGridView2.CurrentCell.RowIndex == 7)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt38.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt38);

                row = dt38.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 7].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 7];
                dataGridViewComboBoxCell.DataSource = dt38;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //９行目
            if (dataGridView2.CurrentCell.RowIndex == 8)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt39.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt39);

                row = dt39.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 8].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 8];
                dataGridViewComboBoxCell.DataSource = dt39;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }

            //１０行目
            if (dataGridView2.CurrentCell.RowIndex == 9)
            {
                //選択されたアイテムを表示
                DataGridViewComboBoxEditingControl cb = (DataGridViewComboBoxEditingControl)sender;
                main = cb.EditingControlFormattedValue.ToString();

                dt40.Clear();

                #region "品名検索"
                string sql_str3 = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_name = '" + main + "';";
                adapter2 = new NpgsqlDataAdapter(sql_str3, conn);
                adapter2.Fill(dt40);

                row = dt40.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 9].Value = itemCode;
                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 9];
                dataGridViewComboBoxCell.DataSource = dt40;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
                #endregion
            }


            conn.Close();
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        private void ZenginData_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            #region"重量変更時"
            int rowNumber = dataGridView1.Rows.Count;
            TotalWeight = 0;

            for (int i = 0; i < rowNumber; i++)
            {
                if (dataGridView1[4, i].Value != null && dataGridView1[4, i].Value.ToString() != "")
                {
                    unitWeight = decimal.Parse(dataGridView1[4, i].Value.ToString());
                    TotalWeight += unitWeight;
                }
            }

            totalWeightTextBox.Text = TotalWeight.ToString("n1");
            #endregion

            #region"数量変更時"
            TotalCount = 0;

            for (int i = 0; i < rowNumber; i++)
            {
                if (dataGridView1[6, i].Value != null && dataGridView1[6, i].Value.ToString() != "")
                {
                    unitCount = int.Parse(dataGridView1[6, i].Value.ToString());
                    TotalCount += unitCount;
                }
            }

            totalCountTextBox.Text = TotalCount.ToString("n0");
            #endregion

            #region"金額変更時"
            TotalMoney = 0;

            for (int i = 0; i < rowNumber; i++)
            {
                if (dataGridView1[7, i].Value != null && dataGridView1[7, i].Value.ToString() != "")
                {
                    unitMoney = decimal.Parse(dataGridView1[7, i].Value.ToString().Substring(1));
                    TotalMoney += unitMoney;
                }
            }
            subTotal.Text = TotalMoney.ToString("c0");
            sumTextBox.Text = TotalMoney.ToString("c0");
            #endregion

            #region"計算列"
            //重量×単価（数量は空白）
            if (dataGridView1[4, e.RowIndex].Value != null && dataGridView1[5, e.RowIndex].Value != null && dataGridView1[6, e.RowIndex].Value == null &&
                dataGridView1[4, e.RowIndex].Value.ToString() != "" && dataGridView1[5, e.RowIndex].Value.ToString() != "")
            {
                weight = decimal.Parse(dataGridView1[4, e.RowIndex].Value.ToString());
                unitprice = decimal.Parse(dataGridView1[5, e.RowIndex].Value.ToString());

                dataGridView1[7, e.RowIndex].Value = (weight * unitprice).ToString("c0");
            }

            //数量×単価（重量は空白）
            if (dataGridView1[4, e.RowIndex].Value == null && dataGridView1[5, e.RowIndex].Value != null && dataGridView1[6, e.RowIndex].Value != null &&
                dataGridView1[5, e.RowIndex].Value.ToString() != "" && dataGridView1[6, e.RowIndex].Value.ToString() != "")
            {
                unitprice = decimal.Parse(dataGridView1[5, e.RowIndex].Value.ToString());
                count = int.Parse(dataGridView1[6, e.RowIndex].Value.ToString());

                dataGridView1[7, e.RowIndex].Value = (unitprice * count).ToString("c0");
            }
            #endregion
        }

        #region"計算書の datagridview の入力制御"
        //datagridview において数値、'.'、'バックスペース'のみ入力可
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        //datagridview において数値と'バックスペース'のみ入力可
        private void dataGridView1_KeyPressIntOnly(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        #endregion

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value))
                {
                    if (dataGridView1.Rows[i].Cells[7].Value != null)
                    {
                        sub -= decimal.Parse((dataGridView1.Rows[i].Cells[7].Value.ToString()).Substring(1));
                        subTotal.Text = sub.ToString("c0");
                        sumTextBox.Text = sub.ToString("c0");
                    }
                    dataGridView1.Rows.RemoveAt(i);
                    i--;
                    code--;
                }
            }
            subTotal.Text = sub.ToString();
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView2[1, CODE].Value = itemMainCategoryCode;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            string sql_RowsAdded = "";

            //２行目追加
            if (dataGridView2.CurrentCell.RowIndex == 0)
            {
                dt32.Clear();
                sql_RowsAdded= "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt32);

                row = dt32.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 1].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 1];
                dataGridViewComboBoxCell.DataSource = dt32;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //３行目追加
            if (dataGridView2.CurrentCell.RowIndex == 1)
            {
                dt33.Clear();
                sql_RowsAdded = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt33);

                row = dt33.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 2].Value = itemCode;


                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 2];
                dataGridViewComboBoxCell.DataSource = dt33;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //４行目追加
            if (dataGridView2.CurrentCell.RowIndex == 2)
            {
                dt34.Clear();
                sql_RowsAdded = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt34);

                row = dt34.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 3].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 3];
                dataGridViewComboBoxCell.DataSource = dt34;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //５行目追加
            if (dataGridView2.CurrentCell.RowIndex == 3)
            {
                dt35.Clear();
                sql_RowsAdded = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt35);

                row = dt35.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 4].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 4];
                dataGridViewComboBoxCell.DataSource = dt35;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //６行目追加
            if (dataGridView2.CurrentCell.RowIndex == 4)
            {
                dt36.Clear();
                sql_RowsAdded = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt36);

                row = dt36.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 5].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 5];
                dataGridViewComboBoxCell.DataSource = dt36;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //７行目追加
            if (dataGridView2.CurrentCell.RowIndex == 5)
            {
                dt37.Clear();
                sql_RowsAdded = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt37);

                row = dt37.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 6].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 6];
                dataGridViewComboBoxCell.DataSource = dt37;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //８行目追加
            if (dataGridView2.CurrentCell.RowIndex == 6)
            {
                dt38.Clear();
                sql_RowsAdded = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt38);

                row = dt38.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 7].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 7];
                dataGridViewComboBoxCell.DataSource = dt38;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //９行目追加
            if (dataGridView2.CurrentCell.RowIndex == 7)
            {
                dt39.Clear();
                sql_RowsAdded = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt39);

                row = dt39.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 8].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 8];
                dataGridViewComboBoxCell.DataSource = dt39;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            //１０行目追加
            if (dataGridView2.CurrentCell.RowIndex == 8)
            {
                dt40.Clear();
                sql_RowsAdded = "select * from item_m A inner join main_category_m B on A.main_category_code = B.main_category_code where B.main_category_code = '" + itemMainCategoryCode + "';";
                adapter = new NpgsqlDataAdapter(sql_RowsAdded, conn);
                adapter.Fill(dt40);

                row = dt40.Rows[0];
                itemCode = (int)row["item_code"];

                dataGridView2[2, 9].Value = itemCode;

                dataGridViewComboBoxCell = (DataGridViewComboBoxCell)dataGridView2[2, 9];
                dataGridViewComboBoxCell.DataSource = dt40;
                dataGridViewComboBoxCell.DisplayMember = "item_name";
                dataGridViewComboBoxCell.ValueMember = "item_code";
            }

            CODE++;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            dataGridView1 = (DataGridView)sender;
            if (dataGridView1.Columns[e.ColumnIndex].Name == "WeightColumn" && e.DesiredType == typeof(Object))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells != null && e.Value.ToString() != "")
                {
                    e.Value = decimal.Parse(e.Value.ToString());
                    e.ParsingApplied = true;
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "UnitPriceColumn" && e.DesiredType == typeof(Object))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells != null && e.Value.ToString() != "")
                {
                    e.Value = decimal.Parse(e.Value.ToString());
                    e.ParsingApplied = true;
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "CountColumn" && e.DesiredType == typeof(Object))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells != null && e.Value.ToString() != "")
                {
                    e.Value = int.Parse(e.Value.ToString());
                    e.ParsingApplied = true;
                }
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            #region"コンボボックスの処理"
            //表示されているコントロールがDataGridViewComboBoxEditingControlか調べる
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                DataGridView dgv = (DataGridView)sender;

                //該当する列か調べる
                if (dgv.CurrentCell.OwningColumn.Name == "MainCategoryColumn1")
                {
                    //編集のために表示されているコントロールを取得
                    this.dataGridViewComboBox2 = (DataGridViewComboBoxEditingControl)e.Control;
                    //SelectedIndexChangedイベントハンドラを追加
                    this.dataGridViewComboBox2.SelectedIndexChanged += new EventHandler(dataGridViewComboBox2_SelectedIndexChanged);
                }
            }
            #endregion

            #region"重量・単価・数量の入力制御"
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                dataGridView = (DataGridView)sender;

                //編集のために表示されているコントローラを取得
                DataGridViewTextBoxEditingControl control = (DataGridViewTextBoxEditingControl)e.Control;

                //イベントハンドラを削除
                control.KeyPress -= new KeyPressEventHandler(dataGridView2_KeyPress);
                control.KeyPress -= new KeyPressEventHandler(dataGridView2_KeyPressIntOnly);

                //該当する列か調べる
                if (dataGridView.CurrentCell.OwningColumn.Name == "WeightColumn1")
                {
                    control.KeyPress += new KeyPressEventHandler(dataGridView2_KeyPress);
                }

                if (dataGridView.CurrentCell.OwningColumn.Name == "CountColumn1" || dataGridView.CurrentCell.OwningColumn.Name == "UnitPriceColumn1")
                {
                    control.KeyPress += new KeyPressEventHandler(dataGridView2_KeyPressIntOnly);
                }
            }
            #endregion
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            #region"重量変更時"
            int rowNumber = dataGridView2.Rows.Count;
            TotalWeight = 0;

            for (int i = 0; i < rowNumber; i++)
            {
                if (dataGridView2[4, i].Value != null && dataGridView2[4, i].Value.ToString() != "")
                {
                    unitWeight = decimal.Parse(dataGridView2[4, i].Value.ToString());
                    TotalWeight += unitWeight;
                }
            }

            totalWeightTextBox1.Text = TotalWeight.ToString("n1");
            #endregion

            #region"数量変更時"
            TotalCount = 0;

            for (int i = 0; i < rowNumber; i++)
            {
                if (dataGridView2[6, i].Value != null && dataGridView2[6, i].Value.ToString() != "")
                {
                    unitCount = int.Parse(dataGridView2[6, i].Value.ToString());
                    TotalCount += unitCount;
                }
            }

            totalCountTextBox1.Text = TotalCount.ToString("n0");
            #endregion

            #region"金額変更時"
            TotalMoney = 0;

            for (int i = 0; i < rowNumber; i++)
            {
                if (dataGridView2[7, i].Value != null && dataGridView2[7, i].Value.ToString() != "")
                {
                    unitMoney = decimal.Parse(dataGridView2[7, i].Value.ToString().Substring(1));
                    TotalMoney += unitMoney;
                }
            }
            SUB = TotalMoney;
            taxAmount = SUB * Tax / (100 + Tax);
            if (comboBox11.SelectedIndex == 1)
            {
                subTotal2.Text = (TotalMoney - taxAmount).ToString("c0");
                taxAmount2.Text = taxAmount.ToString("c0");
            }
            else
            {
                subTotal2.Text = TotalMoney.ToString("c0");
                taxAmount2.Text = "";
            }
            
            sumTextBox2.Text = TotalMoney.ToString("c0");
            #endregion

            #region"計算列"
            //重量×単価（数量は空白）
            if (dataGridView2[4, e.RowIndex].Value != null && dataGridView2[5, e.RowIndex].Value != null && dataGridView2[6, e.RowIndex].Value == null &&
                dataGridView2[4, e.RowIndex].Value.ToString() != "" && dataGridView2[5, e.RowIndex].Value.ToString() != "")
            {
                weight = decimal.Parse(dataGridView2[4, e.RowIndex].Value.ToString());
                unitprice = decimal.Parse(dataGridView2[5, e.RowIndex].Value.ToString());

                dataGridView2[7, e.RowIndex].Value = (weight * unitprice).ToString("c0");
            }

            //数量×単価（重量は空白）
            if (dataGridView2[4, e.RowIndex].Value == null && dataGridView2[5, e.RowIndex].Value != null && dataGridView2[6, e.RowIndex].Value != null &&
                dataGridView2[5, e.RowIndex].Value.ToString() != "" && dataGridView2[6, e.RowIndex].Value.ToString() != "")
            {
                unitprice = decimal.Parse(dataGridView2[5, e.RowIndex].Value.ToString());
                count = int.Parse(dataGridView2[6, e.RowIndex].Value.ToString());

                dataGridView2[7, e.RowIndex].Value = (unitprice * count).ToString("c0");
            }
            #endregion
        }

        private void RemoveButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridView2.Rows[i].Cells[0].Value))
                {
                    if (dataGridView2.Rows[i].Cells[7].Value != null)
                    {
                        SUB -= decimal.Parse(dataGridView2.Rows[i].Cells[7].Value.ToString().Substring(1));
                        subTotal2.Text = SUB.ToString("c0");
                        sumTextBox2.Text = SUB.ToString("c0");
                        taxAmount = SUB - (SUB * 10 / 11);
                        taxAmount2.Text = taxAmount.ToString("n0");
                    }
                    dataGridView2.Rows.RemoveAt(i);
                    i--;
                    CODE--;
                }
            }
            subTotal.Text = sub.ToString();
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewComboBox2 != null)
            {
                this.dataGridViewComboBox2.SelectedIndexChanged -= new EventHandler(dataGridViewComboBox2_SelectedIndexChanged);
                this.dataGridViewComboBox2 = null;
            }
        }

        private void dataGridView2_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            dataGridView2 = (DataGridView)sender;
            if (dataGridView2.Columns[e.ColumnIndex].Name == "WeightColumn1" && e.DesiredType == typeof(Object))
            {
                if (dataGridView2.Rows[e.RowIndex].Cells != null && e.Value.ToString() != "")
                {
                    e.Value = decimal.Parse(e.Value.ToString());
                    e.ParsingApplied = true;
                }
            }

            if (dataGridView2.Columns[e.ColumnIndex].Name == "UnitPriceColumn1" && e.DesiredType == typeof(Object))
            {
                if (dataGridView2.Rows[e.RowIndex].Cells != null && e.Value.ToString() != "")
                {
                    e.Value = decimal.Parse(e.Value.ToString());
                    e.ParsingApplied = true;
                }
            }

            if (dataGridView2.Columns[e.ColumnIndex].Name == "CountColumn1" && e.DesiredType == typeof(Object))
            {
                if (dataGridView2.Rows[e.RowIndex].Cells != null && e.Value.ToString() != "")
                {
                    e.Value = int.Parse(e.Value.ToString());
                    e.ParsingApplied = true;
                }
            }
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }
        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        //datagridview において数値と'バックスペース'のみ入力可
        private void dataGridView2_KeyPressIntOnly(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        #region"datagridview の行数制限"
        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 13)
            {
                dataGridView1.AllowUserToAddRows = false;
            }
        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            dataGridView1.AllowUserToAddRows = true;
        }

        private void dataGridView2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            dataGridView2.AllowUserToAddRows = true;
        }

        private void dataGridView2_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count > 10)
            {
                dataGridView2.AllowUserToAddRows = false;
            }
        }
        #endregion

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((data != "S" && data != "D" && antiqueNumber == null && documentNumber == null && Grade == 0) && tabControl1.SelectedIndex != 0 && Delivery)
            {
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();

                string Str = "insert into delivery_m (control_number, staff_code) values ('" + Number + "', '" + staff_id + "');";
                cmd = new NpgsqlCommand(Str, conn);
                cmd.ExecuteNonQuery();
                Delivery = false;

                conn.Close();
            }
        }

        private void subTotal_TextChanged(object sender, EventArgs e)
        {
            if (subTotal.Text != "0")
            {
                sub = decimal.Parse(subTotal.Text.Substring(1));
                if (sub >= 2000000)
                {
                    groupBox1.Show();
                    groupBox1.BackColor = Color.OrangeRed;
                }
                else
                {
                    groupBox1.Hide();
                }
            }
            else
            {
                groupBox1.Hide();
            }
        }
    }
}