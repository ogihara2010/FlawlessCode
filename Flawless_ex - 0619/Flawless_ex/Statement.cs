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
        bool NameChange;
        bool CarryOver;
        bool MonthCatalog;

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
        decimal weisum;     //総重量計算用
        int countsum;        //総数計算用
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
        string search1;
        string search2;
        string search3;
        string pass;

        #region"コメントアウト"
        /*
        #region"単価が数値じゃないとき"
        #region"計算書"
        bool NotUnit0;
        bool NotUnit1;
        bool NotUnit2;
        bool NotUnit3;
        bool NotUnit4;
        bool NotUnit5;
        bool NotUnit6;
        bool NotUnit7;
        bool NotUnit8;
        bool NotUnit9;
        bool NotUnit10;
        bool NotUnit11;
        bool NotUnit12;
        #endregion
        #region"納品書"
        bool NotUnit00;
        bool NotUnit01;
        bool NotUnit02;
        bool NotUnit03;
        bool NotUnit04;
        bool NotUnit05;
        bool NotUnit06;
        bool NotUnit07;
        bool NotUnit08;
        bool NotUnit09;
        bool NotUnit010;
        bool NotUnit011;
        bool NotUnit012;
        #endregion
        #endregion
        */
        #endregion
        
        #region"数値のフォーマット未処理"
        #region"計算書　単価"
        decimal UnitUnformat0;
        decimal UnitUnformat1;
        decimal UnitUnformat2;
        decimal UnitUnformat3;
        decimal UnitUnformat4;
        decimal UnitUnformat5;
        decimal UnitUnformat6;
        decimal UnitUnformat7;
        decimal UnitUnformat8;
        decimal UnitUnformat9;
        decimal UnitUnformat10;
        decimal UnitUnformat11;
        decimal UnitUnformat12;
        #endregion
        #region"計算書　重量"
        decimal WeightUnformat0;
        decimal WeightUnformat1;
        decimal WeightUnformat2;
        decimal WeightUnformat3;
        decimal WeightUnformat4;
        decimal WeightUnformat5;
        decimal WeightUnformat6;
        decimal WeightUnformat7;
        decimal WeightUnformat8;
        decimal WeightUnformat9;
        decimal WeightUnformat10;
        decimal WeightUnformat11;
        decimal WeightUnformat12;
        #endregion
        #region"計算書　数値"
        decimal CountUnformat0;
        decimal CountUnformat1;
        decimal CountUnformat2;
        decimal CountUnformat3;
        decimal CountUnformat4;
        decimal CountUnformat5;
        decimal CountUnformat6;
        decimal CountUnformat7;
        decimal CountUnformat8;
        decimal CountUnformat9;
        decimal CountUnformat10;
        decimal CountUnformat11;
        decimal CountUnformat12;
        #endregion
        #region"計算書　金額"
        decimal MoneyUnformat0;
        decimal MoneyUnformat1;
        decimal MoneyUnformat2;
        decimal MoneyUnformat3;
        decimal MoneyUnformat4;
        decimal MoneyUnformat5;
        decimal MoneyUnformat6;
        decimal MoneyUnformat7;
        decimal MoneyUnformat8;
        decimal MoneyUnformat9;
        decimal MoneyUnformat10;
        decimal MoneyUnformat11;
        decimal MoneyUnformat12;
        #endregion
        #region"納品書　単価"
        decimal UnitUnformat00;
        decimal UnitUnformat01;
        decimal UnitUnformat02;
        decimal UnitUnformat03;
        decimal UnitUnformat04;
        decimal UnitUnformat05;
        decimal UnitUnformat06;
        decimal UnitUnformat07;
        decimal UnitUnformat08;
        decimal UnitUnformat09;
        decimal UnitUnformat010;
        decimal UnitUnformat011;
        decimal UnitUnformat012;
        #endregion
        #region"納品書　重量"
        decimal WeightUnformat00;
        decimal WeightUnformat01;
        decimal WeightUnformat02;
        decimal WeightUnformat03;
        decimal WeightUnformat04;
        decimal WeightUnformat05;
        decimal WeightUnformat06;
        decimal WeightUnformat07;
        decimal WeightUnformat08;
        decimal WeightUnformat09;
        decimal WeightUnformat010;
        decimal WeightUnformat011;
        decimal WeightUnformat012;
        #endregion
        #region"納品書　数値"
        decimal CountUnformat00;
        decimal CountUnformat01;
        decimal CountUnformat02;
        decimal CountUnformat03;
        decimal CountUnformat04;
        decimal CountUnformat05;
        decimal CountUnformat06;
        decimal CountUnformat07;
        decimal CountUnformat08;
        decimal CountUnformat09;
        decimal CountUnformat010;
        decimal CountUnformat011;
        decimal CountUnformat012;
        #endregion
        #region"納品書　金額"
        decimal MoneyUnformat00;
        decimal MoneyUnformat01;
        decimal MoneyUnformat02;
        decimal MoneyUnformat03;
        decimal MoneyUnformat04;
        decimal MoneyUnformat05;
        decimal MoneyUnformat06;
        decimal MoneyUnformat07;
        decimal MoneyUnformat08;
        decimal MoneyUnformat09;
        decimal MoneyUnformat010;
        decimal MoneyUnformat011;
        decimal MoneyUnformat012;
        #endregion
        #endregion
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
        #endregion

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        NpgsqlDataReader reader;
        NpgsqlTransaction transaction;
        NpgsqlCommandBuilder builder;

        public Statement(MainMenu main, int id, int type, string client_staff_name, string address, string access_auth, decimal Total, string Pass, string document, int control, string data, string search1, string search2, string search3)
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
            this.search1 = search1;
            this.search2 = search2;
            this.search3 = search3;
            this.pass = Pass;
        }

        private void Statement_Load(object sender, EventArgs e)
        {
            #region"コメントアウト"
            /*
            #region"単価が数値じゃないとき"
            #region"計算書"
            NotUnit0 = string.IsNullOrEmpty(unitPriceTextBox0.Text) || unitPriceTextBox0.Text == "単価 -> 重量 or 数量";
            NotUnit1 = string.IsNullOrEmpty(unitPriceTextBox1.Text) || unitPriceTextBox1.Text == "単価 -> 重量 or 数量";
            NotUnit2 = string.IsNullOrEmpty(unitPriceTextBox2.Text) || unitPriceTextBox2.Text == "単価 -> 重量 or 数量";
            NotUnit3 = string.IsNullOrEmpty(unitPriceTextBox3.Text) || unitPriceTextBox3.Text == "単価 -> 重量 or 数量";
            NotUnit4 = string.IsNullOrEmpty(unitPriceTextBox4.Text) || unitPriceTextBox4.Text == "単価 -> 重量 or 数量";
            NotUnit5 = string.IsNullOrEmpty(unitPriceTextBox5.Text) || unitPriceTextBox5.Text == "単価 -> 重量 or 数量";
            NotUnit6 = string.IsNullOrEmpty(unitPriceTextBox6.Text) || unitPriceTextBox6.Text == "単価 -> 重量 or 数量";
            NotUnit7 = string.IsNullOrEmpty(unitPriceTextBox7.Text) || unitPriceTextBox7.Text == "単価 -> 重量 or 数量";
            NotUnit8 = string.IsNullOrEmpty(unitPriceTextBox8.Text) || unitPriceTextBox8.Text == "単価 -> 重量 or 数量";
            NotUnit9 = string.IsNullOrEmpty(unitPriceTextBox9.Text) || unitPriceTextBox9.Text == "単価 -> 重量 or 数量";
            NotUnit10 = string.IsNullOrEmpty(unitPriceTextBox10.Text) || unitPriceTextBox10.Text == "単価 -> 重量 or 数量";
            NotUnit11 = string.IsNullOrEmpty(unitPriceTextBox11.Text) || unitPriceTextBox11.Text == "単価 -> 重量 or 数量";
            NotUnit12 = string.IsNullOrEmpty(unitPriceTextBox12.Text) || unitPriceTextBox12.Text == "単価 -> 重量 or 数量";
            #endregion
            #region"納品書"
            NotUnit00 = string.IsNullOrEmpty(unitPriceTextBox00.Text) || unitPriceTextBox00.Text == "単価 -> 重量 or 数量";
            NotUnit01 = string.IsNullOrEmpty(unitPriceTextBox01.Text) || unitPriceTextBox01.Text == "単価 -> 重量 or 数量";
            NotUnit02 = string.IsNullOrEmpty(unitPriceTextBox02.Text) || unitPriceTextBox02.Text == "単価 -> 重量 or 数量";
            NotUnit03 = string.IsNullOrEmpty(unitPriceTextBox03.Text) || unitPriceTextBox03.Text == "単価 -> 重量 or 数量";
            NotUnit04 = string.IsNullOrEmpty(unitPriceTextBox04.Text) || unitPriceTextBox04.Text == "単価 -> 重量 or 数量";
            NotUnit05 = string.IsNullOrEmpty(unitPriceTextBox05.Text) || unitPriceTextBox05.Text == "単価 -> 重量 or 数量";
            NotUnit06 = string.IsNullOrEmpty(unitPriceTextBox06.Text) || unitPriceTextBox06.Text == "単価 -> 重量 or 数量";
            NotUnit07 = string.IsNullOrEmpty(unitPriceTextBox07.Text) || unitPriceTextBox07.Text == "単価 -> 重量 or 数量";
            NotUnit08 = string.IsNullOrEmpty(unitPriceTextBox08.Text) || unitPriceTextBox08.Text == "単価 -> 重量 or 数量";
            NotUnit09 = string.IsNullOrEmpty(unitPriceTextBox09.Text) || unitPriceTextBox09.Text == "単価 -> 重量 or 数量";
            NotUnit010 = string.IsNullOrEmpty(unitPriceTextBox010.Text) || unitPriceTextBox010.Text == "単価 -> 重量 or 数量";
            NotUnit011 = string.IsNullOrEmpty(unitPriceTextBox011.Text) || unitPriceTextBox011.Text == "単価 -> 重量 or 数量";
            NotUnit012 = string.IsNullOrEmpty(unitPriceTextBox012.Text) || unitPriceTextBox012.Text == "単価 -> 重量 or 数量";
            #endregion
            #endregion
            */
            #endregion


            #region "ボタン"
            if (data == "S")
            {
                #region "計算書のタブのみ表示"
                tabControl1.SelectedTab = SettlementDayBox;
                tabControl1.TabPages.Remove(tabPage2);
                #endregion
                #region "登録、顧客選択ボタンは使用禁止"
                this.previewButton.Enabled = true;
                this.RecordListButton.Enabled = true;
                this.addButton.Enabled = false;
                this.client_Button.Enabled = false;
                this.AntiqueSelectionButton1.Enabled = false;
                this.taxCertificateButton.Enabled = false;
                this.financialButton.Enabled = false;
                this.sealCertificationButton.Enabled = false;
                this.residenceButton.Enabled = false;
                #endregion
                #region "すべてReadOnlyにする"
                #region "品物詳細"
                this.itemDetail0.ReadOnly = true;
                this.itemDetail1.ReadOnly = true;
                this.itemDetail2.ReadOnly = true;
                this.itemDetail3.ReadOnly = true;
                this.itemDetail4.ReadOnly = true;
                this.itemDetail5.ReadOnly = true;
                this.itemDetail6.ReadOnly = true;
                this.itemDetail7.ReadOnly = true;
                this.itemDetail8.ReadOnly = true;
                this.itemDetail9.ReadOnly = true;
                this.itemDetail10.ReadOnly = true;
                this.itemDetail11.ReadOnly = true;
                this.itemDetail12.ReadOnly = true;
                #endregion
                #region "数量"
                countTextBox0.ReadOnly = true;
                countTextBox1.ReadOnly = true;
                countTextBox2.ReadOnly = true;
                countTextBox3.ReadOnly = true;
                countTextBox4.ReadOnly = true;
                countTextBox5.ReadOnly = true;
                countTextBox6.ReadOnly = true;
                countTextBox7.ReadOnly = true;
                countTextBox8.ReadOnly = true;
                countTextBox9.ReadOnly = true;
                countTextBox10.ReadOnly = true;
                countTextBox11.ReadOnly = true;
                countTextBox12.ReadOnly = true;
                #endregion
                #region "重量"
                weightTextBox0.ReadOnly = true;
                weightTextBox1.ReadOnly = true;
                weightTextBox2.ReadOnly = true;
                weightTextBox3.ReadOnly = true;
                weightTextBox4.ReadOnly = true;
                weightTextBox5.ReadOnly = true;
                weightTextBox6.ReadOnly = true;
                weightTextBox7.ReadOnly = true;
                weightTextBox8.ReadOnly = true;
                weightTextBox9.ReadOnly = true;
                weightTextBox10.ReadOnly = true;
                weightTextBox11.ReadOnly = true;
                weightTextBox12.ReadOnly = true;
                #endregion
                #region "単価"
                unitPriceTextBox0.ReadOnly = true;
                unitPriceTextBox1.ReadOnly = true;
                unitPriceTextBox2.ReadOnly = true;
                unitPriceTextBox3.ReadOnly = true;
                unitPriceTextBox4.ReadOnly = true;
                unitPriceTextBox5.ReadOnly = true;
                unitPriceTextBox6.ReadOnly = true;
                unitPriceTextBox7.ReadOnly = true;
                unitPriceTextBox8.ReadOnly = true;
                unitPriceTextBox9.ReadOnly = true;
                unitPriceTextBox10.ReadOnly = true;
                unitPriceTextBox11.ReadOnly = true;
                unitPriceTextBox12.ReadOnly = true;
                #endregion
                #region "金額"
                this.moneyTextBox0.ReadOnly = true;
                this.moneyTextBox1.ReadOnly = true;
                this.moneyTextBox2.ReadOnly = true;
                this.moneyTextBox3.ReadOnly = true;
                this.moneyTextBox4.ReadOnly = true;
                this.moneyTextBox5.ReadOnly = true;
                this.moneyTextBox6.ReadOnly = true;
                this.moneyTextBox7.ReadOnly = true;
                this.moneyTextBox8.ReadOnly = true;
                this.moneyTextBox9.ReadOnly = true;
                this.moneyTextBox10.ReadOnly = true;
                this.moneyTextBox11.ReadOnly = true;
                this.moneyTextBox12.ReadOnly = true;
                #endregion
                #region "備考"
                this.remarks0.ReadOnly = true;
                this.remarks1.ReadOnly = true;
                this.remarks2.ReadOnly = true;
                this.remarks3.ReadOnly = true;
                this.remarks4.ReadOnly = true;
                this.remarks5.ReadOnly = true;
                this.remarks6.ReadOnly = true;
                this.remarks7.ReadOnly = true;
                this.remarks8.ReadOnly = true;
                this.remarks9.ReadOnly = true;
                this.remarks10.ReadOnly = true;
                this.remarks11.ReadOnly = true;
                this.remarks12.ReadOnly = true;
                #endregion
                this.clientRemarksTextBox.ReadOnly = true;
                this.paymentMethodsComboBox.Enabled = false;
                this.deliveryComboBox.Enabled = false;
                #endregion
            }
            else if (data == "D")
            {
                #region "納品書のタブのみ表示"
                tabControl1.SelectedTab = tabPage2;
                tabControl1.TabPages.Remove(SettlementDayBox);
                #endregion
                #region "登録ボタンは使用禁止"
                this.client_searchButton1.Enabled = false;
                this.Register.Enabled = false;
                this.AntiqueSelectionButton2.Enabled = false;
                this.DeliveryPreviewButton.Enabled = true;
                #endregion
                #region "すべてReadOnlyにする"
                #region "品物詳細"
                this.itemDetail00.ReadOnly = true;
                this.itemDetail01.ReadOnly = true;
                this.itemDetail02.ReadOnly = true;
                this.itemDetail03.ReadOnly = true;
                this.itemDetail04.ReadOnly = true;
                this.itemDetail05.ReadOnly = true;
                this.itemDetail06.ReadOnly = true;
                this.itemDetail07.ReadOnly = true;
                this.itemDetail08.ReadOnly = true;
                this.itemDetail09.ReadOnly = true;
                this.itemDetail010.ReadOnly = true;
                this.itemDetail011.ReadOnly = true;
                this.itemDetail012.ReadOnly = true;
                #endregion
                #region "数量"
                countTextBox00.ReadOnly = true;
                countTextBox01.ReadOnly = true;
                countTextBox02.ReadOnly = true;
                countTextBox03.ReadOnly = true;
                countTextBox04.ReadOnly = true;
                countTextBox05.ReadOnly = true;
                countTextBox06.ReadOnly = true;
                countTextBox07.ReadOnly = true;
                countTextBox08.ReadOnly = true;
                countTextBox09.ReadOnly = true;
                countTextBox010.ReadOnly = true;
                countTextBox011.ReadOnly = true;
                countTextBox012.ReadOnly = true;
                #endregion
                #region "重量"
                weightTextBox00.ReadOnly = true;
                weightTextBox01.ReadOnly = true;
                weightTextBox02.ReadOnly = true;
                weightTextBox03.ReadOnly = true;
                weightTextBox04.ReadOnly = true;
                weightTextBox05.ReadOnly = true;
                weightTextBox06.ReadOnly = true;
                weightTextBox07.ReadOnly = true;
                weightTextBox08.ReadOnly = true;
                weightTextBox09.ReadOnly = true;
                weightTextBox010.ReadOnly = true;
                weightTextBox011.ReadOnly = true;
                weightTextBox012.ReadOnly = true;
                #endregion
                #region "単価"
                unitPriceTextBox00.ReadOnly = true;
                unitPriceTextBox01.ReadOnly = true;
                unitPriceTextBox02.ReadOnly = true;
                unitPriceTextBox03.ReadOnly = true;
                unitPriceTextBox04.ReadOnly = true;
                unitPriceTextBox05.ReadOnly = true;
                unitPriceTextBox06.ReadOnly = true;
                unitPriceTextBox07.ReadOnly = true;
                unitPriceTextBox08.ReadOnly = true;
                unitPriceTextBox09.ReadOnly = true;
                unitPriceTextBox010.ReadOnly = true;
                unitPriceTextBox011.ReadOnly = true;
                unitPriceTextBox012.ReadOnly = true;
                #endregion
                #region "金額"
                this.moneyTextBox00.ReadOnly = true;
                this.moneyTextBox01.ReadOnly = true;
                this.moneyTextBox02.ReadOnly = true;
                this.moneyTextBox03.ReadOnly = true;
                this.moneyTextBox04.ReadOnly = true;
                this.moneyTextBox05.ReadOnly = true;
                this.moneyTextBox06.ReadOnly = true;
                this.moneyTextBox07.ReadOnly = true;
                this.moneyTextBox08.ReadOnly = true;
                this.moneyTextBox09.ReadOnly = true;
                this.moneyTextBox010.ReadOnly = true;
                this.moneyTextBox011.ReadOnly = true;
                this.moneyTextBox012.ReadOnly = true;
                #endregion
                #region "備考"
                this.remarks00.ReadOnly = true;
                this.remarks01.ReadOnly = true;
                this.remarks02.ReadOnly = true;
                this.remarks03.ReadOnly = true;
                this.remarks04.ReadOnly = true;
                this.remarks05.ReadOnly = true;
                this.remarks06.ReadOnly = true;
                this.remarks07.ReadOnly = true;
                this.remarks08.ReadOnly = true;
                this.remarks09.ReadOnly = true;
                this.remarks010.ReadOnly = true;
                this.remarks011.ReadOnly = true;
                this.remarks012.ReadOnly = true;
                #endregion
                this.clientRemarksTextBox2.ReadOnly = true;
                this.name.ReadOnly = true;
                this.RemarkRegister.ReadOnly = true;
                this.titleComboBox.Enabled = false;
                this.RemarkRegister.ReadOnly = true;
                this.typeComboBox.Enabled = false;
                this.paymentMethodComboBox.Enabled = false;
                this.CoinComboBox.Enabled = false;
                this.comboBox11.Enabled = false;
                #endregion
            }
            else
            {
                this.previewButton.Enabled = false;
                this.RecordListButton.Enabled = false;
                this.DeliveryPreviewButton.Enabled = false;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
            }
            #endregion
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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

                if (!string.IsNullOrEmpty(conNum.ToString()))
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
                    int sum = (int)row1["total"];
                    this.sumTextBox.Text = row1["total"].ToString();
                    sumTextBox.Text = string.Format("{0:C}", decimal.Parse(sumTextBox.Text, System.Globalization.NumberStyles.Number));
                    this.taxAmount.Text = row1["tax_amount"].ToString();
                    taxAmount.Text = string.Format("{0:C}", decimal.Parse(taxAmount.Text, System.Globalization.NumberStyles.Number));
                    this.paymentMethodsComboBox.SelectedItem = row1["payment_method"].ToString();
                    this.deliveryComboBox.SelectedItem = row1["delivery_method"].ToString();
                    this.totalCount.Text = row1["total_amount"].ToString();
                    this.totalWeight.Text = row1["total_weight"].ToString();
                    if (sum >= 2000000)
                    {
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
                    this.subTotal.Text = row1["subtotal"].ToString();
                    subTotal.Text = string.Format("{0:C}", decimal.Parse(subTotal.Text, System.Globalization.NumberStyles.Number));
                    this.sumTextBox.Text = row1["total"].ToString();
                    int sum = (int)row1["total"];
                    sumTextBox.Text = string.Format("{0:C}", decimal.Parse(sumTextBox.Text, System.Globalization.NumberStyles.Number));
                    this.taxAmount.Text = row1["tax_amount"].ToString();
                    taxAmount.Text = string.Format("{0:C}", decimal.Parse(taxAmount.Text, System.Globalization.NumberStyles.Number));
                    this.paymentMethodsComboBox.SelectedItem = row1["payment_method"].ToString();
                    this.deliveryComboBox.SelectedItem = row1["delivery_method"].ToString();
                    this.totalCount.Text = row1["total_amount"].ToString();
                    this.totalWeight.Text = row1["total_weight"].ToString();
                    if (sum >= 2000000)
                    {
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
                        string sql_document = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode0 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document, conn);
                        adapter.Fill(dt23);
                        mainCategoryComboBox0.DataSource = dt23;
                        mainCategoryComboBox0.DisplayMember = "main_category_name";
                        mainCategoryComboBox0.ValueMember = "main_category_code";
                        mainCategoryComboBox0.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt24 = new DataTable();
                        string sql_item1 = "select * from item_m  where invalid = 0 and item_code = " + itemCode0 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item1, conn);
                        adapter.Fill(dt24);
                        itemComboBox0.DataSource = dt24;
                        itemComboBox0.DisplayMember = "item_name";
                        itemComboBox0.ValueMember = "item_code";
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
                        string sql_document1 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode1 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document1, conn);
                        adapter.Fill(dt231);
                        mainCategoryComboBox1.DataSource = dt231;
                        mainCategoryComboBox1.DisplayMember = "main_category_name";
                        mainCategoryComboBox1.ValueMember = "main_category_code";
                        mainCategoryComboBox1.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt241 = new DataTable();
                        string sql_item2 = "select * from item_m  where invalid = 0 and item_code = " + itemCode1 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item2, conn);
                        adapter.Fill(dt241);
                        itemComboBox1.DataSource = dt241;
                        itemComboBox1.DisplayMember = "item_name";
                        itemComboBox1.ValueMember = "item_code";
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
                        string sql_document2 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode2 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document2, conn);
                        adapter.Fill(dt232);
                        mainCategoryComboBox2.DataSource = dt232;
                        mainCategoryComboBox2.DisplayMember = "main_category_name";
                        mainCategoryComboBox2.ValueMember = "main_category_code";
                        mainCategoryComboBox2.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt242 = new DataTable();
                        string sql_item3 = "select * from item_m  where invalid = 0 and item_code = " + itemCode2 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item3, conn);
                        adapter.Fill(dt242);
                        itemComboBox2.DataSource = dt242;
                        itemComboBox2.DisplayMember = "item_name";
                        itemComboBox2.ValueMember = "item_code";
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
                        string sql_document3 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode3 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document3, conn);
                        adapter.Fill(dt233);
                        mainCategoryComboBox3.DataSource = dt233;
                        mainCategoryComboBox3.DisplayMember = "main_category_name";
                        mainCategoryComboBox3.ValueMember = "main_category_code";
                        mainCategoryComboBox3.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt243 = new DataTable();
                        string sql_item4 = "select * from item_m  where invalid = 0 and item_code = " + itemCode3 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item4, conn);
                        adapter.Fill(dt243);
                        itemComboBox3.DataSource = dt243;
                        itemComboBox3.DisplayMember = "item_name";
                        itemComboBox3.ValueMember = "item_code";
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
                        string sql_document4 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode4 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document4, conn);
                        adapter.Fill(dt234);
                        mainCategoryComboBox4.DataSource = dt234;
                        mainCategoryComboBox4.DisplayMember = "main_category_name";
                        mainCategoryComboBox4.ValueMember = "main_category_code";
                        mainCategoryComboBox4.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt244 = new DataTable();
                        string sql_item5 = "select * from item_m  where invalid = 0 and item_code = " + itemCode4 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item5, conn);
                        adapter.Fill(dt244);
                        itemComboBox4.DataSource = dt244;
                        itemComboBox4.DisplayMember = "item_name";
                        itemComboBox4.ValueMember = "item_code";
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
                        string sql_document5 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode5 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document5, conn);
                        adapter.Fill(dt235);
                        mainCategoryComboBox5.DataSource = dt235;
                        mainCategoryComboBox5.DisplayMember = "main_category_name";
                        mainCategoryComboBox5.ValueMember = "main_category_code";
                        mainCategoryComboBox5.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt245 = new DataTable();
                        string sql_item6 = "select * from item_m  where invalid = 0 and item_code = " + itemCode5 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item6, conn);
                        adapter.Fill(dt245);
                        itemComboBox5.DataSource = dt245;
                        itemComboBox5.DisplayMember = "item_name";
                        itemComboBox5.ValueMember = "item_code";
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
                        string sql_document6 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode6 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document6, conn);
                        adapter.Fill(dt236);
                        mainCategoryComboBox6.DataSource = dt236;
                        mainCategoryComboBox6.DisplayMember = "main_category_name";
                        mainCategoryComboBox6.ValueMember = "main_category_code";
                        mainCategoryComboBox6.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt246 = new DataTable();
                        string sql_item7 = "select * from item_m  where invalid = 0 and item_code = " + itemCode6 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item7, conn);
                        adapter.Fill(dt246);
                        itemComboBox6.DataSource = dt246;
                        itemComboBox6.DisplayMember = "item_name";
                        itemComboBox6.ValueMember = "item_code";
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
                        string sql_document7 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode7 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document7, conn);
                        adapter.Fill(dt237);
                        mainCategoryComboBox7.DataSource = dt237;
                        mainCategoryComboBox7.DisplayMember = "main_category_name";
                        mainCategoryComboBox7.ValueMember = "main_category_code";
                        mainCategoryComboBox7.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt247 = new DataTable();
                        string sql_item8 = "select * from item_m  where invalid = 0 and item_code = " + itemCode7 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item8, conn);
                        adapter.Fill(dt247);
                        itemComboBox7.DataSource = dt247;
                        itemComboBox7.DisplayMember = "item_name";
                        itemComboBox7.ValueMember = "item_code";
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
                        string sql_document8 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode8 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document8, conn);
                        adapter.Fill(dt238);
                        mainCategoryComboBox8.DataSource = dt238;
                        mainCategoryComboBox8.DisplayMember = "main_category_name";
                        mainCategoryComboBox8.ValueMember = "main_category_code";
                        mainCategoryComboBox8.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt248 = new DataTable();
                        string sql_item9 = "select * from item_m  where invalid = 0 and item_code = " + itemCode8 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item9, conn);
                        adapter.Fill(dt248);
                        itemComboBox8.DataSource = dt248;
                        itemComboBox8.DisplayMember = "item_name";
                        itemComboBox8.ValueMember = "item_code";
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
                        string sql_document9 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode9 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document9, conn);
                        adapter.Fill(dt239);
                        mainCategoryComboBox9.DataSource = dt239;
                        mainCategoryComboBox9.DisplayMember = "main_category_name";
                        mainCategoryComboBox9.ValueMember = "main_category_code";
                        mainCategoryComboBox9.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt249 = new DataTable();
                        string sql_item10 = "select * from item_m  where invalid = 0 and item_code = " + itemCode9 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item10, conn);
                        adapter.Fill(dt249);
                        itemComboBox9.DataSource = dt249;
                        itemComboBox9.DisplayMember = "item_name";
                        itemComboBox9.ValueMember = "item_code";
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
                        string sql_document10 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode10 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document10, conn);
                        adapter.Fill(dt2310);
                        mainCategoryComboBox10.DataSource = dt2310;
                        mainCategoryComboBox10.DisplayMember = "main_category_name";
                        mainCategoryComboBox10.ValueMember = "main_category_code";
                        mainCategoryComboBox10.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2410 = new DataTable();
                        string sql_item11 = "select * from item_m  where invalid = 0 and item_code = " + itemCode10 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item11, conn);
                        adapter.Fill(dt2410);
                        itemComboBox10.DataSource = dt2410;
                        itemComboBox10.DisplayMember = "item_name";
                        itemComboBox10.ValueMember = "item_code";
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
                        string sql_document11 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode11 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document11, conn);
                        adapter.Fill(dt2311);
                        mainCategoryComboBox11.DataSource = dt2311;
                        mainCategoryComboBox11.DisplayMember = "main_category_name";
                        mainCategoryComboBox11.ValueMember = "main_category_code";
                        mainCategoryComboBox11.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2411 = new DataTable();
                        string sql_item12 = "select * from item_m  where invalid = 0 and item_code = " + itemCode11 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item12, conn);
                        adapter.Fill(dt2411);
                        itemComboBox11.DataSource = dt2411;
                        itemComboBox11.DisplayMember = "item_name";
                        itemComboBox11.ValueMember = "item_code";
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
                        string sql_document12 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode12 + ";";
                        adapter = new NpgsqlDataAdapter(sql_document12, conn);
                        adapter.Fill(dt2312);
                        mainCategoryComboBox12.DataSource = dt2312;
                        mainCategoryComboBox12.DisplayMember = "main_category_name";
                        mainCategoryComboBox12.ValueMember = "main_category_code";
                        mainCategoryComboBox12.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2412 = new DataTable();
                        string sql_item13 = "select * from item_m  where invalid = 0 and item_code = " + itemCode12 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item13, conn);
                        adapter.Fill(dt2412);
                        itemComboBox12.DataSource = dt2412;
                        itemComboBox12.DisplayMember = "item_name";
                        itemComboBox12.ValueMember = "item_code";
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
                    //this.PayeeComboBox.SelectedItem = row1[""].ToString();
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
                    this.totalWeight2.Text = row1["total_weight"].ToString();
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
                    //this.PayeeComboBox.SelectedItem = row1[""].ToString();
                    this.CoinComboBox.SelectedItem = row1["currency"].ToString();
                    this.comboBox11.SelectedItem = row["vat"].ToString();
                    if (yes == "する")
                    {
                        sealY.Checked = true;
                    }
                    if (yes == "しない")
                    {
                        sealN.Checked = true;
                    }
                    this.totalCount2.Text = row1["total_count"].ToString();
                    this.totalWeight2.Text = row1["total_weight"].ToString();
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
                        string sql_control1 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode00 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control1, conn);
                        adapter.Fill(dt26);
                        mainCategoryComboBox00.DataSource = dt26;
                        mainCategoryComboBox00.DisplayMember = "main_category_name";
                        mainCategoryComboBox00.ValueMember = "main_category_code";
                        mainCategoryComboBox00.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt27 = new DataTable();
                        string sql_item1 = "select * from item_m  where invalid = 0 and item_code = " + itemCode00 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item1, conn);
                        adapter.Fill(dt27);
                        itemComboBox00.DataSource = dt27;
                        itemComboBox00.DisplayMember = "item_name";
                        itemComboBox00.ValueMember = "item_code";
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
                        string sql_control2 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode01 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control2, conn);
                        adapter.Fill(dt261);
                        mainCategoryComboBox01.DataSource = dt261;
                        mainCategoryComboBox01.DisplayMember = "main_category_name";
                        mainCategoryComboBox01.ValueMember = "main_category_code";
                        mainCategoryComboBox01.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt271 = new DataTable();
                        string sql_item2 = "select * from item_m  where invalid = 0 and item_code = " + itemCode01 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item2, conn);
                        adapter.Fill(dt271);
                        itemComboBox01.DataSource = dt271;
                        itemComboBox01.DisplayMember = "item_name";
                        itemComboBox01.ValueMember = "item_code";
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
                        string sql_control3 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode02 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control3, conn);
                        adapter.Fill(dt262);
                        mainCategoryComboBox02.DataSource = dt262;
                        mainCategoryComboBox02.DisplayMember = "main_category_name";
                        mainCategoryComboBox02.ValueMember = "main_category_code";
                        mainCategoryComboBox02.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt272 = new DataTable();
                        string sql_item3 = "select * from item_m  where invalid = 0 and item_code = " + itemCode02 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item3, conn);
                        adapter.Fill(dt272);
                        itemComboBox02.DataSource = dt272;
                        itemComboBox02.DisplayMember = "item_name";
                        itemComboBox02.ValueMember = "item_code";
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
                        string sql_control4 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode03 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control4, conn);
                        adapter.Fill(dt263);
                        mainCategoryComboBox03.DataSource = dt263;
                        mainCategoryComboBox03.DisplayMember = "main_category_name";
                        mainCategoryComboBox03.ValueMember = "main_category_code";
                        mainCategoryComboBox03.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt273 = new DataTable();
                        string sql_item4 = "select * from item_m  where invalid = 0 and item_code = " + itemCode03 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item4, conn);
                        adapter.Fill(dt273);
                        itemComboBox03.DataSource = dt273;
                        itemComboBox03.DisplayMember = "item_name";
                        itemComboBox03.ValueMember = "item_code";
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
                        string sql_control5 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode04 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control5, conn);
                        adapter.Fill(dt264);
                        mainCategoryComboBox04.DataSource = dt264;
                        mainCategoryComboBox04.DisplayMember = "main_category_name";
                        mainCategoryComboBox04.ValueMember = "main_category_code";
                        mainCategoryComboBox04.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt274 = new DataTable();
                        string sql_item4 = "select * from item_m  where invalid = 0 and item_code = " + itemCode04 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item4, conn);
                        adapter.Fill(dt274);
                        itemComboBox04.DataSource = dt274;
                        itemComboBox04.DisplayMember = "item_name";
                        itemComboBox04.ValueMember = "item_code";
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
                        string sql_control6 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode05 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control6, conn);
                        adapter.Fill(dt265);
                        mainCategoryComboBox05.DataSource = dt265;
                        mainCategoryComboBox05.DisplayMember = "main_category_name";
                        mainCategoryComboBox05.ValueMember = "main_category_code";
                        mainCategoryComboBox05.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt275 = new DataTable();
                        string sql_item6 = "select * from item_m  where invalid = 0 and item_code = " + itemCode05 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item6, conn);
                        adapter.Fill(dt275);
                        itemComboBox05.DataSource = dt275;
                        itemComboBox05.DisplayMember = "item_name";
                        itemComboBox05.ValueMember = "item_code";
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
                        string sql_control7 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode06 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control7, conn);
                        adapter.Fill(dt266);
                        mainCategoryComboBox06.DataSource = dt266;
                        mainCategoryComboBox06.DisplayMember = "main_category_name";
                        mainCategoryComboBox06.ValueMember = "main_category_code";
                        mainCategoryComboBox06.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt276 = new DataTable();
                        string sql_item7 = "select * from item_m  where invalid = 0 and item_code = " + itemCode06 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item7, conn);
                        adapter.Fill(dt276);
                        itemComboBox06.DataSource = dt276;
                        itemComboBox06.DisplayMember = "item_name";
                        itemComboBox06.ValueMember = "item_code";
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
                        string sql_control8 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode07 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control8, conn);
                        adapter.Fill(dt267);
                        mainCategoryComboBox07.DataSource = dt267;
                        mainCategoryComboBox07.DisplayMember = "main_category_name";
                        mainCategoryComboBox07.ValueMember = "main_category_code";
                        mainCategoryComboBox07.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt277 = new DataTable();
                        string sql_item8 = "select * from item_m  where invalid = 0 and item_code = " + itemCode07 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item8, conn);
                        adapter.Fill(dt277);
                        itemComboBox07.DataSource = dt277;
                        itemComboBox07.DisplayMember = "item_name";
                        itemComboBox07.ValueMember = "item_code";
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
                        string sql_control9 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode08 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control9, conn);
                        adapter.Fill(dt268);
                        mainCategoryComboBox08.DataSource = dt268;
                        mainCategoryComboBox08.DisplayMember = "main_category_name";
                        mainCategoryComboBox08.ValueMember = "main_category_code";
                        mainCategoryComboBox08.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt278 = new DataTable();
                        string sql_item9 = "select * from item_m  where invalid = 0 and item_code = " + itemCode08 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item9, conn);
                        adapter.Fill(dt278);
                        itemComboBox08.DataSource = dt278;
                        itemComboBox08.DisplayMember = "item_name";
                        itemComboBox08.ValueMember = "item_code";
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
                        string sql_control10 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode09 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control10, conn);
                        adapter.Fill(dt269);
                        mainCategoryComboBox09.DataSource = dt269;
                        mainCategoryComboBox09.DisplayMember = "main_category_name";
                        mainCategoryComboBox09.ValueMember = "main_category_code";
                        mainCategoryComboBox09.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt279 = new DataTable();
                        string sql_item10 = "select * from item_m  where invalid = 0 and item_code = " + itemCode09 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item10, conn);
                        adapter.Fill(dt279);
                        itemComboBox09.DataSource = dt279;
                        itemComboBox09.DisplayMember = "item_name";
                        itemComboBox09.ValueMember = "item_code";
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
                        string sql_control11 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode010 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control11, conn);
                        adapter.Fill(dt2610);
                        mainCategoryComboBox010.DataSource = dt2610;
                        mainCategoryComboBox010.DisplayMember = "main_category_name";
                        mainCategoryComboBox010.ValueMember = "main_category_code";
                        mainCategoryComboBox010.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2710 = new DataTable();
                        string sql_item11 = "select * from item_m  where invalid = 0 and item_code = " + itemCode010 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item11, conn);
                        adapter.Fill(dt2710);
                        itemComboBox010.DataSource = dt2710;
                        itemComboBox010.DisplayMember = "item_name";
                        itemComboBox010.ValueMember = "item_code";
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
                        string sql_control12 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode011 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control12, conn);
                        adapter.Fill(dt2611);
                        mainCategoryComboBox011.DataSource = dt2611;
                        mainCategoryComboBox011.DisplayMember = "main_category_name";
                        mainCategoryComboBox011.ValueMember = "main_category_code";
                        mainCategoryComboBox011.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2711 = new DataTable();
                        string sql_item12 = "select * from item_m  where invalid = 0 and item_code = " + itemCode011 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item12, conn);
                        adapter.Fill(dt2711);
                        itemComboBox011.DataSource = dt2711;
                        itemComboBox011.DisplayMember = "item_name";
                        itemComboBox011.ValueMember = "item_code";
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
                        string sql_control13 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode012 + ";";
                        adapter = new NpgsqlDataAdapter(sql_control13, conn);
                        adapter.Fill(dt2612);
                        mainCategoryComboBox012.DataSource = dt2612;
                        mainCategoryComboBox012.DisplayMember = "main_category_name";
                        mainCategoryComboBox012.ValueMember = "main_category_code";
                        mainCategoryComboBox012.SelectedIndex = 0;//担当者ごとの初期値設定
                        #endregion
                        #region "品名"
                        //品名検索用
                        DataTable dt2712 = new DataTable();
                        string sql_item13 = "select * from item_m  where invalid = 0 and item_code = " + itemCode012 + ";";
                        adapter = new NpgsqlDataAdapter(sql_item13, conn);
                        adapter.Fill(dt2712);
                        itemComboBox012.DataSource = dt2712;
                        itemComboBox012.DisplayMember = "item_name";
                        itemComboBox012.ValueMember = "item_code";
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
            if (total != 0)
            {
                DataTable dt8 = new DataTable();
                int Number = int.Parse(Num) + 1;
                string str_sql_if = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' ORDER BY registration_date DESC;";
                adapter = new NpgsqlDataAdapter(str_sql_if, conn);
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
                    string str_sql_if2 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' ORDER BY registration_date DESC;";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode0 = (int)dataRow1["main_category_code"];
                            int itemCode0 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str6 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode0 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str6, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox0.DataSource = dt16;
                            mainCategoryComboBox0.DisplayMember = "main_category_name";
                            mainCategoryComboBox0.ValueMember = "main_category_code";
                            mainCategoryComboBox0.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str7 = "select * from item_m  where invalid = 0 and item_code = " + itemCode0 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str7, conn);
                            adapter.Fill(dt17);
                            itemComboBox0.DataSource = dt17;
                            itemComboBox0.DisplayMember = "item_name";
                            itemComboBox0.ValueMember = "item_code";
                            #endregion
                            #endregion
                            #region "入力された項目 1行目"
                            this.subTotal.Text = dataRow1["subtotal"].ToString();
                            this.sumTextBox.Text = dataRow1["total"].ToString();
                            this.taxAmount.Text = dataRow1["tax_amount"].ToString();
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode1 = (int)dataRow1["main_category_code"];
                            int itemCode1 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str8 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode1 + ";";
                            DataTable dt161 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str8, conn);
                            adapter.Fill(dt161);
                            mainCategoryComboBox1.DataSource = dt161;
                            mainCategoryComboBox1.DisplayMember = "main_category_name";
                            mainCategoryComboBox1.ValueMember = "main_category_code";
                            mainCategoryComboBox1.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str9 = "select * from item_m  where invalid = 0 and item_code = " + itemCode1 + ";";
                            DataTable dt171 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str9, conn);
                            adapter.Fill(dt171);
                            itemComboBox1.DataSource = dt171;
                            itemComboBox1.DisplayMember = "item_name";
                            itemComboBox1.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode2 = (int)dataRow1["main_category_code"];
                            int itemCode2 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str10 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode2 + ";";
                            DataTable dt162 = new DataTable();
                            adapter = new NpgsqlDataAdapter(sql_str10, conn);
                            adapter.Fill(dt162);
                            mainCategoryComboBox2.DataSource = dt162;
                            mainCategoryComboBox2.DisplayMember = "main_category_name";
                            mainCategoryComboBox2.ValueMember = "main_category_code";
                            mainCategoryComboBox2.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str11 = "select * from item_m  where invalid = 0 and item_code = " + itemCode2 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str11, conn);
                            DataTable dt172 = new DataTable();
                            adapter.Fill(dt172);
                            itemComboBox2.DataSource = dt172;
                            itemComboBox2.DisplayMember = "item_name";
                            itemComboBox2.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode3 = (int)dataRow1["main_category_code"];
                            int itemCode3 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str12 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode3 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str12, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox3.DataSource = dt16;
                            mainCategoryComboBox3.DisplayMember = "main_category_name";
                            mainCategoryComboBox3.ValueMember = "main_category_code";
                            mainCategoryComboBox3.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str13 = "select * from item_m where invalid = 0 and item_code = " + itemCode3 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str13, conn);
                            adapter.Fill(dt17);
                            itemComboBox3.DataSource = dt17;
                            itemComboBox3.DisplayMember = "item_name";
                            itemComboBox3.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode4 = (int)dataRow1["main_category_code"];
                            int itemCode4 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str14 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode4 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str14, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox4.DataSource = dt16;
                            mainCategoryComboBox4.DisplayMember = "main_category_name";
                            mainCategoryComboBox4.ValueMember = "main_category_code";
                            mainCategoryComboBox4.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str15 = "select * from item_m  where item_code = " + itemCode4 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str15, conn);
                            adapter.Fill(dt17);
                            itemComboBox4.DataSource = dt17;
                            itemComboBox4.DisplayMember = "item_name";
                            itemComboBox4.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode5 = (int)dataRow1["main_category_code"];
                            int itemCode5 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str16 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode5 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str16, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox5.DataSource = dt16;
                            mainCategoryComboBox5.DisplayMember = "main_category_name";
                            mainCategoryComboBox5.ValueMember = "main_category_code";
                            mainCategoryComboBox5.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str17 = "select * from item_m where item_code = " + itemCode5 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str17, conn);
                            adapter.Fill(dt17);
                            itemComboBox5.DataSource = dt17;
                            itemComboBox5.DisplayMember = "item_name";
                            itemComboBox5.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode6 = (int)dataRow1["main_category_code"];
                            int itemCode6 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str18 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode6 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str18, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox6.DataSource = dt16;
                            mainCategoryComboBox6.DisplayMember = "main_category_name";
                            mainCategoryComboBox6.ValueMember = "main_category_code";
                            mainCategoryComboBox6.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str19 = "select * from item_m where item_code = " + itemCode6 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str19, conn);
                            adapter.Fill(dt17);
                            itemComboBox6.DataSource = dt17;
                            itemComboBox6.DisplayMember = "item_name";
                            itemComboBox6.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode7 = (int)dataRow1["main_category_code"];
                            int itemCode7 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str20 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode7 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str20, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox7.DataSource = dt16;
                            mainCategoryComboBox7.DisplayMember = "main_category_name";
                            mainCategoryComboBox7.ValueMember = "main_category_code";
                            mainCategoryComboBox7.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str21 = "select * from item_m  where item_code = " + itemCode7 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str21, conn);
                            adapter.Fill(dt17);
                            itemComboBox7.DataSource = dt17;
                            itemComboBox7.DisplayMember = "item_name";
                            itemComboBox7.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode8 = (int)dataRow1["main_category_code"];
                            int itemCode8 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str22 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode8 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str22, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox8.DataSource = dt16;
                            mainCategoryComboBox8.DisplayMember = "main_category_name";
                            mainCategoryComboBox8.ValueMember = "main_category_code";
                            mainCategoryComboBox8.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str23 = "select * from item_m  where item_code = " + itemCode8 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str23, conn);
                            adapter.Fill(dt17);
                            itemComboBox8.DataSource = dt17;
                            itemComboBox8.DisplayMember = "item_name";
                            itemComboBox8.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode9 = (int)dataRow1["main_category_code"];
                            int itemCode9 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str24 = "select * from main_category_m where invalid = 0 and main_category_code = " + itemMainCategoryCode9 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str24, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox9.DataSource = dt16;
                            mainCategoryComboBox9.DisplayMember = "main_category_name";
                            mainCategoryComboBox9.ValueMember = "main_category_code";
                            mainCategoryComboBox9.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str25 = "select * from item_m  where item_m.main_category_code = " + itemCode9 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str25, conn);
                            adapter.Fill(dt17);
                            itemComboBox9.DataSource = dt17;
                            itemComboBox9.DisplayMember = "item_name";
                            itemComboBox9.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode10 = (int)dataRow1["main_category_code"];
                            int itemCode10 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str26 = "select * from main_category_m where invalid = 0 order by main_category_code = " + itemMainCategoryCode10 + "asc;";
                            adapter = new NpgsqlDataAdapter(sql_str26, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox10.DataSource = dt16;
                            mainCategoryComboBox10.DisplayMember = "main_category_name";
                            mainCategoryComboBox10.ValueMember = "main_category_code";
                            mainCategoryComboBox10.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str27 = "select * from item_m where item_code = " + itemCode10 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str27, conn);
                            adapter.Fill(dt17);
                            itemComboBox10.DataSource = dt17;
                            itemComboBox10.DisplayMember = "item_name";
                            itemComboBox10.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode11 = (int)dataRow1["main_category_code"];
                            int itemCode11 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str28 = "select * from main_category_m where invalid = 0 order by main_category_code = " + itemMainCategoryCode11 + "asc;";
                            adapter = new NpgsqlDataAdapter(sql_str28, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox11.DataSource = dt16;
                            mainCategoryComboBox11.DisplayMember = "main_category_name";
                            mainCategoryComboBox11.ValueMember = "main_category_code";
                            mainCategoryComboBox11.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str29 = "select * from item_m  where item_m.main_category_code = " + itemCode11 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str29, conn);
                            adapter.Fill(dt17);
                            itemComboBox11.DataSource = dt17;
                            itemComboBox11.DisplayMember = "item_name";
                            itemComboBox11.ValueMember = "item_code";
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
                            string str_sql_if3 = "select * from statement_calc_data_if where document_number = '" + "F" + Number + "' and total = '" + total + "' and record_number = " + (i + 1) + ";";
                            adapter = new NpgsqlDataAdapter(str_sql_if3, conn);
                            adapter.Fill(dt10);
                            DataRow dataRow1;
                            dataRow1 = dt10.Rows[0];
                            int itemMainCategoryCode12 = (int)dataRow1["main_category_code"];
                            int itemCode12 = (int)dataRow1["item_code"];
                            #region "コンボボックス"
                            #region "大分類"
                            string sql_str30 = "select * from main_category_m where invalid = 0 order by main_category_code = " + itemMainCategoryCode12 + "asc;";
                            adapter = new NpgsqlDataAdapter(sql_str30, conn);
                            adapter.Fill(dt16);
                            mainCategoryComboBox12.DataSource = dt16;
                            mainCategoryComboBox12.DisplayMember = "main_category_name";
                            mainCategoryComboBox12.ValueMember = "main_category_code";
                            mainCategoryComboBox12.SelectedIndex = 0;//担当者ごとの初期値設定
                            #endregion
                            #region "品名"
                            //品名検索用
                            string sql_str31 = "select * from item_m where item_m.main_category_code = " + itemCode12 + ";";
                            adapter = new NpgsqlDataAdapter(sql_str31, conn);
                            adapter.Fill(dt17);
                            itemComboBox12.DataSource = dt17;
                            itemComboBox12.DisplayMember = "item_name";
                            itemComboBox12.ValueMember = "item_code";
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
            //tabControl1.ItemSize = new Size(300, 40);

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
        private void mainCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e) //大分類選択分岐
        {
            //大分類によって品名変更　1行目
            if (a > 1)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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
            else if (a > 1 && total != 0)
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

        #region "納品書　大分類変更"
        private void mainCategoryComboBox00_SelectedIndexChanged(object sender, EventArgs e) //大分類選択分岐
        {
            //大分類によって品名変更　1行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox01_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 2行目
            if (b > 1)
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
            else
            {

            }
        }

        private void mainCategoryComboBox02_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 3行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox03_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 4行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox04_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 5行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox05_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 6行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox06_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 7行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox07_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 8行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox08_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 9行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox09_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 10行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox010_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 11行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox011_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 12行目
            if (b > 1)
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
        }

        private void mainCategoryComboBox012_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 13行目
            if (b > 1)
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
            else
            {
                b++;
            }
        }
        #endregion

        private void client_Button_Click(object sender, EventArgs e)//顧客選択メニュー（計算書）
        {
            if (string.IsNullOrEmpty(sumTextBox.Text))
            {
                
            }
            else
            {
                if (count != 0 )
                {
                    DialogResult result = MessageBox.Show("データを保持しますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        #region "データ一時置き場"
                        string DocumentNumber = documentNumberTextBox.Text;
                        decimal TotalWeight = decimal.Parse(this.totalWeight.Text);
                        int Amount = countsum;
                        decimal SubTotal = Math.Round(subSum, MidpointRounding.AwayFromZero);
                        total = Math.Round(subSum, MidpointRounding.AwayFromZero);
                        //string subTotal = this.subTotal.Text;
                        decimal TotalCount = decimal.Parse(this.totalCount.Text);
                        string TaxAmount = this.taxAmount.Text;
                        DateTime date = DateTime.Now;
                        string dat = date.ToString();
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
            }
            
            using (client_search search2 = new client_search(mainMenu, staff_id, type, client_staff_name, address, total, access_auth, pass))
            {
                this.Hide();
                search2.ShowDialog();
            }
            #region"コメントアウト"
            /*
            if (count != 0)
            {
                DataRow row;
                row = clientDt.Rows[0];
                int type = (int)row["type"];

                if (type == 0)
                {
                    string companyNmae = row["company_name"].ToString();
                    string shopName = row["shop_name"].ToString();
                    string staff_name = row["staff_name"].ToString();
                    address = row["address"].ToString();
                    register_date = row["registr_date"].ToString();
                    remarks = row["remarks"].ToString();

                    typeTextBox.Text = "法人";
                    companyTextBox.Text = companyNmae;
                    shopNameTextBox.Text = shopName;
                    clientNameTextBox.Text = staff_name;
                    antiqueLicenceTextBox.Text = address;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                }
                else if (type == 1)
                {
                    string name = row["name"].ToString();
                    address = row["address"].ToString();
                    remarks = row["remarks"].ToString();

                    typeTextBox.Text = "個人";
                    clientNameTextBox.Text = name;
                    antiqueLicenceTextBox.Text = address;
                    clientRemarksTextBox.Text = remarks;
                }
            }*/
            #endregion
        }

        private void clientSelectButton_Click(object sender, EventArgs e)//顧客選択メニュー（納品書）
        {
            if (string.IsNullOrEmpty(sumTextBox2.Text))
            {

            }
            using (client_search search2 = new client_search(mainMenu, staff_id, type, client_staff_name, address, total, access_auth, pass))
            {
                this.Hide();
                search2.ShowDialog();
            }

            if (count != 0)
            {
                DataRow row;
                row = clientDt.Rows[0];
                int type = (int)row["type"];

                if (type == 0)
                {
                    string companyName = row["company_name"].ToString();
                    string shopName = row["shop_name"].ToString();
                    string staff_name = row["staff_name"].ToString();
                    string address = row["address"].ToString();
                    string register_date = row["register_date"].ToString();
                    string remarks = row["remarks"].ToString();

                    typeTextBox2.Text = "法人";
                    companyTextBox2.Text = companyName;
                    shopNameTextBox2.Text = shopName;
                    clientNameTextBox2.Text = staff_name;
                    antiqueLicenceTextBox2.Text = address;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                }
                else if (type == 1)
                {
                    string name = row["name"].ToString();
                    string address = row["address"].ToString();
                    string remarks = row["remarks"].ToString();
                    string antique_license = row["antique_license"].ToString();

                    typeTextBox2.Text = "個人";
                    clientNameTextBox2.Text = name;
                    antiqueLicenceTextBox2.Text = address;
                    clientRemarksTextBox2.Text = remarks;
                }
            }
        }

        #region"計算書　登録ボタン"
        private void AddButton_Click(object sender, EventArgs e)        //計算書用登録ボタン
        {
            if ((unitPriceTextBox0.Text == "単価 -> 重量 or 数量") || (unitPriceTextBox0.Text == "") )
            {
                MessageBox.Show("計算書に入力されておりません"+"\r\n"+"顧客選択がまだの場合は顧客選択を先にしてください", "登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
            DateTime date = DateTime.Now.Date;
            string Date = date.ToLongDateString();
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
                            cmd.ExecuteNonQuery();
                            transaction.Commit();

                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set aol_financial_shareholder='" + AolFinancialShareholder + "' where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
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
                        if (typeTextBox.Text == "法人")
                        {
                            string SQL_STR = @"update client_m_corporate set tax_certificate='" + TaxCertification + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set tax_certificate='" + TaxCertification + "'  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
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
                        if (typeTextBox.Text == "法人")
                        {
                            string SQL_STR = @"update client_m_corporate set seal_certification='" + SealCertification + "' where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set seal_certification='" + SealCertification + "' where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
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
                        if (typeTextBox.Text == "法人")
                        {
                            string SQL_STR = @"update client_m_corporate set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "') where company_name ='" + CompanyName + "' and shop_name ='" + ShopName + "' and staff_name ='" + client_staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "')  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
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
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        else if (typeTextBox.Text == "個人")
                        {
                            string SQL_STR = @"update client_m_individual set antique_license ='" + AntiqueLicence + "'  where name ='" + companyTextBox.Text + "' and birthday ='" + shopNameTextBox.Text + "' and occupation ='" + clientNameTextBox.Text + "' and registration_date='" + registerDateTextBox.Text + "';";
                            cmd = new NpgsqlCommand(SQL_STR, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                }

            }
            #endregion

            string sql_str = "Insert into statement_data (antique_number, id_number, staff_code, total_weight, total_amount, sub_total, tax_amount, total, delivery_method, payment_method, settlement_date, delivery_date, document_number, company_name, shop_name, staff_name, name, type, birthday, occupation, address, assessment_date) VALUES ('" + AntiqueNumber + "','" + ID_Number + "' , '" + staff_id + "' , '" + TotalWeight + "' ,  '" + Amount + "' , '" + SubTotal + "', '" + TaxAmount + "' , '" + Total + "' , '" + DeliveryMethod + "' , '" + PaymentMethod + "' , '" + SettlementDate + "' , '" + DeliveryDate + "', '" + DocumentNumber + "','" + CompanyName + "','" + ShopName + "','" + StaffName + "','" + Name + "','" + TYPE + "','" + Birthday + "','" + Work + "', '" + address + "','" + Date + "');";


            int record = 1;     //行数
            int mainCategory = mainCategoryCode0;
            int item = itemCode0;
            string Detail = itemDetail0.Text;
            decimal Weight = decimal.Parse(weightTextBox0.Text);
            int Count = int.Parse(countTextBox0.Text);
            decimal UnitPrice = decimal.Parse(unitPriceTextBox0.Text);
            decimal amount = money0;
            string Remarks = remarks0.Text;

            DataTable dt2 = new DataTable();
            string sql_str2 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "');";

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);

            if (!string.IsNullOrEmpty(unitPriceTextBox1.Text) && !(unitPriceTextBox1.Text == "単価 -> 重量 or 数量"))
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

                string sql_str4 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str4, conn);
                adapter.Fill(dt4);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox2.Text) && !(unitPriceTextBox2.Text == "単価 -> 重量 or 数量"))
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

                string sql_str5 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str5, conn);
                adapter.Fill(dt5);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox3.Text) && !(unitPriceTextBox3.Text == "単価 -> 重量 or 数量"))
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

                string sql_str6 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str6, conn);
                adapter.Fill(dt6);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox4.Text) && !(unitPriceTextBox4.Text == "単価 -> 重量 or 数量"))
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

                string sql_str7 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str7, conn);
                adapter.Fill(dt7);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox5.Text) && !(unitPriceTextBox5.Text == "単価 -> 重量 or 数量"))
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

                string sql_str8 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str8, conn);
                adapter.Fill(dt8);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox6.Text) && !(unitPriceTextBox6.Text == "単価 -> 重量 or 数量"))
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

                string sql_str9 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str9, conn);
                adapter.Fill(dt9);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox7.Text) && !(unitPriceTextBox7.Text == "単価 -> 重量 or 数量"))
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

                string sql_str10 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str10, conn);
                adapter.Fill(dt10);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox8.Text) && !(unitPriceTextBox8.Text == "単価 -> 重量 or 数量"))
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

                string sql_str11 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str11, conn);
                adapter.Fill(dt11);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox9.Text) && !(unitPriceTextBox9.Text == "単価 -> 重量 or 数量"))
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

                string sql_str12 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "','" + Detail + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str12, conn);
                adapter.Fill(dt12);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox10.Text) && !(unitPriceTextBox10.Text == "単価 -> 重量 or 数量"))
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

                string sql_str13 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str13, conn);
                adapter.Fill(dt13);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox11.Text) && !(unitPriceTextBox11.Text == "単価 -> 重量 or 数量"))
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

                string sql_str14 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";

                adapter = new NpgsqlDataAdapter(sql_str14, conn);
                adapter.Fill(dt14);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox12.Text) && !(unitPriceTextBox12.Text == "単価 -> 重量 or 数量"))
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

                string sql_str15 = "Insert into statement_calc_data VALUES ( '" + mainCategory + "','" + item + "', '" + Weight + "' ,  '" + Count + "' , '" + UnitPrice + "', '" + amount + "' , '" + Remarks + "','" + DocumentNumber + "', '" + record + "','" + Detail + "' );";
                adapter = new NpgsqlDataAdapter(sql_str15, conn);
                adapter.Fill(dt15);
            }

            conn.Close();
            MessageBox.Show("登録しました。");
            this.previewButton.Enabled = true;
            this.RecordListButton.Enabled = true;
        }
        #endregion

        #region"計算書・納品書　計算処理"

        #region　"計算書　単価を入力したら重量、数値入力可　TextChangedイベント"
        private void unitPriceTextBox0_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
            {
                weightTextBox0.ReadOnly = true;
                countTextBox0.ReadOnly = true;
                unitPriceTextBox1.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox0.Text) && !(unitPriceTextBox0.Text == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox0.ReadOnly = false;
                countTextBox0.ReadOnly = false;
            }
        }
        private void unitPriceTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox1.Text))
            {
                weightTextBox1.ReadOnly = true;
                countTextBox1.ReadOnly = true;
                unitPriceTextBox2.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox1.Text) && !(unitPriceTextBox1.Text == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox1.ReadOnly = false;
                countTextBox1.ReadOnly = false;
            }
        }
        private void unitPriceTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox2.Text))
            {
                weightTextBox2.ReadOnly = true;
                countTextBox2.ReadOnly = true;
                unitPriceTextBox3.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox2.Text) && !(unitPriceTextBox2.Text == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox2.ReadOnly = false;
                countTextBox2.ReadOnly = false;
            }
        }
        private void unitPriceTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox3.Text))
            {
                weightTextBox3.ReadOnly = true;
                countTextBox3.ReadOnly = true;
                unitPriceTextBox4.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox3.Text) && !(unitPriceTextBox3.Text == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox3.ReadOnly = false;
                countTextBox3.ReadOnly = false;
            }
        }
        private void unitPriceTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox4.Text))
            {
                weightTextBox4.ReadOnly = true;
                countTextBox4.ReadOnly = true;
                unitPriceTextBox5.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox4.Text) && !(unitPriceTextBox4.Text == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox4.ReadOnly = false;
                countTextBox4.ReadOnly = false;
            }
        }
        private void unitPriceTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox5.Text))
            {
                weightTextBox5.ReadOnly = true;
                countTextBox5.ReadOnly = true;
                unitPriceTextBox6.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox5.Text) && !(unitPriceTextBox5.Text == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox5.ReadOnly = false;
                countTextBox5.ReadOnly = false;
            }
        }
        private void unitPriceTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox6.Text))
            {
                weightTextBox6.ReadOnly = true;
                countTextBox6.ReadOnly = true;
                unitPriceTextBox7.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox6.Text) && !(unitPriceTextBox6.Text == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox6.ReadOnly = false;
                countTextBox6.ReadOnly = false;
            }
        }
        private void unitPriceTextBox7_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox7.Text))
            {
                weightTextBox7.ReadOnly = true;
                countTextBox7.ReadOnly = true;
                unitPriceTextBox8.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox7.Text) && !(unitPriceTextBox7.Text == "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox7.ReadOnly = false;
                countTextBox7.ReadOnly = false;
            }
        }
        private void unitPriceTextBox8_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox8.Text))
            {
                weightTextBox8.ReadOnly = true;
                countTextBox8.ReadOnly = true;
                unitPriceTextBox9.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox8.Text) && !(unitPriceTextBox8.Text == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox8.ReadOnly = false;
                countTextBox8.ReadOnly = false;
            }
        }
        private void unitPriceTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox9.Text))
            {
                weightTextBox9.ReadOnly = true;
                countTextBox9.ReadOnly = true;
                unitPriceTextBox10.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox9.Text) && !(unitPriceTextBox9.Text == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox9.ReadOnly = false;
                countTextBox9.ReadOnly = false;
            }
        }
        private void unitPriceTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox10.Text))
            {
                weightTextBox10.ReadOnly = true;
                countTextBox10.ReadOnly = true;
                unitPriceTextBox11.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox10.Text) && !(unitPriceTextBox10.Text == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox10.ReadOnly = false;
                countTextBox10.ReadOnly = false;
            }
        }
        private void unitPriceTextBox11_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox11.Text))
            {
                weightTextBox11.ReadOnly = true;
                countTextBox11.ReadOnly = true;
                unitPriceTextBox12.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox11.Text) && !(unitPriceTextBox11.Text == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox11.ReadOnly = false;
                countTextBox11.ReadOnly = false;
            }
        }
        private void unitPriceTextBox12_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox12.Text))
            {
                weightTextBox12.ReadOnly = true;
                countTextBox12.ReadOnly = true;
            }
            else if (!string.IsNullOrEmpty(unitPriceTextBox12.Text) && !(unitPriceTextBox12.Text == "単価から重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
            {
                weightTextBox12.ReadOnly = false;
                countTextBox12.ReadOnly = false;
            }
        }

        #endregion

        #region "計算書　フォーカス時初期状態ならnull　Enterイベント"
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
                    unitPriceTextBox0.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox0.ReadOnly = true;
                    return;
                }
                if (unitPriceTextBox0.Text.ToString() == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox0.Text = "";
                }
            }
            
            
        }
        private void unitPriceTextBox1_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox1.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox1.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox0.Text)))
                {
                    unitPriceTextBox1.ReadOnly = false;
                }
            }
            
        }
        private void unitPriceTextBox2_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox2.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox2.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox1.Text)))
                {
                    unitPriceTextBox2.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox3_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox3.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox3.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox2.Text)))
                {
                    unitPriceTextBox3.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox4_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox4.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox4.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox3.Text)))
                {
                    unitPriceTextBox4.ReadOnly = false;
                }
            }
        }
    
        private void unitPriceTextBox5_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox5.Text.ToString() == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox5.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox4.Text)))
                {
                    unitPriceTextBox5.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox6_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox6.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox6.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox5.Text)))
                {
                    unitPriceTextBox6.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox7_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox7.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox7.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox6.Text)))
                {
                    unitPriceTextBox7.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox8_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox8.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox8.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox7.Text)))
                {
                    unitPriceTextBox8.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox9_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox9.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox9.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox8.Text)))
                {
                    unitPriceTextBox9.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox10_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox10.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox10.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox9.Text)))
                {
                    unitPriceTextBox10.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox11_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox11.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox11.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox10.Text)))
                {
                    unitPriceTextBox11.ReadOnly = false;
                }
            }
        }
        private void unitPriceTextBox12_Enter(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (unitPriceTextBox12.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox12.Text = "";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox11.Text)))
                {
                    unitPriceTextBox12.ReadOnly = false;
                }
            }
            
        }
        #endregion

        #region"計算書　単価３桁区切り＋フォーカスが外れた時　Leaveイベント"
        private void unitPriceTextBox0_Leave(object sender, EventArgs e)
        {
            if (data == "S")//
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
                {
                    unitPriceTextBox0.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && !string.IsNullOrEmpty(unitPriceTextBox0.Text))       //顧客変更 and 現行の単価が null じゃないとき
                {
                    unitPriceTextBox1.ReadOnly = false;
                }
                else
                {
                    //顧客選択後、現行が null じゃないとき
                    unitPriceTextBox1.ReadOnly = false;
                    unitPriceTextBox1.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
                    if (unitPriceTextBox1.Text != "単価 -> 重量 or 数量" && !string.IsNullOrEmpty(unitPriceTextBox1.Text)) 
                    {
                        return;
                    }
                }
            }
            
        }

        private void unitPriceTextBox1_Leave(object sender, EventArgs e)
        {
            if (data == "S")    //成績一覧から来た時
            {
            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox1.Text))       //単価がnullだったとき
                {
                    unitPriceTextBox1.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox1.ReadOnly = true;
                }

                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox1.Text)))  //前の行に 0 以外の数値が入力されていない時
                {
                    return;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox2.Text)))      //顧客変更時 and (単価がnullじゃない or 単価が初期状態)
                {
                    unitPriceTextBox2.ReadOnly = false;
                    unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    //顧客選択後、前行と現行が null 、初期状態じゃないとき
                    unitPriceTextBox2.ReadOnly = false;
                    unitPriceTextBox2.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox2_Leave(object sender, EventArgs e)
        {
            //if (unitPriceTextBox1.Text == "" || unitPriceTextBox1.Text == "単価 -> 重量 or 数量")
            //{
            //    return;
            //}

            if (data == "S")    //成績一覧から来た時
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox2.Text))
                {
                    unitPriceTextBox2.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox2.Text)))
                {
                    unitPriceTextBox3.ReadOnly = false;
                    unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox2.Text)))
                {
                    unitPriceTextBox3.ReadOnly = false;
                    unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox3.ReadOnly = false;
                    unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
                }
            }
            
        }

        private void unitPriceTextBox3_Leave(object sender, EventArgs e)
        {
            if (data == "S")    //成績一覧から来た時
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox3.Text))
                {
                    unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox3.Text)))
                {
                    unitPriceTextBox4.ReadOnly = false;
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox3.Text)))
                {
                    unitPriceTextBox4.ReadOnly = false;
                    unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox4.ReadOnly = false;
                    unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox4_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox4.Text))
                {
                    unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox4.Text)))
                {
                    unitPriceTextBox5.ReadOnly = false;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox4.Text)))
                {
                    unitPriceTextBox5.ReadOnly = false;
                    unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox5.ReadOnly = false;
                    unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox5_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox5.Text))
                {
                    unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox5.Text)))
                {
                    unitPriceTextBox6.ReadOnly = false;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox5.Text)))
                {
                    unitPriceTextBox6.ReadOnly = false;
                    unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox6.ReadOnly = false;
                    unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox6_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox6.Text))
                {
                    unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox6.Text)))
                {
                    unitPriceTextBox7.ReadOnly = false;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox6.Text)))
                {
                    unitPriceTextBox7.ReadOnly = false;
                    unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox7.ReadOnly = false;
                    unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox7_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox7.Text))
                {
                    unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox7.Text)))
                {
                    unitPriceTextBox8.ReadOnly = false;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox7.Text)))
                {
                    unitPriceTextBox8.ReadOnly = false;
                    unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox8.ReadOnly = false;
                    unitPriceTextBox8.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox8_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox8.Text))
                {
                    unitPriceTextBox8.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox8.Text)))
                {
                    unitPriceTextBox9.ReadOnly = false;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox8.Text)))
                {
                    unitPriceTextBox9.ReadOnly = false;
                    unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox9.ReadOnly = false;
                    unitPriceTextBox9.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox9_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox9.Text))
                {
                    unitPriceTextBox9.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox9.Text)))
                {
                    unitPriceTextBox10.ReadOnly = false;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox9.Text)))
                {
                    unitPriceTextBox10.ReadOnly = false;
                    unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox10.ReadOnly = false;
                    unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox10_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox10.Text))
                {
                    unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox10.Text)))
                {
                    unitPriceTextBox11.ReadOnly = false;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox10.Text)))
                {
                    unitPriceTextBox11.ReadOnly = false;
                    unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox11.ReadOnly = false;
                    unitPriceTextBox11.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox11_Leave(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox11.Text))
                {
                    unitPriceTextBox11.Text = "単価 -> 重量 or 数量";
                }
                else if (total != 0 && (!string.IsNullOrEmpty(unitPriceTextBox11.Text)))
                {
                    unitPriceTextBox12.ReadOnly = false;
                }
                else if (total != 0 && (string.IsNullOrEmpty(unitPriceTextBox11.Text)))
                {
                    unitPriceTextBox12.ReadOnly = false;
                    unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
                }
                else
                {
                    unitPriceTextBox12.ReadOnly = false;
                    unitPriceTextBox12.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox12_Leave(object sender, EventArgs e)
        {
            if (data == "S")
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

        #region"計算書　金額が入力されたら次の単価が入力可＋総重量 or 総数計算、自動計算　TextChangedイベント"
        private void moneyTextBox0_TextChanged(object sender, EventArgs e)
        {
            if (data == "S")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox0.Text) && string.IsNullOrEmpty(countTextBox0.Text))
                {
                    countTextBox0.Text = 0.ToString();
                }
                //数量×単価
                if (!string.IsNullOrEmpty(countTextBox0.Text) && string.IsNullOrEmpty(weightTextBox0.Text))
                {
                    weightTextBox0.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox0.Text) && string.IsNullOrEmpty(countTextBox0.Text))
                {
                    return;
                }

                if (total != 0)
                {
                    countsum = int.Parse(countTextBox0.Text);
                    weisum = decimal.Parse(weightTextBox0.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum = int.Parse(countTextBox0.Text);
                    weisum = decimal.Parse(weightTextBox0.Text);
                    subSum = money0;        //計算書は税込み
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
            }

            if (subSum >= 2000000)
            {
                groupBox1.BackColor = Color.OrangeRed;
            }
            else
            {
                groupBox1.BackColor = Color.GreenYellow;
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox1.Text) && string.IsNullOrEmpty(weightTextBox1.Text))
                {
                    weightTextBox1.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox1.Text) && string.IsNullOrEmpty(countTextBox1.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox1.Text) == 0 && decimal.Parse(weightTextBox1.Text) != 0))
                {
                    countsum += int.Parse(countTextBox1.Text);
                    weisum = decimal.Parse(weightTextBox1.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox1.Text) != 0 && decimal.Parse(weightTextBox1.Text) == 0))
                {
                    countsum = int.Parse(countTextBox1.Text);
                    weisum += decimal.Parse(weightTextBox1.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox1.Text);
                    weisum += decimal.Parse(weightTextBox1.Text);
                    subSum += money1;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));


                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
            
        }
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
                if (!string.IsNullOrEmpty(countTextBox2.Text) && string.IsNullOrEmpty(weightTextBox2.Text))
                {
                    weightTextBox2.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox2.Text) && string.IsNullOrEmpty(countTextBox2.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox2.Text) != 0 && decimal.Parse(weightTextBox2.Text) == 0))
                {
                    countsum += int.Parse(countTextBox2.Text);
                    weisum += decimal.Parse(weightTextBox2.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox2.Text) == 0 && decimal.Parse(weightTextBox2.Text) != 0))
                {
                    countsum += int.Parse(countTextBox2.Text);
                    weisum += decimal.Parse(weightTextBox2.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox2.Text);
                    weisum += decimal.Parse(weightTextBox2.Text);
                    subSum += money2;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
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
                if (!string.IsNullOrEmpty(countTextBox3.Text) && string.IsNullOrEmpty(weightTextBox3.Text))
                {
                    weightTextBox3.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox3.Text) && string.IsNullOrEmpty(countTextBox3.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox3.Text) != 0 && decimal.Parse(weightTextBox3.Text) == 0))
                {
                    countsum += int.Parse(countTextBox3.Text);
                    weisum += decimal.Parse(weightTextBox3.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox3.Text) == 0 && decimal.Parse(weightTextBox3.Text) != 0))
                {
                    countsum += int.Parse(countTextBox3.Text);
                    weisum += decimal.Parse(weightTextBox3.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox3.Text);
                    weisum += decimal.Parse(weightTextBox3.Text);
                    subSum += money3;     //増やしていく
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
 
        }
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
                if (!string.IsNullOrEmpty(countTextBox4.Text) && string.IsNullOrEmpty(weightTextBox4.Text))
                {
                    weightTextBox4.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox4.Text) && string.IsNullOrEmpty(countTextBox4.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox4.Text) != 0 && decimal.Parse(weightTextBox4.Text) == 0))
                {
                    countsum += int.Parse(countTextBox4.Text);
                    weisum += decimal.Parse(weightTextBox4.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox4.Text) == 0 && decimal.Parse(weightTextBox4.Text) != 0))
                {
                    countsum += int.Parse(countTextBox4.Text);
                    weisum += decimal.Parse(weightTextBox4.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox4.Text);
                    weisum += decimal.Parse(weightTextBox4.Text);
                    subSum += money4;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
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
                if (!string.IsNullOrEmpty(countTextBox5.Text) && string.IsNullOrEmpty(weightTextBox5.Text))
                {
                    weightTextBox5.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox5.Text) && string.IsNullOrEmpty(countTextBox5.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox5.Text) != 0 && decimal.Parse(weightTextBox5.Text) == 0))
                {
                    countsum += int.Parse(countTextBox5.Text);
                    weisum += decimal.Parse(weightTextBox5.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox5.Text) == 0 && decimal.Parse(weightTextBox5.Text) != 0))
                {
                    countsum += int.Parse(countTextBox5.Text);
                    weisum += decimal.Parse(weightTextBox5.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox5.Text);
                    weisum += decimal.Parse(weightTextBox5.Text);
                    subSum += money5;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
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
                if (!string.IsNullOrEmpty(countTextBox6.Text) && string.IsNullOrEmpty(weightTextBox6.Text))
                {
                    weightTextBox6.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox6.Text) && string.IsNullOrEmpty(countTextBox6.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox6.Text) != 0 && decimal.Parse(weightTextBox6.Text) == 0))
                {
                    countsum += int.Parse(countTextBox6.Text);
                    weisum += decimal.Parse(weightTextBox6.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox6.Text) == 0 && decimal.Parse(weightTextBox6.Text) != 0))
                {
                    countsum += int.Parse(countTextBox6.Text);
                    weisum += decimal.Parse(weightTextBox6.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox6.Text);
                    weisum += decimal.Parse(weightTextBox6.Text);
                    subSum += money6;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
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
                if (!string.IsNullOrEmpty(countTextBox7.Text) && string.IsNullOrEmpty(weightTextBox7.Text))
                {
                    weightTextBox7.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox7.Text) && string.IsNullOrEmpty(countTextBox7.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox7.Text) != 0 && decimal.Parse(weightTextBox7.Text) == 0))
                {
                    countsum += int.Parse(countTextBox7.Text);
                    weisum += decimal.Parse(weightTextBox7.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox7.Text) == 0 && decimal.Parse(weightTextBox7.Text) != 0))
                {
                    countsum += int.Parse(countTextBox7.Text);
                    weisum += decimal.Parse(weightTextBox7.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox7.Text);
                    weisum += decimal.Parse(weightTextBox7.Text);
                    subSum += money7;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox8.Text) && string.IsNullOrEmpty(weightTextBox8.Text))
                {
                    weightTextBox8.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox8.Text) && string.IsNullOrEmpty(countTextBox8.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox8.Text) != 0 && decimal.Parse(weightTextBox8.Text) == 0))
                {
                    countsum += int.Parse(countTextBox8.Text);
                    weisum += decimal.Parse(weightTextBox8.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox8.Text) == 0 && decimal.Parse(weightTextBox8.Text) != 0))
                {
                    countsum += int.Parse(countTextBox8.Text);
                    weisum += decimal.Parse(weightTextBox8.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox8.Text);
                    weisum += decimal.Parse(weightTextBox8.Text);
                    subSum += money8;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox9.Text) && string.IsNullOrEmpty(weightTextBox9.Text))
                {
                    weightTextBox9.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox9.Text) && string.IsNullOrEmpty(countTextBox9.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox9.Text) != 0 && decimal.Parse(weightTextBox9.Text) == 0))
                {
                    countsum += int.Parse(countTextBox9.Text);
                    weisum += decimal.Parse(weightTextBox9.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox9.Text) == 0 && decimal.Parse(weightTextBox9.Text) != 0))
                {
                    countsum += int.Parse(countTextBox9.Text);
                    weisum += decimal.Parse(weightTextBox9.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox9.Text);
                    weisum += decimal.Parse(weightTextBox9.Text);
                    subSum += money9;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }

        }
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
                if (!string.IsNullOrEmpty(countTextBox10.Text) && string.IsNullOrEmpty(weightTextBox10.Text))
                {
                    weightTextBox10.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox10.Text) && string.IsNullOrEmpty(countTextBox10.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox10.Text) != 0 && decimal.Parse(weightTextBox10.Text) == 0))
                {
                    countsum += int.Parse(countTextBox10.Text);
                    weisum += decimal.Parse(weightTextBox10.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox10.Text) == 0 && decimal.Parse(weightTextBox10.Text) != 0))
                {
                    countsum += int.Parse(countTextBox10.Text);
                    weisum += decimal.Parse(weightTextBox10.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox10.Text);
                    weisum += decimal.Parse(weightTextBox10.Text);
                    subSum += money10;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox11.Text) && string.IsNullOrEmpty(weightTextBox11.Text))
                {
                    weightTextBox11.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox11.Text) && string.IsNullOrEmpty(countTextBox11.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox11.Text) != 0 && decimal.Parse(weightTextBox11.Text) == 0))
                {
                    countsum += int.Parse(countTextBox11.Text);
                    weisum += decimal.Parse(weightTextBox11.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox11.Text) == 0 && decimal.Parse(weightTextBox11.Text) != 0))
                {
                    countsum += int.Parse(countTextBox11.Text);
                    weisum += decimal.Parse(weightTextBox11.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox11.Text);
                    weisum += decimal.Parse(weightTextBox11.Text);
                    subSum += money11;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
            
        }
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
                if (!string.IsNullOrEmpty(countTextBox12.Text) && string.IsNullOrEmpty(weightTextBox12.Text))
                {
                    weightTextBox12.Text = 0.ToString();
                }

                if (string.IsNullOrEmpty(weightTextBox12.Text) && string.IsNullOrEmpty(countTextBox12.Text))
                {
                    return;
                }

                if (total != 0 && (decimal.Parse(countTextBox12.Text) != 0 && decimal.Parse(weightTextBox12.Text) == 0))
                {
                    countsum += int.Parse(countTextBox12.Text);
                    weisum += decimal.Parse(weightTextBox12.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else if (total != 0 && (decimal.Parse(countTextBox12.Text) == 0 && decimal.Parse(weightTextBox12.Text) != 0))
                {
                    countsum += int.Parse(countTextBox12.Text);
                    weisum += decimal.Parse(weightTextBox12.Text);
                    subSum = total;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }
                else
                {
                    countsum += int.Parse(countTextBox12.Text);
                    weisum += decimal.Parse(weightTextBox12.Text);
                    subSum += money12;
                    TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                }


                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(subSum, MidpointRounding.AwayFromZero));

                if (subSum >= 2000000)
                {
                    groupBox1.BackColor = Color.OrangeRed;
                }
            }
            
        }
        #endregion

        #region　"納品書　単価を入力したら重量、数値入力可　TextChangdイベント"
        private void unitPriceTextBox00_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox00.Text))
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
        private void unitPriceTextBox01_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox01.Text))
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
        private void unitPriceTextBox02_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox02.Text))
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
        private void unitPriceTextBox03_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox03.Text))
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
        private void unitPriceTextBox04_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox04.Text))
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
        private void unitPriceTextBox05_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox05.Text))
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
        private void unitPriceTextBox06_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox06.Text))
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
        private void unitPriceTextBox07_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox07.Text))
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
        private void unitPriceTextBox08_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox08.Text))
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
        private void unitPriceTextBox09_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox09.Text))
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
        private void unitPriceTextBox010_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox010.Text))
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
        private void unitPriceTextBox011_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox011.Text))
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
        private void unitPriceTextBox012_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox012.Text))
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

        #region"納品書　フォーカス時初期状態ならnull　Enterイベント"
        private void unitPriceTextBox00_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox00.Text.ToString() == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox00.Text = "";
                }
            }
        }

        private void unitPriceTextBox01_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox01.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox01.Text = "";
                }
            }
        }
        private void unitPriceTextBox02_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox02.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox02.Text = "";
                }
            }
        }
        private void unitPriceTextBox03_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox03.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox03.Text = "";
                }
            }
        }
        private void unitPriceTextBox04_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox04.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox04.Text = "";
                }
            }
        }
        private void unitPriceTextBox05_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox05.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox05.Text = "";
                }
            }
        }
        private void unitPriceTextBox06_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox06.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox06.Text = "";
                }
            }
        }
        private void unitPriceTextBox07_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox07.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox07.Text = "";
                }
            }
        }
        private void unitPriceTextBox08_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox08.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox08.Text = "";
                }
            }
        }
        private void unitPriceTextBox09_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox09.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox09.Text = "";
                }
            }
        }
        private void unitPriceTextBox010_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox010.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox010.Text = "";
                }
            }
        }
        private void unitPriceTextBox011_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox011.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox011.Text = "";
                }
            }
        }
        private void unitPriceTextBox012_Enter(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (unitPriceTextBox012.Text == "単価 -> 重量 or 数量")
                {
                    unitPriceTextBox012.Text = "";
                }
            }
        }
        #endregion

        #region"納品書　数量を入力したら重量を入力不可　TextChangedイベント"

        private void countTextBox00_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox00.Text))
                {
                    weightTextBox00.ReadOnly = true;
                }
                else
                {
                    weightTextBox00.ReadOnly = false;
                }
            }
        }

        private void countTextBox01_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox01.Text))
                {
                    weightTextBox01.ReadOnly = true;
                }
                else
                {
                    weightTextBox01.ReadOnly = false;
                }
            }
        }

        private void countTextBox02_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox02.Text))
                {
                    weightTextBox02.ReadOnly = true;
                }
                else
                {
                    weightTextBox02.ReadOnly = false;
                }
            }
        }

        private void countTextBox03_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox03.Text))
                {
                    weightTextBox03.ReadOnly = true;
                }
                else
                {
                    weightTextBox03.ReadOnly = false;
                }
            }
        }

        private void countTextBox04_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox04.Text))
                {
                    weightTextBox04.ReadOnly = true;
                }
                else
                {
                    weightTextBox04.ReadOnly = false;
                }
            }
        }

        private void countTextBox05_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox05.Text))
                {
                    weightTextBox05.ReadOnly = true;
                }
                else
                {
                    weightTextBox05.ReadOnly = false;
                }
            }
        }

        private void countTextBox06_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox06.Text))
                {
                    weightTextBox06.ReadOnly = true;
                }
                else
                {
                    weightTextBox06.ReadOnly = false;
                }
            }
        }

        private void countTextBox07_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox07.Text))
                {
                    weightTextBox07.ReadOnly = true;
                }
                else
                {
                    weightTextBox07.ReadOnly = false;
                }
            }
        }

        private void countTextBox08_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox08.Text))
                {
                    weightTextBox08.ReadOnly = true;
                }
                else
                {
                    weightTextBox08.ReadOnly = false;
                }
            }
        }

        private void countTextBox09_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox09.Text))
                {
                    weightTextBox09.ReadOnly = true;
                }
                else
                {
                    weightTextBox09.ReadOnly = false;
                }
            }
        }

        private void countTextBox010_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox010.Text))
                {
                    weightTextBox010.ReadOnly = true;
                }
                else
                {
                    weightTextBox010.ReadOnly = false;
                }
            }
        }

        private void countTextBox011_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox011.Text))
                {
                    weightTextBox011.ReadOnly = true;
                }
                else
                {
                    weightTextBox011.ReadOnly = false;
                }
            }
        }

        private void countTextBox012_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox012.Text))
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

        #region"納品書　税込み税抜き選択　TextChangedイベント"

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
                    subSum = decimal.Parse(moneyTextBox00.Text) + decimal.Parse(moneyTextBox01.Text) + decimal.Parse(moneyTextBox02.Text) + decimal.Parse(moneyTextBox03.Text) + decimal.Parse(moneyTextBox04.Text)
                            + decimal.Parse(moneyTextBox05.Text) + decimal.Parse(moneyTextBox06.Text) + decimal.Parse(moneyTextBox07.Text) + decimal.Parse(moneyTextBox08.Text) + decimal.Parse(moneyTextBox09.Text)
                            + decimal.Parse(moneyTextBox010.Text) + decimal.Parse(moneyTextBox011.Text) + decimal.Parse(moneyTextBox012.Text);

                    subTotal2.Text = string.Format("{0:#,0}", Math.Round(subSum, MidpointRounding.AwayFromZero));    //税抜き表記
                }
                else        //税込み選択
                {
                    subSum = decimal.Parse(moneyTextBox00.Text) + decimal.Parse(moneyTextBox01.Text) + decimal.Parse(moneyTextBox02.Text) + decimal.Parse(moneyTextBox03.Text) + decimal.Parse(moneyTextBox04.Text)
                            + decimal.Parse(moneyTextBox05.Text) + decimal.Parse(moneyTextBox06.Text) + decimal.Parse(moneyTextBox07.Text) + decimal.Parse(moneyTextBox08.Text) + decimal.Parse(moneyTextBox09.Text)
                            + decimal.Parse(moneyTextBox010.Text) + decimal.Parse(moneyTextBox011.Text) + decimal.Parse(moneyTextBox012.Text);

                    sum = subSum * Tax;

                    subTotal2.Text = string.Format("{0:#,0}", Math.Round(sum, MidpointRounding.AwayFromZero));    //税込み表記
                }
                sumTextBox2.Text = string.Format("{0:#,0}", Math.Round(sum, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            }
        }
        #endregion

        #region"納品書　単価３桁区切り＋フォーカスが外れた時　Leaveイベント"
        private void unitPriceTextBox00_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox00.Text))
                {
                    unitPriceTextBox00.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox01.ReadOnly = false;
                    unitPriceTextBox01.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox01_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox01.Text))
                {
                    unitPriceTextBox01.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox02.ReadOnly = false;
                    unitPriceTextBox02.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox02_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox02.Text))
                {
                    unitPriceTextBox02.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox03.ReadOnly = false;
                    unitPriceTextBox03.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox03_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox03.Text))
                {
                    unitPriceTextBox03.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox04.ReadOnly = false;
                    unitPriceTextBox04.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox04_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox04.Text))
                {
                    unitPriceTextBox04.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox05.ReadOnly = false;
                    unitPriceTextBox05.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox05_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox05.Text))
                {
                    unitPriceTextBox05.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox06.ReadOnly = false;
                    unitPriceTextBox06.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox06_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox06.Text))
                {
                    unitPriceTextBox06.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox07.ReadOnly = false;
                    unitPriceTextBox07.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox07_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox07.Text))
                {
                    unitPriceTextBox07.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox08.ReadOnly = false;
                    unitPriceTextBox08.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox08_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox08.Text))
                {
                    unitPriceTextBox08.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox09.ReadOnly = false;
                    unitPriceTextBox09.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox09_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox09.Text))
                {
                    unitPriceTextBox09.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox10.ReadOnly = false;
                    unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox010_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox010.Text))
                {
                    unitPriceTextBox010.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox011.ReadOnly = false;
                    unitPriceTextBox011.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox011_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (string.IsNullOrEmpty(unitPriceTextBox011.Text))
                {
                    unitPriceTextBox011.Text = "単価 -> 重量 or 数量";
                }
                else
                {
                    unitPriceTextBox012.ReadOnly = false;
                    unitPriceTextBox012.Text = "単価 -> 重量 or 数量";
                    unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
                }
            }
        }

        private void unitPriceTextBox012_Leave(object sender, EventArgs e)
        {
            if (data == "D")
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

        //修正中
        #region"計算書　重量×単価　Leaveイベント"
        private void weightTextBox0_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(weightTextBox0.Text) && string.IsNullOrEmpty(countTextBox0.Text))
            //{
            //    return;
            //}

            //if (weightTextBox0.Text == "0" && !string.IsNullOrEmpty(countTextBox0.Text)) 
            //{
            //    weightTextBox0.ReadOnly = true;
            //    return;
            //}

            if (!string.IsNullOrEmpty(weightTextBox0.Text) && !(weightTextBox0.Text == "0"))
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
        private void weightTextBox1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox1.Text) && !(weightTextBox1.Text == "0"))
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
        private void weightTextBox2_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(weightTextBox2.Text) && !(weightTextBox2.Text == "0"))
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
        private void weightTextBox3_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox3.Text) && !(weightTextBox3.Text == "0"))
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
        private void weightTextBox4_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(weightTextBox4.Text) && !(weightTextBox4.Text == "0"))
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
        private void weightTextBox5_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox5.Text) && !(weightTextBox5.Text == "0"))
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
        private void weightTextBox6_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox6.Text) && !(weightTextBox6.Text == "0"))
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
        private void weightTextBox7_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox7.Text) && !(weightTextBox7.Text == "0"))
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
        private void weightTextBox8_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox8.Text) && !(weightTextBox8.Text == "0"))
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
        private void weightTextBox9_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox9.Text) && !(weightTextBox9.Text == "0"))
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
        private void weightTextBox10_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox10.Text) && !(weightTextBox10.Text == "0"))
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
        private void weightTextBox11_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox11.Text) && !(weightTextBox11.Text == "0"))
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
        private void weightTextBox12_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox12.Text) && !(weightTextBox12.Text == "0"))
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

        //修正中
        #region"計算書　数量×単価 Leaveイベント"
        private void countTextBox0_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox0.Text) && !(countTextBox0.Text == "0"))
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
        private void countTextBox1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox1.Text) && !(countTextBox1.Text == "0"))
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
        private void countTextBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox2.Text) && !(countTextBox2.Text == "0"))
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
        private void countTextBox3_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox3.Text) && !(countTextBox3.Text == "0"))
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
        private void countTextBox4_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox4.Text) && !(countTextBox4.Text == "0"))
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
        private void countTextBox5_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox5.Text) && !(countTextBox5.Text == "0"))
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
        private void countTextBox6_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox6.Text) && !(countTextBox6.Text == "0"))
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
        private void countTextBox7_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox7.Text) && !(countTextBox7.Text == "0"))
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
        private void countTextBox8_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox8.Text) && !(countTextBox8.Text == "0"))
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
        private void countTextBox9_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox9.Text) && !(countTextBox9.Text == "0"))
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
        private void countTextBox10_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox10.Text) && !(countTextBox10.Text == "0"))
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
        private void countTextBox11_Leave(object sender, EventArgs e)
        {
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
        private void countTextBox12_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox12.Text) && !(countTextBox12.Text == "0"))
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

        #region "納品書　重量×単価 Leaveイベント"
        private void weightTextBox00_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox00.Text) && !(weightTextBox00.Text == "0"))
            {
                int j = weightTextBox00.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight0 = Math.Round(decimal.Parse(weightTextBox00.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox00.Text = weight0.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight0 * decimal.Parse(unitPriceTextBox00.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
                }
                countTextBox00.ReadOnly = true;
            }
            else
            {
                countTextBox00.ReadOnly = false;
            }
            money0 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
        private void weightTextBox01_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox01.Text) && !(weightTextBox01.Text == "0"))
            {
                int j = weightTextBox01.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight1 = Math.Round(decimal.Parse(weightTextBox01.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox01.Text = weight1.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight1 * decimal.Parse(unitPriceTextBox01.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
                }
                countTextBox01.ReadOnly = true;
            }
            else
            {
                countTextBox01.ReadOnly = false;
            }
            money1 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
        private void weightTextBox02_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox02.Text) && !(weightTextBox02.Text == "0"))
            {
                int j = weightTextBox02.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight2 = Math.Round(decimal.Parse(weightTextBox02.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox02.Text = weight2.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight2 * decimal.Parse(unitPriceTextBox02.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
                }
                countTextBox02.ReadOnly = true;
            }
            else
            {
                countTextBox02.ReadOnly = false;
            }
            money2 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
        private void weightTextBox03_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox03.Text) && !(weightTextBox03.Text == "0"))
            {
                int j = weightTextBox03.Text.IndexOf(".");
                if (j > -1)     //小数点あり
                {
                    //重量の四捨五入
                    decimal weight3 = Math.Round(decimal.Parse(weightTextBox03.Text), 1, MidpointRounding.AwayFromZero);
                    weightTextBox03.Text = weight3.ToString();

                    //重量×単価の切捨て
                    sub = Math.Floor(weight3 * decimal.Parse(unitPriceTextBox03.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
                }
                countTextBox03.ReadOnly = true;
            }
            else
            {
                countTextBox03.ReadOnly = false;
            }
            money3 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight4 * decimal.Parse(unitPriceTextBox04.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
                }
                countTextBox04.ReadOnly = true;
            }
            else
            {
                countTextBox04.ReadOnly = false;
            }
            money4 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight5 * decimal.Parse(unitPriceTextBox05.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
                }
                countTextBox05.ReadOnly = true;
            }
            else
            {
                countTextBox05.ReadOnly = false;
            }
            money5 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight6 * decimal.Parse(unitPriceTextBox06.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
                }
                countTextBox06.ReadOnly = true;
            }
            else
            {
                countTextBox06.ReadOnly = false;
            }
            money6 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight7 * decimal.Parse(unitPriceTextBox07.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
                }
                countTextBox07.ReadOnly = true;
            }
            else
            {
                countTextBox07.ReadOnly = false;
            }
            money7 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight8 * decimal.Parse(unitPriceTextBox08.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
                }
                countTextBox08.ReadOnly = true;
            }
            else
            {
                countTextBox08.ReadOnly = false;
            }
            money8 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight9 * decimal.Parse(unitPriceTextBox09.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
                }
                countTextBox09.ReadOnly = true;
            }
            else
            {
                countTextBox09.ReadOnly = false;
            }
            money9 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight10 * decimal.Parse(unitPriceTextBox010.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
                }
                countTextBox010.ReadOnly = true;
            }
            else
            {
                countTextBox010.ReadOnly = false;
            }
            money10 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight11 * decimal.Parse(unitPriceTextBox011.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
                }
                countTextBox011.ReadOnly = true;
            }
            else
            {
                countTextBox011.ReadOnly = false;
            }
            money11 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
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
                    sub = Math.Floor(weight12 * decimal.Parse(unitPriceTextBox012.Text));
                }
                else        //小数点なし
                {
                    sub = decimal.Parse(weightTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
                }
                countTextBox012.ReadOnly = true;
            }
            else
            {
                countTextBox012.ReadOnly = false;
            }
            money12 = sub;
            sub1 = sub + sub * Tax / 100;
            moneyTextBox012.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
        }
        #endregion

        #region　"納品書　数量×単価　Leaveイベント"
        private void countTextBox00_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox00.Text) && !(countTextBox00.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
                    weightTextBox00.ReadOnly = true;
                }
                else
                {
                    weightTextBox00.ReadOnly = false;
                }
                money0 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox00.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox01_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox01.Text) && !(countTextBox01.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
                    weightTextBox01.ReadOnly = true;
                }
                else
                {
                    weightTextBox01.ReadOnly = false;
                }
                money1 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox01.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox02_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox02.Text) && !(countTextBox02.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
                    weightTextBox02.ReadOnly = true;
                }
                else
                {
                    weightTextBox02.ReadOnly = false;
                }
                money2 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox02.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox03_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox03.Text) && !(countTextBox03.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
                    weightTextBox03.ReadOnly = true;
                }
                else
                {
                    weightTextBox03.ReadOnly = false;
                }
                money3 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox03.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox04_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox04.Text) && !(countTextBox04.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
                    weightTextBox04.ReadOnly = true;
                }
                else
                {
                    weightTextBox04.ReadOnly = false;
                }
                money4 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox04.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox05_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox05.Text) && !(countTextBox05.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
                    weightTextBox05.ReadOnly = true;
                }
                else
                {
                    weightTextBox05.ReadOnly = false;
                }
                money5 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox05.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox06_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox06.Text) && !(countTextBox06.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
                    weightTextBox06.ReadOnly = true;
                }
                else
                {
                    weightTextBox06.ReadOnly = false;
                }
                money6 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox06.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox07_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox07.Text) && !(countTextBox07.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
                    weightTextBox07.ReadOnly = true;
                }
                else
                {
                    weightTextBox07.ReadOnly = false;
                }
                money7 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox07.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox08_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox08.Text) && !(countTextBox08.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
                    weightTextBox08.ReadOnly = true;
                }
                else
                {
                    weightTextBox08.ReadOnly = false;
                }
                money8 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox08.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox09_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox09.Text) && !(countTextBox09.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
                    weightTextBox09.ReadOnly = true;
                }
                else
                {
                    weightTextBox09.ReadOnly = false;
                }
                money9 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox09.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
            
        }
        private void countTextBox010_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox010.Text) && !(countTextBox010.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
                    weightTextBox010.ReadOnly = true;
                }
                else
                {
                    weightTextBox010.ReadOnly = false;
                }
                money10 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox010.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox011_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox011.Text) && !(countTextBox011.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
                    weightTextBox011.ReadOnly = true;
                }
                else
                {
                    weightTextBox011.ReadOnly = false;
                }
                money11 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox011.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        private void countTextBox012_Leave(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(countTextBox012.Text) && !(countTextBox012.Text == "0"))
                {
                    sub = decimal.Parse(countTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
                    weightTextBox012.ReadOnly = true;
                }
                else
                {
                    weightTextBox012.ReadOnly = false;
                }
                money12 = sub;
                sub1 = sub + sub * Tax / 100;
                moneyTextBox012.Text = string.Format("{0:C}", Math.Round(sub1, MidpointRounding.AwayFromZero));
            }
        }
        #endregion

        #region"納品書　金額が入力されたら総重量 or 総数計算、自動計算　TextChangedイベント"
        private void moneyTextBox00_TextChanged(object sender, EventArgs e)
        {
            if (data == "D")
            {

            }
            else
            {
                //重量×単価
                if (!string.IsNullOrEmpty(weightTextBox00.Text) && string.IsNullOrEmpty(countTextBox00.Text))
                {
                    countTextBox00.Text = 0.ToString();

                }
                //数量×単価
                if (!string.IsNullOrEmpty(countTextBox00.Text) && string.IsNullOrEmpty(weightTextBox00.Text))
                {
                    weightTextBox00.Text = 0.ToString();
                }
                countsum = int.Parse(countTextBox00.Text);
                weisum = decimal.Parse(weightTextBox00.Text);
                subSum = money0;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox01.Text) && string.IsNullOrEmpty(weightTextBox01.Text))
                {
                    weightTextBox01.Text = 0.ToString();
                }

                countsum += int.Parse(countTextBox01.Text);
                weisum += decimal.Parse(weightTextBox01.Text);
                subSum += money1;       //納品書は税抜き
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            
        }
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
                if (!string.IsNullOrEmpty(countTextBox02.Text) && string.IsNullOrEmpty(weightTextBox02.Text))
                {
                    weightTextBox02.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox02.Text);
                weisum += decimal.Parse(weightTextBox02.Text);
                subSum += money2;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            
        }
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
                if (!string.IsNullOrEmpty(countTextBox03.Text) && string.IsNullOrEmpty(weightTextBox03.Text))
                {
                    weightTextBox03.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox03.Text);
                weisum += decimal.Parse(weightTextBox03.Text);
                subSum += money3;     //増やしていく
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox04.Text) && string.IsNullOrEmpty(weightTextBox04.Text))
                {
                    weightTextBox04.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox04.Text);
                weisum += decimal.Parse(weightTextBox04.Text);
                subSum += money4;     //増やしていく
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox05.Text) && string.IsNullOrEmpty(weightTextBox05.Text))
                {
                    weightTextBox05.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox05.Text);
                weisum += decimal.Parse(weightTextBox05.Text);
                subSum += money5;     //増やしていく
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox06.Text) && string.IsNullOrEmpty(weightTextBox06.Text))
                {
                    weightTextBox06.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox06.Text);
                weisum += decimal.Parse(weightTextBox06.Text);
                subSum += money6;     //増やしていく
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
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
            if (!string.IsNullOrEmpty(countTextBox07.Text) && string.IsNullOrEmpty(weightTextBox07.Text))
            {
                weightTextBox07.Text = 0.ToString();
            }
            countsum += int.Parse(countTextBox07.Text);
            weisum += decimal.Parse(weightTextBox07.Text);
            subSum += money7;     //増やしていく
            TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
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
            if (!string.IsNullOrEmpty(countTextBox08.Text) && string.IsNullOrEmpty(weightTextBox08.Text))
            {
                weightTextBox08.Text = 0.ToString();
            }
            countsum += int.Parse(countTextBox08.Text);
            weisum += decimal.Parse(weightTextBox08.Text);
            subSum += money8;     //増やしていく
            TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
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
                if (!string.IsNullOrEmpty(countTextBox09.Text) && string.IsNullOrEmpty(weightTextBox09.Text))
                {
                    weightTextBox09.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox09.Text);
                weisum += decimal.Parse(weightTextBox09.Text);
                subSum += money9;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox010.Text) && string.IsNullOrEmpty(weightTextBox010.Text))
                {
                    weightTextBox010.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox010.Text);
                weisum += decimal.Parse(weightTextBox010.Text);
                subSum += money10;
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox011.Text) && string.IsNullOrEmpty(weightTextBox011.Text))
                {
                    weightTextBox011.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox011.Text);
                weisum += decimal.Parse(weightTextBox011.Text);
                subSum += money11;     //増やしていく
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
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
                if (!string.IsNullOrEmpty(countTextBox012.Text) && string.IsNullOrEmpty(weightTextBox012.Text))
                {
                    weightTextBox012.Text = 0.ToString();
                }
                countsum += int.Parse(countTextBox012.Text);
                weisum += decimal.Parse(weightTextBox012.Text);
                subSum += money12;     //増やしていく
                TaxAmount = Math.Round(subSum * Tax / 100, MidpointRounding.AwayFromZero);
                sum = subSum + TaxAmount;

                totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount2.Text = string.Format("{0:#,0}", countsum);
                subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion

        #endregion

        #region"右上の×で戻る"
        private void Statement_FormClosed(object sender, FormClosedEventArgs e)
        {
              mainMenu.Show();   
        }
        #endregion

        #region"納品書　登録ボタン"
        private void Register_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

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

            string OrderDate = orderDateTimePicker.Text;
            string DeliveryDate = DeliveryDateTimePicker.Text;
            string SettlementDate = SettlementDateTimePicker.Text;
            string PaymentMethod = paymentMethodComboBox.SelectedItem.ToString();
            string Name = name.Text;                                           //宛名
            string Title = titleComboBox.SelectedItem.ToString();              //敬称
            string Type = typeComboBox.SelectedItem.ToString();                //納品書or請求書
            string payee = PayeeComboBox.SelectedItem.ToString();              //振り込み先
            string coin = CoinComboBox.SelectedItem.ToString();                //通貨
            string remark = RemarkRegister.Text;

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            DataTable dt = new DataTable();
            string sql_str = "Insert into delivery_m (control_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, hororific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payable, currency, remark2, total_count, total_weight, types1) VALUES ( '" + ControlNumber + "','" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "' '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "'," + type +");";

            //管理番号：ControlNumber
            int record = 1;     //行数
            int mainCategory = mainCategoryCode00;
            int item = itemCode00;
            string Detail = itemDetail00.Text;
            decimal Weight = decimal.Parse(weightTextBox00.Text);
            int Count = int.Parse(countTextBox00.Text);
            decimal UnitPrice = decimal.Parse(unitPriceTextBox00.Text);
            decimal amount = money0 + money0 * Tax / 100;
            string Remarks = remarks00.Text;

            DataTable dt2 = new DataTable();
            string sql_str2 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);

            if (!string.IsNullOrEmpty(unitPriceTextBox01.Text) && !(unitPriceTextBox01.Text == "単価 -> 重量 or 数量"))
            {
                record = 2;
                mainCategory = mainCategoryCode01;
                item = itemCode01;
                Detail = itemDetail01.Text;
                Weight = decimal.Parse(weightTextBox01.Text);
                Count = int.Parse(countTextBox01.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox01.Text);
                amount = decimal.Parse(moneyTextBox01.Text);
                Remarks = remarks01.Text;

                DataTable dt4 = new DataTable();
                string sql_str4 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str4, conn);
                adapter.Fill(dt4);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox02.Text) && !(unitPriceTextBox02.Text == "単価 -> 重量 or 数量"))
            {
                record = 3;
                mainCategory = mainCategoryCode02;
                item = itemCode02;
                Detail = itemDetail02.Text;
                Weight = decimal.Parse(weightTextBox02.Text);
                Count = int.Parse(countTextBox02.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox02.Text);
                amount = decimal.Parse(moneyTextBox02.Text);
                Remarks = remarks02.Text;

                DataTable dt5 = new DataTable();
                string sql_str5 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str5, conn);
                adapter.Fill(dt5);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox03.Text) && !(unitPriceTextBox03.Text == "単価 -> 重量 or 数量"))
            {
                record = 4;
                mainCategory = mainCategoryCode03;
                item = itemCode03;
                Detail = itemDetail03.Text;
                Weight = decimal.Parse(weightTextBox03.Text);
                Count = int.Parse(countTextBox03.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox03.Text);
                amount = decimal.Parse(moneyTextBox03.Text);
                Remarks = remarks03.Text;

                DataTable dt6 = new DataTable();
                string sql_str6 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str6, conn);
                adapter.Fill(dt6);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox04.Text) && !(unitPriceTextBox04.Text == "単価 -> 重量 or 数量"))
            {
                record = 5;
                mainCategory = mainCategoryCode04;
                item = itemCode04;
                Detail = itemDetail04.Text;
                Weight = decimal.Parse(weightTextBox04.Text);
                Count = int.Parse(countTextBox04.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox04.Text);
                amount = decimal.Parse(moneyTextBox04.Text);
                Remarks = remarks04.Text;

                DataTable dt7 = new DataTable();
                string sql_str7 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str7, conn);
                adapter.Fill(dt7);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox05.Text) && !(unitPriceTextBox05.Text == "単価 -> 重量 or 数量"))
            {
                record = 6;
                mainCategory = mainCategoryCode05;
                item = itemCode05;
                Detail = itemDetail05.Text;
                Weight = decimal.Parse(weightTextBox05.Text);
                Count = int.Parse(countTextBox05.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox05.Text);
                amount = decimal.Parse(moneyTextBox05.Text);
                Remarks = remarks05.Text;

                DataTable dt8 = new DataTable();
                string sql_str8 = "Insert into delivery_calc VALUES  ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str8, conn);
                adapter.Fill(dt8);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox06.Text) && !(unitPriceTextBox06.Text == "単価 -> 重量 or 数量"))
            {
                record = 7;
                mainCategory = mainCategoryCode06;
                item = itemCode06;
                Detail = itemDetail06.Text;
                Weight = decimal.Parse(weightTextBox06.Text);
                Count = int.Parse(countTextBox06.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox06.Text);
                amount = decimal.Parse(moneyTextBox06.Text);
                Remarks = remarks06.Text;

                DataTable dt9 = new DataTable();
                string sql_str9 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str9, conn);
                adapter.Fill(dt9);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox07.Text) && !(unitPriceTextBox07.Text == "単価 -> 重量 or 数量"))
            {
                record = 8;
                mainCategory = mainCategoryCode07;
                item = itemCode07;
                Detail = itemDetail07.Text;
                Weight = decimal.Parse(weightTextBox07.Text);
                Count = int.Parse(countTextBox07.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox07.Text);
                amount = decimal.Parse(moneyTextBox07.Text);
                Remarks = remarks07.Text;

                DataTable dt10 = new DataTable();
                string sql_str10 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str10, conn);
                adapter.Fill(dt10);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox08.Text) && !(unitPriceTextBox08.Text == "単価 -> 重量 or 数量"))
            {
                record = 9;
                mainCategory = mainCategoryCode08;
                item = itemCode08;
                Detail = itemDetail08.Text;
                Weight = decimal.Parse(weightTextBox08.Text);
                Count = int.Parse(countTextBox08.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox08.Text);
                amount = decimal.Parse(moneyTextBox08.Text);
                Remarks = remarks08.Text;

                DataTable dt11 = new DataTable();
                string sql_str11 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str11, conn);
                adapter.Fill(dt11);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox09.Text) && !(unitPriceTextBox09.Text == "単価 -> 重量 or 数量"))
            {
                record = 10;
                mainCategory = mainCategoryCode09;
                item = itemCode09;
                Detail = itemDetail09.Text;
                Weight = decimal.Parse(weightTextBox09.Text);
                Count = int.Parse(countTextBox09.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox09.Text);
                amount = decimal.Parse(moneyTextBox09.Text);
                Remarks = remarks09.Text;

                DataTable dt12 = new DataTable();
                string sql_str12 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str12, conn);
                adapter.Fill(dt12);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox010.Text) && !(unitPriceTextBox010.Text == "単価 -> 重量 or 数量"))
            {
                record = 11;
                mainCategory = mainCategoryCode010;
                item = itemCode010;
                Detail = itemDetail010.Text;
                Weight = decimal.Parse(weightTextBox010.Text);
                Count = int.Parse(countTextBox010.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox010.Text);
                amount = decimal.Parse(moneyTextBox010.Text);
                Remarks = remarks010.Text;

                DataTable dt13 = new DataTable();
                string sql_str13 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str13, conn);
                adapter.Fill(dt13);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox011.Text) && !(unitPriceTextBox011.Text == "単価 -> 重量 or 数量"))
            {
                record = 12;
                mainCategory = mainCategoryCode011;
                item = itemCode011;
                Detail = itemDetail011.Text;
                Weight = decimal.Parse(weightTextBox011.Text);
                Count = int.Parse(countTextBox011.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox011.Text);
                amount = decimal.Parse(moneyTextBox011.Text);
                Remarks = remarks011.Text;

                DataTable dt14 = new DataTable();
                string sql_str14 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str14, conn);
                adapter.Fill(dt14);
            }

            if (!string.IsNullOrEmpty(unitPriceTextBox012.Text) && !(unitPriceTextBox012.Text == "単価 -> 重量 or 数量"))
            {
                record = 13;
                mainCategory = mainCategoryCode012;
                item = itemCode012;
                Detail = itemDetail012.Text;
                Weight = decimal.Parse(weightTextBox012.Text);
                Count = int.Parse(countTextBox012.Text);
                UnitPrice = decimal.Parse(unitPriceTextBox012.Text);
                amount = decimal.Parse(moneyTextBox012.Text);
                Remarks = remarks012.Text;

                DataTable dt15 = new DataTable();
                string sql_str15 = "Insert into delivery_calc VALUES ( '" + ControlNumber + "' ,'" + record + "' , " + item + " , " + Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount + " , '" + Remarks + "'," + mainCategory + "','" + Detail + "');";

                adapter = new NpgsqlDataAdapter(sql_str15, conn);
                adapter.Fill(dt15);
            }

            conn.Close();
            MessageBox.Show("登録しました。");
            this.DeliveryPreviewButton.Enabled = true;
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
            pictureBox1.ImageLocation = articlesTextBox.Text;
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
            e.Graphics.DrawString("：" + paymentMethodComboBox.SelectedIndex.ToString(), font, brush, new PointF(600 + d3, 180));    //決済方法

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
            e.Graphics.DrawString("：" + paymentMethodComboBox.SelectedIndex.ToString(), font, brush, new PointF(570 + d3, 990));    //決済方法
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
            DeliveryPreview deliveryPreview = new DeliveryPreview(mainMenu, staff_id, type, access_auth, pass);
            this.Close();
            deliveryPreview.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataSearchResults dataSearchResults = new DataSearchResults(mainMenu, type, staff_id, client_staff_name, phone, address, item, search1, search2, search3, data, pass, access_auth);
            this.Close();
            mainMenu.Hide();
            dataSearchResults.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DataSearchResults dataSearchResults = new DataSearchResults(mainMenu, type, staff_id, client_staff_name, phone, address, item, search1, search2, search3, data, pass, access_auth) ;
            this.Close();
            mainMenu.Hide();
            dataSearchResults.Show();
        }
    }
}
