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
using System.Threading.Tasks;
using System.Windows.Forms;

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
        string access_auth;
        string staff_name;
        string address;

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
        string Name;                        //名前（個人）
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

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        NpgsqlDataAdapter adapter;

        //list_resultに登録するようのデータテイブル
        DataTable DataTable = new DataTable();         
        #region"list_result2 に登録する各行のデータテイブル"
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

        public RecordList(Statement statement, int staff_id, string Staff_Name , int type , string slipnumber)
        {
            InitializeComponent();
            this.statement = statement;
            this.staff_id = staff_id;
            this.staff_name = Staff_Name;
            this.type = type;
            this.SlipNumber = slipnumber;
        }

        private void RecordList_Load(object sender, EventArgs e)
        {
            #region"画面上の会社・個人情報と合計金額"
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            StaffNameTextBox.Text = staff_name;

            #region"削除予定"
            /*
             *計算書で登録しないと
             *成績一覧や印刷プレビューを押せないようにしたら
             *削除予定
            */
            string sql_str = "select * from statement_data where staff_code = '" + staff_id + "';";
            cmd = new NpgsqlCommand(sql_str, conn);
            conn.Open();
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    SlipNumber = reader["document_number"].ToString();
                }
            }
            conn.Close();
            /*
             * 削除予定範囲終了
             * 
            */
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
            conn.Open();

            using (reader = cmd.ExecuteReader())
            {
                if (type == 0)
                {
                    NameOrCompanyNameLabel.Text = "会社名";
                    OccupationOrShopNameLabel.Text = "店舗名";
                    AddressOrClientStaffNameLabel.Text = "担当者名";
                    AddressOrClientStaffNameTextBox.Size = new System.Drawing.Size(130, 20);
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
                    AddressOrClientStaffNameTextBox.Location = new System.Drawing.Point(420, 85);

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
            string Sql_Str = "select * from statement_calc_data where document_number = '" + SlipNumber + "';";
            //伝票番号と表の行番号から表のデータを取得し、大分類マスタと品名マスタを結合
            string Sql_Str1 = "select * from statement_calc_data inner join main_category_m on statement_calc_data.main_category_code = main_category_m.main_category_code inner join item_m on statement_calc_data.item_code = item_m.item_code where document_number = '" + SlipNumber + "';";

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

            conn.Close();

            #region"大分類ごとの金額"

            if (record >= 1)
            {
                switch (MainCategoryCode1)
                {
                    case 100:
                        PurChaseMetal = PurchaseUnFormat1;
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat1;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat1;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat1;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat1;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat2 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat2 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat2 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat2 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat3 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat3 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat3 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat3 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat4 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat4 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat4 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat4 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat5 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat5 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat5 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat5 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat6 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat6 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat6 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat6 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat7 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat7 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat7 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat7 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat8 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat8 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat8 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat8 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat9 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat9 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat9 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat9 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat10 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat10 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat10 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat10 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat11 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat11 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat11 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat11 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat12 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat12 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat12 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat12 + PurChaseOther;
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
                        MetalPurchaseTextBox.Text = string.Format("{0:C}", PurChaseMetal);
                        break;
                    case 101:
                        PurChaseDiamond = PurchaseUnFormat13 + PurChaseDiamond;
                        DiamondPurchaseTextBox.Text = string.Format("{0:C}", PurChaseDiamond);
                        break;
                    case 102:
                        PurChaseBrand = PurchaseUnFormat13 + PurChaseBrand;
                        BrandPurchaseTextBox.Text = string.Format("{0:C}", PurChaseBrand);
                        break;
                    case 103:
                        PurChaseProduct = PurchaseUnFormat13 + PurChaseProduct;
                        ProductPurchaseTextBox.Text = string.Format("{0:C}", PurChaseProduct);
                        break;
                    case 104:
                        PurChaseOther = PurchaseUnFormat13 + PurChaseOther;
                        OtherPurchaseTextBox.Text = string.Format("{0:C}", PurChaseOther);
                        break;
                }
            }
            #endregion

            #endregion

            ///<summary>
            ///納品書を検索できるようにする機能を追加したら削除予定
            ///範囲開始
            /// </summary>
            ///
            int x = 750;
            AssessmentLabel.Left = x;
            AssessmentDateTextBox.Left = x + 50;
            SlipNumberLabel.Left = x + 200;
            SlipNumberTextBox.Left = x + 260;
            GradeNumberLabel.Left = x + 360;
            GradeNumberTextBox.Left = x + 420;
            ///<summary>
            ///納品書を検索できるようにする機能を追加したら削除予定
            /// 範囲終了
            /// </summary>
        }

        #region"成績入力画面から計算書へ"
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            statement = new Statement(mainmenu, staff_id, type, staff_name, address, SlipNumber, access_auth);

            this.Close();
            statement.Show();
        }

        private void RecordList_FormClosed(object sender, FormClosedEventArgs e)
        {
            statement.Show();
        }



        #endregion

        #region"下の表"

        #region"上の表で卸値入力 -> 左下の表（大分類毎と合計）と右下の表の卸値記入（￥マーク設定済み）Leaveイベント"
        private void WholesalePriceTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
            {
                return;
            }

            switch (MainCategoryCode1)
            {
                case 100:
                    WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                    WholeSaleMetal = WholeSaleUnFormat1 + WholeSaleMetal;                                 //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                    WholeSaleDiamond = WholeSaleUnFormat1 + WholeSaleDiamond;                                 //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                    WholeSaleBrand = WholeSaleUnFormat1 + WholeSaleBrand;                                 //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                    WholeSaleProduct = WholeSaleUnFormat1 + WholeSaleProduct;                                 //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat1 + WholeSale;                                        //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat1 = decimal.Parse(WholesalePriceTextBox1.Text);
                    WholeSaleOther = WholeSaleUnFormat1 + WholeSaleOther;                                 //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat1 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox1.Text = string.Format("{0:C}", WholeSaleUnFormat1);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }

        private void WholesalePriceTextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
            {
                return;
            }

            switch (MainCategoryCode2)
            {
                case 100:
                    WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                    WholeSaleMetal = WholeSaleUnFormat2 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                    WholeSaleDiamond = WholeSaleUnFormat2 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                    WholeSaleBrand = WholeSaleUnFormat2 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                    WholeSaleProduct = WholeSaleUnFormat2 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat2 = decimal.Parse(WholesalePriceTextBox2.Text);
                    WholeSaleOther = WholeSaleUnFormat2 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat2 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox2.Text = string.Format("{0:C}", WholeSaleUnFormat2);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
            {
                return;
            }

            switch (MainCategoryCode3)
            {
                case 100:
                    WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                    WholeSaleMetal = WholeSaleUnFormat3 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                    WholeSaleDiamond = WholeSaleUnFormat3 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                    WholeSaleBrand = WholeSaleUnFormat3 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                    WholeSaleProduct = WholeSaleUnFormat3 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat3 = decimal.Parse(WholesalePriceTextBox3.Text);
                    WholeSaleOther = WholeSaleUnFormat3 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat3 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox3.Text = string.Format("{0:C}", WholeSaleUnFormat3);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
            {
                return;
            }

            switch (MainCategoryCode4)
            {
                case 100:
                    WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                    WholeSaleMetal = WholeSaleUnFormat4 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                    WholeSaleDiamond = WholeSaleUnFormat4 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                    WholeSaleBrand = WholeSaleUnFormat4 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                    WholeSaleProduct = WholeSaleUnFormat4 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat4 = decimal.Parse(WholesalePriceTextBox4.Text);
                    WholeSaleOther = WholeSaleUnFormat4 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat4 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox4.Text = string.Format("{0:C}", WholeSaleUnFormat4);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
            {
                return;
            }

            switch (MainCategoryCode5)
            {
                case 100:
                    WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                    WholeSaleMetal = WholeSaleUnFormat5 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                    WholeSaleDiamond = WholeSaleUnFormat5 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                    WholeSaleBrand = WholeSaleUnFormat5 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                    WholeSaleProduct = WholeSaleUnFormat5 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat5 = decimal.Parse(WholesalePriceTextBox5.Text);
                    WholeSaleOther = WholeSaleUnFormat5 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat5 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox5.Text = string.Format("{0:C}", WholeSaleUnFormat5);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }

        private void WholesalePriceTextBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
            {
                return;
            }

            switch (MainCategoryCode6)
            {
                case 100:
                    WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                    WholeSaleMetal = WholeSaleUnFormat6 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                    WholeSaleDiamond = WholeSaleUnFormat6 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                    WholeSaleBrand = WholeSaleUnFormat6 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                    WholeSaleProduct = WholeSaleUnFormat6 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat6 = decimal.Parse(WholesalePriceTextBox6.Text);
                    WholeSaleOther = WholeSaleUnFormat6 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat6 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox6.Text = string.Format("{0:C}", WholeSaleUnFormat6);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
            {
                return;
            }

            switch (MainCategoryCode7)
            {
                case 100:
                    WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                    WholeSaleMetal = WholeSaleUnFormat7 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                    WholeSaleDiamond = WholeSaleUnFormat7 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                    WholeSaleBrand = WholeSaleUnFormat7 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                    WholeSaleProduct = WholeSaleUnFormat7 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat7 = decimal.Parse(WholesalePriceTextBox7.Text);
                    WholeSaleOther = WholeSaleUnFormat7 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat7 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox7.Text = string.Format("{0:C}", WholeSaleUnFormat7);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox8_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
            {
                return;
            }

            switch (MainCategoryCode8)
            {
                case 100:
                    WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                    WholeSaleMetal = WholeSaleUnFormat8 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                    WholeSaleDiamond = WholeSaleUnFormat8 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                    WholeSaleBrand = WholeSaleUnFormat8 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                    WholeSaleProduct = WholeSaleUnFormat8 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat8 = decimal.Parse(WholesalePriceTextBox8.Text);
                    WholeSaleOther = WholeSaleUnFormat8 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat8 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox8.Text = string.Format("{0:C}", WholeSaleUnFormat8);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox9_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
            {
                return;
            }

            switch (MainCategoryCode9)
            {
                case 100:
                    WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                    WholeSaleMetal = WholeSaleUnFormat9 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                    WholeSaleDiamond = WholeSaleUnFormat9 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                    WholeSaleBrand = WholeSaleUnFormat9 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                    WholeSaleProduct = WholeSaleUnFormat9 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat9 = decimal.Parse(WholesalePriceTextBox9.Text);
                    WholeSaleOther = WholeSaleUnFormat9 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat9 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox9.Text = string.Format("{0:C}", WholeSaleUnFormat9);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
            {
                return;
            }

            switch (MainCategoryCode10)
            {
                case 100:
                    WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                    WholeSaleMetal = WholeSaleUnFormat10 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                    WholeSaleDiamond = WholeSaleUnFormat10 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                    WholeSaleBrand = WholeSaleUnFormat10 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                    WholeSaleProduct = WholeSaleUnFormat10 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat10 = decimal.Parse(WholesalePriceTextBox10.Text);
                    WholeSaleOther = WholeSaleUnFormat10 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat10 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox10.Text = string.Format("{0:C}", WholeSaleUnFormat10);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox11_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
            {
                return;
            }

            switch (MainCategoryCode11)
            {
                case 100:
                    WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                    WholeSaleMetal = WholeSaleUnFormat11 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                    WholeSaleDiamond = WholeSaleUnFormat11 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                    WholeSaleBrand = WholeSaleUnFormat11 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                    WholeSaleProduct = WholeSaleUnFormat11 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat11 = decimal.Parse(WholesalePriceTextBox11.Text);
                    WholeSaleOther = WholeSaleUnFormat11 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat11 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox11.Text = string.Format("{0:C}", WholeSaleUnFormat11);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox12_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
            {
                return;
            }

            switch (MainCategoryCode12)
            {
                case 100:
                    WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                    WholeSaleMetal = WholeSaleUnFormat12 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                    WholeSaleDiamond = WholeSaleUnFormat12 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                    WholeSaleBrand = WholeSaleUnFormat12 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                    WholeSaleProduct = WholeSaleUnFormat12 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat12 = decimal.Parse(WholesalePriceTextBox12.Text);
                    WholeSaleOther = WholeSaleUnFormat12 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat12 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox12.Text = string.Format("{0:C}", WholeSaleUnFormat12);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        private void WholesalePriceTextBox13_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
            {
                return;
            }

            switch (MainCategoryCode13)
            {
                case 100:
                    WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                    WholeSaleMetal = WholeSaleUnFormat13 + WholeSaleMetal;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                    MetalWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleMetal);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 101:
                    WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                    WholeSaleDiamond = WholeSaleUnFormat13 + WholeSaleDiamond;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                    DiamondWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleDiamond);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 102:
                    WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                    WholeSaleBrand = WholeSaleUnFormat13 + WholeSaleBrand;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                    BrandWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleBrand);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 103:
                    WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                    WholeSaleProduct = WholeSaleUnFormat13 + WholeSaleProduct;                        //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                    ProductWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleProduct);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
                case 104:
                    WholeSaleUnFormat13 = decimal.Parse(WholesalePriceTextBox13.Text);
                    WholeSaleOther = WholeSaleUnFormat13 + WholeSaleOther;                            //左下表の地金の卸値に入力
                    WholeSale = WholeSaleUnFormat13 + WholeSale;                                      //卸値の合計

                    //￥マーク表示
                    WholesalePriceTextBox13.Text = string.Format("{0:C}", WholeSaleUnFormat13);
                    OtherWholesaleTextBox.Text = string.Format("{0:C}", WholeSaleOther);
                    TotalWholesaleTextBox.Text = string.Format("{0:C}", WholeSale);
                    WholesaleTotalTextBox.Text = string.Format("{0:C}", WholeSale);
                    break;
            }
        }
        #endregion

        #region"金額が下の表に表示されたら合計金額表示（￥マーク設定済み）TextChangedイベント"
        private void MetalPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurChase = PurChaseMetal + PurChase;
                TotalPurchaseTextBox.Text = string.Format("{0:C}", PurChase);
                PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
            }
            else
            {
                first = true;
            }
        }
        private void DiamondPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurChase = PurChaseDiamond + PurChase;
                TotalPurchaseTextBox.Text = string.Format("{0:C}", PurChase);
                PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
            }
            else
            {
                first = true;
            }
        }
        private void BrandPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurChase = PurChaseBrand + PurChase;
                TotalPurchaseTextBox.Text = string.Format("{0:C}", PurChase);
                PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
            }
            else
            {
                first = true;
            }
        }
        private void ProductPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurChase = PurChaseProduct + PurChase;
                TotalPurchaseTextBox.Text = string.Format("{0:C}", PurChase);
                PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
            }
            else
            {
                first = true;
            }
        }
        private void OtherPurchaseTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                PurChase = PurChaseOther + PurChase;
                TotalPurchaseTextBox.Text = string.Format("{0:C}", PurChase);
                PurchaseTotalTextBox.Text = string.Format("{0:C}", PurChase);
            }
            else
            {
                first = true;
            }
        }
        #endregion

        #region"下の表に卸値が表示されたら左下の表に大分類ごとに利益表示（￥マーク設定済み）TextChangedイベント"
        private void MetalWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                ProFitMetal = WholeSaleMetal - PurChaseMetal;
                MetalProfitTextBox.Text = string.Format("{0:C}", ProFitMetal);
            }
            else
            {
                first = true;
            }
        }

        private void DiamondWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                ProFitDiamond = WholeSaleDiamond - PurChaseDiamond;
                DiamondProfitTextBox.Text = string.Format("{0:C}", ProFitDiamond);
            }
            else
            {
                first = true;
            }
        }

        private void BrandWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                ProFitBrand = WholeSaleBrand - PurChaseBrand;
                BrandProfitTextBox.Text = string.Format("{0:C}", ProFitBrand);
            }
            else
            {
                first = true;
            }
        }

        private void ProductWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                ProFitProduct = WholeSaleProduct - PurChaseProduct;
                ProductProfitTextBox.Text = string.Format("{0:C}", ProFitProduct);
            }
            else
            {
                first = true;
            }
        }

        private void OtherWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                ProFitOther = WholeSaleOther - PurChaseOther;
                OtherProfitTextBox.Text = string.Format("{0:C}", ProFitOther);
            }
            else
            {
                first = true;
            }
        }
        #endregion

        #region"左下表と右下表の利益の合計表示（\マーク設定済み）TextChangedイベント"
        private void TotalWholesaleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                ProFit = WholeSale - PurChase;
                TotalProfitTextBox.Text = string.Format("{0:C}", ProFit);
                ProfitTotalTextBox.Text = string.Format("{0:C}", ProFit);
            }
            else
            {
                first = true;
            }
        }

        #endregion

        #endregion

        #region"上の表"

        #region"次月持ち越しチェック"
        private void NextMonthCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox1.Checked)
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
            else
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

        private void NextMonthCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox2.Checked)
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
            else
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

        private void NextMonthCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox3.Checked)
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
            else
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

        private void NextMonthCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox4.Checked)
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
            else
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

        private void NextMonthCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox5.Checked)
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
            else
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

        private void NextMonthCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox6.Checked)
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
            else
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

        private void NextMonthCheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox7.Checked)
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
            else
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

        private void NextMonthCheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox8.Checked)
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
            else
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

        private void NextMonthCheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox9.Checked)
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
            else
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

        private void NextMonthCheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox10.Checked)
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
            else
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

        private void NextMonthCheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox11.Checked)
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
            else
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

        private void NextMonthCheckBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox12.Checked)
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
            else
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

        private void NextMonthCheckBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (NextMonthCheckBox13.Checked)
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
            else
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

        #region"単価の数値を３桁区切りで表示 TextChangedイベント"
        private void unitPriceText1_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat1 = decimal.Parse(unitPriceText1.Text);
                unitPriceText1.Text = string.Format("{0:#,0}", UnitPriceUnFormat1);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText2_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat2 = decimal.Parse(unitPriceText2.Text);
                unitPriceText2.Text = string.Format("{0:#,0}", UnitPriceUnFormat2);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText3_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat3 = decimal.Parse(unitPriceText3.Text);
                unitPriceText3.Text = string.Format("{0:#,0}", UnitPriceUnFormat3);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText4_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat4 = decimal.Parse(unitPriceText4.Text);
                unitPriceText4.Text = string.Format("{0:#,0}", UnitPriceUnFormat4);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText5_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat5 = decimal.Parse(unitPriceText5.Text);
                unitPriceText5.Text = string.Format("{0:#,0}", UnitPriceUnFormat5);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText6_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat6 = decimal.Parse(unitPriceText6.Text);
                unitPriceText6.Text = string.Format("{0:#,0}", UnitPriceUnFormat6);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText7_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat7 = decimal.Parse(unitPriceText7.Text);
                unitPriceText7.Text = string.Format("{0:#,0}", UnitPriceUnFormat7);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText8_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat8 = decimal.Parse(unitPriceText8.Text);
                unitPriceText8.Text = string.Format("{0:#,0}", UnitPriceUnFormat8);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText9_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat9 = decimal.Parse(unitPriceText9.Text);
                unitPriceText9.Text = string.Format("{0:#,0}", UnitPriceUnFormat9);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText10_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat10 = decimal.Parse(unitPriceText10.Text);
                unitPriceText10.Text = string.Format("{0:#,0}", UnitPriceUnFormat10);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText11_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat11 = decimal.Parse(unitPriceText11.Text);
                unitPriceText11.Text = string.Format("{0:#,0}", UnitPriceUnFormat11);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText12_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat12 = decimal.Parse(unitPriceText12.Text);
                unitPriceText12.Text = string.Format("{0:#,0}", UnitPriceUnFormat12);
            }
            else
            {
                first = true;
            }
        }

        private void unitPriceText13_TextChanged(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                UnitPriceUnFormat13 = decimal.Parse(unitPriceText13.Text);
                unitPriceText13.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceText13.Text));
            }
            else
            {
                first = true;
            }
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

        #endregion


        #region"集計ボタンクリック"
        private void UpdateSearchButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("登録・検索しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            //伝票番号は引数から使用
            DateTime dt = new DateTime();
            Registration = dt.ToLongDateString();
            GradeNumber = int.Parse(GradeNumberTextBox.Text);                   //消すかも？
            TotalPurchase = PurChase;
            TotalWholesale = WholeSale;
            TotalProfit = ProFit;
            DNumber = SlipNumber;
            MetalPurchase = PurChaseMetal;
            MetalWholesale = WholeSaleMetal;
            MetalProfit = ProFitMetal;
            DiamondPurchase = PurChaseDiamond;
            DiamondWholesale = WholeSaleDiamond;
            DiamondProfit = ProFitDiamond;
            BrandPurchase = PurChaseBrand;
            BrandWholesale = WholeSaleBrand;
            BrandProfit = ProFitBrand;
            ProductPurchase = PurChaseProduct;
            ProductWholesale = WholeSaleProduct;
            ProductProfit = ProFitProduct;
            OtherPurchase = PurChaseOther;
            OtherWholesale = WholeSaleOther;
            OtherProfit = ProFitOther;
            ControlNumber = int.Parse(ManagementNumberTextBox.Text);

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();
            string sql_str = "";

            if (type == 0)
            {
                CName = NameOrCompanyNameTextBox.Text;
                CShop = OccupationOrShopNameTextBox.Text;
                CStaff = AddressOrClientStaffNameTextBox.Text;

                sql_str = "Insert into list_result(company_name, shop_name, staff_name, type, staff_code, registration_date, result, sum_money, sum_wholesale_price, profit, document_number, metal_purchase, metal_wholesale, metal_profit, diamond_purchase, diamond_wholesale, diamond_profit, brand_purchase, brand_wholesale, brand_profit, product_purchase, product_wholesale, product_profit, other_purchase, other_wholesale, other_profit, control_number) values ('" + CName + "','" + CShop + "','" + CStaff + "','" + 0 + "','" + staff_id + "','" + Registration + "','" + GradeNumber + "','" + TotalPurchase + "','" + TotalWholesale + "','" + TotalProfit + "','" + DNumber + "','" + MetalPurchase + "','" + MetalWholesale + "','" + MetalProfit + "','" + DiamondPurchase + "','" + DiamondWholesale + "','" + DiamondProfit + "','" + BrandPurchase + "','" + BrandWholesale + "','" + BrandProfit + "','" + ProductPurchase + "','" + ProductWholesale + "','" + ProductProfit + "','" + OtherPurchase + "','" + OtherWholesale + "','" + OtherProfit + "','" + ControlNumber + "');";
            }
            else if (type == 1)
            {
                Name = NameOrCompanyNameTextBox.Text;
                Occupation = OccupationOrShopNameTextBox.Text;
                Address = AddressOrClientStaffNameTextBox.Text;
                BirthDay = BirthdayTextBox.Text;

                sql_str = "Insert into list_result(name, address, occupation, birthday, staff_code, registration_date, result, sum_money, sum_wholesale_price, profit, document_number, metal_purchase, metal_wholesale, metal_profit, diamond_purchase, diamond_wholesale, diamond_profit, brand_purchase, brand_wholesale, brand_profit, product_purchase, product_wholesale, product_profit, other_purchase, other_wholesale, other_profit, control_number) values ('" + Name + "','" + Address + "','" + Occupation + "','" + BirthDay + "','" + 1 + "','" + staff_id + "','" + Registration + "','" + GradeNumber + "','" + TotalPurchase + "','" + TotalWholesale + "','" + TotalProfit + "','" + DNumber + "','" + MetalPurchase + "','" + MetalWholesale + "','" + MetalProfit + "','" + DiamondPurchase + "','" + DiamondWholesale + "','" + DiamondProfit + "','" + BrandPurchase + "','" + BrandWholesale + "','" + BrandProfit + "','" + ProductPurchase + "','" + ProductWholesale + "','" + ProductProfit + "','" + OtherPurchase + "','" + OtherWholesale + "','" + OtherProfit + "','" + ControlNumber + "');";
            }
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(DataTable);

            //卸値 -> 売却日と売却先を登録

            AssessmentDate = AssessmentDateTextBox.Text;
            string Sql_Str = "";

            for (int i = 1; i <= record; i++)
            {
                switch (i) {
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox1.Text))
                        {
                            SaleDate = BuyDateTimePicker1.Value.ToLongDateString();
                            Buyer = BuyerTextBox1.Text;
                        }

                        if (NextMonthCheckBox1.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data1);
                        
                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox2.Text))
                        {
                            SaleDate = BuyDateTimePicker2.Value.ToLongDateString();
                            Buyer = BuyerTextBox2.Text;
                        }

                        if (NextMonthCheckBox2.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data2);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox3.Text))
                        {
                            SaleDate = BuyDateTimePicker3.Value.ToLongDateString();
                            Buyer = BuyerTextBox3.Text;
                        }

                        if (NextMonthCheckBox3.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data3);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox4.Text))
                        {
                            SaleDate = BuyDateTimePicker4.Value.ToLongDateString();
                            Buyer = BuyerTextBox4.Text;
                        }

                        if (NextMonthCheckBox4.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data4);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox5.Text))
                        {
                            SaleDate = BuyDateTimePicker5.Value.ToLongDateString();
                            Buyer = BuyerTextBox5.Text;
                        }

                        if (NextMonthCheckBox5.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data5);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox6.Text))
                        {
                            SaleDate = BuyDateTimePicker6.Value.ToLongDateString();
                            Buyer = BuyerTextBox6.Text;
                        }

                        if (NextMonthCheckBox6.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data6);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox7.Text))
                        {
                            SaleDate = BuyDateTimePicker7.Value.ToLongDateString();
                            Buyer = BuyerTextBox7.Text;
                        }

                        if (NextMonthCheckBox7.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data7);

                        break;
                    case 8:
                        record = 8;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox8.Text))
                        {
                            SaleDate = BuyDateTimePicker8.Value.ToLongDateString();
                            Buyer = BuyerTextBox8.Text;
                        }

                        if (NextMonthCheckBox8.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data8);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox9.Text))
                        {
                            SaleDate = BuyDateTimePicker9.Value.ToLongDateString();
                            Buyer = BuyerTextBox9.Text;
                        }

                        if (NextMonthCheckBox9.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data9);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox10.Text))
                        {
                            SaleDate = BuyDateTimePicker10.Value.ToLongDateString();
                            Buyer = BuyerTextBox10.Text;
                        }

                        if (NextMonthCheckBox10.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data10);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox11.Text))
                        {
                            SaleDate = BuyDateTimePicker11.Value.ToLongDateString();
                            Buyer = BuyerTextBox11.Text;
                        }

                        if (NextMonthCheckBox11.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data11);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox12.Text))
                        {
                            SaleDate = BuyDateTimePicker12.Value.ToLongDateString();
                            Buyer = BuyerTextBox12.Text;
                        }

                        if (NextMonthCheckBox12.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data12);

                        break;
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

                        if (!string.IsNullOrEmpty(WholesalePriceTextBox13.Text))
                        {
                            SaleDate = BuyDateTimePicker13.Value.ToLongDateString();
                            Buyer = BuyerTextBox13.Text;
                        }

                        if (NextMonthCheckBox13.Checked)
                        {
                            NextMonth = 1;
                        }
                        Sql_Str = "Insert into list_result2(assessment_date, sale_date, main_category_code, item_code, money, wholesale_price, buyer, remarks, carry_over_month, grade_number, record_number, document_number, profit, item_detail, weight, unit_price, count) values('" + AssessmentDate + "','" + SaleDate + "','" + MainCategoryCode + "','" + ItemCategoryCode + "','" + Purchase + "','" + Wholesale + "','" + Buyer + "','" + Remark + "','" + NextMonth + "','" + GradeNumber + "','" + Record + "','" + DNumber + "','" + Profit + "','" + ItemDetail + "','" + Weight + "','" + UnitPrice + "','" + Count + "');";
                        adapter = new NpgsqlDataAdapter(Sql_Str, conn);
                        adapter.Fill(Data13);

                        break;
                }
            }

            MessageBox.Show("登録しました", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        #endregion
    }
}
