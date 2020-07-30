using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        Statement statement;
        string SlipNumber;                  //伝票番号
        int record;                         //行数
        string Birthday;                    //個人の誕生日
        DateTime BirthdayDate;

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

        /*"品名コード取得用"
        #region
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
        */

        DataTable dataTable = new DataTable();

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        NpgsqlDataAdapter adapter;

        public RecordList(Statement statement, int staff_id, string slipnumber)
        {
            InitializeComponent();
            this.statement = statement;
            this.staff_id = staff_id;
            this.SlipNumber = slipnumber;
        }

        private void RecordList_Load(object sender, EventArgs e)
        {
            #region"画面上の会社・個人情報と合計金額"
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select staff_name from staff_m where staff_code = '" + staff_id + "';";　//担当者名取得用
            cmd = new NpgsqlCommand(sql_str, conn);


            conn.Open();
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    StaffNameTextBox.Text = reader["staff_name"].ToString();
                }
            }
            conn.Close();

            //伝票番号 -> 法人・個人
            string SQL_STR = "select document_number from statement_data where staff_code = '" + staff_id + "';";       //伝票番号 取得用

            cmd = new NpgsqlCommand(SQL_STR, conn);
            
            conn.Open();
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    SlipNumberTextBox.Text = reader["document_number"].ToString();
                }
            }
            conn.Close();
            SlipNumber = SlipNumberTextBox.Text;

            string SQL_STR1= @"select type from statement_data where document_number = '" + SlipNumber + "';";         //法人・個人 取得
            cmd = new NpgsqlCommand(SQL_STR1, conn);

            conn.Open();
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    type = (int)reader["type"];
                }
            }
            conn.Close();

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
                    AddressOrClientStaffNameTextBox.Size = new Size(130, 20);
                    BirthdayLabel.Visible = false;
                    BirthdayTextBox.Visible = false;

                    while (reader.Read())
                    {
                        PurchaseTotalTextBox.Text = reader["total"].ToString();
                        NameOrCompanyNameTextBox.Text = reader["company_name"].ToString();
                        OccupationOrShopNameTextBox.Text = reader["shop_name"].ToString();
                        AddressOrClientStaffNameTextBox.Text = reader["staff_name"].ToString();
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
                    }
                }
            }
            
            conn.Close();
            #endregion

            #region"表のデータ"

            //伝票番号から表の行番号と大分類コードを取得
            string Sql_Str = "select * from statement_calc_data where document_number = '" + SlipNumber + "';";
            //伝票番号と表の行番号から表のデータを取得し、大分類マスタと品名マスタを結合
            string Sql_Str1 = "select * from statement_calc_data inner join main_category_m on statement_calc_data.main_category_code = main_category_m.main_category_code inner join item_m on statement_calc_data.item_code = item_m.item_code where document_number = '" + SlipNumber + "';";

            cmd = new NpgsqlCommand(Sql_Str, conn);
            adapter = new NpgsqlDataAdapter(Sql_Str1, conn);
            adapter.Fill(dataTable);

            conn.Open();

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
                        MetalPurchaseTextBox.Text = purchaseTextBox1.Text;
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = purchaseTextBox1.Text;
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = purchaseTextBox1.Text;
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = purchaseTextBox1.Text;
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = purchaseTextBox1.Text;
                        break;
                }
            }

            if (record >= 2)
            {
                switch (MainCategoryCode2)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox2.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox2.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox2.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox2.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox2.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 3)
            {
                switch (MainCategoryCode3)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox3.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox3.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox3.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox3.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox3.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 4)
            {
                switch (MainCategoryCode4)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox4.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox4.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox4.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox4.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox4.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 5)
            {
                switch (MainCategoryCode5)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox5.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox5.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox5.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox5.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox5.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 6)
            {
                switch (MainCategoryCode6)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox6.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox6.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox6.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox6.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox6.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 7)
            {
                switch (MainCategoryCode7)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox7.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox7.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox7.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox7.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox7.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 8)
            {
                switch (MainCategoryCode8)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox8.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox8.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox8.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox8.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox8.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 9)
            {
                switch (MainCategoryCode9)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox9.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox9.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox9.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox9.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox9.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 10)
            {
                switch (MainCategoryCode10)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox10.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox10.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox10.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox10.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox10.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 11)
            {
                switch (MainCategoryCode11)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox11.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox11.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox11.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox11.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox11.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 12)
            {
                switch (MainCategoryCode12)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox12.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox12.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox12.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox12.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox12.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }

            if (record >= 13)
            {
                switch (MainCategoryCode13)
                {
                    case 100:
                        MetalPurchaseTextBox.Text = (int.Parse(purchaseTextBox13.Text) + int.Parse(MetalPurchaseTextBox.Text)).ToString();
                        break;
                    case 101:
                        DiamondPurchaseTextBox.Text = (int.Parse(purchaseTextBox13.Text) + int.Parse(DiamondPurchaseTextBox.Text)).ToString();
                        break;
                    case 102:
                        BrandPurchaseTextBox.Text = (int.Parse(purchaseTextBox13.Text) + int.Parse(BrandPurchaseTextBox.Text)).ToString();
                        break;
                    case 103:
                        ProductPurchaseTextBox.Text = (int.Parse(purchaseTextBox13.Text) + int.Parse(ProductPurchaseTextBox.Text)).ToString();
                        break;
                    case 104:
                        OtherPurchaseTextBox.Text = (int.Parse(purchaseTextBox13.Text) + int.Parse(OtherPurchaseTextBox.Text)).ToString();
                        break;
                }
            }
            #endregion

            #endregion

        }

        #region"上の表で卸値入力後"
        private void WholesalePriceTextBox1_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode1)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox1.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox1.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox1.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox1.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox1.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox2_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode2)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox2.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox2.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox2.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox2.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox2.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox3_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode3)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox3.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox3.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox3.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox3.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox3.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox4_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode4)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox4.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox4.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox4.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox4.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox4.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox5_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode5)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox5.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox5.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox5.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox5.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox5.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox6_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode6)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox6.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox6.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox6.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox6.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox6.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox7_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode7)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox7.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox7.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox7.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox7.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox7.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox8_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode8)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox8.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox8.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox8.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox8.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox8.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox9_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode9)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox9.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox9.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox9.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox9.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox9.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox10_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode10)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox10.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox10.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox10.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox10.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox10.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox11_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode11)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox11.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox11.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox11.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox11.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox11.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox12_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode12)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox12.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox12.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox12.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox12.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox12.Text;
                    break;
            }
        }

        private void WholesalePriceTextBox13_Leave(object sender, EventArgs e)
        {
            switch (MainCategoryCode13)
            {
                case 100:
                    MetalWholesaleTextBox.Text = WholesalePriceTextBox13.Text;
                    break;
                case 101:
                    DiamondWholesaleTextBox.Text = WholesalePriceTextBox13.Text;
                    break;
                case 102:
                    BrandWholesaleTextBox.Text = WholesalePriceTextBox13.Text;
                    break;
                case 103:
                    ProductWholesaleTextBox.Text = WholesalePriceTextBox13.Text;
                    break;
                case 104:
                    OtherWholesaleTextBox.Text = WholesalePriceTextBox13.Text;
                    break;
            }
        }


        #endregion

        private void purchaseTextBox1_TextChanged(object sender, EventArgs e)
        {
            MetalPurchaseTextBox.Text = purchaseTextBox1.Text;
        }
    }
}
