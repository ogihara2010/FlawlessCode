using Microsoft.SqlServer.Server;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Flawless_ex
{
    public partial class RecordList : Form
    {
        int staff_id;                       //ログインしてる人の id
        int type;                           //法人・個人
        Statement statement;                //計算書
        MainMenu mainmenu;                  //メインメニュー
        string SlipNumber;                  //伝票番号
        int record;                         //行数
        string Birthday;                    //誕生日（個人）, DBから持ってきた値を保持
        DateTime BirthdayDate;
        string Access_auth;
        string staff_name;                  //顧客の名前
        string address;
        decimal total;
        int grade;
        RecordList recordList;
        int AntiqueNumber;
        int ID_Number;
        string Pass;
        int Control;
        string Data;
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
        string date01;
        string date02;
        string method1;
        string amountA;
        string amountB;
        string antiqueNumber;
        string documentNumber;
        #endregion
        bool screan = true;
        bool NameChange = false;                    //品名を変更したら true
        bool CarryOver;                      //次月持ち越しから画面遷移したとき
        bool MonthCatalog;                      //月間成績一覧
        bool NotFinish = true;                         //画面を閉じる場合は false
        bool NameChangeButton = false;                   //品名変更画面に移動する際は true
        bool DeliveryButton = false;            //納品書検索
        DialogResult result;
        #region"各行の datetimepicker"
        DateTime? date1;
        DateTime? date2;
        DateTime? date3;
        DateTime? date4;
        DateTime? date5;
        DateTime? date6;
        DateTime? date7;
        DateTime? date8;
        DateTime? date9;
        DateTime? date10;
        DateTime? date11;
        DateTime? date12;
        DateTime? date13;
        #endregion

        //品名変更後、登録・再登録をしないと画面を閉じれないようにしているのは品名変更時に履歴に登録していないため

        #region"卸値を再入力時にすでに入力されているかどうかを検知"
        bool ReEnter1 = true;
        bool ReEnter2 = true;
        bool ReEnter3 = true;
        bool ReEnter4 = true;
        bool ReEnter5 = true;
        bool ReEnter6 = true;
        bool ReEnter7 = true;
        bool ReEnter8 = true;
        bool ReEnter9 = true;
        bool ReEnter10 = true;
        bool ReEnter11 = true;
        bool ReEnter12 = true;
        bool ReEnter13 = true;
        #endregion

        #region"再登録時、すでに登録されている売却日（現在入力されている売却日と比較用）"
        string CheckBuyDate1;
        string CheckBuyDate2;
        string CheckBuyDate3;
        string CheckBuyDate4;
        string CheckBuyDate5;
        string CheckBuyDate6;
        string CheckBuyDate7;
        string CheckBuyDate8;
        string CheckBuyDate9;
        string CheckBuyDate10;
        string CheckBuyDate11;
        string CheckBuyDate12;
        string CheckBuyDate13;
        #endregion

        #region"フォーマット未処理保持"
        bool first = true;                          //３桁、￥マーク処理
        #region"各行の単価（フォーマット未処理）"
        decimal UnitPriceUnFormat1;
        decimal UnitPriceUnFormat2;
        decimal UnitPriceUnFormat3;
        decimal UnitPriceUnFormat4;
        decimal UnitPriceUnFormat5;
        decimal UnitPriceUnFormat6;
        decimal UnitPriceUnFormat7;
        decimal UnitPriceUnFormat8;
        decimal UnitPriceUnFormat9;
        decimal UnitPriceUnFormat10;
        decimal UnitPriceUnFormat11;
        decimal UnitPriceUnFormat12;
        decimal UnitPriceUnFormat13;
        #endregion
        #region"各行の買取額（フォーマット未処理）：￥マーク"
        decimal PurchaseUnFormat1;
        decimal PurchaseUnFormat2;
        decimal PurchaseUnFormat3;
        decimal PurchaseUnFormat4;
        decimal PurchaseUnFormat5;
        decimal PurchaseUnFormat6;
        decimal PurchaseUnFormat7;
        decimal PurchaseUnFormat8;
        decimal PurchaseUnFormat9;
        decimal PurchaseUnFormat10;
        decimal PurchaseUnFormat11;
        decimal PurchaseUnFormat12;
        decimal PurchaseUnFormat13;
        #endregion
        #region"各行の卸値（フォーマット未処理）：￥マーク"
        decimal WholeSaleUnFormat1;
        decimal WholeSaleUnFormat2;
        decimal WholeSaleUnFormat3;
        decimal WholeSaleUnFormat4;
        decimal WholeSaleUnFormat5;
        decimal WholeSaleUnFormat6;
        decimal WholeSaleUnFormat7;
        decimal WholeSaleUnFormat8;
        decimal WholeSaleUnFormat9;
        decimal WholeSaleUnFormat10;
        decimal WholeSaleUnFormat11;
        decimal WholeSaleUnFormat12;
        decimal WholeSaleUnFormat13;
        #endregion
        decimal PurChaseMetal;                      //下表の地金の合計買取額（フォーマット未処理）：￥マーク
        decimal PurChaseDiamond;                    //下表のダイヤの合計買取額（フォーマット未処理）：￥マーク
        decimal PurChaseBrand;                      //下表のブランドの合計買取額（フォーマット未処理）：￥マーク
        decimal PurChaseProduct;                    //下表の製品の合計買取額（フォーマット未処理）：￥マーク
        decimal PurChaseOther;                      //下表のその他の合計買取額（フォーマット未処理）：￥マーク
        decimal WholeSaleMetal;                     //下表の地金の合計卸値（フォーマット未処理）：￥マーク
        decimal WholeSaleDiamond;                   //下表のダイヤの合計卸値（フォーマット未処理）：￥マーク
        decimal WholeSaleBrand;                     //下表のブランドの合計卸値（フォーマット未処理）：￥マーク
        decimal WholeSaleProduct;                   //下表の製品の合計卸値（フォーマット未処理）：￥マーク
        decimal WholeSaleOther;                     //下表のその他の合計卸値（フォーマット未処理）：￥マーク
        decimal ProFitMetal;                        //下表の地金の合計利益（フォーマット未処理）：￥マーク
        decimal ProFitDiamond;                      //下表のダイヤの合計利益（フォーマット未処理）：￥マーク
        decimal ProFitBrand;                        //下表のブランドの合計利益（フォーマット未処理）：￥マーク
        decimal ProFitProduct;                      //下表の製品の合計利益（フォーマット未処理）：￥マーク
        decimal ProFitOther;                        //下表のその他の合計利益（フォーマット未処理）：￥マーク
        decimal PurChase;                           //買取額合計（フォーマット未処理）：￥マーク
        decimal WholeSale;                          //卸値合計（フォーマット未処理）：￥マーク
        decimal ProFit;                             //利益合計（フォーマット未処理）：￥マーク
        #endregion

        #region"list_result"
        string CName;                       //会社名（法人）
        string CShop;                       //店舗名（法人）
        string CStaff;                      //担当者名（法人）
        string name;                        //名前（個人）
        string Address;                     //住所（個人）
        string Registration;                //登録日
        int GradeNumber;                    //成績番号
        decimal TotalPurchase;              //合計（買取金額）
        decimal TotalWholesale;             //合計（卸値）
        decimal TotalProfit;                //合計（利益）
        string DNumber;                     //伝票番号
        decimal MetalPurchase;              //地金買取
        decimal MetalWholesale;             //地金卸値
        decimal MetalProfit;                //地金利益
        decimal DiamondPurchase;            //ダイヤ買取
        decimal DiamondWholesale;           //ダイヤ卸値
        decimal DiamondProfit;              //ダイヤ利益
        decimal BrandPurchase;              //ブランド買取
        decimal BrandWholesale;             //ブランド卸値
        decimal BrandProfit;                //ブランド利益
        decimal ProductPurchase;            //製品・ジュエリー買取
        decimal ProductWholesale;           //製品・ジュエリー卸値
        decimal ProductProfit;              //製品・ジュエリー利益
        decimal OtherPurchase;              //その他買取
        decimal OtherWholesale;             //その他卸値
        decimal OtherProfit;                //その他利益
        int ControlNumber;                  //管理番号
        string Occupation;                  //職業（個人）
        string BirthDay;                    //誕生日（個人）
        #endregion

        #region"list_result2"
        string AssessmentDate;              //査定日
        string SaleDate = "";               //売却日
        int MainCategoryCode;               //大分類コード
        int ItemCategoryCode;               //品名コード
        decimal Purchase;                   //買取額
        decimal Wholesale;                  //卸値
        string Buyer = "";                  //売却先
        string Remark;                      //備考
        int NextMonth = 0;                  //次月持ち越し
        int Record;                         //登録する行数
        decimal Profit;                     //利益
        string ItemDetail;                  //品物詳細
        decimal Weight;                     //重量
        decimal UnitPrice;                  //単価
        int Count;                          //数量
        #endregion

        #region"品名変更時 保持する値"
        string Document;
        int GRADE;
        decimal WHOLESALE;                      //卸値
        string REMARK;                          //備考
        string BUYDATE;                         //売却日
        string BUYER;                           //売却先
        int ChangeCheck = 0;                    //品名変更チェック
        decimal PROFIT;                         //利益
        #endregion

        #region"大分類コード取得用"
        int MainCategoryCode1;
        int MainCategoryCode2;
        int MainCategoryCode3;
        int MainCategoryCode4;
        int MainCategoryCode5;
        int MainCategoryCode6;
        int MainCategoryCode7;
        int MainCategoryCode8;
        int MainCategoryCode9;
        int MainCategoryCode10;
        int MainCategoryCode11;
        int MainCategoryCode12;
        int MainCategoryCode13;
        #endregion

        #region"品名コード取得用"
        int ItemCategoryCode1;
        int ItemCategoryCode2;
        int ItemCategoryCode3;
        int ItemCategoryCode4;
        int ItemCategoryCode5;
        int ItemCategoryCode6;
        int ItemCategoryCode7;
        int ItemCategoryCode8;
        int ItemCategoryCode9;
        int ItemCategoryCode10;
        int ItemCategoryCode11;
        int ItemCategoryCode12;
        int ItemCategoryCode13;
        #endregion

        DataTable dataTable = new DataTable();
        DataTable data = new DataTable();

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        NpgsqlDataAdapter adapter;
        NpgsqlTransaction transaction;

        //list_resultに登録する用のデータテイブル
        DataTable DataTable = new DataTable();
        #region"list_result2 に登録・更新する各行のデータテイブル"
        DataTable Data1 = new DataTable();
        DataTable Data2 = new DataTable();
        DataTable Data3 = new DataTable();
        DataTable Data4 = new DataTable();
        DataTable Data5 = new DataTable();
        DataTable Data6 = new DataTable();
        DataTable Data7 = new DataTable();
        DataTable Data8 = new DataTable();
        DataTable Data9 = new DataTable();
        DataTable Data10 = new DataTable();
        DataTable Data11 = new DataTable();
        DataTable Data12 = new DataTable();
        DataTable Data13 = new DataTable();
        #endregion

        //list_result_revisions に登録する際のデータテイブル                         < - 登録時・再登録時・品名変更の新規登録時・品名変更の再登録時で分ける必要があるかも？
        DataTable DATATable = new DataTable();
        #region"list_result_revisions2 に登録する際の各行のデータテイブル"
        DataTable DATA1 = new DataTable();
        DataTable DATA2 = new DataTable();
        DataTable DATA3 = new DataTable();
        DataTable DATA4 = new DataTable();
        DataTable DATA5 = new DataTable();
        DataTable DATA6 = new DataTable();
        DataTable DATA7 = new DataTable();
        DataTable DATA8 = new DataTable();
        DataTable DATA9 = new DataTable();
        DataTable DATA10 = new DataTable();
        DataTable DATA11 = new DataTable();
        DataTable DATA12 = new DataTable();
        DataTable DATA13 = new DataTable();
        #endregion
        

        public RecordList(Statement statement, int staff_id, string Staff_Name, int type, string slipnumber, int Grade, int antique, int id, string access_auth, string pass, bool namechange, bool carryover, bool monthCatalog)
        {
            InitializeComponent();

            this.statement = statement;
            this.staff_id = staff_id;
            this.staff_name = Staff_Name;
            this.type = type;
            this.SlipNumber = slipnumber;
            this.grade = Grade;
            this.AntiqueNumber = antique;
            this.ID_Number = id;
            this.Access_auth = access_auth;
            this.Pass = pass;
            this.NameChange = namechange;
            this.CarryOver = carryover;
            this.MonthCatalog = monthCatalog;
        }

        private void RecordList_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            #region"画面上の会社・個人情報と合計金額"

            conn.Open();

            string sql = "select * from staff_m where staff_code = " + staff_id + ";";
            cmd = new NpgsqlCommand(sql, conn);
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    StaffNameTextBox.Text = reader["staff_name"].ToString();
                }
            }

            #region"権限"
            if (Access_auth == "C")
            {
                ItemNameChangeButton.Enabled = false;
            }
            #endregion

            SlipNumberTextBox.Text = SlipNumber;

            GradeNumberTextBox.Text = SlipNumber.Trim('F');

            #region"法人・個人情報"

            string sql_str1 = "";
            if (type == 0)
            {   //法人用   合計金額, 会社名, 店舗名, 担当者名を取得
                sql_str1 = "select * from statement_data where document_number = '" + SlipNumber + "' and type = '" + 0 + "';";
            }
            else if (type == 1)
            {   //個人用    合計金額、名前、職業、住所、生年月日を取得
                sql_str1 = "select * from statement_data where document_number = '" + SlipNumber + "' and type = '" + 1 + "';";
            }

            #endregion
            cmd = new NpgsqlCommand(sql_str1, conn);

            //会社名、店舗名、担当者名、名前、職業、住所取得

            using (reader = cmd.ExecuteReader())
            {
                if (type == 0)
                {
                    NameOrCompanyNameLabel.Text = "会社名";
                    OccupationOrShopNameLabel.Text = "店舗名";
                    AddressOrClientStaffNameLabel.Text = "担当者名";
                    AddressOrClientStaffNameTextBox.Size = new Size(400, 60);
                    NameOrCompanyNameTextBox.Size = new Size(466, 60);
                    NameOrCompanyNameTextBox.Location = new Point(240, 205);
                    OccupationOrShopNameLabel.Location = new Point(720, 210);
                    OccupationOrShopNameTextBox.Location = new Point(920, 205);
                    AddressOrClientStaffNameLabel.Location = new Point(1300, 210);
                    AddressOrClientStaffNameTextBox.Location = new Point(1540, 205);
                    BirthdayLabel.Visible = false;
                    BirthdayTextBox.Visible = false;

                    while (reader.Read())
                    {
                        NameOrCompanyNameTextBox.Text = reader["company_name"].ToString();
                        OccupationOrShopNameTextBox.Text = reader["shop_name"].ToString();
                        AddressOrClientStaffNameTextBox.Text = reader["staff_name"].ToString();
                        AssessmentDateTextBox.Text = reader["assessment_date"].ToString();
                    }
                }
                else if (type == 1)
                {
                    while (reader.Read())
                    {
                        NameOrCompanyNameTextBox.Text = reader["name"].ToString();
                        OccupationOrShopNameTextBox.Text = reader["occupation"].ToString();
                        AddressOrClientStaffNameTextBox.Text = reader["address"].ToString();
                        Birthday = reader["birthday"].ToString();
                        BirthdayDate = DateTime.Parse(Birthday);
                        BirthdayTextBox.Text = BirthdayDate.ToString("yyyy年MM月dd日");
                        AssessmentDateTextBox.Text = reader["assessment_date"].ToString();
                    }
                }
            }

            #endregion

            #region"表のデータ"

            //伝票番号から表の行番号を取得
            string Sql_Str = "select * from statement_calc_data where document_number = '" + SlipNumber + "' order by record_number;";
            //伝票番号と表の行番号から表のデータを取得し、大分類マスタと品名マスタを結合
            string Sql_Str1 = "select * from statement_calc_data inner join main_category_m on statement_calc_data.main_category_code = main_category_m.main_category_code inner join item_m on statement_calc_data.item_code = item_m.item_code where document_number = '" + SlipNumber + "' order by  record_number;";

            cmd = new NpgsqlCommand(Sql_Str, conn);
            adapter = new NpgsqlDataAdapter(Sql_Str1, conn);
            adapter.Fill(dataTable);

            #region"計算書で登録した表の行数を取得"
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    record = (int)reader["record_number"];
                }
            }
            #endregion

            #region"計算書で登録したデータを表示"
            for (int i = 0; i < record; i++)
            {
                DataRow row = dataTable.Rows[i];
                switch (i)
                {
                    case 0:
                        itemMainCategoryTextBox1.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox1.Text = row["item_name"].ToString();
                        itemDetailTextBox1.Text = row["detail"].ToString();
                        weightTextBox1.Text = row["weight"].ToString();
                        unitPriceText1.Text = row["unit_price"].ToString();
                        countTextBox1.Text = row["count"].ToString();
                        purchaseTextBox1.Text = row["amount"].ToString();
                        remark1.Text = row["remarks"].ToString();
                        MainCategoryCode1 = (int)row["main_category_code"];
                        ItemCategoryCode1 = (int)row["item_code"];
                        break;
                    case 1:
                        itemMainCategoryTextBox2.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox2.Text = row["item_name"].ToString();
                        itemDetailTextBox2.Text = row["detail"].ToString();
                        weightTextBox2.Text = row["weight"].ToString();
                        unitPriceText2.Text = row["unit_price"].ToString();
                        countTextBox2.Text = row["count"].ToString();
                        purchaseTextBox2.Text = row["amount"].ToString();
                        remark2.Text = row["remarks"].ToString();
                        MainCategoryCode2 = (int)row["main_category_code"];
                        ItemCategoryCode2 = (int)row["item_code"];
                        break;
                    case 2:
                        itemMainCategoryTextBox3.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox3.Text = row["item_name"].ToString();
                        itemDetailTextBox3.Text = row["detail"].ToString();
                        weightTextBox3.Text = row["weight"].ToString();
                        unitPriceText3.Text = row["unit_price"].ToString();
                        countTextBox3.Text = row["count"].ToString();
                        purchaseTextBox3.Text = row["amount"].ToString();
                        remark3.Text = row["remarks"].ToString();
                        MainCategoryCode3 = (int)row["main_category_code"];
                        ItemCategoryCode3 = (int)row["item_code"];
                        break;
                    case 3:
                        itemMainCategoryTextBox4.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox4.Text = row["item_name"].ToString();
                        itemDetailTextBox4.Text = row["detail"].ToString();
                        weightTextBox4.Text = row["weight"].ToString();
                        unitPriceText4.Text = row["unit_price"].ToString();
                        countTextBox4.Text = row["count"].ToString();
                        purchaseTextBox4.Text = row["amount"].ToString();
                        remark4.Text = row["remarks"].ToString();
                        MainCategoryCode4 = (int)row["main_category_code"];
                        ItemCategoryCode4 = (int)row["item_code"];
                        break;
                    case 4:
                        itemMainCategoryTextBox5.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox5.Text = row["item_name"].ToString();
                        itemDetailTextBox5.Text = row["detail"].ToString();
                        weightTextBox5.Text = row["weight"].ToString();
                        unitPriceText5.Text = row["unit_price"].ToString();
                        countTextBox5.Text = row["count"].ToString();
                        purchaseTextBox5.Text = row["amount"].ToString();
                        remark5.Text = row["remarks"].ToString();
                        MainCategoryCode5 = (int)row["main_category_code"];
                        ItemCategoryCode5 = (int)row["item_code"];
                        break;
                    case 5:
                        itemMainCategoryTextBox6.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox6.Text = row["item_name"].ToString();
                        itemDetailTextBox6.Text = row["detail"].ToString();
                        weightTextBox6.Text = row["weight"].ToString();
                        unitPriceText6.Text = row["unit_price"].ToString();
                        countTextBox6.Text = row["count"].ToString();
                        purchaseTextBox6.Text = row["amount"].ToString();
                        remark6.Text = row["remarks"].ToString();
                        MainCategoryCode6 = (int)row["main_category_code"];
                        ItemCategoryCode6 = (int)row["item_code"];
                        break;
                    case 6:
                        itemMainCategoryTextBox7.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox7.Text = row["item_name"].ToString();
                        itemDetailTextBox7.Text = row["detail"].ToString();
                        weightTextBox7.Text = row["weight"].ToString();
                        unitPriceText7.Text = row["unit_price"].ToString();
                        countTextBox7.Text = row["count"].ToString();
                        purchaseTextBox7.Text = row["amount"].ToString();
                        remark7.Text = row["remarks"].ToString();
                        MainCategoryCode7 = (int)row["main_category_code"];
                        ItemCategoryCode7 = (int)row["item_code"];
                        break;
                    case 7:
                        itemMainCategoryTextBox8.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox8.Text = row["item_name"].ToString();
                        itemDetailTextBox8.Text = row["detail"].ToString();
                        weightTextBox8.Text = row["weight"].ToString();
                        unitPriceText8.Text = row["unit_price"].ToString();
                        countTextBox8.Text = row["count"].ToString();
                        purchaseTextBox8.Text = row["amount"].ToString();
                        remark8.Text = row["remarks"].ToString();
                        MainCategoryCode8 = (int)row["main_category_code"];
                        ItemCategoryCode8 = (int)row["item_code"];
                        break;
                    case 8:
                        itemMainCategoryTextBox9.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox9.Text = row["item_name"].ToString();
                        itemDetailTextBox9.Text = row["detail"].ToString();
                        weightTextBox9.Text = row["weight"].ToString();
                        unitPriceText9.Text = row["unit_price"].ToString();
                        countTextBox9.Text = row["count"].ToString();
                        purchaseTextBox9.Text = row["amount"].ToString();
                        remark9.Text = row["remarks"].ToString();
                        MainCategoryCode9 = (int)row["main_category_code"];
                        ItemCategoryCode9 = (int)row["item_code"];
                        break;
                    case 9:
                        itemMainCategoryTextBox10.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox10.Text = row["item_name"].ToString();
                        itemDetailTextBox10.Text = row["detail"].ToString();
                        weightTextBox10.Text = row["weight"].ToString();
                        unitPriceText10.Text = row["unit_price"].ToString();
                        countTextBox10.Text = row["count"].ToString();
                        purchaseTextBox10.Text = row["amount"].ToString();
                        remark10.Text = row["remarks"].ToString();
                        MainCategoryCode10 = (int)row["main_category_code"];
                        ItemCategoryCode10 = (int)row["item_code"];
                        break;
                    case 10:
                        itemMainCategoryTextBox11.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox11.Text = row["item_name"].ToString();
                        itemDetailTextBox11.Text = row["detail"].ToString();
                        weightTextBox11.Text = row["weight"].ToString();
                        unitPriceText11.Text = row["unit_price"].ToString();
                        countTextBox11.Text = row["count"].ToString();
                        purchaseTextBox11.Text = row["amount"].ToString();
                        remark11.Text = row["remarks"].ToString();
                        MainCategoryCode11 = (int)row["main_category_code"];
                        ItemCategoryCode11 = (int)row["item_code"];
                        break;
                    case 11:
                        itemMainCategoryTextBox12.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox12.Text = row["item_name"].ToString();
                        itemDetailTextBox12.Text = row["detail"].ToString();
                        weightTextBox12.Text = row["weight"].ToString();
                        unitPriceText12.Text = row["unit_price"].ToString();
                        countTextBox12.Text = row["count"].ToString();
                        purchaseTextBox12.Text = row["amount"].ToString();
                        remark12.Text = row["remarks"].ToString();
                        MainCategoryCode12 = (int)row["main_category_code"];
                        ItemCategoryCode12 = (int)row["item_code"];
                        break;
                    case 12:
                        itemMainCategoryTextBox13.Text = row["main_category_name"].ToString();
                        itemCategoryTextBox13.Text = row["item_name"].ToString();
                        itemDetailTextBox13.Text = row["detail"].ToString();
                        weightTextBox13.Text = row["weight"].ToString();
                        unitPriceText13.Text = row["unit_price"].ToString();
                        countTextBox13.Text = row["count"].ToString();
                        purchaseTextBox13.Text = row["amount"].ToString();
                        remark13.Text = row["remarks"].ToString();
                        MainCategoryCode13 = (int)row["main_category_code"];
                        ItemCategoryCode13 = (int)row["item_code"];
                        break;
                    default:
                        break;
                }
            }
            #endregion

            #region"datetimepicker"
            date1 = null;
            setDateTimePicker1(date1);
            date2 = null;
            setDateTimePicker2(date2);
            date3 = null;
            setDateTimePicker3(date3);
            date4 = null;
            setDateTimePicker4(date4);
            date5 = null;
            setDateTimePicker5(date5);
            date6 = null;
            setDateTimePicker6(date6);
            date7 = null;
            setDateTimePicker7(date7);
            date8 = null;
            setDateTimePicker8(date8);
            date9 = null;
            setDateTimePicker9(date9);
            date10 = null;
            setDateTimePicker10(date10);
            date11 = null;
            setDateTimePicker11(date11);
            date12 = null;
            setDateTimePicker12(date12);
            date13 = null;
            setDateTimePicker13(date13);
            #endregion

            #region"大分類ごとの買取金額"

            if (record >= 1)
            {
                switch (MainCategoryCode1)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat1;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat1;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat1;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat1;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat1;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 2)
            {
                switch (MainCategoryCode2)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat2 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat2 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat2 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat2 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat2 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 3)
            {
                switch (MainCategoryCode3)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat3 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat3 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat3 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat3 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat3 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 4)
            {
                switch (MainCategoryCode4)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat4 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat4 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat4 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat4 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat4 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 5)
            {
                switch (MainCategoryCode5)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat5 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat5 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat5 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat5 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat5 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 6)
            {
                switch (MainCategoryCode6)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat6 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat6 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat6 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat6 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat6 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 7)
            {
                switch (MainCategoryCode7)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat7 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat7 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat7 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat7 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat7 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 8)
            {
                switch (MainCategoryCode8)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat8 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat8 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat8 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat8 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat8 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 9)
            {
                switch (MainCategoryCode9)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat9 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat9 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat9 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat9 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat9 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 10)
            {
                switch (MainCategoryCode10)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat10 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat10 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat10 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat10 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat10 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 11)
            {
                switch (MainCategoryCode11)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat11 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat11 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat11 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat11 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat11 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 12)
            {
                switch (MainCategoryCode12)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat12 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat12 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat12 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat12 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat12 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }

            if (record >= 13)
            {
                switch (MainCategoryCode13)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat13 + PurChaseMetal;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat13 + PurChaseDiamond;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat13 + PurChaseBrand;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat13 + PurChaseProduct;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat13 + PurChaseOther;
                        PurChase = PurChaseMetal + PurChaseDiamond + PurChaseBrand + PurChaseProduct + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }
            #endregion

            #region"計算書以外から成績入力に画面遷移したとき"
            if (grade != 0)
            {
                string Sql_Str2 = "select * from list_result2 inner join main_category_m on list_result2.main_category_code = main_category_m.main_category_code inner join item_m on list_result2.item_code = item_m.item_code where grade_number = '" + grade + "' order by record_number;";
                adapter = new NpgsqlDataAdapter(Sql_Str2, conn);
                adapter.Fill(data);

                for (int i = 0; i < record; i++)
                {
                    DataRow row = data.Rows[i];
                    switch (i)
                    {
                        #region"１行目"
                        case 0:
                            itemMainCategoryTextBox1.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox1.Text = row["item_name"].ToString();
                            remark1.Text = row["remarks"].ToString();
                            MainCategoryCode1 = (int)row["main_category_code"];
                            ItemCategoryCode1 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox1.Text = row["wholesale_price"].ToString();
                                BuyerTextBox1.Text = row["buyer"].ToString();
                                BuyDateTimePicker1.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox1_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox1.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox1.Checked = true;
                            }
                            break;
                        #endregion
                        #region"２行目"
                        case 1:
                            itemMainCategoryTextBox2.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox2.Text = row["item_name"].ToString();
                            remark2.Text = row["remarks"].ToString();
                            MainCategoryCode2 = (int)row["main_category_code"];
                            ItemCategoryCode2 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox2.Text = row["wholesale_price"].ToString();
                                BuyerTextBox2.Text = row["buyer"].ToString();
                                BuyDateTimePicker2.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox2_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox2.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox2.Checked = true;
                            }
                            break;
                        #endregion
                        #region"３行目"
                        case 2:
                            itemMainCategoryTextBox3.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox3.Text = row["item_name"].ToString();
                            remark3.Text = row["remarks"].ToString();
                            MainCategoryCode3 = (int)row["main_category_code"];
                            ItemCategoryCode3 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox3.Text = row["wholesale_price"].ToString();
                                BuyerTextBox3.Text = row["buyer"].ToString();
                                BuyDateTimePicker3.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox3_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox3.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox3.Checked = true;
                            }
                            break;
                        #endregion
                        #region"４行目"
                        case 3:
                            itemMainCategoryTextBox4.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox4.Text = row["item_name"].ToString();
                            remark4.Text = row["remarks"].ToString();
                            MainCategoryCode4 = (int)row["main_category_code"];
                            ItemCategoryCode4 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox4.Text = row["wholesale_price"].ToString();
                                BuyerTextBox4.Text = row["buyer"].ToString();
                                BuyDateTimePicker4.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox4_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox4.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox4.Checked = true;
                            }
                            break;
                        #endregion
                        #region"５行目"
                        case 4:
                            itemMainCategoryTextBox5.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox5.Text = row["item_name"].ToString();
                            remark5.Text = row["remarks"].ToString();
                            MainCategoryCode5 = (int)row["main_category_code"];
                            ItemCategoryCode5 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox5.Text = row["wholesale_price"].ToString();
                                BuyerTextBox5.Text = row["buyer"].ToString();
                                BuyDateTimePicker5.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox5_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox5.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox5.Checked = true;
                            }
                            break;
                        #endregion
                        #region"６行目"
                        case 5:
                            itemMainCategoryTextBox6.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox6.Text = row["item_name"].ToString();
                            remark6.Text = row["remarks"].ToString();
                            MainCategoryCode6 = (int)row["main_category_code"];
                            ItemCategoryCode6 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox6.Text = row["wholesale_price"].ToString();
                                BuyerTextBox6.Text = row["buyer"].ToString();
                                BuyDateTimePicker6.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox6_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox6.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox6.Checked = true;
                            }
                            break;
                        #endregion
                        #region"７行目"
                        case 6:
                            itemMainCategoryTextBox7.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox7.Text = row["item_name"].ToString();
                            remark7.Text = row["remarks"].ToString();
                            MainCategoryCode7 = (int)row["main_category_code"];
                            ItemCategoryCode7 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox7.Text = row["wholesale_price"].ToString();
                                BuyerTextBox7.Text = row["buyer"].ToString();
                                BuyDateTimePicker7.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox7_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox7.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox7.Checked = true;
                            }
                            break;
                        #endregion
                        #region"８行目"
                        case 7:
                            itemMainCategoryTextBox8.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox8.Text = row["item_name"].ToString();
                            remark8.Text = row["remarks"].ToString();
                            MainCategoryCode8 = (int)row["main_category_code"];
                            ItemCategoryCode8 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox8.Text = row["wholesale_price"].ToString();
                                BuyerTextBox8.Text = row["buyer"].ToString();
                                BuyDateTimePicker8.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox8_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox8.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox8.Checked = true;
                            }
                            break;
                        #endregion
                        #region"９行目"
                        case 8:
                            itemMainCategoryTextBox9.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox9.Text = row["item_name"].ToString();
                            remark9.Text = row["remarks"].ToString();
                            MainCategoryCode9 = (int)row["main_category_code"];
                            ItemCategoryCode9 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox9.Text = row["wholesale_price"].ToString();
                                BuyerTextBox9.Text = row["buyer"].ToString();
                                BuyDateTimePicker9.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox9_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox9.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox9.Checked = true;
                            }
                            break;
                        #endregion
                        #region"１０行目"
                        case 9:
                            itemMainCategoryTextBox10.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox10.Text = row["item_name"].ToString();
                            remark10.Text = row["remarks"].ToString();
                            MainCategoryCode10 = (int)row["main_category_code"];
                            ItemCategoryCode10 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox10.Text = row["wholesale_price"].ToString();
                                BuyerTextBox10.Text = row["buyer"].ToString();
                                BuyDateTimePicker10.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox10_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox10.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox10.Checked = true;
                            }
                            break;
                        #endregion
                        #region"１１行目"
                        case 10:
                            itemMainCategoryTextBox11.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox11.Text = row["item_name"].ToString();
                            remark11.Text = row["remarks"].ToString();
                            MainCategoryCode11 = (int)row["main_category_code"];
                            ItemCategoryCode11 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox11.Text = row["wholesale_price"].ToString();
                                BuyerTextBox11.Text = row["buyer"].ToString();
                                BuyDateTimePicker11.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox11_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox11.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox11.Checked = true;
                            }
                            break;
                        #endregion
                        #region"１２行目"
                        case 11:
                            itemMainCategoryTextBox12.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox12.Text = row["item_name"].ToString();
                            remark12.Text = row["remarks"].ToString();
                            MainCategoryCode12 = (int)row["main_category_code"];
                            ItemCategoryCode12 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox12.Text = row["wholesale_price"].ToString();
                                BuyerTextBox12.Text = row["buyer"].ToString();
                                BuyDateTimePicker12.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox12_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox12.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox12.Checked = true;
                            }
                            break;
                        #endregion
                        #region"１３行目"
                        case 12:
                            itemMainCategoryTextBox13.Text = row["main_category_name"].ToString();
                            itemCategoryTextBox13.Text = row["item_name"].ToString();
                            remark13.Text = row["remarks"].ToString();
                            MainCategoryCode13 = (int)row["main_category_code"];
                            ItemCategoryCode13 = (int)row["item_code"];
                            //卸値。売却先、売却日
                            if ((decimal)row["wholesale_price"] != 0)
                            {
                                WholesalePriceTextBox13.Text = row["wholesale_price"].ToString();
                                BuyerTextBox13.Text = row["buyer"].ToString();
                                BuyDateTimePicker13.Value = DateTime.Parse(row["sale_date"].ToString());
                                WholesalePriceTextBox13_Leave(sender, e);
                            }
                            if ((int)row["carry_over_month"] == 1)
                            {
                                NextMonthCheckBox13.Checked = true;
                            }
                            if ((int)row["item_name_change"] == 1)
                            {
                                ItemNameChangeCheckBox13.Checked = true;
                            }
                            break;
                            #endregion
                    }
                }
            }
            #endregion

            #endregion

            conn.Close();
            #region"色"
            #region"1"
            itemMainCategoryTextBox1.BackColor = SystemColors.Control;
            itemCategoryTextBox1.BackColor = SystemColors.Control;
            itemDetailTextBox1.BackColor = SystemColors.Control;
            weightTextBox1.BackColor = SystemColors.Control;
            unitPriceText1.BackColor = SystemColors.Control;
            countTextBox1.BackColor = SystemColors.Control;
            purchaseTextBox1.BackColor = SystemColors.Control;
            #endregion
            #region"2"
            itemMainCategoryTextBox2.BackColor = SystemColors.Control;
            itemCategoryTextBox2.BackColor = SystemColors.Control;
            itemDetailTextBox2.BackColor = SystemColors.Control;
            weightTextBox2.BackColor = SystemColors.Control;
            unitPriceText2.BackColor = SystemColors.Control;
            countTextBox2.BackColor = SystemColors.Control;
            purchaseTextBox2.BackColor = SystemColors.Control;
            #endregion
            #region"3"
            itemMainCategoryTextBox3.BackColor = SystemColors.Control;
            itemCategoryTextBox3.BackColor = SystemColors.Control;
            itemDetailTextBox3.BackColor = SystemColors.Control;
            weightTextBox3.BackColor = SystemColors.Control;
            unitPriceText3.BackColor = SystemColors.Control;
            countTextBox3.BackColor = SystemColors.Control;
            purchaseTextBox3.BackColor = SystemColors.Control;
            #endregion
            #region"4"
            itemMainCategoryTextBox4.BackColor = SystemColors.Control;
            itemCategoryTextBox4.BackColor = SystemColors.Control;
            itemDetailTextBox4.BackColor = SystemColors.Control;
            weightTextBox4.BackColor = SystemColors.Control;
            unitPriceText4.BackColor = SystemColors.Control;
            countTextBox4.BackColor = SystemColors.Control;
            purchaseTextBox4.BackColor = SystemColors.Control;
            #endregion
            #region"5"
            itemMainCategoryTextBox5.BackColor = SystemColors.Control;
            itemCategoryTextBox5.BackColor = SystemColors.Control;
            itemDetailTextBox5.BackColor = SystemColors.Control;
            weightTextBox5.BackColor = SystemColors.Control;
            unitPriceText5.BackColor = SystemColors.Control;
            countTextBox5.BackColor = SystemColors.Control;
            purchaseTextBox5.BackColor = SystemColors.Control;
            #endregion
            #region"6"
            itemMainCategoryTextBox6.BackColor = SystemColors.Control;
            itemCategoryTextBox6.BackColor = SystemColors.Control;
            itemDetailTextBox6.BackColor = SystemColors.Control;
            weightTextBox6.BackColor = SystemColors.Control;
            unitPriceText6.BackColor = SystemColors.Control;
            countTextBox6.BackColor = SystemColors.Control;
            purchaseTextBox6.BackColor = SystemColors.Control;
            #endregion
            #region"7"
            itemMainCategoryTextBox7.BackColor = SystemColors.Control;
            itemCategoryTextBox7.BackColor = SystemColors.Control;
            itemDetailTextBox7.BackColor = SystemColors.Control;
            weightTextBox7.BackColor = SystemColors.Control;
            unitPriceText7.BackColor = SystemColors.Control;
            countTextBox7.BackColor = SystemColors.Control;
            purchaseTextBox7.BackColor = SystemColors.Control;
            #endregion
            #region"8"
            itemMainCategoryTextBox8.BackColor = SystemColors.Control;
            itemCategoryTextBox8.BackColor = SystemColors.Control;
            itemDetailTextBox8.BackColor = SystemColors.Control;
            weightTextBox8.BackColor = SystemColors.Control;
            unitPriceText8.BackColor = SystemColors.Control;
            countTextBox8.BackColor = SystemColors.Control;
            purchaseTextBox8.BackColor = SystemColors.Control;
            #endregion
            #region"9"
            itemMainCategoryTextBox9.BackColor = SystemColors.Control;
            itemCategoryTextBox9.BackColor = SystemColors.Control;
            itemDetailTextBox9.BackColor = SystemColors.Control;
            weightTextBox9.BackColor = SystemColors.Control;
            unitPriceText9.BackColor = SystemColors.Control;
            countTextBox9.BackColor = SystemColors.Control;
            purchaseTextBox9.BackColor = SystemColors.Control;
            #endregion
            #region"10"
            itemMainCategoryTextBox10.BackColor = SystemColors.Control;
            itemCategoryTextBox10.BackColor = SystemColors.Control;
            itemDetailTextBox10.BackColor = SystemColors.Control;
            weightTextBox10.BackColor = SystemColors.Control;
            unitPriceText10.BackColor = SystemColors.Control;
            countTextBox10.BackColor = SystemColors.Control;
            purchaseTextBox10.BackColor = SystemColors.Control;
            #endregion
            #region"11"
            itemMainCategoryTextBox11.BackColor = SystemColors.Control;
            itemCategoryTextBox11.BackColor = SystemColors.Control;
            itemDetailTextBox11.BackColor = SystemColors.Control;
            weightTextBox11.BackColor = SystemColors.Control;
            unitPriceText11.BackColor = SystemColors.Control;
            countTextBox11.BackColor = SystemColors.Control;
            purchaseTextBox11.BackColor = SystemColors.Control;
            #endregion
            #region"12"
            itemMainCategoryTextBox12.BackColor = SystemColors.Control;
            itemCategoryTextBox12.BackColor = SystemColors.Control;
            itemDetailTextBox12.BackColor = SystemColors.Control;
            weightTextBox12.BackColor = SystemColors.Control;
            unitPriceText12.BackColor = SystemColors.Control;
            countTextBox12.BackColor = SystemColors.Control;
            purchaseTextBox12.BackColor = SystemColors.Control;
            #endregion
            #region"13"
            itemMainCategoryTextBox13.BackColor = SystemColors.Control;
            itemCategoryTextBox13.BackColor = SystemColors.Control;
            itemDetailTextBox13.BackColor = SystemColors.Control;
            weightTextBox13.BackColor = SystemColors.Control;
            unitPriceText13.BackColor = SystemColors.Control;
            countTextBox13.BackColor = SystemColors.Control;
            purchaseTextBox13.BackColor = SystemColors.Control;
            #endregion
            #region"下の表"
            MetalPurchaseTextBox.BackColor = SystemColors.Control;
            MetalWholesaleTextBox.BackColor = SystemColors.Control;
            MetalProfitTextBox.BackColor = SystemColors.Control;
            DiamondPurchaseTextBox.BackColor = SystemColors.Control;
            DiamondWholesaleTextBox.BackColor = SystemColors.Control;
            DiamondProfitTextBox.BackColor = SystemColors.Control;
            BrandPurchaseTextBox.BackColor = SystemColors.Control;
            BrandWholesaleTextBox.BackColor = SystemColors.Control;
            BrandProfitTextBox.BackColor = SystemColors.Control;
            ProductPurchaseTextBox.BackColor = SystemColors.Control;
            ProductWholesaleTextBox.BackColor = SystemColors.Control;
            ProductProfitTextBox.BackColor = SystemColors.Control;
            OtherPurchaseTextBox.BackColor = SystemColors.Control;
            OtherWholesaleTextBox.BackColor = SystemColors.Control;
            OtherProfitTextBox.BackColor = SystemColors.Control;
            PurchaseTotalTextBox.BackColor = SystemColors.Control;
            WholesaleTotalTextBox.BackColor = SystemColors.Control;
            ProfitTotalTextBox.BackColor = SystemColors.Control;
            #endregion
            #endregion
        }

        #region"成績入力画面から計算書 or 次月持ち越しへ"
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            //品名変更時、再登録をまだしていない場合
            if (NameChange)
            {
                MessageBox.Show("再登録ボタンをクリックして登録をしてください。", "再登録を行う必要があります", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
           
        }

        private void RecordList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DeliveryButton)
            {
                Statement statement = new Statement(mainmenu, staff_id, type, staff_name, address, Access_auth, total, Pass, SlipNumber, Control, Data, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, name1, phoneNumber1, addresskana1, code1, item1, date01, date02, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
                statement.Show();
            }
            //次月持ち越しから画面遷移したとき
            else if (CarryOver && screan) 
            {
                NextMonth next = new NextMonth(mainmenu, staff_id, Pass, Access_auth);
                next.Show();
            }
            //計算書から画面遷移してお客様情報・月間成績一覧・品名変更画面に画面遷移しないとき
            else if (screan)
            {
                Statement statement = new Statement(mainmenu, staff_id, type, staff_name, address, Access_auth, total, Pass, SlipNumber, Control, Data, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, name1, phoneNumber1, addresskana1, code1, item1, date01, date02, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
                statement.Show();
            }
            //計算書から画面遷移してお客様情報・月間成績一覧・品名変更画面に画面遷移したとき
            else
            {
                screan = true;
            }
        }

        #endregion

        #region"下の表"

        #region"上の表で卸値入力 -> 左下の表（大分類毎と合計）と右下の表の卸値記入（￥マーク設定済み）Leaveイベント"
        private void WholesalePriceTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
            {
                WholesalePriceTextBox1.Text = 0.ToString();
            }

            switch (MainCategoryCode1)
            {
                case 100:
                    if (ReEnter1)
                    {
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleMetal = WholeSaleUnFormat1 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter1 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox1.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox1.Text = WholesalePriceTextBox1.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
                            {
                                WholesalePriceTextBox1.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat1;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat1;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleMetal = WholeSaleUnFormat1 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter1)
                    {
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleDiamond = WholeSaleUnFormat1 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter1 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox1.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox1.Text = WholesalePriceTextBox1.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
                            {
                                WholesalePriceTextBox1.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat1;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat1;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleDiamond = WholeSaleUnFormat1 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter1)
                    {
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleBrand = WholeSaleUnFormat1 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter1 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox1.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox1.Text = WholesalePriceTextBox1.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
                            {
                                WholesalePriceTextBox1.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat1;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat1;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleBrand = WholeSaleUnFormat1 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter1)
                    {
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleProduct = WholeSaleUnFormat1 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                        //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter1 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox1.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox1.Text = WholesalePriceTextBox1.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
                            {
                                WholesalePriceTextBox1.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat1;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat1;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleProduct = WholeSaleUnFormat1 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter1)
                    {
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleOther = WholeSaleUnFormat1 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter1 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox1.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox1.Text = WholesalePriceTextBox1.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
                            {
                                WholesalePriceTextBox1.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat1;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat1;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleOther = WholeSaleUnFormat1 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat1 != 0)
            {
                BuyDateTimePicker1.Enabled = true;
            }

        }
        private void WholesalePriceTextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
            {
                WholesalePriceTextBox2.Text = 0.ToString();
            }

            switch (MainCategoryCode2)
            {
                case 100:
                    if (ReEnter2)
                    {
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleMetal = WholeSaleUnFormat2 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter2 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox2.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox2.Text = WholesalePriceTextBox2.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                            {
                                WholesalePriceTextBox2.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat2;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat2;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleMetal = WholeSaleUnFormat2 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter2)
                    {
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleDiamond = WholeSaleUnFormat2 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter2 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox2.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox2.Text = WholesalePriceTextBox2.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                            {
                                WholesalePriceTextBox2.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat2;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat2;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleDiamond = WholeSaleUnFormat2 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter2)
                    {
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleBrand = WholeSaleUnFormat2 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter2 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox2.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox2.Text = WholesalePriceTextBox2.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                            {
                                WholesalePriceTextBox2.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat2;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat2;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleBrand = WholeSaleUnFormat2 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter2)
                    {
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleProduct = WholeSaleUnFormat2 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter2 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox2.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox2.Text = WholesalePriceTextBox2.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                            {
                                WholesalePriceTextBox2.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat2;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat2;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleProduct = WholeSaleUnFormat2 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter2)
                    {
                        WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                        WholeSaleOther = WholeSaleUnFormat2 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter2 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox1.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox1.Text = WholesalePriceTextBox1.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                            {
                                WholesalePriceTextBox2.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat1;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat1;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                        WholeSaleOther = WholeSaleUnFormat1 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat2 != 0)
            {
                BuyDateTimePicker2.Enabled = true;
            }
        }
        private void WholesalePriceTextBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
            {
                WholesalePriceTextBox3.Text = 0.ToString();
            }

            switch (MainCategoryCode3)
            {
                case 100:
                    if (ReEnter3)
                    {
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleMetal = WholeSaleUnFormat3 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter3 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox3.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox3.Text = WholesalePriceTextBox3.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                            {
                                WholesalePriceTextBox3.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat3;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat3;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleMetal = WholeSaleUnFormat3 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter3)
                    {
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleDiamond = WholeSaleUnFormat3 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter3 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox3.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox3.Text = WholesalePriceTextBox3.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                            {
                                WholesalePriceTextBox3.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat3;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat3;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleDiamond = WholeSaleUnFormat3 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter3)
                    {
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleBrand = WholeSaleUnFormat3 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter3 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox3.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox3.Text = WholesalePriceTextBox3.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                            {
                                WholesalePriceTextBox3.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat3;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat3;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleBrand = WholeSaleUnFormat3 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter3)
                    {
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleProduct = WholeSaleUnFormat3 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter3 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox3.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox3.Text = WholesalePriceTextBox3.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                            {
                                WholesalePriceTextBox3.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat3;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat3;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleProduct = WholeSaleUnFormat3 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter3)
                    {
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleOther = WholeSaleUnFormat3 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter3 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox3.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox3.Text = WholesalePriceTextBox3.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                            {
                                WholesalePriceTextBox3.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat3;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat3;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                        WholeSaleOther = WholeSaleUnFormat3 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat3 != 0)
            {
                BuyDateTimePicker3.Enabled = true;
            }
        }
        private void WholesalePriceTextBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
            {
                WholesalePriceTextBox4.Text = 0.ToString();
            }

            switch (MainCategoryCode4)
            {
                case 100:
                    if (ReEnter4)
                    {
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleMetal = WholeSaleUnFormat4 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter4 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox4.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox4.Text = WholesalePriceTextBox4.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                            {
                                WholesalePriceTextBox4.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat4;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat4;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleMetal = WholeSaleUnFormat4 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter4)
                    {
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleDiamond = WholeSaleUnFormat4 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter4 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox4.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox4.Text = WholesalePriceTextBox4.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                            {
                                WholesalePriceTextBox4.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat4;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat4;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleDiamond = WholeSaleUnFormat4 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 102:
                    if (ReEnter4)
                    {
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleBrand = WholeSaleUnFormat4 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter4 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox4.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox4.Text = WholesalePriceTextBox4.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                            {
                                WholesalePriceTextBox4.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat4;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat4;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleBrand = WholeSaleUnFormat4 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 103:
                    if (ReEnter4)
                    {
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleProduct = WholeSaleUnFormat4 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter4 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox4.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox4.Text = WholesalePriceTextBox4.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                            {
                                WholesalePriceTextBox4.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat4;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat4;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleProduct = WholeSaleUnFormat4 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 104:
                    if (ReEnter4)
                    {
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleOther = WholeSaleUnFormat4 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter4 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox4.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox4.Text = WholesalePriceTextBox4.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                            {
                                WholesalePriceTextBox4.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat4;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat4;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                        WholeSaleOther = WholeSaleUnFormat4 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
            }

            if (WholeSaleUnFormat4 != 0)
            {
                BuyDateTimePicker4.Enabled = true;
            }
        }
        private void WholesalePriceTextBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
            {
                WholesalePriceTextBox5.Text = 0.ToString();
            }

            switch (MainCategoryCode5)
            {
                case 100:
                    if (ReEnter5)
                    {
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleMetal = WholeSaleUnFormat5 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter5 = false;
                    }
                    else
                    { // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox5.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox5.Text = WholesalePriceTextBox5.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                            {
                                WholesalePriceTextBox5.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat5;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat5;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleMetal = WholeSaleUnFormat5 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 101:
                    if (ReEnter5)
                    {
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleDiamond = WholeSaleUnFormat5 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter5 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox5.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox5.Text = WholesalePriceTextBox5.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                            {
                                WholesalePriceTextBox5.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat5;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat5;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleDiamond = WholeSaleUnFormat5 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 102:
                    if (ReEnter5)
                    {
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleBrand = WholeSaleUnFormat5 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter5 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox5.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox5.Text = WholesalePriceTextBox5.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                            {
                                WholesalePriceTextBox5.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat5;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat5;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleBrand = WholeSaleUnFormat5 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 103:
                    if (ReEnter5)
                    {
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleProduct = WholeSaleUnFormat5 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter5 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox5.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox5.Text = WholesalePriceTextBox5.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                            {
                                WholesalePriceTextBox5.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat5;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat5;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleProduct = WholeSaleUnFormat5 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 104:
                    if (ReEnter5)
                    {
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleOther = WholeSaleUnFormat5 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter5 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox5.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox5.Text = WholesalePriceTextBox5.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                            {
                                WholesalePriceTextBox5.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat5;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat5;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                        WholeSaleOther = WholeSaleUnFormat5 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat5 != 0)
            {
                BuyDateTimePicker5.Enabled = true;
            }
        }
        private void WholesalePriceTextBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
            {
                WholesalePriceTextBox6.Text = 0.ToString();
            }

            switch (MainCategoryCode6)
            {
                case 100:
                    if (ReEnter6)
                    {
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleMetal = WholeSaleUnFormat6 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter6 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox6.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox6.Text = WholesalePriceTextBox6.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                            {
                                WholesalePriceTextBox6.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat6;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat6;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleMetal = WholeSaleUnFormat6 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 101:
                    if (ReEnter6)
                    {
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleDiamond = WholeSaleUnFormat6 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter6 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox6.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox6.Text = WholesalePriceTextBox6.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                            {
                                WholesalePriceTextBox6.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat6;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat6;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleDiamond = WholeSaleUnFormat6 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 102:
                    if (ReEnter6)
                    {
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleBrand = WholeSaleUnFormat6 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter6 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox6.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox6.Text = WholesalePriceTextBox6.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                            {
                                WholesalePriceTextBox6.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat6;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat6;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleBrand = WholeSaleUnFormat6 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 103:
                    if (ReEnter6)
                    {
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleProduct = WholeSaleUnFormat6 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter6 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox6.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox6.Text = WholesalePriceTextBox6.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                            {
                                WholesalePriceTextBox6.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat6;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat6;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleProduct = WholeSaleUnFormat6 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 104:
                    if (ReEnter6)
                    {
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleOther = WholeSaleUnFormat6 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter6 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox6.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox6.Text = WholesalePriceTextBox6.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                            {
                                WholesalePriceTextBox6.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat6;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat6;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                        WholeSaleOther = WholeSaleUnFormat6 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
            }

            if (WholeSaleUnFormat6 != 0)
            {
                BuyDateTimePicker6.Enabled = true;
            }
        }
        private void WholesalePriceTextBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
            {
                WholesalePriceTextBox7.Text = 0.ToString();
            }

            switch (MainCategoryCode7)
            {
                case 100:
                    if (ReEnter7)
                    {
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleMetal = WholeSaleUnFormat7 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter7 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox7.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox7.Text = WholesalePriceTextBox7.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                            {
                                WholesalePriceTextBox7.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat7;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat7;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleMetal = WholeSaleUnFormat7 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter7)
                    {
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleDiamond = WholeSaleUnFormat7 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter7 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox7.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox7.Text = WholesalePriceTextBox7.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                            {
                                WholesalePriceTextBox7.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat7;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat7;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleDiamond = WholeSaleUnFormat7 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter7)
                    {
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleBrand = WholeSaleUnFormat7 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter7 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox7.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox7.Text = WholesalePriceTextBox7.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                            {
                                WholesalePriceTextBox7.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat7;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat7;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleBrand = WholeSaleUnFormat7 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter7)
                    {
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleProduct = WholeSaleUnFormat7 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter7 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox7.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox7.Text = WholesalePriceTextBox7.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                            {
                                WholesalePriceTextBox7.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat7;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat7;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleProduct = WholeSaleUnFormat7 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter7)
                    {
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleOther = WholeSaleUnFormat7 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter7 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox7.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox7.Text = WholesalePriceTextBox7.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                            {
                                WholesalePriceTextBox7.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat7;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat7;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                        WholeSaleOther = WholeSaleUnFormat7 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat7 != 0)
            {
                BuyDateTimePicker7.Enabled = true;
            }
        }
        private void WholesalePriceTextBox8_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
            {
                WholesalePriceTextBox8.Text = 0.ToString();
            }

            switch (MainCategoryCode8)
            {
                case 100:
                    if (ReEnter8)
                    {
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleMetal = WholeSaleUnFormat8 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter8 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox8.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox8.Text = WholesalePriceTextBox8.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                            {
                                WholesalePriceTextBox8.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat8;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat8;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleMetal = WholeSaleUnFormat8 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter8)
                    {
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleDiamond = WholeSaleUnFormat8 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter8 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox8.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox8.Text = WholesalePriceTextBox8.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                            {
                                WholesalePriceTextBox8.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat8;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat8;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleDiamond = WholeSaleUnFormat8 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter8)
                    {
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleBrand = WholeSaleUnFormat8 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter8 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox8.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox8.Text = WholesalePriceTextBox8.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                            {
                                WholesalePriceTextBox8.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat8;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat8;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleBrand = WholeSaleUnFormat8 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter8)
                    {
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleProduct = WholeSaleUnFormat8 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter8 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox8.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox8.Text = WholesalePriceTextBox8.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                            {
                                WholesalePriceTextBox8.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat8;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat8;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleProduct = WholeSaleUnFormat8 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter8)
                    {
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleOther = WholeSaleUnFormat8 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter8 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox8.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox8.Text = WholesalePriceTextBox8.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                            {
                                WholesalePriceTextBox8.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat8;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat8;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                        WholeSaleOther = WholeSaleUnFormat8 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat8 != 0)
            {
                BuyDateTimePicker8.Enabled = true;
            }
        }
        private void WholesalePriceTextBox9_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
            {
                WholesalePriceTextBox9.Text = 0.ToString();
            }

            switch (MainCategoryCode9)
            {
                case 100:
                    if (ReEnter9)
                    {
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleMetal = WholeSaleUnFormat9 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter9 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox9.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox9.Text = WholesalePriceTextBox9.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                            {
                                WholesalePriceTextBox9.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat9;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat9;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleMetal = WholeSaleUnFormat9 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter9)
                    {
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleDiamond = WholeSaleUnFormat9 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter9 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox9.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox9.Text = WholesalePriceTextBox9.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                            {
                                WholesalePriceTextBox9.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat9;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat9;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleDiamond = WholeSaleUnFormat9 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter9)
                    {
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleBrand = WholeSaleUnFormat9 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter9 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox9.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox9.Text = WholesalePriceTextBox9.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                            {
                                WholesalePriceTextBox9.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat9;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat9;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleBrand = WholeSaleUnFormat9 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter9)
                    {
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleProduct = WholeSaleUnFormat9 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter9 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox9.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox9.Text = WholesalePriceTextBox9.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                            {
                                WholesalePriceTextBox9.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat9;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat9;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleProduct = WholeSaleUnFormat9 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter9)
                    {
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleOther = WholeSaleUnFormat9 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter9 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox9.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox9.Text = WholesalePriceTextBox9.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                            {
                                WholesalePriceTextBox9.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat9;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat9;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                        WholeSaleOther = WholeSaleUnFormat9 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat9 != 0)
            {
                BuyDateTimePicker9.Enabled = true;
            }
        }
        private void WholesalePriceTextBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
            {
                WholesalePriceTextBox10.Text = 0.ToString();
            }

            switch (MainCategoryCode10)
            {
                case 100:
                    if (ReEnter10)
                    {
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleMetal = WholeSaleUnFormat10 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter10 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox10.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox10.Text = WholesalePriceTextBox10.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                            {
                                WholesalePriceTextBox10.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat10;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat10;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleMetal = WholeSaleUnFormat10 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter10)
                    {
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleDiamond = WholeSaleUnFormat10 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter10 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox10.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox10.Text = WholesalePriceTextBox10.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                            {
                                WholesalePriceTextBox10.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat10;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat10;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleDiamond = WholeSaleUnFormat10 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter10)
                    {
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleBrand = WholeSaleUnFormat10 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter10 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox10.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox10.Text = WholesalePriceTextBox10.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                            {
                                WholesalePriceTextBox10.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat10;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat10;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleBrand = WholeSaleUnFormat10 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter10)
                    {
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleProduct = WholeSaleUnFormat10 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter10 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox10.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox10.Text = WholesalePriceTextBox10.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                            {
                                WholesalePriceTextBox10.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat10;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat10;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleProduct = WholeSaleUnFormat10 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter10)
                    {
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleOther = WholeSaleUnFormat10 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter10 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox10.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox10.Text = WholesalePriceTextBox10.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                            {
                                WholesalePriceTextBox10.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat10;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat10;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                        WholeSaleOther = WholeSaleUnFormat10 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat10 != 0)
            {
                BuyDateTimePicker10.Enabled = true;
            }
        }
        private void WholesalePriceTextBox11_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
            {
                WholesalePriceTextBox11.Text = 0.ToString();
            }

            switch (MainCategoryCode11)
            {
                case 100:
                    if (ReEnter11)
                    {
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleMetal = WholeSaleUnFormat11 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter11 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox11.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox11.Text = WholesalePriceTextBox11.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                            {
                                WholesalePriceTextBox11.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat11;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat11;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleMetal = WholeSaleUnFormat11 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                        break;
                case 101:
                    if (ReEnter11)
                    {
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleDiamond = WholeSaleUnFormat11 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter11 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox11.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox11.Text = WholesalePriceTextBox11.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                            {
                                WholesalePriceTextBox11.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat11;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat11;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleDiamond = WholeSaleUnFormat11 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter11)
                    {
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleBrand = WholeSaleUnFormat11 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter11 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox11.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox11.Text = WholesalePriceTextBox11.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                            {
                                WholesalePriceTextBox11.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat11;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat11;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleBrand = WholeSaleUnFormat11 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter11)
                    {
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleProduct = WholeSaleUnFormat11 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter11 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox11.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox11.Text = WholesalePriceTextBox11.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                            {
                                WholesalePriceTextBox11.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat11;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat11;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleProduct = WholeSaleUnFormat11 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter11) {
                    WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                    WholeSaleOther = WholeSaleUnFormat11 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter11 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox11.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox11.Text = WholesalePriceTextBox11.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                            {
                                WholesalePriceTextBox11.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat11;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat11;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                        WholeSaleOther = WholeSaleUnFormat11 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat11 != 0)
            {
                BuyDateTimePicker11.Enabled = true;
            }
        }
        private void WholesalePriceTextBox12_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
            {
                WholesalePriceTextBox12.Text = 0.ToString();
            }

            switch (MainCategoryCode12)
            {
                case 100:
                    if (ReEnter12)
                    {
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleMetal = WholeSaleUnFormat12 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter12 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox12.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox12.Text = WholesalePriceTextBox12.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                            {
                                WholesalePriceTextBox12.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat12;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat12;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleMetal = WholeSaleUnFormat12 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter12)
                    {
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleDiamond = WholeSaleUnFormat12 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter12 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox12.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox12.Text = WholesalePriceTextBox12.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                            {
                                WholesalePriceTextBox12.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat12;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat12;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleDiamond = WholeSaleUnFormat12 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter12)
                    {
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleBrand = WholeSaleUnFormat12 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter12 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox12.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox12.Text = WholesalePriceTextBox12.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                            {
                                WholesalePriceTextBox12.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat12;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat12;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleBrand = WholeSaleUnFormat12 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter12)
                    {
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleProduct = WholeSaleUnFormat12 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter12 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox12.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox12.Text = WholesalePriceTextBox12.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                            {
                                WholesalePriceTextBox12.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat12;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat12;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleProduct = WholeSaleUnFormat12 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 104:
                    if (ReEnter12)
                    {
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleOther = WholeSaleUnFormat12 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter12 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox12.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox12.Text = WholesalePriceTextBox12.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                            {
                                WholesalePriceTextBox12.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat12;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat12;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                        WholeSaleOther = WholeSaleUnFormat12 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat12 != 0)
            {
                BuyDateTimePicker12.Enabled = true;
            }
        }
        private void WholesalePriceTextBox13_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
            {
                WholesalePriceTextBox13.Text = 0.ToString();
            }

            switch (MainCategoryCode13)
            {
                case 100:
                    if (ReEnter13)
                    {
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleMetal = WholeSaleUnFormat13 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter13 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox13.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox13.Text = WholesalePriceTextBox13.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                            {
                                WholesalePriceTextBox13.Text = 0.ToString();
                            }
                        }
                        WholeSaleMetal = WholeSaleMetal - WholeSaleUnFormat13;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat13;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleMetal = WholeSaleUnFormat13 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 101:
                    if (ReEnter13)
                    {
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleDiamond = WholeSaleUnFormat13 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter13 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox13.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox13.Text = WholesalePriceTextBox13.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                            {
                                WholesalePriceTextBox13.Text = 0.ToString();
                            }
                        }
                        WholeSaleDiamond = WholeSaleDiamond - WholeSaleUnFormat13;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat13;                                         //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleDiamond = WholeSaleUnFormat13 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                         //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 102:
                    if (ReEnter13)
                    {
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleBrand = WholeSaleUnFormat13 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter13 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox13.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox13.Text = WholesalePriceTextBox13.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                            {
                                WholesalePriceTextBox13.Text = 0.ToString();
                            }
                        }
                        WholeSaleBrand = WholeSaleBrand - WholeSaleUnFormat13;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat13;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleBrand = WholeSaleUnFormat13 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
                case 103:
                    if (ReEnter13)
                    {
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleProduct = WholeSaleUnFormat13 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter13 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox13.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox13.Text = WholesalePriceTextBox13.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                            {
                                WholesalePriceTextBox13.Text = 0.ToString();
                            }
                        }
                        WholeSaleProduct = WholeSaleProduct - WholeSaleUnFormat13;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat13;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleProduct = WholeSaleUnFormat13 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    };
                    break;
                case 104:
                    if (ReEnter13)
                    {
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleOther = WholeSaleUnFormat13 + WholeSaleOther;                            //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                        ReEnter13 = false;
                    }
                    else
                    {
                        // \ マークがある場合、\ マーク除外
                        if (WholesalePriceTextBox13.Text.StartsWith(@"\"))
                        {
                            WholesalePriceTextBox13.Text = WholesalePriceTextBox13.Text.Substring(1);
                            if (string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                            {
                                WholesalePriceTextBox13.Text = 0.ToString();
                            }
                        }
                        WholeSaleOther = WholeSaleOther - WholeSaleUnFormat13;                           //以前に入力した卸値を合計から引く
                        WholeSale = WholeSale - WholeSaleUnFormat13;                                     //以前に入力した卸値を合計から引く
                        WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                        WholeSaleOther = WholeSaleUnFormat13 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                        WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                        //￥マーク表示
                        WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                        OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                        WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    }
                    break;
            }

            if (WholeSaleUnFormat13 != 0)
            {
                BuyDateTimePicker13.Enabled = true;
            }
        }
        #endregion

        #region"金額が下の表に表示されたら合計金額表示（￥マーク設定済み）TextChangedイベント"
        private void MetalPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
        }

        private void DiamondPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
        }

        private void BrandPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
        }

        private void ProductPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
        }

        private void OtherPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
        }

        #endregion

        #region"下の表に卸値が表示されたら左下の表に大分類ごとに利益表示（￥マーク設定済み）TextChangedイベント"
        private void MetalWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            ProFitMetal = WholeSaleMetal - PurChaseMetal;
            MetalProfitTextBox.Text = string.Format("{0:C}", ProFitMetal);
        }

        private void DiamondWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            ProFitDiamond = WholeSaleDiamond - PurChaseDiamond;
            DiamondProfitTextBox.Text = string.Format("{0:C}", ProFitDiamond);
        }

        private void BrandWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            ProFitBrand = WholeSaleBrand - PurChaseBrand;
            BrandProfitTextBox.Text = string.Format("{0:C}", ProFitBrand);
        }

        private void ProductWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            ProFitProduct = WholeSaleProduct - PurChaseProduct;
            ProductProfitTextBox.Text = string.Format("{0:C}", ProFitProduct);
        }

        private void OtherWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            ProFitOther = WholeSaleOther - PurChaseOther;
            OtherProfitTextBox.Text = string.Format("{0:C}", ProFitOther);
        }
        #endregion

        #region"左下表と右下表の利益の合計表示（\マーク設定済み）TextChangedイベント"
        private void WholesaleTotalTextBox_TextChanged(object sender, EventArgs e)
        {
            ProFit = WholeSale - PurChase;
            ProfitTotalTextBox.Text = string.Format("{0:C}", ProFit);
        }

        #endregion

        #endregion

        #region"上の表"

        #region"次月持ち越しチェック"
        #region"1"
        private void NextMonthCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //次月・品名両方
            if (NextMonthCheckBox1.Checked && ItemNameChangeCheckBox1.Checked)
            {
                itemMainCategoryTextBox1.ForeColor = Color.Purple;
                itemCategoryTextBox1.ForeColor = Color.Purple;
                itemDetailTextBox1.ForeColor = Color.Purple;
                weightTextBox1.ForeColor = Color.Purple;
                unitPriceText1.ForeColor = Color.Purple;
                countTextBox1.ForeColor = Color.Purple;
                purchaseTextBox1.ForeColor = Color.Purple;
                WholesalePriceTextBox1.ForeColor = Color.Purple;
                remark1.ForeColor = Color.Purple;
                BuyerTextBox1.ForeColor = Color.Purple;
            }
            //品名のみ
            if (!NextMonthCheckBox1.Checked && ItemNameChangeCheckBox1.Checked)
            {
                itemMainCategoryTextBox1.ForeColor = Color.Blue;
                itemCategoryTextBox1.ForeColor = Color.Blue;
                itemDetailTextBox1.ForeColor = Color.Blue;
                weightTextBox1.ForeColor = Color.Blue;
                unitPriceText1.ForeColor = Color.Blue;
                countTextBox1.ForeColor = Color.Blue;
                purchaseTextBox1.ForeColor = Color.Blue;
                WholesalePriceTextBox1.ForeColor = Color.Blue;
                remark1.ForeColor = Color.Blue;
                BuyerTextBox1.ForeColor = Color.Blue;
            }
            //次月のみ
            if (NextMonthCheckBox1.Checked && !ItemNameChangeCheckBox1.Checked)
            {
                itemMainCategoryTextBox1.ForeColor = Color.Red;
                itemCategoryTextBox1.ForeColor = Color.Red;
                itemDetailTextBox1.ForeColor = Color.Red;
                weightTextBox1.ForeColor = Color.Red;
                unitPriceText1.ForeColor = Color.Red;
                countTextBox1.ForeColor = Color.Red;
                purchaseTextBox1.ForeColor = Color.Red;
                WholesalePriceTextBox1.ForeColor = Color.Red;
                remark1.ForeColor = Color.Red;
                BuyerTextBox1.ForeColor = Color.Red;
            }
            //どちらもノーチェック
            if (!NextMonthCheckBox1.Checked && !ItemNameChangeCheckBox1.Checked)
            {
                itemMainCategoryTextBox1.ForeColor = Color.Black;
                itemCategoryTextBox1.ForeColor = Color.Black;
                itemDetailTextBox1.ForeColor = Color.Black;
                weightTextBox1.ForeColor = Color.Black;
                unitPriceText1.ForeColor = Color.Black;
                countTextBox1.ForeColor = Color.Black;
                purchaseTextBox1.ForeColor = Color.Black;
                WholesalePriceTextBox1.ForeColor = Color.Black;
                remark1.ForeColor = Color.Black;
                BuyerTextBox1.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"2"
        private void NextMonthCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox2.Checked && ItemNameChangeCheckBox2.Checked)
            {
                itemMainCategoryTextBox2.ForeColor = Color.Blue;
                itemCategoryTextBox2.ForeColor = Color.Blue;
                itemDetailTextBox2.ForeColor = Color.Blue;
                weightTextBox2.ForeColor = Color.Blue;
                unitPriceText2.ForeColor = Color.Blue;
                countTextBox2.ForeColor = Color.Blue;
                purchaseTextBox2.ForeColor = Color.Blue;
                WholesalePriceTextBox2.ForeColor = Color.Blue;
                remark2.ForeColor = Color.Blue;
                BuyerTextBox2.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox2.Checked && ItemNameChangeCheckBox2.Checked)
            {
                itemMainCategoryTextBox2.ForeColor = Color.Purple;
                itemCategoryTextBox2.ForeColor = Color.Purple;
                itemDetailTextBox2.ForeColor = Color.Purple;
                weightTextBox2.ForeColor = Color.Purple;
                unitPriceText2.ForeColor = Color.Purple;
                countTextBox2.ForeColor = Color.Purple;
                purchaseTextBox2.ForeColor = Color.Purple;
                WholesalePriceTextBox2.ForeColor = Color.Purple;
                remark2.ForeColor = Color.Purple;
                BuyerTextBox2.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox2.Checked && !ItemNameChangeCheckBox2.Checked)
            {
                itemMainCategoryTextBox2.ForeColor = Color.Red;
                itemCategoryTextBox2.ForeColor = Color.Red;
                itemDetailTextBox2.ForeColor = Color.Red;
                weightTextBox2.ForeColor = Color.Red;
                unitPriceText2.ForeColor = Color.Red;
                countTextBox2.ForeColor = Color.Red;
                purchaseTextBox2.ForeColor = Color.Red;
                WholesalePriceTextBox2.ForeColor = Color.Red;
                remark2.ForeColor = Color.Red;
                BuyerTextBox2.ForeColor = Color.Red;
            }
            //両方空
            if (!NextMonthCheckBox2.Checked && !ItemNameChangeCheckBox2.Checked)
            {
                itemMainCategoryTextBox2.ForeColor = Color.Black;
                itemCategoryTextBox2.ForeColor = Color.Black;
                itemDetailTextBox2.ForeColor = Color.Black;
                weightTextBox2.ForeColor = Color.Black;
                unitPriceText2.ForeColor = Color.Black;
                countTextBox2.ForeColor = Color.Black;
                purchaseTextBox2.ForeColor = Color.Black;
                WholesalePriceTextBox2.ForeColor = Color.Black;
                remark2.ForeColor = Color.Black;
                BuyerTextBox2.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"3"
        private void NextMonthCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox3.Checked && ItemNameChangeCheckBox3.Checked)
            {
                itemMainCategoryTextBox3.ForeColor = Color.Blue;
                itemCategoryTextBox3.ForeColor = Color.Blue;
                itemDetailTextBox3.ForeColor = Color.Blue;
                weightTextBox3.ForeColor = Color.Blue;
                unitPriceText3.ForeColor = Color.Blue;
                countTextBox3.ForeColor = Color.Blue;
                purchaseTextBox3.ForeColor = Color.Blue;
                WholesalePriceTextBox3.ForeColor = Color.Blue;
                remark3.ForeColor = Color.Blue;
                BuyerTextBox3.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox3.Checked && ItemNameChangeCheckBox3.Checked)
            {
                itemMainCategoryTextBox3.ForeColor = Color.Purple;
                itemCategoryTextBox3.ForeColor = Color.Purple;
                itemDetailTextBox3.ForeColor = Color.Purple;
                weightTextBox3.ForeColor = Color.Purple;
                unitPriceText3.ForeColor = Color.Purple;
                countTextBox3.ForeColor = Color.Purple;
                purchaseTextBox3.ForeColor = Color.Purple;
                WholesalePriceTextBox3.ForeColor = Color.Purple;
                remark3.ForeColor = Color.Purple;
                BuyerTextBox3.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox3.Checked && !ItemNameChangeCheckBox3.Checked)
            {
                itemMainCategoryTextBox3.ForeColor = Color.Red;
                itemCategoryTextBox3.ForeColor = Color.Red;
                itemDetailTextBox3.ForeColor = Color.Red;
                weightTextBox3.ForeColor = Color.Red;
                unitPriceText3.ForeColor = Color.Red;
                countTextBox3.ForeColor = Color.Red;
                purchaseTextBox3.ForeColor = Color.Red;
                WholesalePriceTextBox3.ForeColor = Color.Red;
                remark3.ForeColor = Color.Red;
                BuyerTextBox3.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox3.Checked && !ItemNameChangeCheckBox3.Checked)
            {
                itemMainCategoryTextBox3.ForeColor = Color.Black;
                itemCategoryTextBox3.ForeColor = Color.Black;
                itemDetailTextBox3.ForeColor = Color.Black;
                weightTextBox3.ForeColor = Color.Black;
                unitPriceText3.ForeColor = Color.Black;
                countTextBox3.ForeColor = Color.Black;
                purchaseTextBox3.ForeColor = Color.Black;
                WholesalePriceTextBox3.ForeColor = Color.Black;
                remark3.ForeColor = Color.Black;
                BuyerTextBox3.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"4"
        private void NextMonthCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox4.Checked && ItemNameChangeCheckBox4.Checked)
            {
                itemMainCategoryTextBox4.ForeColor = Color.Blue;
                itemCategoryTextBox4.ForeColor = Color.Blue;
                itemDetailTextBox4.ForeColor = Color.Blue;
                weightTextBox4.ForeColor = Color.Blue;
                unitPriceText4.ForeColor = Color.Blue;
                countTextBox4.ForeColor = Color.Blue;
                purchaseTextBox4.ForeColor = Color.Blue;
                WholesalePriceTextBox4.ForeColor = Color.Blue;
                remark4.ForeColor = Color.Blue;
                BuyerTextBox4.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox4.Checked && ItemNameChangeCheckBox4.Checked)
            {
                itemMainCategoryTextBox4.ForeColor = Color.Purple;
                itemCategoryTextBox4.ForeColor = Color.Purple;
                itemDetailTextBox4.ForeColor = Color.Purple;
                weightTextBox4.ForeColor = Color.Purple;
                unitPriceText4.ForeColor = Color.Purple;
                countTextBox4.ForeColor = Color.Purple;
                purchaseTextBox4.ForeColor = Color.Purple;
                WholesalePriceTextBox4.ForeColor = Color.Purple;
                remark4.ForeColor = Color.Purple;
                BuyerTextBox4.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox4.Checked && !ItemNameChangeCheckBox4.Checked)
            {
                itemMainCategoryTextBox4.ForeColor = Color.Red;
                itemCategoryTextBox4.ForeColor = Color.Red;
                itemDetailTextBox4.ForeColor = Color.Red;
                weightTextBox4.ForeColor = Color.Red;
                unitPriceText4.ForeColor = Color.Red;
                countTextBox4.ForeColor = Color.Red;
                purchaseTextBox4.ForeColor = Color.Red;
                WholesalePriceTextBox4.ForeColor = Color.Red;
                remark4.ForeColor = Color.Red;
                BuyerTextBox4.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox4.Checked && !ItemNameChangeCheckBox4.Checked)
            {
                itemMainCategoryTextBox4.ForeColor = Color.Black;
                itemCategoryTextBox4.ForeColor = Color.Black;
                itemDetailTextBox4.ForeColor = Color.Black;
                weightTextBox4.ForeColor = Color.Black;
                unitPriceText4.ForeColor = Color.Black;
                countTextBox4.ForeColor = Color.Black;
                purchaseTextBox4.ForeColor = Color.Black;
                WholesalePriceTextBox4.ForeColor = Color.Black;
                remark4.ForeColor = Color.Black;
                BuyerTextBox4.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"5"
        private void NextMonthCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox5.Checked && ItemNameChangeCheckBox5.Checked)
            {
                itemMainCategoryTextBox5.ForeColor = Color.Blue;
                itemCategoryTextBox5.ForeColor = Color.Blue;
                itemDetailTextBox5.ForeColor = Color.Blue;
                weightTextBox5.ForeColor = Color.Blue;
                unitPriceText5.ForeColor = Color.Blue;
                countTextBox5.ForeColor = Color.Blue;
                purchaseTextBox5.ForeColor = Color.Blue;
                WholesalePriceTextBox5.ForeColor = Color.Blue;
                remark5.ForeColor = Color.Blue;
                BuyerTextBox5.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox5.Checked && ItemNameChangeCheckBox5.Checked)
            {
                itemMainCategoryTextBox5.ForeColor = Color.Purple;
                itemCategoryTextBox5.ForeColor = Color.Purple;
                itemDetailTextBox5.ForeColor = Color.Purple;
                weightTextBox5.ForeColor = Color.Purple;
                unitPriceText5.ForeColor = Color.Purple;
                countTextBox5.ForeColor = Color.Purple;
                purchaseTextBox5.ForeColor = Color.Purple;
                WholesalePriceTextBox5.ForeColor = Color.Purple;
                remark5.ForeColor = Color.Purple;
                BuyerTextBox5.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox5.Checked && !ItemNameChangeCheckBox5.Checked)
            {
                itemMainCategoryTextBox5.ForeColor = Color.Red;
                itemCategoryTextBox5.ForeColor = Color.Red;
                itemDetailTextBox5.ForeColor = Color.Red;
                weightTextBox5.ForeColor = Color.Red;
                unitPriceText5.ForeColor = Color.Red;
                countTextBox5.ForeColor = Color.Red;
                purchaseTextBox5.ForeColor = Color.Red;
                WholesalePriceTextBox5.ForeColor = Color.Red;
                remark5.ForeColor = Color.Red;
                BuyerTextBox5.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox5.Checked && !ItemNameChangeCheckBox5.Checked)
            {
                itemMainCategoryTextBox5.ForeColor = Color.Black;
                itemCategoryTextBox5.ForeColor = Color.Black;
                itemDetailTextBox5.ForeColor = Color.Black;
                weightTextBox5.ForeColor = Color.Black;
                unitPriceText5.ForeColor = Color.Black;
                countTextBox5.ForeColor = Color.Black;
                purchaseTextBox5.ForeColor = Color.Black;
                WholesalePriceTextBox5.ForeColor = Color.Black;
                remark5.ForeColor = Color.Black;
                BuyerTextBox5.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"6"
        private void NextMonthCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox6.Checked && ItemNameChangeCheckBox6.Checked)
            {
                itemMainCategoryTextBox6.ForeColor = Color.Blue;
                itemCategoryTextBox6.ForeColor = Color.Blue;
                itemDetailTextBox6.ForeColor = Color.Blue;
                weightTextBox6.ForeColor = Color.Blue;
                unitPriceText6.ForeColor = Color.Blue;
                countTextBox6.ForeColor = Color.Blue;
                purchaseTextBox6.ForeColor = Color.Blue;
                WholesalePriceTextBox6.ForeColor = Color.Blue;
                remark6.ForeColor = Color.Blue;
                BuyerTextBox6.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox6.Checked && ItemNameChangeCheckBox6.Checked)
            {
                itemMainCategoryTextBox6.ForeColor = Color.Purple;
                itemCategoryTextBox6.ForeColor = Color.Purple;
                itemDetailTextBox6.ForeColor = Color.Purple;
                weightTextBox6.ForeColor = Color.Purple;
                unitPriceText6.ForeColor = Color.Purple;
                countTextBox6.ForeColor = Color.Purple;
                purchaseTextBox6.ForeColor = Color.Purple;
                WholesalePriceTextBox6.ForeColor = Color.Purple;
                remark6.ForeColor = Color.Purple;
                BuyerTextBox6.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox6.Checked && !ItemNameChangeCheckBox6.Checked)
            {
                itemMainCategoryTextBox6.ForeColor = Color.Red;
                itemCategoryTextBox6.ForeColor = Color.Red;
                itemDetailTextBox6.ForeColor = Color.Red;
                weightTextBox6.ForeColor = Color.Red;
                unitPriceText6.ForeColor = Color.Red;
                countTextBox6.ForeColor = Color.Red;
                purchaseTextBox6.ForeColor = Color.Red;
                WholesalePriceTextBox6.ForeColor = Color.Red;
                remark6.ForeColor = Color.Red;
                BuyerTextBox6.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox6.Checked && !ItemNameChangeCheckBox6.Checked)
            {
                itemMainCategoryTextBox6.ForeColor = Color.Black;
                itemCategoryTextBox6.ForeColor = Color.Black;
                itemDetailTextBox6.ForeColor = Color.Black;
                weightTextBox6.ForeColor = Color.Black;
                unitPriceText6.ForeColor = Color.Black;
                countTextBox6.ForeColor = Color.Black;
                purchaseTextBox6.ForeColor = Color.Black;
                WholesalePriceTextBox6.ForeColor = Color.Black;
                remark6.ForeColor = Color.Black;
                BuyerTextBox6.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"7"
        private void NextMonthCheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox7.Checked && ItemNameChangeCheckBox7.Checked)
            {
                itemMainCategoryTextBox7.ForeColor = Color.Blue;
                itemCategoryTextBox7.ForeColor = Color.Blue;
                itemDetailTextBox7.ForeColor = Color.Blue;
                weightTextBox7.ForeColor = Color.Blue;
                unitPriceText7.ForeColor = Color.Blue;
                countTextBox7.ForeColor = Color.Blue;
                purchaseTextBox7.ForeColor = Color.Blue;
                WholesalePriceTextBox7.ForeColor = Color.Blue;
                remark7.ForeColor = Color.Blue;
                BuyerTextBox7.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox7.Checked && ItemNameChangeCheckBox7.Checked)
            {
                itemMainCategoryTextBox7.ForeColor = Color.Purple;
                itemCategoryTextBox7.ForeColor = Color.Purple;
                itemDetailTextBox7.ForeColor = Color.Purple;
                weightTextBox7.ForeColor = Color.Purple;
                unitPriceText7.ForeColor = Color.Purple;
                countTextBox7.ForeColor = Color.Purple;
                purchaseTextBox7.ForeColor = Color.Purple;
                WholesalePriceTextBox7.ForeColor = Color.Purple;
                remark7.ForeColor = Color.Purple;
                BuyerTextBox7.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox7.Checked && !ItemNameChangeCheckBox7.Checked)
            {
                itemMainCategoryTextBox7.ForeColor = Color.Red;
                itemCategoryTextBox7.ForeColor = Color.Red;
                itemDetailTextBox7.ForeColor = Color.Red;
                weightTextBox7.ForeColor = Color.Red;
                unitPriceText7.ForeColor = Color.Red;
                countTextBox7.ForeColor = Color.Red;
                purchaseTextBox7.ForeColor = Color.Red;
                WholesalePriceTextBox7.ForeColor = Color.Red;
                remark7.ForeColor = Color.Red;
                BuyerTextBox7.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox7.Checked && !ItemNameChangeCheckBox7.Checked)
            {
                itemMainCategoryTextBox7.ForeColor = Color.Black;
                itemCategoryTextBox7.ForeColor = Color.Black;
                itemDetailTextBox7.ForeColor = Color.Black;
                weightTextBox7.ForeColor = Color.Black;
                unitPriceText7.ForeColor = Color.Black;
                countTextBox7.ForeColor = Color.Black;
                purchaseTextBox7.ForeColor = Color.Black;
                WholesalePriceTextBox7.ForeColor = Color.Black;
                remark7.ForeColor = Color.Black;
                BuyerTextBox7.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"8"
        private void NextMonthCheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox8.Checked && ItemNameChangeCheckBox8.Checked)
            {
                itemMainCategoryTextBox8.ForeColor = Color.Blue;
                itemCategoryTextBox8.ForeColor = Color.Blue;
                itemDetailTextBox8.ForeColor = Color.Blue;
                weightTextBox8.ForeColor = Color.Blue;
                unitPriceText8.ForeColor = Color.Blue;
                countTextBox8.ForeColor = Color.Blue;
                purchaseTextBox8.ForeColor = Color.Blue;
                WholesalePriceTextBox8.ForeColor = Color.Blue;
                remark8.ForeColor = Color.Blue;
                BuyerTextBox8.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox8.Checked && ItemNameChangeCheckBox8.Checked)
            {
                itemMainCategoryTextBox8.ForeColor = Color.Purple;
                itemCategoryTextBox8.ForeColor = Color.Purple;
                itemDetailTextBox8.ForeColor = Color.Purple;
                weightTextBox8.ForeColor = Color.Purple;
                unitPriceText8.ForeColor = Color.Purple;
                countTextBox8.ForeColor = Color.Purple;
                purchaseTextBox8.ForeColor = Color.Purple;
                WholesalePriceTextBox8.ForeColor = Color.Purple;
                remark8.ForeColor = Color.Purple;
                BuyerTextBox8.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox8.Checked && !ItemNameChangeCheckBox8.Checked)
            {
                itemMainCategoryTextBox8.ForeColor = Color.Red;
                itemCategoryTextBox8.ForeColor = Color.Red;
                itemDetailTextBox8.ForeColor = Color.Red;
                weightTextBox8.ForeColor = Color.Red;
                unitPriceText8.ForeColor = Color.Red;
                countTextBox8.ForeColor = Color.Red;
                purchaseTextBox8.ForeColor = Color.Red;
                WholesalePriceTextBox8.ForeColor = Color.Red;
                remark8.ForeColor = Color.Red;
                BuyerTextBox8.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox8.Checked && !ItemNameChangeCheckBox8.Checked)
            {
                itemMainCategoryTextBox8.ForeColor = Color.Black;
                itemCategoryTextBox8.ForeColor = Color.Black;
                itemDetailTextBox8.ForeColor = Color.Black;
                weightTextBox8.ForeColor = Color.Black;
                unitPriceText8.ForeColor = Color.Black;
                countTextBox8.ForeColor = Color.Black;
                purchaseTextBox8.ForeColor = Color.Black;
                WholesalePriceTextBox8.ForeColor = Color.Black;
                remark8.ForeColor = Color.Black;
                BuyerTextBox8.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"9"
        private void NextMonthCheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            //品名
            if (!NextMonthCheckBox9.Checked && ItemNameChangeCheckBox9.Checked)
            {
                itemMainCategoryTextBox9.ForeColor = Color.Blue;
                itemCategoryTextBox9.ForeColor = Color.Blue;
                itemDetailTextBox9.ForeColor = Color.Blue;
                weightTextBox9.ForeColor = Color.Blue;
                unitPriceText9.ForeColor = Color.Blue;
                countTextBox9.ForeColor = Color.Blue;
                purchaseTextBox9.ForeColor = Color.Blue;
                WholesalePriceTextBox9.ForeColor = Color.Blue;
                remark9.ForeColor = Color.Blue;
                BuyerTextBox9.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox9.Checked && ItemNameChangeCheckBox9.Checked)
            {
                itemMainCategoryTextBox9.ForeColor = Color.Purple;
                itemCategoryTextBox9.ForeColor = Color.Purple;
                itemDetailTextBox9.ForeColor = Color.Purple;
                weightTextBox9.ForeColor = Color.Purple;
                unitPriceText9.ForeColor = Color.Purple;
                countTextBox9.ForeColor = Color.Purple;
                purchaseTextBox9.ForeColor = Color.Purple;
                WholesalePriceTextBox9.ForeColor = Color.Purple;
                remark9.ForeColor = Color.Purple;
                BuyerTextBox9.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox9.Checked && !ItemNameChangeCheckBox9.Checked)
            {
                itemMainCategoryTextBox9.ForeColor = Color.Red;
                itemCategoryTextBox9.ForeColor = Color.Red;
                itemDetailTextBox9.ForeColor = Color.Red;
                weightTextBox9.ForeColor = Color.Red;
                unitPriceText9.ForeColor = Color.Red;
                countTextBox9.ForeColor = Color.Red;
                purchaseTextBox9.ForeColor = Color.Red;
                WholesalePriceTextBox9.ForeColor = Color.Red;
                remark9.ForeColor = Color.Red;
                BuyerTextBox9.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox9.Checked && !ItemNameChangeCheckBox9.Checked)
            {
                itemMainCategoryTextBox9.ForeColor = Color.Black;
                itemCategoryTextBox9.ForeColor = Color.Black;
                itemDetailTextBox9.ForeColor = Color.Black;
                weightTextBox9.ForeColor = Color.Black;
                unitPriceText9.ForeColor = Color.Black;
                countTextBox9.ForeColor = Color.Black;
                purchaseTextBox9.ForeColor = Color.Black;
                WholesalePriceTextBox9.ForeColor = Color.Black;
                remark9.ForeColor = Color.Black;
                BuyerTextBox9.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"10"
        private void NextMonthCheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox10.Checked && ItemNameChangeCheckBox10.Checked)
            {
                itemMainCategoryTextBox10.ForeColor = Color.Blue;
                itemCategoryTextBox10.ForeColor = Color.Blue;
                itemDetailTextBox10.ForeColor = Color.Blue;
                weightTextBox10.ForeColor = Color.Blue;
                unitPriceText10.ForeColor = Color.Blue;
                countTextBox10.ForeColor = Color.Blue;
                purchaseTextBox10.ForeColor = Color.Blue;
                WholesalePriceTextBox10.ForeColor = Color.Blue;
                remark10.ForeColor = Color.Blue;
                BuyerTextBox10.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox10.Checked && ItemNameChangeCheckBox10.Checked)
            {
                itemMainCategoryTextBox10.ForeColor = Color.Purple;
                itemCategoryTextBox10.ForeColor = Color.Purple;
                itemDetailTextBox10.ForeColor = Color.Purple;
                weightTextBox10.ForeColor = Color.Purple;
                unitPriceText10.ForeColor = Color.Purple;
                countTextBox10.ForeColor = Color.Purple;
                purchaseTextBox10.ForeColor = Color.Purple;
                WholesalePriceTextBox10.ForeColor = Color.Purple;
                remark10.ForeColor = Color.Purple;
                BuyerTextBox10.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox10.Checked && !ItemNameChangeCheckBox10.Checked)
            {
                itemMainCategoryTextBox10.ForeColor = Color.Red;
                itemCategoryTextBox10.ForeColor = Color.Red;
                itemDetailTextBox10.ForeColor = Color.Red;
                weightTextBox10.ForeColor = Color.Red;
                unitPriceText10.ForeColor = Color.Red;
                countTextBox10.ForeColor = Color.Red;
                purchaseTextBox10.ForeColor = Color.Red;
                WholesalePriceTextBox10.ForeColor = Color.Red;
                remark10.ForeColor = Color.Red;
                BuyerTextBox10.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox10.Checked && !ItemNameChangeCheckBox10.Checked)
            {
                itemMainCategoryTextBox10.ForeColor = Color.Black;
                itemCategoryTextBox10.ForeColor = Color.Black;
                itemDetailTextBox10.ForeColor = Color.Black;
                weightTextBox10.ForeColor = Color.Black;
                unitPriceText10.ForeColor = Color.Black;
                countTextBox10.ForeColor = Color.Black;
                purchaseTextBox10.ForeColor = Color.Black;
                WholesalePriceTextBox10.ForeColor = Color.Black;
                remark10.ForeColor = Color.Black;
                BuyerTextBox10.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"11"
        private void NextMonthCheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox11.Checked && ItemNameChangeCheckBox11.Checked)
            {
                itemMainCategoryTextBox11.ForeColor = Color.Blue;
                itemCategoryTextBox11.ForeColor = Color.Blue;
                itemDetailTextBox11.ForeColor = Color.Blue;
                weightTextBox11.ForeColor = Color.Blue;
                unitPriceText11.ForeColor = Color.Blue;
                countTextBox11.ForeColor = Color.Blue;
                purchaseTextBox11.ForeColor = Color.Blue;
                WholesalePriceTextBox11.ForeColor = Color.Blue;
                remark11.ForeColor = Color.Blue;
                BuyerTextBox11.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox11.Checked && ItemNameChangeCheckBox11.Checked)
            {
                itemMainCategoryTextBox11.ForeColor = Color.Purple;
                itemCategoryTextBox11.ForeColor = Color.Purple;
                itemDetailTextBox11.ForeColor = Color.Purple;
                weightTextBox11.ForeColor = Color.Purple;
                unitPriceText11.ForeColor = Color.Purple;
                countTextBox11.ForeColor = Color.Purple;
                purchaseTextBox11.ForeColor = Color.Purple;
                WholesalePriceTextBox11.ForeColor = Color.Purple;
                remark11.ForeColor = Color.Purple;
                BuyerTextBox11.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox11.Checked && !ItemNameChangeCheckBox11.Checked)
            {
                itemMainCategoryTextBox11.ForeColor = Color.Red;
                itemCategoryTextBox11.ForeColor = Color.Red;
                itemDetailTextBox11.ForeColor = Color.Red;
                weightTextBox11.ForeColor = Color.Red;
                unitPriceText11.ForeColor = Color.Red;
                countTextBox11.ForeColor = Color.Red;
                purchaseTextBox11.ForeColor = Color.Red;
                WholesalePriceTextBox11.ForeColor = Color.Red;
                remark11.ForeColor = Color.Red;
                BuyerTextBox11.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox11.Checked && !ItemNameChangeCheckBox11.Checked)
            {
                itemMainCategoryTextBox11.ForeColor = Color.Black;
                itemCategoryTextBox11.ForeColor = Color.Black;
                itemDetailTextBox11.ForeColor = Color.Black;
                weightTextBox11.ForeColor = Color.Black;
                unitPriceText11.ForeColor = Color.Black;
                countTextBox11.ForeColor = Color.Black;
                purchaseTextBox11.ForeColor = Color.Black;
                WholesalePriceTextBox11.ForeColor = Color.Black;
                remark11.ForeColor = Color.Black;
                BuyerTextBox11.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"12"
        private void NextMonthCheckBox12_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox12.Checked && ItemNameChangeCheckBox12.Checked)
            {
                itemMainCategoryTextBox12.ForeColor = Color.Blue;
                itemCategoryTextBox12.ForeColor = Color.Blue;
                itemDetailTextBox12.ForeColor = Color.Blue;
                weightTextBox12.ForeColor = Color.Blue;
                unitPriceText12.ForeColor = Color.Blue;
                countTextBox12.ForeColor = Color.Blue;
                purchaseTextBox12.ForeColor = Color.Blue;
                WholesalePriceTextBox12.ForeColor = Color.Blue;
                remark12.ForeColor = Color.Blue;
                BuyerTextBox12.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox12.Checked && ItemNameChangeCheckBox12.Checked)
            {
                itemMainCategoryTextBox12.ForeColor = Color.Purple;
                itemCategoryTextBox12.ForeColor = Color.Purple;
                itemDetailTextBox12.ForeColor = Color.Purple;
                weightTextBox12.ForeColor = Color.Purple;
                unitPriceText12.ForeColor = Color.Purple;
                countTextBox12.ForeColor = Color.Purple;
                purchaseTextBox12.ForeColor = Color.Purple;
                WholesalePriceTextBox12.ForeColor = Color.Purple;
                remark12.ForeColor = Color.Purple;
                BuyerTextBox12.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox12.Checked && !ItemNameChangeCheckBox12.Checked)
            {
                itemMainCategoryTextBox12.ForeColor = Color.Red;
                itemCategoryTextBox12.ForeColor = Color.Red;
                itemDetailTextBox12.ForeColor = Color.Red;
                weightTextBox12.ForeColor = Color.Red;
                unitPriceText12.ForeColor = Color.Red;
                countTextBox12.ForeColor = Color.Red;
                purchaseTextBox12.ForeColor = Color.Red;
                WholesalePriceTextBox12.ForeColor = Color.Red;
                remark12.ForeColor = Color.Red;
                BuyerTextBox12.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox12.Checked && !ItemNameChangeCheckBox12.Checked)
            {
                itemMainCategoryTextBox12.ForeColor = Color.Black;
                itemCategoryTextBox12.ForeColor = Color.Black;
                itemDetailTextBox12.ForeColor = Color.Black;
                weightTextBox12.ForeColor = Color.Black;
                unitPriceText12.ForeColor = Color.Black;
                countTextBox12.ForeColor = Color.Black;
                purchaseTextBox12.ForeColor = Color.Black;
                WholesalePriceTextBox12.ForeColor = Color.Black;
                remark12.ForeColor = Color.Black;
                BuyerTextBox12.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"13"
        private void NextMonthCheckBox13_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox13.Checked && ItemNameChangeCheckBox13.Checked)
            {
                itemMainCategoryTextBox13.ForeColor = Color.Blue;
                itemCategoryTextBox13.ForeColor = Color.Blue;
                itemDetailTextBox13.ForeColor = Color.Blue;
                weightTextBox13.ForeColor = Color.Blue;
                unitPriceText13.ForeColor = Color.Blue;
                countTextBox13.ForeColor = Color.Blue;
                purchaseTextBox13.ForeColor = Color.Blue;
                WholesalePriceTextBox13.ForeColor = Color.Blue;
                remark13.ForeColor = Color.Blue;
                BuyerTextBox13.ForeColor = Color.Blue;

            }
            //次月・品名両方チェック
            if (NextMonthCheckBox13.Checked && ItemNameChangeCheckBox13.Checked)
            {
                itemMainCategoryTextBox13.ForeColor = Color.Purple;
                itemCategoryTextBox13.ForeColor = Color.Purple;
                itemDetailTextBox13.ForeColor = Color.Purple;
                weightTextBox13.ForeColor = Color.Purple;
                unitPriceText13.ForeColor = Color.Purple;
                countTextBox13.ForeColor = Color.Purple;
                purchaseTextBox13.ForeColor = Color.Purple;
                WholesalePriceTextBox13.ForeColor = Color.Purple;
                remark13.ForeColor = Color.Purple;
                BuyerTextBox13.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox13.Checked && !ItemNameChangeCheckBox13.Checked)
            {
                itemMainCategoryTextBox13.ForeColor = Color.Red;
                itemCategoryTextBox13.ForeColor = Color.Red;
                itemDetailTextBox13.ForeColor = Color.Red;
                weightTextBox13.ForeColor = Color.Red;
                unitPriceText13.ForeColor = Color.Red;
                countTextBox13.ForeColor = Color.Red;
                purchaseTextBox13.ForeColor = Color.Red;
                WholesalePriceTextBox13.ForeColor = Color.Red;
                remark13.ForeColor = Color.Red;
                BuyerTextBox13.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox13.Checked && !ItemNameChangeCheckBox13.Checked)
            {
                itemMainCategoryTextBox13.ForeColor = Color.Black;
                itemCategoryTextBox13.ForeColor = Color.Black;
                itemDetailTextBox13.ForeColor = Color.Black;
                weightTextBox13.ForeColor = Color.Black;
                unitPriceText13.ForeColor = Color.Black;
                countTextBox13.ForeColor = Color.Black;
                purchaseTextBox13.ForeColor = Color.Black;
                WholesalePriceTextBox13.ForeColor = Color.Black;
                remark13.ForeColor = Color.Black;
                BuyerTextBox13.ForeColor = Color.Black;
            }
        }
        #endregion
        #endregion

        #region"単価の数値を３桁区切りで表示 TextChangedイベント"
        private void unitPriceText1_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat1 = decimal.Parse(unitPriceText1.Text);
            unitPriceText1.Text = string.Format("{0:#,0}", UnitPriceUnFormat1);
        }

        private void unitPriceText2_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat2 = decimal.Parse(unitPriceText2.Text);
            unitPriceText2.Text = string.Format("{0:#,0}", UnitPriceUnFormat2);
        }

        private void unitPriceText3_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat3 = decimal.Parse(unitPriceText3.Text);
            unitPriceText3.Text = string.Format("{0:#,0}", UnitPriceUnFormat3);
        }

        private void unitPriceText4_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat4 = decimal.Parse(unitPriceText4.Text);
            unitPriceText4.Text = string.Format("{0:#,0}", UnitPriceUnFormat4);
        }


        private void unitPriceText5_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat5 = decimal.Parse(unitPriceText5.Text);
            unitPriceText5.Text = string.Format("{0:#,0}", UnitPriceUnFormat5);
        }

        private void unitPriceText6_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat6 = decimal.Parse(unitPriceText6.Text);
            unitPriceText6.Text = string.Format("{0:#,0}", UnitPriceUnFormat6);
        }

        private void unitPriceText7_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat7 = decimal.Parse(unitPriceText7.Text);
            unitPriceText7.Text = string.Format("{0:#,0}", UnitPriceUnFormat7);
        }

        private void unitPriceText8_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat8 = decimal.Parse(unitPriceText8.Text);
            unitPriceText8.Text = string.Format("{0:#,0}", UnitPriceUnFormat8);
        }

        private void unitPriceText9_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat9 = decimal.Parse(unitPriceText9.Text);
            unitPriceText9.Text = string.Format("{0:#,0}", UnitPriceUnFormat9);
        }

        private void unitPriceText10_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat10 = decimal.Parse(unitPriceText10.Text);
            unitPriceText10.Text = string.Format("{0:#,0}", UnitPriceUnFormat10);
        }

        private void unitPriceText11_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat11 = decimal.Parse(unitPriceText11.Text);
            unitPriceText11.Text = string.Format("{0:#,0}", UnitPriceUnFormat11);
        }

        private void unitPriceText12_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat12 = decimal.Parse(unitPriceText12.Text);
            unitPriceText12.Text = string.Format("{0:#,0}", UnitPriceUnFormat12);
        }

        private void unitPriceText13_TextChanged(object sender, EventArgs e)
        {
            UnitPriceUnFormat13 = decimal.Parse(unitPriceText13.Text);
            unitPriceText13.Text = string.Format("{0:#,0}", UnitPriceUnFormat13);
        }
        #endregion

        #region"買取額（上の表）３桁区切り TextChangedイベント"
        private void purchaseTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat1 = decimal.Parse(purchaseTextBox1.Text);
                purchaseTextBox1.Text = string.Format("{0:C}", PurchaseUnFormat1);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat2 = decimal.Parse(purchaseTextBox2.Text);
                purchaseTextBox2.Text = string.Format("{0:C}", PurchaseUnFormat2);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat3 = decimal.Parse(purchaseTextBox3.Text);
                purchaseTextBox3.Text = string.Format("{0:C}", PurchaseUnFormat3);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat4 = decimal.Parse(purchaseTextBox4.Text);
                purchaseTextBox4.Text = string.Format("{0:C}", PurchaseUnFormat4);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat5 = decimal.Parse(purchaseTextBox5.Text);
                purchaseTextBox5.Text = string.Format("{0:C}", PurchaseUnFormat5);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat6 = decimal.Parse(purchaseTextBox6.Text);
                purchaseTextBox6.Text = string.Format("{0:C}", PurchaseUnFormat6);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox7_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat7 = decimal.Parse(purchaseTextBox7.Text);
                purchaseTextBox7.Text = string.Format("{0:C}", PurchaseUnFormat7);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox8_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat8 = decimal.Parse(purchaseTextBox8.Text);
                purchaseTextBox8.Text = string.Format("{0:C}", PurchaseUnFormat8);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat9 = decimal.Parse(purchaseTextBox9.Text);
                purchaseTextBox9.Text = string.Format("{0:C}", PurchaseUnFormat9);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat10 = decimal.Parse(purchaseTextBox10.Text);
                purchaseTextBox10.Text = string.Format("{0:C}", PurchaseUnFormat10);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox11_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat11 = decimal.Parse(purchaseTextBox11.Text);
                purchaseTextBox11.Text = string.Format("{0:C}", PurchaseUnFormat11);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox12_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat12 = decimal.Parse(purchaseTextBox12.Text);
                purchaseTextBox12.Text = string.Format("{0:C}", PurchaseUnFormat12);
            }
            else
            {
                first = true;
            }
        }
        private void purchaseTextBox13_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurchaseUnFormat13 = decimal.Parse(purchaseTextBox13.Text);
                purchaseTextBox13.Text = string.Format("{0:C}", PurchaseUnFormat13);
            }
            else
            {
                first = true;
            }
        }
        #endregion

        #region"品名変更ボタン　色"
        #region"1"
        private void ItemNameChangeCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //次月・品名両方
            if (NextMonthCheckBox1.Checked && ItemNameChangeCheckBox1.Checked)
            {
                itemMainCategoryTextBox1.ForeColor = Color.Purple;
                itemCategoryTextBox1.ForeColor = Color.Purple;
                itemDetailTextBox1.ForeColor = Color.Purple;
                weightTextBox1.ForeColor = Color.Purple;
                unitPriceText1.ForeColor = Color.Purple;
                countTextBox1.ForeColor = Color.Purple;
                purchaseTextBox1.ForeColor = Color.Purple;
                WholesalePriceTextBox1.ForeColor = Color.Purple;
                remark1.ForeColor = Color.Purple;
                BuyerTextBox1.ForeColor = Color.Purple;
            }
            //品名のみ
            if (!NextMonthCheckBox1.Checked && ItemNameChangeCheckBox1.Checked)
            {
                itemMainCategoryTextBox1.ForeColor = Color.Blue;
                itemCategoryTextBox1.ForeColor = Color.Blue;
                itemDetailTextBox1.ForeColor = Color.Blue;
                weightTextBox1.ForeColor = Color.Blue;
                unitPriceText1.ForeColor = Color.Blue;
                countTextBox1.ForeColor = Color.Blue;
                purchaseTextBox1.ForeColor = Color.Blue;
                WholesalePriceTextBox1.ForeColor = Color.Blue;
                remark1.ForeColor = Color.Blue;
                BuyerTextBox1.ForeColor = Color.Blue;
            }
            //次月のみ
            if (NextMonthCheckBox1.Checked && !ItemNameChangeCheckBox1.Checked)
            {
                itemMainCategoryTextBox1.ForeColor = Color.Red;
                itemCategoryTextBox1.ForeColor = Color.Red;
                itemDetailTextBox1.ForeColor = Color.Red;
                weightTextBox1.ForeColor = Color.Red;
                unitPriceText1.ForeColor = Color.Red;
                countTextBox1.ForeColor = Color.Red;
                purchaseTextBox1.ForeColor = Color.Red;
                WholesalePriceTextBox1.ForeColor = Color.Red;
                remark1.ForeColor = Color.Red;
                BuyerTextBox1.ForeColor = Color.Red;
            }
            //どちらもノーチェック
            if (!NextMonthCheckBox1.Checked && !ItemNameChangeCheckBox1.Checked)
            {
                itemMainCategoryTextBox1.ForeColor = Color.Black;
                itemCategoryTextBox1.ForeColor = Color.Black;
                itemDetailTextBox1.ForeColor = Color.Black;
                weightTextBox1.ForeColor = Color.Black;
                unitPriceText1.ForeColor = Color.Black;
                countTextBox1.ForeColor = Color.Black;
                purchaseTextBox1.ForeColor = Color.Black;
                WholesalePriceTextBox1.ForeColor = Color.Black;
                remark1.ForeColor = Color.Black;
                BuyerTextBox1.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"2"
        private void ItemNameChangeCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox2.Checked && ItemNameChangeCheckBox2.Checked)
            {
                itemMainCategoryTextBox2.ForeColor = Color.Blue;
                itemCategoryTextBox2.ForeColor = Color.Blue;
                itemDetailTextBox2.ForeColor = Color.Blue;
                weightTextBox2.ForeColor = Color.Blue;
                unitPriceText2.ForeColor = Color.Blue;
                countTextBox2.ForeColor = Color.Blue;
                purchaseTextBox2.ForeColor = Color.Blue;
                WholesalePriceTextBox2.ForeColor = Color.Blue;
                remark2.ForeColor = Color.Blue;
                BuyerTextBox2.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox2.Checked && ItemNameChangeCheckBox2.Checked)
            {
                itemMainCategoryTextBox2.ForeColor = Color.Purple;
                itemCategoryTextBox2.ForeColor = Color.Purple;
                itemDetailTextBox2.ForeColor = Color.Purple;
                weightTextBox2.ForeColor = Color.Purple;
                unitPriceText2.ForeColor = Color.Purple;
                countTextBox2.ForeColor = Color.Purple;
                purchaseTextBox2.ForeColor = Color.Purple;
                WholesalePriceTextBox2.ForeColor = Color.Purple;
                remark2.ForeColor = Color.Purple;
                BuyerTextBox2.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox2.Checked && !ItemNameChangeCheckBox2.Checked)
            {
                itemMainCategoryTextBox2.ForeColor = Color.Red;
                itemCategoryTextBox2.ForeColor = Color.Red;
                itemDetailTextBox2.ForeColor = Color.Red;
                weightTextBox2.ForeColor = Color.Red;
                unitPriceText2.ForeColor = Color.Red;
                countTextBox2.ForeColor = Color.Red;
                purchaseTextBox2.ForeColor = Color.Red;
                WholesalePriceTextBox2.ForeColor = Color.Red;
                remark2.ForeColor = Color.Red;
                BuyerTextBox2.ForeColor = Color.Red;
            }
            //両方空
            if (!NextMonthCheckBox2.Checked && !ItemNameChangeCheckBox2.Checked)
            {
                itemMainCategoryTextBox2.ForeColor = Color.Black;
                itemCategoryTextBox2.ForeColor = Color.Black;
                itemDetailTextBox2.ForeColor = Color.Black;
                weightTextBox2.ForeColor = Color.Black;
                unitPriceText2.ForeColor = Color.Black;
                countTextBox2.ForeColor = Color.Black;
                purchaseTextBox2.ForeColor = Color.Black;
                WholesalePriceTextBox2.ForeColor = Color.Black;
                remark2.ForeColor = Color.Black;
                BuyerTextBox2.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"3"
        private void ItemNameChangeCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox3.Checked && ItemNameChangeCheckBox3.Checked)
            {
                itemMainCategoryTextBox3.ForeColor = Color.Blue;
                itemCategoryTextBox3.ForeColor = Color.Blue;
                itemDetailTextBox3.ForeColor = Color.Blue;
                weightTextBox3.ForeColor = Color.Blue;
                unitPriceText3.ForeColor = Color.Blue;
                countTextBox3.ForeColor = Color.Blue;
                purchaseTextBox3.ForeColor = Color.Blue;
                WholesalePriceTextBox3.ForeColor = Color.Blue;
                remark3.ForeColor = Color.Blue;
                BuyerTextBox3.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox3.Checked && ItemNameChangeCheckBox3.Checked)
            {
                itemMainCategoryTextBox3.ForeColor = Color.Purple;
                itemCategoryTextBox3.ForeColor = Color.Purple;
                itemDetailTextBox3.ForeColor = Color.Purple;
                weightTextBox3.ForeColor = Color.Purple;
                unitPriceText3.ForeColor = Color.Purple;
                countTextBox3.ForeColor = Color.Purple;
                purchaseTextBox3.ForeColor = Color.Purple;
                WholesalePriceTextBox3.ForeColor = Color.Purple;
                remark3.ForeColor = Color.Purple;
                BuyerTextBox3.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox3.Checked && !ItemNameChangeCheckBox3.Checked)
            {
                itemMainCategoryTextBox3.ForeColor = Color.Red;
                itemCategoryTextBox3.ForeColor = Color.Red;
                itemDetailTextBox3.ForeColor = Color.Red;
                weightTextBox3.ForeColor = Color.Red;
                unitPriceText3.ForeColor = Color.Red;
                countTextBox3.ForeColor = Color.Red;
                purchaseTextBox3.ForeColor = Color.Red;
                WholesalePriceTextBox3.ForeColor = Color.Red;
                remark3.ForeColor = Color.Red;
                BuyerTextBox3.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox3.Checked && !ItemNameChangeCheckBox3.Checked)
            {
                itemMainCategoryTextBox3.ForeColor = Color.Black;
                itemCategoryTextBox3.ForeColor = Color.Black;
                itemDetailTextBox3.ForeColor = Color.Black;
                weightTextBox3.ForeColor = Color.Black;
                unitPriceText3.ForeColor = Color.Black;
                countTextBox3.ForeColor = Color.Black;
                purchaseTextBox3.ForeColor = Color.Black;
                WholesalePriceTextBox3.ForeColor = Color.Black;
                remark3.ForeColor = Color.Black;
                BuyerTextBox3.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"4"
        private void ItemNameChangeCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox4.Checked && ItemNameChangeCheckBox4.Checked)
            {
                itemMainCategoryTextBox4.ForeColor = Color.Blue;
                itemCategoryTextBox4.ForeColor = Color.Blue;
                itemDetailTextBox4.ForeColor = Color.Blue;
                weightTextBox4.ForeColor = Color.Blue;
                unitPriceText4.ForeColor = Color.Blue;
                countTextBox4.ForeColor = Color.Blue;
                purchaseTextBox4.ForeColor = Color.Blue;
                WholesalePriceTextBox4.ForeColor = Color.Blue;
                remark4.ForeColor = Color.Blue;
                BuyerTextBox4.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox4.Checked && ItemNameChangeCheckBox4.Checked)
            {
                itemMainCategoryTextBox4.ForeColor = Color.Purple;
                itemCategoryTextBox4.ForeColor = Color.Purple;
                itemDetailTextBox4.ForeColor = Color.Purple;
                weightTextBox4.ForeColor = Color.Purple;
                unitPriceText4.ForeColor = Color.Purple;
                countTextBox4.ForeColor = Color.Purple;
                purchaseTextBox4.ForeColor = Color.Purple;
                WholesalePriceTextBox4.ForeColor = Color.Purple;
                remark4.ForeColor = Color.Purple;
                BuyerTextBox4.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox4.Checked && !ItemNameChangeCheckBox4.Checked)
            {
                itemMainCategoryTextBox4.ForeColor = Color.Red;
                itemCategoryTextBox4.ForeColor = Color.Red;
                itemDetailTextBox4.ForeColor = Color.Red;
                weightTextBox4.ForeColor = Color.Red;
                unitPriceText4.ForeColor = Color.Red;
                countTextBox4.ForeColor = Color.Red;
                purchaseTextBox4.ForeColor = Color.Red;
                WholesalePriceTextBox4.ForeColor = Color.Red;
                remark4.ForeColor = Color.Red;
                BuyerTextBox4.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox4.Checked && !ItemNameChangeCheckBox4.Checked)
            {
                itemMainCategoryTextBox4.ForeColor = Color.Black;
                itemCategoryTextBox4.ForeColor = Color.Black;
                itemDetailTextBox4.ForeColor = Color.Black;
                weightTextBox4.ForeColor = Color.Black;
                unitPriceText4.ForeColor = Color.Black;
                countTextBox4.ForeColor = Color.Black;
                purchaseTextBox4.ForeColor = Color.Black;
                WholesalePriceTextBox4.ForeColor = Color.Black;
                remark4.ForeColor = Color.Black;
                BuyerTextBox4.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"5"
        private void ItemNameChangeCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox5.Checked && ItemNameChangeCheckBox5.Checked)
            {
                itemMainCategoryTextBox5.ForeColor = Color.Blue;
                itemCategoryTextBox5.ForeColor = Color.Blue;
                itemDetailTextBox5.ForeColor = Color.Blue;
                weightTextBox5.ForeColor = Color.Blue;
                unitPriceText5.ForeColor = Color.Blue;
                countTextBox5.ForeColor = Color.Blue;
                purchaseTextBox5.ForeColor = Color.Blue;
                WholesalePriceTextBox5.ForeColor = Color.Blue;
                remark5.ForeColor = Color.Blue;
                BuyerTextBox5.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox5.Checked && ItemNameChangeCheckBox5.Checked)
            {
                itemMainCategoryTextBox5.ForeColor = Color.Purple;
                itemCategoryTextBox5.ForeColor = Color.Purple;
                itemDetailTextBox5.ForeColor = Color.Purple;
                weightTextBox5.ForeColor = Color.Purple;
                unitPriceText5.ForeColor = Color.Purple;
                countTextBox5.ForeColor = Color.Purple;
                purchaseTextBox5.ForeColor = Color.Purple;
                WholesalePriceTextBox5.ForeColor = Color.Purple;
                remark5.ForeColor = Color.Purple;
                BuyerTextBox5.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox5.Checked && !ItemNameChangeCheckBox5.Checked)
            {
                itemMainCategoryTextBox5.ForeColor = Color.Red;
                itemCategoryTextBox5.ForeColor = Color.Red;
                itemDetailTextBox5.ForeColor = Color.Red;
                weightTextBox5.ForeColor = Color.Red;
                unitPriceText5.ForeColor = Color.Red;
                countTextBox5.ForeColor = Color.Red;
                purchaseTextBox5.ForeColor = Color.Red;
                WholesalePriceTextBox5.ForeColor = Color.Red;
                remark5.ForeColor = Color.Red;
                BuyerTextBox5.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox5.Checked && !ItemNameChangeCheckBox5.Checked)
            {
                itemMainCategoryTextBox5.ForeColor = Color.Black;
                itemCategoryTextBox5.ForeColor = Color.Black;
                itemDetailTextBox5.ForeColor = Color.Black;
                weightTextBox5.ForeColor = Color.Black;
                unitPriceText5.ForeColor = Color.Black;
                countTextBox5.ForeColor = Color.Black;
                purchaseTextBox5.ForeColor = Color.Black;
                WholesalePriceTextBox5.ForeColor = Color.Black;
                remark5.ForeColor = Color.Black;
                BuyerTextBox5.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"6"
        private void ItemNameChangeCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox6.Checked && ItemNameChangeCheckBox6.Checked)
            {
                itemMainCategoryTextBox6.ForeColor = Color.Blue;
                itemCategoryTextBox6.ForeColor = Color.Blue;
                itemDetailTextBox6.ForeColor = Color.Blue;
                weightTextBox6.ForeColor = Color.Blue;
                unitPriceText6.ForeColor = Color.Blue;
                countTextBox6.ForeColor = Color.Blue;
                purchaseTextBox6.ForeColor = Color.Blue;
                WholesalePriceTextBox6.ForeColor = Color.Blue;
                remark6.ForeColor = Color.Blue;
                BuyerTextBox6.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox6.Checked && ItemNameChangeCheckBox6.Checked)
            {
                itemMainCategoryTextBox6.ForeColor = Color.Purple;
                itemCategoryTextBox6.ForeColor = Color.Purple;
                itemDetailTextBox6.ForeColor = Color.Purple;
                weightTextBox6.ForeColor = Color.Purple;
                unitPriceText6.ForeColor = Color.Purple;
                countTextBox6.ForeColor = Color.Purple;
                purchaseTextBox6.ForeColor = Color.Purple;
                WholesalePriceTextBox6.ForeColor = Color.Purple;
                remark6.ForeColor = Color.Purple;
                BuyerTextBox6.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox6.Checked && !ItemNameChangeCheckBox6.Checked)
            {
                itemMainCategoryTextBox6.ForeColor = Color.Red;
                itemCategoryTextBox6.ForeColor = Color.Red;
                itemDetailTextBox6.ForeColor = Color.Red;
                weightTextBox6.ForeColor = Color.Red;
                unitPriceText6.ForeColor = Color.Red;
                countTextBox6.ForeColor = Color.Red;
                purchaseTextBox6.ForeColor = Color.Red;
                WholesalePriceTextBox6.ForeColor = Color.Red;
                remark6.ForeColor = Color.Red;
                BuyerTextBox6.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox6.Checked && !ItemNameChangeCheckBox6.Checked)
            {
                itemMainCategoryTextBox6.ForeColor = Color.Black;
                itemCategoryTextBox6.ForeColor = Color.Black;
                itemDetailTextBox6.ForeColor = Color.Black;
                weightTextBox6.ForeColor = Color.Black;
                unitPriceText6.ForeColor = Color.Black;
                countTextBox6.ForeColor = Color.Black;
                purchaseTextBox6.ForeColor = Color.Black;
                WholesalePriceTextBox6.ForeColor = Color.Black;
                remark6.ForeColor = Color.Black;
                BuyerTextBox6.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"7"
        private void ItemNameChangeCheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox7.Checked && ItemNameChangeCheckBox7.Checked)
            {
                itemMainCategoryTextBox7.ForeColor = Color.Blue;
                itemCategoryTextBox7.ForeColor = Color.Blue;
                itemDetailTextBox7.ForeColor = Color.Blue;
                weightTextBox7.ForeColor = Color.Blue;
                unitPriceText7.ForeColor = Color.Blue;
                countTextBox7.ForeColor = Color.Blue;
                purchaseTextBox7.ForeColor = Color.Blue;
                WholesalePriceTextBox7.ForeColor = Color.Blue;
                remark7.ForeColor = Color.Blue;
                BuyerTextBox7.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox7.Checked && ItemNameChangeCheckBox7.Checked)
            {
                itemMainCategoryTextBox7.ForeColor = Color.Purple;
                itemCategoryTextBox7.ForeColor = Color.Purple;
                itemDetailTextBox7.ForeColor = Color.Purple;
                weightTextBox7.ForeColor = Color.Purple;
                unitPriceText7.ForeColor = Color.Purple;
                countTextBox7.ForeColor = Color.Purple;
                purchaseTextBox7.ForeColor = Color.Purple;
                WholesalePriceTextBox7.ForeColor = Color.Purple;
                remark7.ForeColor = Color.Purple;
                BuyerTextBox7.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox7.Checked && !ItemNameChangeCheckBox7.Checked)
            {
                itemMainCategoryTextBox7.ForeColor = Color.Red;
                itemCategoryTextBox7.ForeColor = Color.Red;
                itemDetailTextBox7.ForeColor = Color.Red;
                weightTextBox7.ForeColor = Color.Red;
                unitPriceText7.ForeColor = Color.Red;
                countTextBox7.ForeColor = Color.Red;
                purchaseTextBox7.ForeColor = Color.Red;
                WholesalePriceTextBox7.ForeColor = Color.Red;
                remark7.ForeColor = Color.Red;
                BuyerTextBox7.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox7.Checked && !ItemNameChangeCheckBox7.Checked)
            {
                itemMainCategoryTextBox7.ForeColor = Color.Black;
                itemCategoryTextBox7.ForeColor = Color.Black;
                itemDetailTextBox7.ForeColor = Color.Black;
                weightTextBox7.ForeColor = Color.Black;
                unitPriceText7.ForeColor = Color.Black;
                countTextBox7.ForeColor = Color.Black;
                purchaseTextBox7.ForeColor = Color.Black;
                WholesalePriceTextBox7.ForeColor = Color.Black;
                remark7.ForeColor = Color.Black;
                BuyerTextBox7.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"8"
        private void ItemNameChangeCheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox8.Checked && ItemNameChangeCheckBox8.Checked)
            {
                itemMainCategoryTextBox8.ForeColor = Color.Blue;
                itemCategoryTextBox8.ForeColor = Color.Blue;
                itemDetailTextBox8.ForeColor = Color.Blue;
                weightTextBox8.ForeColor = Color.Blue;
                unitPriceText8.ForeColor = Color.Blue;
                countTextBox8.ForeColor = Color.Blue;
                purchaseTextBox8.ForeColor = Color.Blue;
                WholesalePriceTextBox8.ForeColor = Color.Blue;
                remark8.ForeColor = Color.Blue;
                BuyerTextBox8.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox8.Checked && ItemNameChangeCheckBox8.Checked)
            {
                itemMainCategoryTextBox8.ForeColor = Color.Purple;
                itemCategoryTextBox8.ForeColor = Color.Purple;
                itemDetailTextBox8.ForeColor = Color.Purple;
                weightTextBox8.ForeColor = Color.Purple;
                unitPriceText8.ForeColor = Color.Purple;
                countTextBox8.ForeColor = Color.Purple;
                purchaseTextBox8.ForeColor = Color.Purple;
                WholesalePriceTextBox8.ForeColor = Color.Purple;
                remark8.ForeColor = Color.Purple;
                BuyerTextBox8.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox8.Checked && !ItemNameChangeCheckBox8.Checked)
            {
                itemMainCategoryTextBox8.ForeColor = Color.Red;
                itemCategoryTextBox8.ForeColor = Color.Red;
                itemDetailTextBox8.ForeColor = Color.Red;
                weightTextBox8.ForeColor = Color.Red;
                unitPriceText8.ForeColor = Color.Red;
                countTextBox8.ForeColor = Color.Red;
                purchaseTextBox8.ForeColor = Color.Red;
                WholesalePriceTextBox8.ForeColor = Color.Red;
                remark8.ForeColor = Color.Red;
                BuyerTextBox8.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox8.Checked && !ItemNameChangeCheckBox8.Checked)
            {
                itemMainCategoryTextBox8.ForeColor = Color.Black;
                itemCategoryTextBox8.ForeColor = Color.Black;
                itemDetailTextBox8.ForeColor = Color.Black;
                weightTextBox8.ForeColor = Color.Black;
                unitPriceText8.ForeColor = Color.Black;
                countTextBox8.ForeColor = Color.Black;
                purchaseTextBox8.ForeColor = Color.Black;
                WholesalePriceTextBox8.ForeColor = Color.Black;
                remark8.ForeColor = Color.Black;
                BuyerTextBox8.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"9"
        private void ItemNameChangeCheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            //品名
            if (!NextMonthCheckBox9.Checked && ItemNameChangeCheckBox9.Checked)
            {
                itemMainCategoryTextBox9.ForeColor = Color.Blue;
                itemCategoryTextBox9.ForeColor = Color.Blue;
                itemDetailTextBox9.ForeColor = Color.Blue;
                weightTextBox9.ForeColor = Color.Blue;
                unitPriceText9.ForeColor = Color.Blue;
                countTextBox9.ForeColor = Color.Blue;
                purchaseTextBox9.ForeColor = Color.Blue;
                WholesalePriceTextBox9.ForeColor = Color.Blue;
                remark9.ForeColor = Color.Blue;
                BuyerTextBox9.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox9.Checked && ItemNameChangeCheckBox9.Checked)
            {
                itemMainCategoryTextBox9.ForeColor = Color.Purple;
                itemCategoryTextBox9.ForeColor = Color.Purple;
                itemDetailTextBox9.ForeColor = Color.Purple;
                weightTextBox9.ForeColor = Color.Purple;
                unitPriceText9.ForeColor = Color.Purple;
                countTextBox9.ForeColor = Color.Purple;
                purchaseTextBox9.ForeColor = Color.Purple;
                WholesalePriceTextBox9.ForeColor = Color.Purple;
                remark9.ForeColor = Color.Purple;
                BuyerTextBox9.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox9.Checked && !ItemNameChangeCheckBox9.Checked)
            {
                itemMainCategoryTextBox9.ForeColor = Color.Red;
                itemCategoryTextBox9.ForeColor = Color.Red;
                itemDetailTextBox9.ForeColor = Color.Red;
                weightTextBox9.ForeColor = Color.Red;
                unitPriceText9.ForeColor = Color.Red;
                countTextBox9.ForeColor = Color.Red;
                purchaseTextBox9.ForeColor = Color.Red;
                WholesalePriceTextBox9.ForeColor = Color.Red;
                remark9.ForeColor = Color.Red;
                BuyerTextBox9.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox9.Checked && !ItemNameChangeCheckBox9.Checked)
            {
                itemMainCategoryTextBox9.ForeColor = Color.Black;
                itemCategoryTextBox9.ForeColor = Color.Black;
                itemDetailTextBox9.ForeColor = Color.Black;
                weightTextBox9.ForeColor = Color.Black;
                unitPriceText9.ForeColor = Color.Black;
                countTextBox9.ForeColor = Color.Black;
                purchaseTextBox9.ForeColor = Color.Black;
                WholesalePriceTextBox9.ForeColor = Color.Black;
                remark9.ForeColor = Color.Black;
                BuyerTextBox9.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"10"
        private void ItemNameChangeCheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox10.Checked && ItemNameChangeCheckBox10.Checked)
            {
                itemMainCategoryTextBox10.ForeColor = Color.Blue;
                itemCategoryTextBox10.ForeColor = Color.Blue;
                itemDetailTextBox10.ForeColor = Color.Blue;
                weightTextBox10.ForeColor = Color.Blue;
                unitPriceText10.ForeColor = Color.Blue;
                countTextBox10.ForeColor = Color.Blue;
                purchaseTextBox10.ForeColor = Color.Blue;
                WholesalePriceTextBox10.ForeColor = Color.Blue;
                remark10.ForeColor = Color.Blue;
                BuyerTextBox10.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox10.Checked && ItemNameChangeCheckBox10.Checked)
            {
                itemMainCategoryTextBox10.ForeColor = Color.Purple;
                itemCategoryTextBox10.ForeColor = Color.Purple;
                itemDetailTextBox10.ForeColor = Color.Purple;
                weightTextBox10.ForeColor = Color.Purple;
                unitPriceText10.ForeColor = Color.Purple;
                countTextBox10.ForeColor = Color.Purple;
                purchaseTextBox10.ForeColor = Color.Purple;
                WholesalePriceTextBox10.ForeColor = Color.Purple;
                remark10.ForeColor = Color.Purple;
                BuyerTextBox10.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox10.Checked && !ItemNameChangeCheckBox10.Checked)
            {
                itemMainCategoryTextBox10.ForeColor = Color.Red;
                itemCategoryTextBox10.ForeColor = Color.Red;
                itemDetailTextBox10.ForeColor = Color.Red;
                weightTextBox10.ForeColor = Color.Red;
                unitPriceText10.ForeColor = Color.Red;
                countTextBox10.ForeColor = Color.Red;
                purchaseTextBox10.ForeColor = Color.Red;
                WholesalePriceTextBox10.ForeColor = Color.Red;
                remark10.ForeColor = Color.Red;
                BuyerTextBox10.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox10.Checked && !ItemNameChangeCheckBox10.Checked)
            {
                itemMainCategoryTextBox10.ForeColor = Color.Black;
                itemCategoryTextBox10.ForeColor = Color.Black;
                itemDetailTextBox10.ForeColor = Color.Black;
                weightTextBox10.ForeColor = Color.Black;
                unitPriceText10.ForeColor = Color.Black;
                countTextBox10.ForeColor = Color.Black;
                purchaseTextBox10.ForeColor = Color.Black;
                WholesalePriceTextBox10.ForeColor = Color.Black;
                remark10.ForeColor = Color.Black;
                BuyerTextBox10.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"11"
        private void ItemNameChangeCheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox11.Checked && ItemNameChangeCheckBox11.Checked)
            {
                itemMainCategoryTextBox11.ForeColor = Color.Blue;
                itemCategoryTextBox11.ForeColor = Color.Blue;
                itemDetailTextBox11.ForeColor = Color.Blue;
                weightTextBox11.ForeColor = Color.Blue;
                unitPriceText11.ForeColor = Color.Blue;
                countTextBox11.ForeColor = Color.Blue;
                purchaseTextBox11.ForeColor = Color.Blue;
                WholesalePriceTextBox11.ForeColor = Color.Blue;
                remark11.ForeColor = Color.Blue;
                BuyerTextBox11.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox11.Checked && ItemNameChangeCheckBox11.Checked)
            {
                itemMainCategoryTextBox11.ForeColor = Color.Purple;
                itemCategoryTextBox11.ForeColor = Color.Purple;
                itemDetailTextBox11.ForeColor = Color.Purple;
                weightTextBox11.ForeColor = Color.Purple;
                unitPriceText11.ForeColor = Color.Purple;
                countTextBox11.ForeColor = Color.Purple;
                purchaseTextBox11.ForeColor = Color.Purple;
                WholesalePriceTextBox11.ForeColor = Color.Purple;
                remark11.ForeColor = Color.Purple;
                BuyerTextBox11.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox11.Checked && !ItemNameChangeCheckBox11.Checked)
            {
                itemMainCategoryTextBox11.ForeColor = Color.Red;
                itemCategoryTextBox11.ForeColor = Color.Red;
                itemDetailTextBox11.ForeColor = Color.Red;
                weightTextBox11.ForeColor = Color.Red;
                unitPriceText11.ForeColor = Color.Red;
                countTextBox11.ForeColor = Color.Red;
                purchaseTextBox11.ForeColor = Color.Red;
                WholesalePriceTextBox11.ForeColor = Color.Red;
                remark11.ForeColor = Color.Red;
                BuyerTextBox11.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox11.Checked && !ItemNameChangeCheckBox11.Checked)
            {
                itemMainCategoryTextBox11.ForeColor = Color.Black;
                itemCategoryTextBox11.ForeColor = Color.Black;
                itemDetailTextBox11.ForeColor = Color.Black;
                weightTextBox11.ForeColor = Color.Black;
                unitPriceText11.ForeColor = Color.Black;
                countTextBox11.ForeColor = Color.Black;
                purchaseTextBox11.ForeColor = Color.Black;
                WholesalePriceTextBox11.ForeColor = Color.Black;
                remark11.ForeColor = Color.Black;
                BuyerTextBox11.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"12"
        private void ItemNameChangeCheckBox12_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox12.Checked && ItemNameChangeCheckBox12.Checked)
            {
                itemMainCategoryTextBox12.ForeColor = Color.Blue;
                itemCategoryTextBox12.ForeColor = Color.Blue;
                itemDetailTextBox12.ForeColor = Color.Blue;
                weightTextBox12.ForeColor = Color.Blue;
                unitPriceText12.ForeColor = Color.Blue;
                countTextBox12.ForeColor = Color.Blue;
                purchaseTextBox12.ForeColor = Color.Blue;
                WholesalePriceTextBox12.ForeColor = Color.Blue;
                remark12.ForeColor = Color.Blue;
                BuyerTextBox12.ForeColor = Color.Blue;
            }
            //次月・品名両方チェック
            if (NextMonthCheckBox12.Checked && ItemNameChangeCheckBox12.Checked)
            {
                itemMainCategoryTextBox12.ForeColor = Color.Purple;
                itemCategoryTextBox12.ForeColor = Color.Purple;
                itemDetailTextBox12.ForeColor = Color.Purple;
                weightTextBox12.ForeColor = Color.Purple;
                unitPriceText12.ForeColor = Color.Purple;
                countTextBox12.ForeColor = Color.Purple;
                purchaseTextBox12.ForeColor = Color.Purple;
                WholesalePriceTextBox12.ForeColor = Color.Purple;
                remark12.ForeColor = Color.Purple;
                BuyerTextBox12.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox12.Checked && !ItemNameChangeCheckBox12.Checked)
            {
                itemMainCategoryTextBox12.ForeColor = Color.Red;
                itemCategoryTextBox12.ForeColor = Color.Red;
                itemDetailTextBox12.ForeColor = Color.Red;
                weightTextBox12.ForeColor = Color.Red;
                unitPriceText12.ForeColor = Color.Red;
                countTextBox12.ForeColor = Color.Red;
                purchaseTextBox12.ForeColor = Color.Red;
                WholesalePriceTextBox12.ForeColor = Color.Red;
                remark12.ForeColor = Color.Red;
                BuyerTextBox12.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox12.Checked && !ItemNameChangeCheckBox12.Checked)
            {
                itemMainCategoryTextBox12.ForeColor = Color.Black;
                itemCategoryTextBox12.ForeColor = Color.Black;
                itemDetailTextBox12.ForeColor = Color.Black;
                weightTextBox12.ForeColor = Color.Black;
                unitPriceText12.ForeColor = Color.Black;
                countTextBox12.ForeColor = Color.Black;
                purchaseTextBox12.ForeColor = Color.Black;
                WholesalePriceTextBox12.ForeColor = Color.Black;
                remark12.ForeColor = Color.Black;
                BuyerTextBox12.ForeColor = Color.Black;
            }
        }
        #endregion
        #region"13"
        private void ItemNameChangeCheckBox13_CheckedChanged(object sender, EventArgs e)
        {
            //品名のみ
            if (!NextMonthCheckBox13.Checked && ItemNameChangeCheckBox13.Checked)
            {
                itemMainCategoryTextBox13.ForeColor = Color.Blue;
                itemCategoryTextBox13.ForeColor = Color.Blue;
                itemDetailTextBox13.ForeColor = Color.Blue;
                weightTextBox13.ForeColor = Color.Blue;
                unitPriceText13.ForeColor = Color.Blue;
                countTextBox13.ForeColor = Color.Blue;
                purchaseTextBox13.ForeColor = Color.Blue;
                WholesalePriceTextBox13.ForeColor = Color.Blue;
                remark13.ForeColor = Color.Blue;
                BuyerTextBox13.ForeColor = Color.Blue;

            }
            //次月・品名両方チェック
            if (NextMonthCheckBox13.Checked && ItemNameChangeCheckBox13.Checked)
            {
                itemMainCategoryTextBox13.ForeColor = Color.Purple;
                itemCategoryTextBox13.ForeColor = Color.Purple;
                itemDetailTextBox13.ForeColor = Color.Purple;
                weightTextBox13.ForeColor = Color.Purple;
                unitPriceText13.ForeColor = Color.Purple;
                countTextBox13.ForeColor = Color.Purple;
                purchaseTextBox13.ForeColor = Color.Purple;
                WholesalePriceTextBox13.ForeColor = Color.Purple;
                remark13.ForeColor = Color.Purple;
                BuyerTextBox13.ForeColor = Color.Purple;
            }
            //次月のみチェック
            if (NextMonthCheckBox13.Checked && !ItemNameChangeCheckBox13.Checked)
            {
                itemMainCategoryTextBox13.ForeColor = Color.Red;
                itemCategoryTextBox13.ForeColor = Color.Red;
                itemDetailTextBox13.ForeColor = Color.Red;
                weightTextBox13.ForeColor = Color.Red;
                unitPriceText13.ForeColor = Color.Red;
                countTextBox13.ForeColor = Color.Red;
                purchaseTextBox13.ForeColor = Color.Red;
                WholesalePriceTextBox13.ForeColor = Color.Red;
                remark13.ForeColor = Color.Red;
                BuyerTextBox13.ForeColor = Color.Red;
            }
            //空
            if (!NextMonthCheckBox13.Checked && !ItemNameChangeCheckBox13.Checked)
            {
                itemMainCategoryTextBox13.ForeColor = Color.Black;
                itemCategoryTextBox13.ForeColor = Color.Black;
                itemDetailTextBox13.ForeColor = Color.Black;
                weightTextBox13.ForeColor = Color.Black;
                unitPriceText13.ForeColor = Color.Black;
                countTextBox13.ForeColor = Color.Black;
                purchaseTextBox13.ForeColor = Color.Black;
                WholesalePriceTextBox13.ForeColor = Color.Black;
                remark13.ForeColor = Color.Black;
                BuyerTextBox13.ForeColor = Color.Black;
            }
        }
        #endregion
        #endregion

        #endregion

        #region"登録ボタンクリック"
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            #region"次月と品名変更の両方にチェックが入ってることを通知"
            for (int i = 1; i <= record; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        if (itemMainCategoryTextBox1.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("１行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        if (itemMainCategoryTextBox2.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("２行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        if (itemMainCategoryTextBox3.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("３行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        if (itemMainCategoryTextBox4.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("４行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        if (itemMainCategoryTextBox5.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("５行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        if (itemMainCategoryTextBox6.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("６行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        if (itemMainCategoryTextBox7.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("７行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        if (itemMainCategoryTextBox8.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("８行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        if (itemMainCategoryTextBox9.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("９行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        if (itemMainCategoryTextBox10.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("１０行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        if (itemMainCategoryTextBox11.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("１１行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        if (itemMainCategoryTextBox12.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("１２行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        if (itemMainCategoryTextBox13.ForeColor == Color.Purple)
                        {
                            MessageBox.Show("１３行目に「次月持ち越し」と「品名変更」の両方にチェックが入っています" + "\r\n" + "入力項目を確認してください", "入力内容をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                        #endregion
                }
            }
            #endregion

            #region"次月にチェックが入っていて卸値にも入力されているとき"
            for (int i = 1; i <= record; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        if (WholesalePriceTextBox1.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox1.Text) && WholesalePriceTextBox1.Text != @"\0") 
                        {
                            MessageBox.Show("１行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        if (WholesalePriceTextBox2.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox2.Text) && WholesalePriceTextBox2.Text != @"\0")
                        {
                            MessageBox.Show("２行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        if (WholesalePriceTextBox3.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox3.Text) && WholesalePriceTextBox3.Text != @"\0")
                        {
                            MessageBox.Show("３行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        if (WholesalePriceTextBox4.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox4.Text) && WholesalePriceTextBox4.Text != @"\0")
                        {
                            MessageBox.Show("４行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        if (WholesalePriceTextBox5.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox5.Text) && WholesalePriceTextBox5.Text != @"\0")
                        {
                            MessageBox.Show("５行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        if (WholesalePriceTextBox6.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox6.Text) && WholesalePriceTextBox6.Text != @"\0")
                        {
                            MessageBox.Show("６行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        if (WholesalePriceTextBox7.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox7.Text) && WholesalePriceTextBox7.Text != @"\0")
                        {
                            MessageBox.Show("７行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        if (WholesalePriceTextBox8.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox8.Text) && WholesalePriceTextBox8.Text != @"\0")
                        {
                            MessageBox.Show("８行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        if (WholesalePriceTextBox9.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox9.Text) && WholesalePriceTextBox9.Text != @"\0")
                        {
                            MessageBox.Show("９行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        if (WholesalePriceTextBox10.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox10.Text) && WholesalePriceTextBox10.Text != @"\0")
                        {
                            MessageBox.Show("１０行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        if (WholesalePriceTextBox11.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox11.Text) && WholesalePriceTextBox11.Text != @"\0")
                        {
                            MessageBox.Show("１１行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        if (WholesalePriceTextBox12.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox12.Text) && WholesalePriceTextBox12.Text != @"\0")
                        {
                            MessageBox.Show("１２行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        if (WholesalePriceTextBox13.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox13.Text) && WholesalePriceTextBox13.Text != @"\0")
                        {
                            MessageBox.Show("１３行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                        #endregion
                }
            }
            #endregion

            #region"品名変更のみチェックが入っているとき"
            for (int i = 1; i <= record; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        if (itemMainCategoryTextBox1.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("１行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        if (itemMainCategoryTextBox2.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("２行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        if (itemMainCategoryTextBox3.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("３行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        if (itemMainCategoryTextBox4.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("４行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        if (itemMainCategoryTextBox5.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("５行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        if (itemMainCategoryTextBox6.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("６行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        if (itemMainCategoryTextBox7.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("７行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        if (itemMainCategoryTextBox8.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("８行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        if (itemMainCategoryTextBox9.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("９行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        if (itemMainCategoryTextBox10.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("１０行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        if (itemMainCategoryTextBox11.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("１１行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        if (itemMainCategoryTextBox12.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("１２行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        if (itemMainCategoryTextBox13.ForeColor == Color.Blue)
                        {
                            result = MessageBox.Show("１３行目の品名変更にチェックが入っています" + "\r\n" + "このままでよろしいですか？", "入力項目確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                        #endregion
                }
            }
            #endregion

            #region"卸値が無記入で次月持ち越しにチェックが入っていないとき"
            for (int i = 1; i <= record; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        if (!NextMonthCheckBox1.Checked && string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
                        {
                            MessageBox.Show("１行目の卸値が無記入ですが" + "\r\n" + "１行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        if (!NextMonthCheckBox2.Checked && string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                        {
                            MessageBox.Show("２行目の卸値が無記入ですが" + "\r\n" + "２行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        if (!NextMonthCheckBox3.Checked && string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                        {
                            MessageBox.Show("３行目の卸値が無記入ですが" + "\r\n" + "３行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        if (!NextMonthCheckBox4.Checked && string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                        {
                            MessageBox.Show("４行目の卸値が無記入ですが" + "\r\n" + "４行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        if (!NextMonthCheckBox5.Checked && string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                        {
                            MessageBox.Show("５行目の卸値が無記入ですが" + "\r\n" + "５行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        if (!NextMonthCheckBox6.Checked && string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                        {
                            MessageBox.Show("６行目の卸値が無記入ですが" + "\r\n" + "６行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        if (!NextMonthCheckBox7.Checked && string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                        {
                            MessageBox.Show("７行目の卸値が無記入ですが" + "\r\n" + "７行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        if (!NextMonthCheckBox8.Checked && string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                        {
                            MessageBox.Show("８行目の卸値が無記入ですが" + "\r\n" + "８行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        if (!NextMonthCheckBox9.Checked && string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                        {
                            MessageBox.Show("９行目の卸値が無記入ですが" + "\r\n" + "９行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        if (!NextMonthCheckBox10.Checked && string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                        {
                            MessageBox.Show("１０行目の卸値が無記入ですが" + "\r\n" + "１０行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        if (!NextMonthCheckBox11.Checked && string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                        {
                            MessageBox.Show("１１行目の卸値が無記入ですが" + "\r\n" + "１１行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        if (!NextMonthCheckBox12.Checked && string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                        {
                            MessageBox.Show("１２行目の卸値が無記入ですが" + "\r\n" + "１２行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        if (!NextMonthCheckBox13.Checked && string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                        {
                            MessageBox.Show("１３行目の卸値が無記入ですが" + "\r\n" + "１３行目の次月持ち越しにチェックが入っておりません。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                        #endregion
                }
            }
            #endregion

            #region"卸値に値が入力されていて売却先が無記入のとき"
            for (int i = 1; i <= record; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox1.Text) && string.IsNullOrEmpty(BuyerTextBox1.Text))
                        {
                            MessageBox.Show("１行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox2.Text) && string.IsNullOrEmpty(BuyerTextBox2.Text))
                        {
                            MessageBox.Show("２行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox3.Text) && string.IsNullOrEmpty(BuyerTextBox3.Text))
                        {
                            MessageBox.Show("３行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox4.Text) && string.IsNullOrEmpty(BuyerTextBox4.Text))
                        {
                            MessageBox.Show("４行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox5.Text) && string.IsNullOrEmpty(BuyerTextBox5.Text))
                        {
                            MessageBox.Show("５行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox6.Text) && string.IsNullOrEmpty(BuyerTextBox6.Text))
                        {
                            MessageBox.Show("６行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox7.Text) && string.IsNullOrEmpty(BuyerTextBox7.Text))
                        {
                            MessageBox.Show("７行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox8.Text) && string.IsNullOrEmpty(BuyerTextBox8.Text))
                        {
                            MessageBox.Show("８行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox9.Text) && string.IsNullOrEmpty(BuyerTextBox9.Text))
                        {
                            MessageBox.Show("９行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox10.Text) && string.IsNullOrEmpty(BuyerTextBox10.Text))
                        {
                            MessageBox.Show("１０行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox11.Text) && string.IsNullOrEmpty(BuyerTextBox11.Text))
                        {
                            MessageBox.Show("１１行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox12.Text) && string.IsNullOrEmpty(BuyerTextBox12.Text))
                        {
                            MessageBox.Show("１２行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox13.Text) && string.IsNullOrEmpty(BuyerTextBox13.Text))
                        {
                            MessageBox.Show("１３行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                     #endregion
                }
            }
            #endregion

            if (NotFinish)
            {
                DialogResult dialogResult = MessageBox.Show("登録しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            #region"登録・再登録のコード"
            DateTime date = DateTime.Now;
            Registration = date.ToLongDateString();         //登録日

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            string sql_str = "select * from list_result order by result;";
            cmd = new NpgsqlCommand(sql_str, conn);
            conn.Open();
            string DocumentNumber = "";

            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DocumentNumber = reader["document_number"].ToString();
                }
            }

            if (DocumentNumber == SlipNumber || CarryOver)    //一度登録済み・再登録
            {
                #region"list_resultへの更新"
                TotalPurchase = PurChase;                   //合計買取金額
                TotalWholesale = WholeSale;                 //合計卸値
                TotalProfit = WholeSale - PurChase;         //合計利益
                DNumber = SlipNumber;                       //伝票番号

                #region"合計金額がnull or \0 のとき変数に 0 を代入"
                //地金
                if (string.IsNullOrEmpty(MetalPurchaseTextBox.Text) || MetalPurchaseTextBox.Text == "\0")
                {
                    PurChaseMetal = 0;
                }
                if (string.IsNullOrEmpty(MetalWholesaleTextBox.Text) || MetalWholesaleTextBox.Text == "\0")
                {
                    WholeSaleMetal = 0;
                }
                //ダイヤ
                if (string.IsNullOrEmpty(DiamondPurchaseTextBox.Text) || DiamondPurchaseTextBox.Text == "\0")
                {
                    PurChaseDiamond = 0;
                }
                if (string.IsNullOrEmpty(DiamondWholesaleTextBox.Text) || DiamondWholesaleTextBox.Text == "\0")
                {
                    WholeSaleDiamond = 0;
                }
                //ブランド
                if (string.IsNullOrEmpty(BrandPurchaseTextBox.Text) || BrandPurchaseTextBox.Text == "\0")
                {
                    PurChaseBrand = 0;
                }
                if (string.IsNullOrEmpty(BrandWholesaleTextBox.Text) || BrandWholesaleTextBox.Text == "\0")
                {
                    WholeSaleBrand = 0;
                }
                //製品・ジュエリー
                if (string.IsNullOrEmpty(ProductPurchaseTextBox.Text) || ProductPurchaseTextBox.Text == "\0")
                {
                    PurChaseProduct = 0;
                }
                if (string.IsNullOrEmpty(ProductWholesaleTextBox.Text) || ProductWholesaleTextBox.Text == "\0")
                {
                    WholeSaleProduct = 0;
                }
                //その他
                if (string.IsNullOrEmpty(OtherPurchaseTextBox.Text) || OtherPurchaseTextBox.Text == "\0")
                {
                    PurChaseOther = 0;
                }
                if (string.IsNullOrEmpty(OtherWholesaleTextBox.Text) || OtherWholesaleTextBox.Text == "\0")
                {
                    WholeSaleOther = 0;
                }
                #endregion

                MetalPurchase = PurChaseMetal;              //地金買取額
                MetalWholesale = WholeSaleMetal;            //地金卸値
                MetalProfit = ProFitMetal;                  //地金利益
                DiamondPurchase = PurChaseDiamond;          //ダイヤ買取額
                DiamondWholesale = WholeSaleDiamond;        //ダイヤ卸値
                DiamondProfit = ProFitDiamond;              //ダイヤ利益
                BrandPurchase = PurChaseBrand;              //ブランド買取額
                BrandWholesale = WholeSaleBrand;            //ブランド卸値
                BrandProfit = ProFitBrand;                  //ブランド利益
                ProductPurchase = PurChaseProduct;          //製品買取額
                ProductWholesale = WholeSaleProduct;        //製品卸値
                ProductProfit = ProFitProduct;              //製品利益
                OtherPurchase = PurChaseOther;              //その他買取額
                OtherWholesale = WholeSaleOther;            //その他卸値
                OtherProfit = ProFitOther;                  //その他利益
                GradeNumber = int.Parse(GradeNumberTextBox.Text);   //成績番号

                using (transaction = conn.BeginTransaction())
                {
                    sql_str = "update list_result set sum_money = '" + TotalPurchase + "', sum_wholesale_price = '" + TotalWholesale + "', profit = '" + TotalProfit + "', metal_purchase = '" + MetalPurchase + "', metal_wholesale = '" + MetalWholesale + "', metal_profit = '" + MetalProfit + "', diamond_purchase = '" + DiamondPurchase + "', diamond_wholesale = '" + DiamondWholesale + "', diamond_profit = '" + DiamondProfit + "', brand_purchase = '" + BrandPurchase + "', brand_wholesale = '" + BrandWholesale + "', brand_profit = '" + BrandProfit + "', product_purchase = '" + ProductPurchase + "', product_wholesale = '" + ProductWholesale + "', product_profit = '" + ProductProfit + "', other_purchase = '" + OtherPurchase + "', other_wholesale = '" + OtherWholesale + "', other_profit = '" + OtherProfit + "' where document_number = '" + DNumber + "';";
                    cmd = new NpgsqlCommand(sql_str, conn);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                //履歴に登録
                
                sql_str = "Insert into list_result_revisions values ('" + type + "', '" + staff_id + "','" + Registration + "','" + GradeNumber + "', '" + TotalPurchase + "','" + TotalWholesale + "','" + TotalProfit + "','" + DNumber + "','" + MetalPurchase + "','" + MetalWholesale + "','" + MetalProfit + "','" + DiamondPurchase + "','" + DiamondWholesale + "','" + DiamondProfit + "','" + BrandPurchase + "','" + BrandWholesale + "','" + BrandProfit + "','" + ProductPurchase + "','" + ProductWholesale + "','" + ProductProfit + "','" + OtherPurchase + "','" + OtherWholesale + "','" + OtherProfit + "');";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(DATATable);

                #endregion
                #region"list_result2への更新"
                for (int i = 1; i <= record; i++)
                {
                    switch (i)
                    {
                        #region"１行目"
                        case 1:
                            Record = 1;
                            MainCategoryCode = MainCategoryCode1;
                            ItemCategoryCode = ItemCategoryCode1;
                            Wholesale = WholeSaleUnFormat1;
                            Remark = remark1.Text;
                            Profit = WholeSaleUnFormat1 - PurchaseUnFormat1;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox1.Text) || WholesalePriceTextBox1.Text != @"\0") 
                            {
                                SaleDate = BuyDateTimePicker1.Value.ToLongDateString();
                                Buyer = BuyerTextBox1.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox1.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox1.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA1);

                            break;
                        #endregion
                        #region"２行目"
                        case 2:
                            Record = 2;
                            MainCategoryCode = MainCategoryCode2;
                            ItemCategoryCode = ItemCategoryCode2;
                            Wholesale = WholeSaleUnFormat2;
                            Remark = remark2.Text;
                            Profit = WholeSaleUnFormat2 - PurchaseUnFormat2;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox2.Text)|| WholesalePriceTextBox2.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker2.Value.ToLongDateString();
                                Buyer = BuyerTextBox2.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox2.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox2.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA2);

                            break;
                        #endregion
                        #region"３行目"
                        case 3:
                            Record = 3;
                            MainCategoryCode = MainCategoryCode3;
                            ItemCategoryCode = ItemCategoryCode3;
                            Wholesale = WholeSaleUnFormat3;
                            Remark = remark3.Text;
                            Profit = WholeSaleUnFormat3 - PurchaseUnFormat3;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox3.Text) || WholesalePriceTextBox3.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker3.Value.ToLongDateString();
                                Buyer = BuyerTextBox3.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox3.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox3.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA3);

                            break;
                        #endregion
                        #region"４行目"
                        case 4:
                            Record = 4;
                            MainCategoryCode = MainCategoryCode4;
                            ItemCategoryCode = ItemCategoryCode4;
                            Wholesale = WholeSaleUnFormat4;
                            Remark = remark4.Text;
                            Profit = WholeSaleUnFormat4 - PurchaseUnFormat4;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox4.Text) || WholesalePriceTextBox4.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker4.Value.ToLongDateString();
                                Buyer = BuyerTextBox4.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox4.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox4.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA4);

                            break;
                        #endregion
                        #region"５行目"
                        case 5:
                            Record = 5;
                            MainCategoryCode = MainCategoryCode5;
                            ItemCategoryCode = ItemCategoryCode5;
                            Wholesale = WholeSaleUnFormat5;
                            Remark = remark5.Text;
                            Profit = WholeSaleUnFormat5 - PurchaseUnFormat5;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox5.Text) || WholesalePriceTextBox5.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker5.Value.ToLongDateString();
                                Buyer = BuyerTextBox5.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox5.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox5.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA5);

                            break;
                        #endregion
                        #region"６行目"
                        case 6:
                            Record = 6;
                            MainCategoryCode = MainCategoryCode6;
                            ItemCategoryCode = ItemCategoryCode6;
                            Wholesale = WholeSaleUnFormat6;
                            Remark = remark6.Text;
                            Profit = WholeSaleUnFormat6 - PurchaseUnFormat6;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox6.Text) || WholesalePriceTextBox6.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker6.Value.ToLongDateString();
                                Buyer = BuyerTextBox6.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox6.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox6.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA6);

                            break;
                        #endregion
                        #region"７行目"
                        case 7:
                            Record = 7;
                            MainCategoryCode = MainCategoryCode7;
                            ItemCategoryCode = ItemCategoryCode7;
                            Wholesale = WholeSaleUnFormat7;
                            Remark = remark7.Text;
                            Profit = WholeSaleUnFormat7 - PurchaseUnFormat7;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox7.Text) || WholesalePriceTextBox7.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker7.Value.ToLongDateString();
                                Buyer = BuyerTextBox7.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox7.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox7.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA7);

                            break;
                        #endregion
                        #region"８行目"
                        case 8:
                            Record = 8;
                            MainCategoryCode = MainCategoryCode8;
                            ItemCategoryCode = ItemCategoryCode8;
                            Wholesale = WholeSaleUnFormat8;
                            Remark = remark8.Text;
                            Profit = WholeSaleUnFormat8 - PurchaseUnFormat8;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox8.Text) || WholesalePriceTextBox8.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker8.Value.ToLongDateString();
                                Buyer = BuyerTextBox8.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox8.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox8.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA8);

                            break;
                        #endregion
                        #region"９行目"
                        case 9:
                            Record = 9;
                            MainCategoryCode = MainCategoryCode9;
                            ItemCategoryCode = ItemCategoryCode9;
                            Wholesale = WholeSaleUnFormat9;
                            Remark = remark9.Text;
                            Profit = WholeSaleUnFormat9 - PurchaseUnFormat9;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox9.Text) || WholesalePriceTextBox9.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker9.Value.ToLongDateString();
                                Buyer = BuyerTextBox9.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox9.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox9.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA9);

                            break;
                        #endregion
                        #region"１０行目"
                        case 10:
                            Record = 10;
                            MainCategoryCode = MainCategoryCode10;
                            ItemCategoryCode = ItemCategoryCode10;
                            Wholesale = WholeSaleUnFormat10;
                            Remark = remark10.Text;
                            Profit = WholeSaleUnFormat10 - PurchaseUnFormat10;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox10.Text) || WholesalePriceTextBox10.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker10.Value.ToLongDateString();
                                Buyer = BuyerTextBox10.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox10.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox10.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA10);

                            break;
                        #endregion
                        #region"１１行目"
                        case 11:
                            Record = 11;
                            MainCategoryCode = MainCategoryCode11;
                            ItemCategoryCode = ItemCategoryCode11;
                            Wholesale = WholeSaleUnFormat1;
                            Remark = remark11.Text;
                            Profit = WholeSaleUnFormat11 - PurchaseUnFormat11;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox11.Text) || WholesalePriceTextBox11.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker11.Value.ToLongDateString();
                                Buyer = BuyerTextBox11.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox11.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox11.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA11);

                            break;
                        #endregion
                        #region"１２行目"
                        case 12:
                            Record = 12;
                            MainCategoryCode = MainCategoryCode12;
                            ItemCategoryCode = ItemCategoryCode12;
                            Wholesale = WholeSaleUnFormat12;
                            Remark = remark12.Text;
                            Profit = WholeSaleUnFormat12 - PurchaseUnFormat12;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox12.Text) || WholesalePriceTextBox12.Text != @"\0")
                            {
                                SaleDate = BuyDateTimePicker12.Value.ToLongDateString();
                                Buyer = BuyerTextBox12.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox12.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox12.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA12);

                            break;
                        #endregion
                        #region"１３行目"
                        case 13:
                            Record = 13;
                            MainCategoryCode = MainCategoryCode13;
                            ItemCategoryCode = ItemCategoryCode13;
                            Wholesale = WholeSaleUnFormat13;
                            Remark = remark13.Text;
                            Profit = WholeSaleUnFormat13 - PurchaseUnFormat13;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox13.Text) || WholesalePriceTextBox13.Text != @"\0") 
                            {
                                SaleDate = BuyDateTimePicker13.Value.ToLongDateString();
                                Buyer = BuyerTextBox13.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox13.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox13.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA13);

                            break;
                            #endregion
                    }
                }
                #endregion
            }
            //初登録
            else
            {
                #region"list_result への登録と履歴への登録"
                GradeNumber = int.Parse(GradeNumberTextBox.Text);       //成績番号
                TotalPurchase = PurChase;                               //合計買取金額
                TotalWholesale = WholeSale;                             //合計卸値
                TotalProfit = WholeSale - PurChase;                     //合計利益
                DNumber = SlipNumber;                                   //伝票番号

                #region"合計金額がnull or \0 のとき変数に 0 を代入"
                //地金
                if (string.IsNullOrEmpty(MetalPurchaseTextBox.Text) || MetalPurchaseTextBox.Text == "\0")
                {
                    PurChaseMetal = 0;
                }
                if (string.IsNullOrEmpty(MetalWholesaleTextBox.Text) || MetalWholesaleTextBox.Text == "\0")
                {
                    WholeSaleMetal = 0;
                }
                //ダイヤ
                if (string.IsNullOrEmpty(DiamondPurchaseTextBox.Text) || DiamondPurchaseTextBox.Text == "\0")
                {
                    PurChaseDiamond = 0;
                }
                if (string.IsNullOrEmpty(DiamondWholesaleTextBox.Text) || DiamondWholesaleTextBox.Text == "\0")
                {
                    WholeSaleDiamond = 0;
                }
                //ブランド
                if (string.IsNullOrEmpty(BrandPurchaseTextBox.Text) || BrandPurchaseTextBox.Text == "\0")
                {
                    PurChaseBrand = 0;
                }
                if (string.IsNullOrEmpty(BrandWholesaleTextBox.Text) || BrandWholesaleTextBox.Text == "\0")
                {
                    WholeSaleBrand = 0;
                }
                //製品・ジュエリー
                if (string.IsNullOrEmpty(ProductPurchaseTextBox.Text) || ProductPurchaseTextBox.Text == "\0")
                {
                    PurChaseProduct = 0;
                }
                if (string.IsNullOrEmpty(ProductWholesaleTextBox.Text) || ProductWholesaleTextBox.Text == "\0")
                {
                    WholeSaleProduct = 0;
                }
                //その他
                if (string.IsNullOrEmpty(OtherPurchaseTextBox.Text) || OtherPurchaseTextBox.Text == "\0")
                {
                    PurChaseOther = 0;
                }
                if (string.IsNullOrEmpty(OtherWholesaleTextBox.Text) || OtherWholesaleTextBox.Text == "\0")
                {
                    WholeSaleOther = 0;
                }
                #endregion
                
                MetalPurchase = PurChaseMetal;                          //地金買取額
                MetalWholesale = WholeSaleMetal;                        //地金卸値
                MetalProfit = WholeSaleMetal - PurChaseMetal;                              //地金利益
                DiamondPurchase = PurChaseDiamond;                      //ダイヤ買取額
                DiamondWholesale = WholeSaleDiamond;                    //ダイヤ卸値
                DiamondProfit = WholeSaleDiamond - PurChaseDiamond;                          //ダイヤ利益
                BrandPurchase = PurChaseBrand;                          //ブランド買取額
                BrandWholesale = WholeSaleBrand;                        //ブランド卸値
                BrandProfit = WholeSaleBrand - PurChaseBrand;                              //ブランド利益
                ProductPurchase = PurChaseProduct;                      //製品買取額
                ProductWholesale = WholeSaleProduct;                    //製品卸値
                ProductProfit = WholeSaleProduct - PurChaseProduct;                          //製品利益
                OtherPurchase = PurChaseOther;                          //その他買取額
                OtherWholesale = WholeSaleOther;                        //その他卸値
                OtherProfit = WholeSaleOther - PurChaseOther;                              //その他利益

                Document = SlipNumber;                                  //list_result2 への登録時
                GRADE = int.Parse(GradeNumberTextBox.Text);             //list_result2 への登録時
                AssessmentDate = AssessmentDateTextBox.Text;

                if (type == 0)
                {
                    CName = NameOrCompanyNameTextBox.Text;
                    CShop = OccupationOrShopNameTextBox.Text;
                    CStaff = AddressOrClientStaffNameTextBox.Text;

                    sql_str = "Insert into list_result(company_name, shop_name, staff_name, type, staff_code, registration_date, result, sum_money, sum_wholesale_price, profit, document_number, metal_purchase, metal_wholesale, metal_profit, diamond_purchase, diamond_wholesale, diamond_profit, brand_purchase, brand_wholesale, brand_profit, product_purchase, product_wholesale, product_profit, other_purchase, other_wholesale, other_profit) values ('" + CName + "','" + CShop + "','" + CStaff + "','" + 0 + "','" + staff_id + "','" + Registration + "','" + GradeNumber + "','" + TotalPurchase + "','" + TotalWholesale + "','" + TotalProfit + "','" + DNumber + "','" + MetalPurchase + "','" + MetalWholesale + "','" + MetalProfit + "','" + DiamondPurchase + "','" + DiamondWholesale + "','" + DiamondProfit + "','" + BrandPurchase + "','" + BrandWholesale + "','" + BrandProfit + "','" + ProductPurchase + "','" + ProductWholesale + "','" + ProductProfit + "','" + OtherPurchase + "','" + OtherWholesale + "','" + OtherProfit + "');";

                }
                else if (type == 1)
                {
                    name = NameOrCompanyNameTextBox.Text;
                    Occupation = OccupationOrShopNameTextBox.Text;
                    Address = AddressOrClientStaffNameTextBox.Text;
                    BirthDay = BirthdayTextBox.Text;

                    sql_str = "Insert into list_result(name, address, occupation, birthday, type, staff_code, registration_date, result, sum_money, sum_wholesale_price, profit, document_number, metal_purchase, metal_wholesale, metal_profit, diamond_purchase, diamond_wholesale, diamond_profit, brand_purchase, brand_wholesale, brand_profit, product_purchase, product_wholesale, product_profit, other_purchase, other_wholesale, other_profit) values ('" + name + "','" + Address + "','" + Occupation + "','" + BirthDay + "','" + 1 + "','" + staff_id + "','" + Registration + "','" + GradeNumber + "','" + TotalPurchase + "','" + TotalWholesale + "','" + TotalProfit + "','" + DNumber + "','" + MetalPurchase + "','" + MetalWholesale + "','" + MetalProfit + "','" + DiamondPurchase + "','" + DiamondWholesale + "','" + DiamondProfit + "','" + BrandPurchase + "','" + BrandWholesale + "','" + BrandProfit + "','" + ProductPurchase + "','" + ProductWholesale + "','" + ProductProfit + "','" + OtherPurchase + "','" + OtherWholesale + "','" + OtherProfit + "');";
                }
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(DataTable);

                //履歴用に登録
                sql_str = "Insert into list_result_revisions values ('" + type + "', '" + staff_id + "','" + Registration + "','" + GradeNumber + "', '" + TotalPurchase + "','" + TotalWholesale + "','" + TotalProfit + "','" + DNumber + "','" + MetalPurchase + "','" + MetalWholesale + "','" + MetalProfit + "','" + DiamondPurchase + "','" + DiamondWholesale + "','" + DiamondProfit + "','" + BrandPurchase + "','" + BrandWholesale + "','" + BrandProfit + "','" + ProductPurchase + "','" + ProductWholesale + "','" + ProductProfit + "','" + OtherPurchase + "','" + OtherWholesale + "','" + OtherProfit + "');";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(DATATable);
                #endregion

                #region"list_result2 への登録"

                #region"コメントアウト"
                //for (int i = 1; i <= record; i++)
                //{
                //    switch (i)
                //    {
                //        #region"１行目"
                //        case 1:
                //            REMARK = remark1.Text;
                //            MainCategoryCode = MainCategoryCode1;
                //            ItemCategoryCode = ItemCategoryCode1;
                //            Record = 1;
                //            UnitPrice = UnitPriceUnFormat1;
                //            Purchase = PurchaseUnFormat1;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox1.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox1.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat1;
                //                PROFIT = WholeSaleUnFormat1 - PurchaseUnFormat1;
                //                BUYDATE = BuyDateTimePicker1.Value.ToLongDateString();
                //                BUYER = BuyerTextBox1.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat1 - PurchaseUnFormat1;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data1);
                //            break;
                //        #endregion
                //        #region"２行目"
                //        case 2:
                //            REMARK = remark2.Text;
                //            MainCategoryCode = MainCategoryCode2;
                //            ItemCategoryCode = ItemCategoryCode2;
                //            Record = 2;
                //            UnitPrice = UnitPriceUnFormat2;
                //            Purchase = PurchaseUnFormat2;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox2.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox2.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat2;
                //                PROFIT = WholeSaleUnFormat2 - PurchaseUnFormat2;
                //                BUYDATE = BuyDateTimePicker2.Value.ToLongDateString();
                //                BUYER = BuyerTextBox2.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat2 - PurchaseUnFormat2;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data2);
                //            break;
                //        #endregion
                //        #region"３行目"
                //        case 3:
                //            REMARK = remark3.Text;
                //            MainCategoryCode = MainCategoryCode3;
                //            ItemCategoryCode = ItemCategoryCode3;
                //            Record = 3;
                //            UnitPrice = UnitPriceUnFormat3;
                //            Purchase = PurchaseUnFormat3;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox3.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox3.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat3;
                //                PROFIT = WholeSaleUnFormat3 - PurchaseUnFormat3;
                //                BUYDATE = BuyDateTimePicker3.Value.ToLongDateString();
                //                BUYER = BuyerTextBox3.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat3 - PurchaseUnFormat3;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data3);
                //            break;
                //        #endregion
                //        #region"４行目"
                //        case 4:
                //            REMARK = remark4.Text;
                //            MainCategoryCode = MainCategoryCode4;
                //            ItemCategoryCode = ItemCategoryCode4;
                //            Record = 4;
                //            UnitPrice = UnitPriceUnFormat4;
                //            Purchase = PurchaseUnFormat4;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox4.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox4.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat4;
                //                PROFIT = WholeSaleUnFormat4 - PurchaseUnFormat4;
                //                BUYDATE = BuyDateTimePicker4.Value.ToLongDateString();
                //                BUYER = BuyerTextBox4.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat4 - PurchaseUnFormat4;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data4);
                //            break;
                //        #endregion
                //        #region"５行目"
                //        case 5:
                //            REMARK = remark5.Text;
                //            MainCategoryCode = MainCategoryCode5;
                //            ItemCategoryCode = ItemCategoryCode5;
                //            Record = 5;
                //            UnitPrice = UnitPriceUnFormat5;
                //            Purchase = PurchaseUnFormat5;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox5.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox5.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat5;
                //                PROFIT = WholeSaleUnFormat5 - PurchaseUnFormat5;
                //                BUYDATE = BuyDateTimePicker5.Value.ToLongDateString();
                //                BUYER = BuyerTextBox5.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat5 - PurchaseUnFormat5;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data5);
                //            break;
                //        #endregion
                //        #region"６行目"
                //        case 6:
                //            REMARK = remark6.Text;
                //            MainCategoryCode = MainCategoryCode6;
                //            ItemCategoryCode = ItemCategoryCode6;
                //            Record = 6;
                //            UnitPrice = UnitPriceUnFormat6;
                //            Purchase = PurchaseUnFormat6;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox6.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox6.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat6;
                //                PROFIT = WholeSaleUnFormat6 - PurchaseUnFormat6;
                //                BUYDATE = BuyDateTimePicker6.Value.ToLongDateString();
                //                BUYER = BuyerTextBox6.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat6 - PurchaseUnFormat6;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data6);
                //            break;
                //        #endregion
                //        #region"７行目"
                //        case 7:
                //            REMARK = remark7.Text;
                //            MainCategoryCode = MainCategoryCode7;
                //            ItemCategoryCode = ItemCategoryCode7;
                //            Record = 7;
                //            UnitPrice = UnitPriceUnFormat7;
                //            Purchase = PurchaseUnFormat7;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox7.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox7.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat7;
                //                PROFIT = WholeSaleUnFormat7 - PurchaseUnFormat7;
                //                BUYDATE = BuyDateTimePicker7.Value.ToLongDateString();
                //                BUYER = BuyerTextBox7.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat7 - PurchaseUnFormat7;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data7);
                //            break;
                //        #endregion
                //        #region"８行目"
                //        case 8:
                //            REMARK = remark8.Text;
                //            MainCategoryCode = MainCategoryCode8;
                //            ItemCategoryCode = ItemCategoryCode8;
                //            Record = 8;
                //            UnitPrice = UnitPriceUnFormat8;
                //            Purchase = PurchaseUnFormat8;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox8.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox8.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat8;
                //                PROFIT = WholeSaleUnFormat8 - PurchaseUnFormat8;
                //                BUYDATE = BuyDateTimePicker8.Value.ToLongDateString();
                //                BUYER = BuyerTextBox8.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat8 - PurchaseUnFormat8;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data8);
                //            break;
                //        #endregion
                //        #region"９行目"
                //        case 9:
                //            REMARK = remark9.Text;
                //            MainCategoryCode = MainCategoryCode9;
                //            ItemCategoryCode = ItemCategoryCode9;
                //            Record = 9;
                //            UnitPrice = UnitPriceUnFormat9;
                //            Purchase = PurchaseUnFormat9;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox9.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox9.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat9;
                //                PROFIT = WholeSaleUnFormat9 - PurchaseUnFormat9;
                //                BUYDATE = BuyDateTimePicker9.Value.ToLongDateString();
                //                BUYER = BuyerTextBox9.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat9 - PurchaseUnFormat9;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data9);
                //            break;
                //        #endregion
                //        #region"１０行目"
                //        case 10:
                //            REMARK = remark10.Text;
                //            MainCategoryCode = MainCategoryCode10;
                //            ItemCategoryCode = ItemCategoryCode10;
                //            Record = 10;
                //            UnitPrice = UnitPriceUnFormat10;
                //            Purchase = PurchaseUnFormat10;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox10.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox10.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat10;
                //                PROFIT = WholeSaleUnFormat10 - PurchaseUnFormat10;
                //                BUYDATE = BuyDateTimePicker10.Value.ToLongDateString();
                //                BUYER = BuyerTextBox10.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat10 - PurchaseUnFormat10;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data10);
                //            break;
                //        #endregion
                //        #region"１１行目"
                //        case 11:
                //            REMARK = remark11.Text;
                //            MainCategoryCode = MainCategoryCode11;
                //            ItemCategoryCode = ItemCategoryCode11;
                //            Record = 11;
                //            UnitPrice = UnitPriceUnFormat11;
                //            Purchase = PurchaseUnFormat11;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox11.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox11.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat11;
                //                PROFIT = WholeSaleUnFormat11 - PurchaseUnFormat11;
                //                BUYDATE = BuyDateTimePicker11.Value.ToLongDateString();
                //                BUYER = BuyerTextBox11.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat11 - PurchaseUnFormat11;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data11);
                //            break;
                //        #endregion
                //        #region"１２行目"
                //        case 12:
                //            REMARK = remark12.Text;
                //            MainCategoryCode = MainCategoryCode12;
                //            ItemCategoryCode = ItemCategoryCode12;
                //            Record = 12;
                //            UnitPrice = UnitPriceUnFormat12;
                //            Purchase = PurchaseUnFormat12;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox12.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox12.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat12;
                //                PROFIT = WholeSaleUnFormat12 - PurchaseUnFormat12;
                //                BUYDATE = BuyDateTimePicker12.Value.ToLongDateString();
                //                BUYER = BuyerTextBox12.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat12 - PurchaseUnFormat12;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data12);
                //            break;
                //        #endregion
                //        #region"１３行目"
                //        case 13:
                //            REMARK = remark13.Text;
                //            MainCategoryCode = MainCategoryCode13;
                //            ItemCategoryCode = ItemCategoryCode13;
                //            Record = 13;
                //            UnitPrice = UnitPriceUnFormat13;
                //            Purchase = PurchaseUnFormat13;
                //            #region"チェックボックス"
                //            if (NextMonthCheckBox13.Checked)
                //            {
                //                NextMonth = 1;
                //            }
                //            else
                //            {
                //                NextMonth = 0;
                //            }

                //            if (ItemNameChangeCheckBox13.Checked)
                //            {
                //                ChangeCheck = 1;
                //            }
                //            else
                //            {
                //                ChangeCheck = 0;
                //            }
                //            #endregion
                //            if (!string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                //            {
                //                WHOLESALE = WholeSaleUnFormat13;
                //                PROFIT = WholeSaleUnFormat13 - PurchaseUnFormat13;
                //                BUYDATE = BuyDateTimePicker13.Value.ToLongDateString();
                //                BUYER = BuyerTextBox13.Text;
                //            }
                //            else
                //            {
                //                WHOLESALE = 0;
                //                PROFIT = WholeSaleUnFormat13 - PurchaseUnFormat13;
                //                BUYDATE = "";
                //                BUYER = "";
                //            }

                //            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                //            adapter = new NpgsqlDataAdapter(sql_str, conn);
                //            adapter.Fill(Data13);
                //            break;
                //            #endregion
                //    }
                //}
                #endregion

                for (int i = 1; i <= record; i++)
                {
                    switch (i)
                    {
                        #region"１行目"
                        case 1:
                            Record = 1;
                            MainCategoryCode = MainCategoryCode1;
                            ItemCategoryCode = ItemCategoryCode1;
                            Purchase = PurchaseUnFormat1;
                            Wholesale = WholeSaleUnFormat1;
                            Remark = remark1.Text;
                            Profit = WholeSaleUnFormat1 - PurchaseUnFormat1;
                            ItemDetail = itemDetailTextBox1.Text;
                            Weight = decimal.Parse(weightTextBox1.Text);
                            UnitPrice = UnitPriceUnFormat1;
                            Count = int.Parse(countTextBox1.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox1.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker1.Value.ToLongDateString();
                                Buyer = BuyerTextBox1.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox1.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox1.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data1);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA1);
                            break;
                        #endregion
                        #region"２行目"
                        case 2:
                            Record = 2;
                            MainCategoryCode = MainCategoryCode2;
                            ItemCategoryCode = ItemCategoryCode2;
                            Purchase = PurchaseUnFormat2;
                            Wholesale = WholeSaleUnFormat2;
                            Remark = remark2.Text;
                            Profit = WholeSaleUnFormat2 - PurchaseUnFormat2;
                            ItemDetail = itemDetailTextBox2.Text;
                            Weight = decimal.Parse(weightTextBox2.Text);
                            UnitPrice = UnitPriceUnFormat2;
                            Count = int.Parse(countTextBox2.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox2.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker2.Value.ToLongDateString();
                                Buyer = BuyerTextBox2.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox2.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox2.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data2);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA2);
                            break;
                        #endregion
                        #region"３行目"
                        case 3:
                            Record = 3;
                            MainCategoryCode = MainCategoryCode3;
                            ItemCategoryCode = ItemCategoryCode3;
                            Purchase = PurchaseUnFormat3;
                            Wholesale = WholeSaleUnFormat3;
                            Remark = remark3.Text;
                            Profit = WholeSaleUnFormat3 - PurchaseUnFormat3;
                            ItemDetail = itemDetailTextBox3.Text;
                            Weight = decimal.Parse(weightTextBox3.Text);
                            UnitPrice = UnitPriceUnFormat3;
                            Count = int.Parse(countTextBox3.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox3.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker3.Value.ToLongDateString();
                                Buyer = BuyerTextBox3.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox3.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox3.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data3);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA3);
                            break;
                        #endregion
                        #region"４行目"
                        case 4:
                            Record = 4;
                            MainCategoryCode = MainCategoryCode4;
                            ItemCategoryCode = ItemCategoryCode4;
                            Purchase = PurchaseUnFormat4;
                            Wholesale = WholeSaleUnFormat4;
                            Remark = remark4.Text;
                            Profit = WholeSaleUnFormat4 - PurchaseUnFormat4;
                            ItemDetail = itemDetailTextBox4.Text;
                            Weight = decimal.Parse(weightTextBox4.Text);
                            UnitPrice = UnitPriceUnFormat4;
                            Count = int.Parse(countTextBox4.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox4.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker4.Value.ToLongDateString();
                                Buyer = BuyerTextBox4.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox4.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox4.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data4);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA4);
                            break;
                        #endregion
                        #region"５行目"
                        case 5:
                            Record = 5;
                            MainCategoryCode = MainCategoryCode5;
                            ItemCategoryCode = ItemCategoryCode5;
                            Purchase = PurchaseUnFormat5;
                            Wholesale = WholeSaleUnFormat5;
                            Remark = remark5.Text;
                            Profit = WholeSaleUnFormat5 - PurchaseUnFormat5;
                            ItemDetail = itemDetailTextBox5.Text;
                            Weight = decimal.Parse(weightTextBox5.Text);
                            UnitPrice = UnitPriceUnFormat5;
                            Count = int.Parse(countTextBox5.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox5.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker5.Value.ToLongDateString();
                                Buyer = BuyerTextBox5.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox5.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox5.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data5);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA5);
                            break;
                        #endregion
                        #region"６行目"
                        case 6:
                            Record = 6;
                            MainCategoryCode = MainCategoryCode6;
                            ItemCategoryCode = ItemCategoryCode6;
                            Purchase = PurchaseUnFormat6;
                            Wholesale = WholeSaleUnFormat6;
                            Remark = remark6.Text;
                            Profit = WholeSaleUnFormat6 - PurchaseUnFormat6;
                            ItemDetail = itemDetailTextBox6.Text;
                            Weight = decimal.Parse(weightTextBox6.Text);
                            UnitPrice = UnitPriceUnFormat6;
                            Count = int.Parse(countTextBox6.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox6.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker6.Value.ToLongDateString();
                                Buyer = BuyerTextBox6.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox6.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox6.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data6);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA6);
                            break;
                        #endregion
                        #region"７行目"
                        case 7:
                            Record = 7;
                            MainCategoryCode = MainCategoryCode7;
                            ItemCategoryCode = ItemCategoryCode7;
                            Purchase = PurchaseUnFormat7;
                            Wholesale = WholeSaleUnFormat7;
                            Remark = remark1.Text;
                            Profit = WholeSaleUnFormat7 - PurchaseUnFormat7;
                            ItemDetail = itemDetailTextBox7.Text;
                            Weight = decimal.Parse(weightTextBox7.Text);
                            UnitPrice = UnitPriceUnFormat7;
                            Count = int.Parse(countTextBox7.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox7.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker7.Value.ToLongDateString();
                                Buyer = BuyerTextBox7.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox7.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox7.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data7);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA7);
                            break;
                        #endregion
                        #region"８行目"
                        case 8:
                            Record = 8;
                            MainCategoryCode = MainCategoryCode8;
                            ItemCategoryCode = ItemCategoryCode8;
                            Purchase = PurchaseUnFormat8;
                            Wholesale = WholeSaleUnFormat8;
                            Remark = remark8.Text;
                            Profit = WholeSaleUnFormat8 - PurchaseUnFormat8;
                            ItemDetail = itemDetailTextBox8.Text;
                            Weight = decimal.Parse(weightTextBox8.Text);
                            UnitPrice = UnitPriceUnFormat8;
                            Count = int.Parse(countTextBox8.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox8.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker8.Value.ToLongDateString();
                                Buyer = BuyerTextBox8.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox8.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox8.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data8);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA8);
                            break;
                        #endregion
                        #region"９行目"
                        case 9:
                            Record = 9;
                            MainCategoryCode = MainCategoryCode9;
                            ItemCategoryCode = ItemCategoryCode9;
                            Purchase = PurchaseUnFormat9;
                            Wholesale = WholeSaleUnFormat9;
                            Remark = remark9.Text;
                            Profit = WholeSaleUnFormat9 - PurchaseUnFormat9;
                            ItemDetail = itemDetailTextBox9.Text;
                            Weight = decimal.Parse(weightTextBox9.Text);
                            UnitPrice = UnitPriceUnFormat9;
                            Count = int.Parse(countTextBox9.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox9.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker9.Value.ToLongDateString();
                                Buyer = BuyerTextBox9.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox9.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox9.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data9);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA9);
                            break;
                        #endregion
                        #region"１０行目"
                        case 10:
                            Record = 10;
                            MainCategoryCode = MainCategoryCode10;
                            ItemCategoryCode = ItemCategoryCode10;
                            Purchase = PurchaseUnFormat10;
                            Wholesale = WholeSaleUnFormat10;
                            Remark = remark10.Text;
                            Profit = WholeSaleUnFormat10 - PurchaseUnFormat10;
                            ItemDetail = itemDetailTextBox10.Text;
                            Weight = decimal.Parse(weightTextBox10.Text);
                            UnitPrice = UnitPriceUnFormat10;
                            Count = int.Parse(countTextBox10.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox10.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker10.Value.ToLongDateString();
                                Buyer = BuyerTextBox10.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox10.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox10.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data10);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA10);
                            break;
                        #endregion
                        #region"１１行目"
                        case 11:
                            Record = 11;
                            MainCategoryCode = MainCategoryCode11;
                            ItemCategoryCode = ItemCategoryCode11;
                            Purchase = PurchaseUnFormat11;
                            Wholesale = WholeSaleUnFormat11;
                            Remark = remark11.Text;
                            Profit = WholeSaleUnFormat11 - PurchaseUnFormat11;
                            ItemDetail = itemDetailTextBox11.Text;
                            Weight = decimal.Parse(weightTextBox11.Text);
                            UnitPrice = UnitPriceUnFormat11;
                            Count = int.Parse(countTextBox11.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox11.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker11.Value.ToLongDateString();
                                Buyer = BuyerTextBox11.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox11.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox11.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data11);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA11);
                            break;
                        #endregion
                        #region"１２行目"
                        case 12:
                            Record = 12;
                            MainCategoryCode = MainCategoryCode12;
                            ItemCategoryCode = ItemCategoryCode12;
                            Purchase = PurchaseUnFormat12;
                            Wholesale = WholeSaleUnFormat12;
                            Remark = remark12.Text;
                            Profit = WholeSaleUnFormat12 - PurchaseUnFormat12;
                            ItemDetail = itemDetailTextBox12.Text;
                            Weight = decimal.Parse(weightTextBox12.Text);
                            UnitPrice = UnitPriceUnFormat12;
                            Count = int.Parse(countTextBox12.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox12.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker12.Value.ToLongDateString();
                                Buyer = BuyerTextBox12.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox12.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox12.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data12);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA12);
                            break;
                        #endregion
                        #region"１３行目"
                        case 13:
                            Record = 13;
                            MainCategoryCode = MainCategoryCode13;
                            ItemCategoryCode = ItemCategoryCode13;
                            Purchase = PurchaseUnFormat13;
                            Wholesale = WholeSaleUnFormat13;
                            Remark = remark13.Text;
                            Profit = WholeSaleUnFormat13 - PurchaseUnFormat13;
                            ItemDetail = itemDetailTextBox13.Text;
                            Weight = decimal.Parse(weightTextBox13.Text);
                            UnitPrice = UnitPriceUnFormat13;
                            Count = int.Parse(countTextBox13.Text);

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox13.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker13.Value.ToLongDateString();
                                Buyer = BuyerTextBox13.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox13.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox13.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion

                            sql_str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count, item_name_change) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data13);

                            //履歴に登録
                            sql_str = "Insert into list_result_revisions2 values ('" + SaleDate + "','" + ItemCategoryCode + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + Registration + "','" + DNumber + "','" + Profit + "','" + MainCategoryCode + "','" + ChangeCheck + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(DATA13);
                            break;
                            #endregion
                    }
                }
                #endregion
            }
            conn.Close();
            #endregion
            NameChange = false;
            MessageBox.Show("入力されたデータを登録しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region"品名変更ボタン"
        private void ItemNameChangeButton_Click(object sender, EventArgs e)
        {
            #region"品名変更にチェックされているかどうか"
            switch (record)
            {
                #region"１行目まで"
                case 1:
                    if (!ItemNameChangeCheckBox1.Checked)
                    {
                        MessageBox.Show("１行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は１行目の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"２行目まで"
                case 2:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked)
                    {
                        MessageBox.Show("１行目～２行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"３行目まで"
                case 3:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked)
                    {
                        MessageBox.Show("１行目～３行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"４行目まで"
                case 4:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked)
                    {
                        MessageBox.Show("１行目～４行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"５行目まで"
                case 5:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked)
                    {
                        MessageBox.Show("１行目～５行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"６行目まで"
                case 6:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked && !ItemNameChangeCheckBox6.Checked)
                    {
                        MessageBox.Show("１行目～６行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"７行目まで"
                case 7:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked && !ItemNameChangeCheckBox6.Checked && !ItemNameChangeCheckBox7.Checked)
                    {
                        MessageBox.Show("１行目～７行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"８行目まで"
                case 8:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked && !ItemNameChangeCheckBox6.Checked && !ItemNameChangeCheckBox7.Checked && !ItemNameChangeCheckBox8.Checked)
                    {
                        MessageBox.Show("１行目～８行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"９行目まで"
                case 9:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked && !ItemNameChangeCheckBox6.Checked && !ItemNameChangeCheckBox7.Checked && !ItemNameChangeCheckBox8.Checked && !ItemNameChangeCheckBox9.Checked)
                    {
                        MessageBox.Show("１行目～９行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"１０行目まで"
                case 10:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked && !ItemNameChangeCheckBox6.Checked && !ItemNameChangeCheckBox7.Checked && !ItemNameChangeCheckBox8.Checked && !ItemNameChangeCheckBox9.Checked && !ItemNameChangeCheckBox10.Checked)
                    {
                        MessageBox.Show("１行目～１０行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"１１行目まで"
                case 11:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked && !ItemNameChangeCheckBox6.Checked && !ItemNameChangeCheckBox7.Checked && !ItemNameChangeCheckBox8.Checked && !ItemNameChangeCheckBox9.Checked && !ItemNameChangeCheckBox10.Checked && !ItemNameChangeCheckBox11.Checked)
                    {
                        MessageBox.Show("１行目～１１行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"１２行目まで"
                case 12:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked && !ItemNameChangeCheckBox6.Checked && !ItemNameChangeCheckBox7.Checked && !ItemNameChangeCheckBox8.Checked && !ItemNameChangeCheckBox9.Checked && !ItemNameChangeCheckBox10.Checked && !ItemNameChangeCheckBox11.Checked && !ItemNameChangeCheckBox12.Checked)
                    {
                        MessageBox.Show("１行目～１２行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                #endregion
                #region"１３行目まで"
                case 13:
                    if (!ItemNameChangeCheckBox1.Checked && !ItemNameChangeCheckBox2.Checked && !ItemNameChangeCheckBox3.Checked && !ItemNameChangeCheckBox4.Checked && !ItemNameChangeCheckBox5.Checked && !ItemNameChangeCheckBox6.Checked && !ItemNameChangeCheckBox7.Checked && !ItemNameChangeCheckBox8.Checked && !ItemNameChangeCheckBox9.Checked && !ItemNameChangeCheckBox10.Checked && !ItemNameChangeCheckBox11.Checked && !ItemNameChangeCheckBox12.Checked && !ItemNameChangeCheckBox13.Checked)
                    {
                        MessageBox.Show("１行目～１３行目の品名変更にチェックが入っておりません。" + "\r\n" + "品名を変更する場合は変更する行の品名変更にチェックを入れてください", "入力項目を確認してください", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                    #endregion
            }
            #endregion

            #region"次月にチェックが入っていて卸値にも入力されているとき"
            for (int i = 1; i <= record; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        if (WholesalePriceTextBox1.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox1.Text)) 
                        {
                            MessageBox.Show("１行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        if (WholesalePriceTextBox2.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                        {
                            MessageBox.Show("２行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        if (WholesalePriceTextBox3.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                        {
                            MessageBox.Show("３行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        if (WholesalePriceTextBox4.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                        {
                            MessageBox.Show("４行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        if (WholesalePriceTextBox5.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                        {
                            MessageBox.Show("５行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        if (WholesalePriceTextBox6.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                        {
                            MessageBox.Show("６行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        if (WholesalePriceTextBox7.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                        {
                            MessageBox.Show("７行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        if (WholesalePriceTextBox8.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                        {
                            MessageBox.Show("８行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        if (WholesalePriceTextBox9.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                        {
                            MessageBox.Show("９行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        if (WholesalePriceTextBox10.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                        {
                            MessageBox.Show("１０行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        if (WholesalePriceTextBox11.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                        {
                            MessageBox.Show("１１行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        if (WholesalePriceTextBox12.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                        {
                            MessageBox.Show("１２行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        if (WholesalePriceTextBox13.ForeColor == Color.Red && !string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                        {
                            MessageBox.Show("１３行目に卸値が入力されています。" + "\r\n" + "「次月持ち越し」にするのかどうかよく確認してください", "入力項目をご確認ください", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;
                        #endregion
                }
            }
            #endregion

            #region"卸値に値が入力されていて売却先が無記入のとき"
            for (int i = 1; i <= record; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox1.Text) && string.IsNullOrEmpty(BuyerTextBox1.Text))
                        {
                            MessageBox.Show("１行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox2.Text) && string.IsNullOrEmpty(BuyerTextBox2.Text))
                        {
                            MessageBox.Show("２行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox3.Text) && string.IsNullOrEmpty(BuyerTextBox3.Text))
                        {
                            MessageBox.Show("３行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox4.Text) && string.IsNullOrEmpty(BuyerTextBox4.Text))
                        {
                            MessageBox.Show("４行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox5.Text) && string.IsNullOrEmpty(BuyerTextBox5.Text))
                        {
                            MessageBox.Show("５行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox6.Text) && string.IsNullOrEmpty(BuyerTextBox6.Text))
                        {
                            MessageBox.Show("６行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox7.Text) && string.IsNullOrEmpty(BuyerTextBox7.Text))
                        {
                            MessageBox.Show("７行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox8.Text) && string.IsNullOrEmpty(BuyerTextBox8.Text))
                        {
                            MessageBox.Show("８行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox9.Text) && string.IsNullOrEmpty(BuyerTextBox9.Text))
                        {
                            MessageBox.Show("９行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox10.Text) && string.IsNullOrEmpty(BuyerTextBox10.Text))
                        {
                            MessageBox.Show("１０行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox11.Text) && string.IsNullOrEmpty(BuyerTextBox11.Text))
                        {
                            MessageBox.Show("１１行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox12.Text) && string.IsNullOrEmpty(BuyerTextBox12.Text))
                        {
                            MessageBox.Show("１２行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        if (!string.IsNullOrEmpty(WholesalePriceTextBox13.Text) && string.IsNullOrEmpty(BuyerTextBox13.Text))
                        {
                            MessageBox.Show("１３行目に卸値が入力されていますが売却先が無記入です。" + "\r\n" + "入力項目を確認してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                        #endregion
                }
            }
            #endregion

            #region"卸値が無記入で次月持ち越しにチェックが入っていないとき"
            for (int i = 1; i <= record; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        if (!NextMonthCheckBox1.Checked && string.IsNullOrEmpty(WholesalePriceTextBox1.Text) && !ItemNameChangeCheckBox1.Checked)
                        {
                            MessageBox.Show("１行目の卸値が無記入ですが" + "\r\n" + "１行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        if (!NextMonthCheckBox2.Checked && string.IsNullOrEmpty(WholesalePriceTextBox2.Text) && !ItemNameChangeCheckBox2.Checked)
                        {
                            MessageBox.Show("２行目の卸値が無記入ですが" + "\r\n" + "２行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        if (!NextMonthCheckBox3.Checked && string.IsNullOrEmpty(WholesalePriceTextBox3.Text) && !ItemNameChangeCheckBox3.Checked)
                        {
                            MessageBox.Show("３行目の卸値が無記入ですが" + "\r\n" + "３行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        if (!NextMonthCheckBox4.Checked && string.IsNullOrEmpty(WholesalePriceTextBox4.Text) && !ItemNameChangeCheckBox4.Checked)
                        {
                            MessageBox.Show("４行目の卸値が無記入ですが" + "\r\n" + "４行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        if (!NextMonthCheckBox5.Checked && string.IsNullOrEmpty(WholesalePriceTextBox5.Text) && !ItemNameChangeCheckBox5.Checked)
                        {
                            MessageBox.Show("５行目の卸値が無記入ですが" + "\r\n" + "５行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        if (!NextMonthCheckBox6.Checked && string.IsNullOrEmpty(WholesalePriceTextBox6.Text) && !ItemNameChangeCheckBox6.Checked)
                        {
                            MessageBox.Show("６行目の卸値が無記入ですが" + "\r\n" + "６行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        if (!NextMonthCheckBox7.Checked && string.IsNullOrEmpty(WholesalePriceTextBox7.Text) && !ItemNameChangeCheckBox7.Checked)
                        {
                            MessageBox.Show("７行目の卸値が無記入ですが" + "\r\n" + "７行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        if (!NextMonthCheckBox8.Checked && string.IsNullOrEmpty(WholesalePriceTextBox8.Text) && !ItemNameChangeCheckBox8.Checked)
                        {
                            MessageBox.Show("８行目の卸値が無記入ですが" + "\r\n" + "８行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        if (!NextMonthCheckBox9.Checked && string.IsNullOrEmpty(WholesalePriceTextBox9.Text) && !ItemNameChangeCheckBox9.Checked)
                        {
                            MessageBox.Show("９行目の卸値が無記入ですが" + "\r\n" + "９行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        if (!NextMonthCheckBox10.Checked && string.IsNullOrEmpty(WholesalePriceTextBox10.Text) && !ItemNameChangeCheckBox10.Checked)
                        {
                            MessageBox.Show("１０行目の卸値が無記入ですが" + "\r\n" + "１０行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        if (!NextMonthCheckBox11.Checked && string.IsNullOrEmpty(WholesalePriceTextBox11.Text) && !ItemNameChangeCheckBox11.Checked)
                        {
                            MessageBox.Show("１１行目の卸値が無記入ですが" + "\r\n" + "１１行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        if (!NextMonthCheckBox12.Checked && string.IsNullOrEmpty(WholesalePriceTextBox12.Text) && !ItemNameChangeCheckBox12.Checked)
                        {
                            MessageBox.Show("１２行目の卸値が無記入ですが" + "\r\n" + "１２行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        if (!NextMonthCheckBox13.Checked && string.IsNullOrEmpty(WholesalePriceTextBox13.Text) && !ItemNameChangeCheckBox13.Checked)
                        {
                            MessageBox.Show("１３行目の卸値が無記入ですが" + "\r\n" + "１３行目の次月持ち越しにチェックが入っておりません。", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                            return;
                        }
                        break;
                        #endregion
                }
            }
            #endregion

            DialogResult dialogResult = MessageBox.Show("選択した品名を変更しますか？", "変更確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            DateTime date = DateTime.Now;
            Registration = date.ToLongDateString();         //登録日

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            string sql_str = "select * from list_result order by result;";
            cmd = new NpgsqlCommand(sql_str, conn);
            conn.Open();
            string DocumentNumber = "";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    DocumentNumber = reader["document_number"].ToString();
                }
            }

            if (DocumentNumber == SlipNumber || CarryOver)    //一度登録済み（品名変更にチェックが付いているものだけ変更）
            {
                #region"list_resultへの更新"
                TotalPurchase = PurChase;                   //合計買取金額
                TotalWholesale = WholeSale;                 //合計卸値
                TotalProfit = WholeSale - PurChase;         //合計利益
                DNumber = SlipNumber;                       //伝票番号

                #region"合計金額がnull or \0 のとき変数に 0 を代入"
                //地金
                if (string.IsNullOrEmpty(MetalPurchaseTextBox.Text) || MetalPurchaseTextBox.Text == "\0")
                {
                    PurChaseMetal = 0;
                }
                if (string.IsNullOrEmpty(MetalWholesaleTextBox.Text) || MetalWholesaleTextBox.Text == "\0")
                {
                    WholeSaleMetal = 0;
                }
                //ダイヤ
                if (string.IsNullOrEmpty(DiamondPurchaseTextBox.Text) || DiamondPurchaseTextBox.Text == "\0")
                {
                    PurChaseDiamond = 0;
                }
                if (string.IsNullOrEmpty(DiamondWholesaleTextBox.Text) || DiamondWholesaleTextBox.Text == "\0")
                {
                    WholeSaleDiamond = 0;
                }
                //ブランド
                if (string.IsNullOrEmpty(BrandPurchaseTextBox.Text) || BrandPurchaseTextBox.Text == "\0")
                {
                    PurChaseBrand = 0;
                }
                if (string.IsNullOrEmpty(BrandWholesaleTextBox.Text) || BrandWholesaleTextBox.Text == "\0")
                {
                    WholeSaleBrand = 0;
                }
                //製品・ジュエリー
                if (string.IsNullOrEmpty(ProductPurchaseTextBox.Text) || ProductPurchaseTextBox.Text == "\0")
                {
                    PurChaseProduct = 0;
                }
                if (string.IsNullOrEmpty(ProductWholesaleTextBox.Text) || ProductWholesaleTextBox.Text == "\0")
                {
                    WholeSaleProduct = 0;
                }
                //その他
                if (string.IsNullOrEmpty(OtherPurchaseTextBox.Text) || OtherPurchaseTextBox.Text == "\0")
                {
                    PurChaseOther = 0;
                }
                if (string.IsNullOrEmpty(OtherWholesaleTextBox.Text) || OtherWholesaleTextBox.Text == "\0")
                {
                    WholeSaleOther = 0;
                }
                #endregion

                MetalPurchase = PurChaseMetal;              //地金買取額
                MetalWholesale = WholeSaleMetal;            //地金卸値
                MetalProfit = ProFitMetal;                  //地金利益
                DiamondPurchase = PurChaseDiamond;          //ダイヤ買取額
                DiamondWholesale = WholeSaleDiamond;        //ダイヤ卸値
                DiamondProfit = ProFitDiamond;              //ダイヤ利益
                BrandPurchase = PurChaseBrand;              //ブランド買取額
                BrandWholesale = WholeSaleBrand;            //ブランド卸値
                BrandProfit = ProFitBrand;                  //ブランド利益
                ProductPurchase = PurChaseProduct;          //製品買取額
                ProductWholesale = WholeSaleProduct;        //製品卸値
                ProductProfit = ProFitProduct;              //製品利益
                OtherPurchase = PurChaseOther;              //その他買取額
                OtherWholesale = WholeSaleOther;            //その他卸値
                OtherProfit = ProFitOther;                  //その他利益

                using (transaction = conn.BeginTransaction())
                {
                    sql_str = "update list_result set sum_money = '" + TotalPurchase + "', sum_wholesale_price = '" + TotalWholesale + "', profit = '" + TotalProfit + "', metal_purchase = '" + MetalPurchase + "', metal_wholesale = '" + MetalWholesale + "', metal_profit = '" + MetalProfit + "', diamond_purchase = '" + DiamondPurchase + "', diamond_wholesale = '" + DiamondWholesale + "', diamond_profit = '" + DiamondProfit + "', brand_purchase = '" + BrandPurchase + "', brand_wholesale = '" + BrandWholesale + "', brand_profit = '" + BrandProfit + "', product_purchase = '" + ProductPurchase + "', product_wholesale = '" + ProductWholesale + "', product_profit = '" + ProductProfit + "', other_purchase = '" + OtherPurchase + "', other_wholesale = '" + OtherWholesale + "', other_profit = '" + OtherProfit + "' where document_number = '" + DNumber + "';";
                    cmd = new NpgsqlCommand(sql_str, conn);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                #endregion
                #region"list_result2への更新"
                for (int i = 1; i <= record; i++)
                {
                    switch (i)
                    {
                        #region"１行目"
                        case 1:
                            Record = 1;
                            MainCategoryCode = MainCategoryCode1;
                            ItemCategoryCode = ItemCategoryCode1;
                            Wholesale = WholeSaleUnFormat1;
                            Remark = remark1.Text;
                            Profit = WholeSaleUnFormat1 - PurchaseUnFormat1;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox1.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker1.Value.ToLongDateString();
                                Buyer = BuyerTextBox1.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox1.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox1.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"２行目"
                        case 2:
                            Record = 2;
                            MainCategoryCode = MainCategoryCode2;
                            ItemCategoryCode = ItemCategoryCode2;
                            Wholesale = WholeSaleUnFormat2;
                            Remark = remark2.Text;
                            Profit = WholeSaleUnFormat2 - PurchaseUnFormat2;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox2.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker2.Value.ToLongDateString();
                                Buyer = BuyerTextBox2.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox2.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox2.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"３行目"
                        case 3:
                            Record = 3;
                            MainCategoryCode = MainCategoryCode3;
                            ItemCategoryCode = ItemCategoryCode3;
                            Wholesale = WholeSaleUnFormat3;
                            Remark = remark3.Text;
                            Profit = WholeSaleUnFormat3 - PurchaseUnFormat3;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox3.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker3.Value.ToLongDateString();
                                Buyer = BuyerTextBox3.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox3.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox3.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"４行目"
                        case 4:
                            Record = 4;
                            MainCategoryCode = MainCategoryCode4;
                            ItemCategoryCode = ItemCategoryCode4;
                            Wholesale = WholeSaleUnFormat4;
                            Remark = remark4.Text;
                            Profit = WholeSaleUnFormat4 - PurchaseUnFormat4;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox4.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker4.Value.ToLongDateString();
                                Buyer = BuyerTextBox4.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox4.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox4.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"５行目"
                        case 5:
                            Record = 5;
                            MainCategoryCode = MainCategoryCode5;
                            ItemCategoryCode = ItemCategoryCode5;
                            Wholesale = WholeSaleUnFormat5;
                            Remark = remark5.Text;
                            Profit = WholeSaleUnFormat5 - PurchaseUnFormat5;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox5.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker5.Value.ToLongDateString();
                                Buyer = BuyerTextBox5.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox5.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox5.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"６行目"
                        case 6:
                            Record = 6;
                            MainCategoryCode = MainCategoryCode6;
                            ItemCategoryCode = ItemCategoryCode6;
                            Wholesale = WholeSaleUnFormat6;
                            Remark = remark6.Text;
                            Profit = WholeSaleUnFormat6 - PurchaseUnFormat6;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox6.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker6.Value.ToLongDateString();
                                Buyer = BuyerTextBox6.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox6.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox6.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"７行目"
                        case 7:
                            Record = 7;
                            MainCategoryCode = MainCategoryCode7;
                            ItemCategoryCode = ItemCategoryCode7;
                            Wholesale = WholeSaleUnFormat7;
                            Remark = remark7.Text;
                            Profit = WholeSaleUnFormat7 - PurchaseUnFormat7;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox7.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker7.Value.ToLongDateString();
                                Buyer = BuyerTextBox7.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox7.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox7.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"８行目"
                        case 8:
                            Record = 8;
                            MainCategoryCode = MainCategoryCode8;
                            ItemCategoryCode = ItemCategoryCode8;
                            Wholesale = WholeSaleUnFormat8;
                            Remark = remark8.Text;
                            Profit = WholeSaleUnFormat8 - PurchaseUnFormat8;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox8.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker8.Value.ToLongDateString();
                                Buyer = BuyerTextBox8.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox8.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox8.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"９行目"
                        case 9:
                            Record = 9;
                            MainCategoryCode = MainCategoryCode9;
                            ItemCategoryCode = ItemCategoryCode9;
                            Wholesale = WholeSaleUnFormat9;
                            Remark = remark9.Text;
                            Profit = WholeSaleUnFormat9 - PurchaseUnFormat9;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox9.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker9.Value.ToLongDateString();
                                Buyer = BuyerTextBox9.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox9.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox9.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"１０行目"
                        case 10:
                            Record = 10;
                            MainCategoryCode = MainCategoryCode10;
                            ItemCategoryCode = ItemCategoryCode10;
                            Wholesale = WholeSaleUnFormat10;
                            Remark = remark10.Text;
                            Profit = WholeSaleUnFormat10 - PurchaseUnFormat10;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox10.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker10.Value.ToLongDateString();
                                Buyer = BuyerTextBox10.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox10.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox10.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"１１行目"
                        case 11:
                            Record = 11;
                            MainCategoryCode = MainCategoryCode11;
                            ItemCategoryCode = ItemCategoryCode11;
                            Wholesale = WholeSaleUnFormat1;
                            Remark = remark11.Text;
                            Profit = WholeSaleUnFormat11 - PurchaseUnFormat11;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox11.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker11.Value.ToLongDateString();
                                Buyer = BuyerTextBox11.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox11.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox11.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"１２行目"
                        case 12:
                            Record = 12;
                            MainCategoryCode = MainCategoryCode12;
                            ItemCategoryCode = ItemCategoryCode12;
                            Wholesale = WholeSaleUnFormat12;
                            Remark = remark12.Text;
                            Profit = WholeSaleUnFormat12 - PurchaseUnFormat12;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox12.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker12.Value.ToLongDateString();
                                Buyer = BuyerTextBox12.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox12.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox12.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                        #endregion
                        #region"１３行目"
                        case 13:
                            Record = 13;
                            MainCategoryCode = MainCategoryCode13;
                            ItemCategoryCode = ItemCategoryCode13;
                            Wholesale = WholeSaleUnFormat13;
                            Remark = remark13.Text;
                            Profit = WholeSaleUnFormat13 - PurchaseUnFormat13;

                            if (!string.IsNullOrEmpty(WholesalePriceTextBox13.Text) || Wholesale != 0)
                            {
                                SaleDate = BuyDateTimePicker13.Value.ToLongDateString();
                                Buyer = BuyerTextBox13.Text;
                            }
                            else
                            {
                                SaleDate = "";
                                Buyer = "";
                            }

                            #region"チェックボックス"
                            if (NextMonthCheckBox13.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }
                            if (ItemNameChangeCheckBox13.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }

                            #endregion
                            using (transaction = conn.BeginTransaction())
                            {
                                sql_str = "update list_result2 set sale_date = '" + SaleDate + "', main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', wholesale_price = '" + Wholesale + "', buyer = '" + Buyer + "', remarks = '" + Remark + "', carry_over_month = '" + NextMonth + "', profit = '" + Profit + "', item_name_change = '" + ChangeCheck + "' where document_number = '" + SlipNumber + "' and record_number = '" + Record + "';";
                                cmd = new NpgsqlCommand(sql_str, conn);
                                cmd.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            break;
                            #endregion
                    }
                }
                #endregion
            }
            else　                              //未登録
            {
                GradeNumber = int.Parse(GradeNumberTextBox.Text);       //成績番号
                TotalPurchase = PurChase;                               //合計買取金額
                TotalWholesale = WholeSale;                             //合計卸値
                TotalProfit = WholeSale - PurChase;                     //合計利益
                DNumber = SlipNumber;                                   //伝票番号

                Document = SlipNumber;                                  //list_result2 への登録時
                GRADE = int.Parse(GradeNumberTextBox.Text);             //list_result2 への登録時
                AssessmentDate = AssessmentDateTextBox.Text;
                string SQL = "insert into list_result (type, staff_code, result, sum_money, sum_wholesale_price, profit, document_number, registration_date) values('" + type + "','" + staff_id + "','" + GradeNumber + "','" + TotalPurchase + "','" + TotalWholesale + "','" + TotalProfit + "','" + DNumber + "', '"+Registration+"');";
                adapter = new NpgsqlDataAdapter(SQL, conn);
                adapter.Fill(DataTable);

                for (int i = 1; i <= record; i++)
                {
                    switch (i)
                    {
                        #region"１行目"
                        case 1:
                            REMARK = remark1.Text;
                            MainCategoryCode = MainCategoryCode1;
                            ItemCategoryCode = ItemCategoryCode1;
                            Record = 1;
                            UnitPrice = UnitPriceUnFormat1;
                            Purchase = PurchaseUnFormat1;
                            #region"チェックボックス"
                            if (NextMonthCheckBox1.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox1.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox1.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat1;
                                PROFIT = WholeSaleUnFormat1 - PurchaseUnFormat1;
                                BUYDATE = BuyDateTimePicker1.Value.ToLongDateString();
                                BUYER = BuyerTextBox1.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat1 - PurchaseUnFormat1;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data1);
                            break;
                        #endregion
                        #region"２行目"
                        case 2:
                            REMARK = remark2.Text;
                            MainCategoryCode = MainCategoryCode2;
                            ItemCategoryCode = ItemCategoryCode2;
                            Record = 2;
                            UnitPrice = UnitPriceUnFormat2;
                            Purchase = PurchaseUnFormat2;
                            #region"チェックボックス"
                            if (NextMonthCheckBox2.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox2.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox2.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat2;
                                PROFIT = WholeSaleUnFormat2 - PurchaseUnFormat2;
                                BUYDATE = BuyDateTimePicker2.Value.ToLongDateString();
                                BUYER = BuyerTextBox2.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat2 - PurchaseUnFormat2;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data2);
                            break;
                        #endregion
                        #region"３行目"
                        case 3:
                            REMARK = remark3.Text;
                            MainCategoryCode = MainCategoryCode3;
                            ItemCategoryCode = ItemCategoryCode3;
                            Record = 3;
                            UnitPrice = UnitPriceUnFormat3;
                            Purchase = PurchaseUnFormat3;
                            #region"チェックボックス"
                            if (NextMonthCheckBox3.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox3.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox3.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat3;
                                PROFIT = WholeSaleUnFormat3 - PurchaseUnFormat3;
                                BUYDATE = BuyDateTimePicker3.Value.ToLongDateString();
                                BUYER = BuyerTextBox3.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat3 - PurchaseUnFormat3;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data3);
                            break;
                        #endregion
                        #region"４行目"
                        case 4:
                            REMARK = remark4.Text;
                            MainCategoryCode = MainCategoryCode4;
                            ItemCategoryCode = ItemCategoryCode4;
                            Record = 4;
                            UnitPrice = UnitPriceUnFormat4;
                            Purchase = PurchaseUnFormat4;
                            #region"チェックボックス"
                            if (NextMonthCheckBox4.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox4.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox4.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat4;
                                PROFIT = WholeSaleUnFormat4 - PurchaseUnFormat4;
                                BUYDATE = BuyDateTimePicker4.Value.ToLongDateString();
                                BUYER = BuyerTextBox4.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat4 - PurchaseUnFormat4;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data4);
                            break;
                        #endregion
                        #region"５行目"
                        case 5:
                            REMARK = remark5.Text;
                            MainCategoryCode = MainCategoryCode5;
                            ItemCategoryCode = ItemCategoryCode5;
                            Record = 5;
                            UnitPrice = UnitPriceUnFormat5;
                            Purchase = PurchaseUnFormat5;
                            #region"チェックボックス"
                            if (NextMonthCheckBox5.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox5.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox5.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat5;
                                PROFIT = WholeSaleUnFormat5 - PurchaseUnFormat5;
                                BUYDATE = BuyDateTimePicker5.Value.ToLongDateString();
                                BUYER = BuyerTextBox5.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat5 - PurchaseUnFormat5;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data5);
                            break;
                        #endregion
                        #region"６行目"
                        case 6:
                            REMARK = remark6.Text;
                            MainCategoryCode = MainCategoryCode6;
                            ItemCategoryCode = ItemCategoryCode6;
                            Record = 6;
                            UnitPrice = UnitPriceUnFormat6;
                            Purchase = PurchaseUnFormat6;
                            #region"チェックボックス"
                            if (NextMonthCheckBox6.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox6.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox6.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat6;
                                PROFIT = WholeSaleUnFormat6 - PurchaseUnFormat6;
                                BUYDATE = BuyDateTimePicker6.Value.ToLongDateString();
                                BUYER = BuyerTextBox6.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat6 - PurchaseUnFormat6;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data6);
                            break;
                        #endregion
                        #region"７行目"
                        case 7:
                            REMARK = remark7.Text;
                            MainCategoryCode = MainCategoryCode7;
                            ItemCategoryCode = ItemCategoryCode7;
                            Record = 7;
                            UnitPrice = UnitPriceUnFormat7;
                            Purchase = PurchaseUnFormat7;
                            #region"チェックボックス"
                            if (NextMonthCheckBox7.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox7.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox7.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat7;
                                PROFIT = WholeSaleUnFormat7 - PurchaseUnFormat7;
                                BUYDATE = BuyDateTimePicker7.Value.ToLongDateString();
                                BUYER = BuyerTextBox7.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat7 - PurchaseUnFormat7;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data7);
                            break;
                        #endregion
                        #region"８行目"
                        case 8:
                            REMARK = remark8.Text;
                            MainCategoryCode = MainCategoryCode8;
                            ItemCategoryCode = ItemCategoryCode8;
                            Record = 8;
                            UnitPrice = UnitPriceUnFormat8;
                            Purchase = PurchaseUnFormat8;
                            #region"チェックボックス"
                            if (NextMonthCheckBox8.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox8.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox8.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat8;
                                PROFIT = WholeSaleUnFormat8 - PurchaseUnFormat8;
                                BUYDATE = BuyDateTimePicker8.Value.ToLongDateString();
                                BUYER = BuyerTextBox8.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat8 - PurchaseUnFormat8;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data8);
                            break;
                        #endregion
                        #region"９行目"
                        case 9:
                            REMARK = remark9.Text;
                            MainCategoryCode = MainCategoryCode9;
                            ItemCategoryCode = ItemCategoryCode9;
                            Record = 9;
                            UnitPrice = UnitPriceUnFormat9;
                            Purchase = PurchaseUnFormat9;
                            #region"チェックボックス"
                            if (NextMonthCheckBox9.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox9.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox9.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat9;
                                PROFIT = WholeSaleUnFormat9 - PurchaseUnFormat9;
                                BUYDATE = BuyDateTimePicker9.Value.ToLongDateString();
                                BUYER = BuyerTextBox9.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat9 - PurchaseUnFormat9;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data9);
                            break;
                        #endregion
                        #region"１０行目"
                        case 10:
                            REMARK = remark10.Text;
                            MainCategoryCode = MainCategoryCode10;
                            ItemCategoryCode = ItemCategoryCode10;
                            Record = 10;
                            UnitPrice = UnitPriceUnFormat10;
                            Purchase = PurchaseUnFormat10;
                            #region"チェックボックス"
                            if (NextMonthCheckBox10.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox10.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox10.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat10;
                                PROFIT = WholeSaleUnFormat10 - PurchaseUnFormat10;
                                BUYDATE = BuyDateTimePicker10.Value.ToLongDateString();
                                BUYER = BuyerTextBox10.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat10 - PurchaseUnFormat10;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data10);
                            break;
                        #endregion
                        #region"１１行目"
                        case 11:
                            REMARK = remark11.Text;
                            MainCategoryCode = MainCategoryCode11;
                            ItemCategoryCode = ItemCategoryCode11;
                            Record = 11;
                            UnitPrice = UnitPriceUnFormat11;
                            Purchase = PurchaseUnFormat11;
                            #region"チェックボックス"
                            if (NextMonthCheckBox11.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox11.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox11.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat11;
                                PROFIT = WholeSaleUnFormat11 - PurchaseUnFormat11;
                                BUYDATE = BuyDateTimePicker11.Value.ToLongDateString();
                                BUYER = BuyerTextBox11.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat11 - PurchaseUnFormat11;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data11);
                            break;
                        #endregion
                        #region"１２行目"
                        case 12:
                            REMARK = remark12.Text;
                            MainCategoryCode = MainCategoryCode12;
                            ItemCategoryCode = ItemCategoryCode12;
                            Record = 12;
                            UnitPrice = UnitPriceUnFormat12;
                            Purchase = PurchaseUnFormat12;
                            #region"チェックボックス"
                            if (NextMonthCheckBox12.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox12.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox12.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat12;
                                PROFIT = WholeSaleUnFormat12 - PurchaseUnFormat12;
                                BUYDATE = BuyDateTimePicker12.Value.ToLongDateString();
                                BUYER = BuyerTextBox12.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat12 - PurchaseUnFormat12;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data12);
                            break;
                        #endregion
                        #region"１３行目"
                        case 13:
                            REMARK = remark13.Text;
                            MainCategoryCode = MainCategoryCode13;
                            ItemCategoryCode = ItemCategoryCode13;
                            Record = 13;
                            UnitPrice = UnitPriceUnFormat13;
                            Purchase = PurchaseUnFormat13;
                            #region"チェックボックス"
                            if (NextMonthCheckBox13.Checked)
                            {
                                NextMonth = 1;
                            }
                            else
                            {
                                NextMonth = 0;
                            }

                            if (ItemNameChangeCheckBox13.Checked)
                            {
                                ChangeCheck = 1;
                            }
                            else
                            {
                                ChangeCheck = 0;
                            }
                            #endregion
                            if (!string.IsNullOrEmpty(WholesalePriceTextBox13.Text) || Wholesale != 0)
                            {
                                WHOLESALE = WholeSaleUnFormat13;
                                PROFIT = WholeSaleUnFormat13 - PurchaseUnFormat13;
                                BUYDATE = BuyDateTimePicker13.Value.ToLongDateString();
                                BUYER = BuyerTextBox13.Text;
                            }
                            else
                            {
                                WHOLESALE = 0;
                                PROFIT = WholeSaleUnFormat13 - PurchaseUnFormat13;
                                BUYDATE = "";
                                BUYER = "";
                            }

                            sql_str = "insert into list_result2 (assessment_date, main_category_code, item_code, carry_over_month, record_number, unit_price, money, item_name_change, document_number, grade_number, wholesale_price, profit, remarks, sale_date, buyer) values ('" + AssessmentDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + NextMonth + "','" + Record + "','" + UnitPrice + "','" + Purchase + "','" + ChangeCheck + "','" + Document + "','" + GRADE + "','" + WHOLESALE + "','" + PROFIT + "','" + REMARK + "','" + BUYDATE + "','" + BUYER + "');";
                            adapter = new NpgsqlDataAdapter(sql_str, conn);
                            adapter.Fill(Data13);
                            break;
                            #endregion
                    }
                }
            }
            conn.Close();
            MessageBox.Show("入力されたデータを登録しました。" + "\r\n" + "品名変更画面に移動します。", "操作確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NameChangeButton = true;
            NameChange = false;
            ItemNameChange nameChange = new ItemNameChange(recordList, int.Parse(GradeNumberTextBox.Text), staff_id, SlipNumber, Pass, Access_auth, NameChange, CarryOver, MonthCatalog);
            screan = false;
            this.Close();
            nameChange.Show();
        }
        #endregion

        #region"お客様情報ボタン"
        private void ClientInformationButton_Click(object sender, EventArgs e)
        {
            if (NameChange)
            {
                MessageBox.Show("再登録を行ってください", "再登録を行う必要があります", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ClientInformation clientInformation = new ClientInformation(recordList, staff_id, staff_name, type, SlipNumber, AntiqueNumber, ID_Number, Pass, grade, Access_auth, MonthCatalog, CarryOver);
            screan = false;
            this.Close();
            clientInformation.Show();
        }
        #endregion

        #region"月間成績表"
        private void button3_Click(object sender, EventArgs e)
        {
            if (NameChange)
            {
                MessageBox.Show("再登録を行っていください。", "再登録を行う必要があります", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MonthCatalog = true;
            MonResult monResult = new MonResult(mainmenu, staff_id, Access_auth, staff_name, type, SlipNumber, Pass, grade, CarryOver, MonthCatalog);
            screan = false;
            this.Close();
            monResult.Show();
        }
        #endregion

        #region"値の検証"
        private void WholesalePriceTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void WholesalePriceTextBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }
        #endregion

        #region"納品書検索"
        private void DeliverySearchButton_Click(object sender, EventArgs e)
        {
            Control = int.Parse(ManagementNumberTextBox.Text);
            DeliveryButton = true;
            this.Close();
        }
        #endregion

        private void RecordList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!NameChangeButton)
            {
                //画面を閉じる前に登録するかの確認
                result = MessageBox.Show("入力されている項目を登録しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    NotFinish = false;
                    RegisterButton_Click(sender, e);
                }
            }

            //品名を変更してまだ登録をしていないとき
            if (NameChange)
            {
                MessageBox.Show("品名を変更しましたが登録できていません"+"\r\n"+"入力画面に戻ります。", "登録未完了", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }

            //納品書検索をクリック
            if (DeliveryButton)
            {
                MessageBox.Show("納品書に移動します", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //次月持ち越しから画面遷移したとき
            else if (CarryOver && screan)
            {
                MessageBox.Show("未入力・次月持ち越し画面に移動します。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //計算書から画面遷移してお客様情報・月間成績一覧・品名変更画面に画面遷移しないとき
            else if (screan)
            {
                MessageBox.Show("計算書に移動します。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region"BuyDateTimePicker"
        #region"datetimepicker メソッド"
        private void setDateTimePicker1(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker1.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker1.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker1.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker1.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker2(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker2.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker2.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker2.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker2.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker3(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker3.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker3.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker3.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker3.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker4(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker4.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker4.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker4.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker4.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker5(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker5.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker5.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker5.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker5.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker6(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker6.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker6.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker6.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker6.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker7(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker7.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker7.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker7.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker7.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker8(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker8.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker8.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker8.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker8.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker9(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker9.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker9.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker9.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker9.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker10(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker10.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker10.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker10.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker10.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker11(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker11.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker11.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker11.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker11.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker12(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker12.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker12.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker12.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker12.Value = (DateTime)dateTime;
            }
        }
        private void setDateTimePicker13(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                BuyDateTimePicker13.Format = DateTimePickerFormat.Custom;
                BuyDateTimePicker13.CustomFormat = " ";
            }
            else
            {
                BuyDateTimePicker13.Format = DateTimePickerFormat.Long;
                BuyDateTimePicker13.Value = (DateTime)dateTime;
            }
        }
        #endregion

        #region"buydatetimepicker1"
        private void BuyDateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker1.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date1 = null;
                setDateTimePicker1(date1);
            }
        }

        private void BuyDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            date1 = BuyDateTimePicker1.Value;
            setDateTimePicker1(date1);
        }
        #endregion

        #region"buydatetimepicker2"
        private void BuyDateTimePicker2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker2.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date2 = null;
                setDateTimePicker2(date2);
            }
        }

        private void BuyDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            date2 = BuyDateTimePicker2.Value;
            setDateTimePicker2(date2);
        }
        #endregion

        #region"buydatetimepicker3"
        private void BuyDateTimePicker3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker3.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date3 = null;
                setDateTimePicker3(date3);
            }
        }

        private void BuyDateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            date3 = BuyDateTimePicker3.Value;
            setDateTimePicker3(date3);
        }
        #endregion

        #region"buydatetimepicker4"
        private void BuyDateTimePicker4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker4.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date4 = null;
                setDateTimePicker4(date4);
            }
        }

        private void BuyDateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            date4 = BuyDateTimePicker4.Value;
            setDateTimePicker4(date4);
        }
        #endregion

        #region"buydatetimepicker5"
        private void BuyDateTimePicker5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker5.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date5 = null;
                setDateTimePicker5(date5);
            }
        }

        private void BuyDateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            date5 = BuyDateTimePicker5.Value;
            setDateTimePicker5(date5);
        }
        #endregion

        #region"buydatetimepicker6"
        private void BuyDateTimePicker6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker6.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date6 = null;
                setDateTimePicker6(date6);
            }
        }

        private void BuyDateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            date6 = BuyDateTimePicker6.Value;
            setDateTimePicker6(date6);
        }
        #endregion

        #region"buydatetimepicker7"
        private void BuyDateTimePicker7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker7.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date7 = null;
                setDateTimePicker7(date7);
            }
        }

        private void BuyDateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            date7 = BuyDateTimePicker7.Value;
            setDateTimePicker7(date7);
        }
        #endregion

        #region"buydatetimepicker8"
        private void BuyDateTimePicker8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker8.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date8 = null;
                setDateTimePicker8(date8);
            }
        }

        private void BuyDateTimePicker8_ValueChanged(object sender, EventArgs e)
        {
            date8 = BuyDateTimePicker8.Value;
            setDateTimePicker8(date8);
        }
        #endregion

        #region"buydatetimepicker9"
        private void BuyDateTimePicker9_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker9.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date9 = null;
                setDateTimePicker9(date9);
            }
        }

        private void BuyDateTimePicker9_ValueChanged(object sender, EventArgs e)
        {
            date9 = BuyDateTimePicker9.Value;
            setDateTimePicker9(date9);
        }
        #endregion

        #region"buydatetimepicker10"
        private void BuyDateTimePicker10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker10.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date10 = null;
                setDateTimePicker10(date10);
            }
        }

        private void BuyDateTimePicker10_ValueChanged(object sender, EventArgs e)
        {
            date10 = BuyDateTimePicker10.Value;
            setDateTimePicker10(date10);
        }
        #endregion

        #region"buydatetimepicker11"
        private void BuyDateTimePicker11_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker11.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date11 = null;
                setDateTimePicker11(date11);
            }
        }

        private void BuyDateTimePicker11_ValueChanged(object sender, EventArgs e)
        {
            date11 = BuyDateTimePicker11.Value;
            setDateTimePicker11(date11);
        }
        #endregion

        #region"buydatetimepicker12"
        private void BuyDateTimePicker12_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker12.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date12 = null;
                setDateTimePicker12(date12);
            }
        }

        private void BuyDateTimePicker12_ValueChanged(object sender, EventArgs e)
        {
            date12 = BuyDateTimePicker12.Value;
            setDateTimePicker12(date12);
        }
        #endregion

        #region"buydatetimepicker13"
        private void BuyDateTimePicker13_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < 0 || e.Y > BuyDateTimePicker13.Height)
            {
                SendKeys.SendWait("%{DOWN}");
            }
        }

        private void BuyDateTimePicker13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape || e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
            {
                date13 = null;
                setDateTimePicker13(date13);
            }
        }

        private void BuyDateTimePicker13_ValueChanged(object sender, EventArgs e)
        {
            date13 = BuyDateTimePicker13.Value;
            setDateTimePicker13(date13);
        }
        #endregion

        #endregion
    }
}
