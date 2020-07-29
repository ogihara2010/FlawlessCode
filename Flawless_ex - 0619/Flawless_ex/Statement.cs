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
        int b = 1;//大分類クリックカウント数（納品書）
        int staff_id;
        int itemMainCategoryCode;
        int type = 0;
        string path;

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

        string companyNmae;
        string shopName;
        string staff_name;
        string address;
        string register_date;
        string remarks;

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

        public Statement(MainMenu main, int id, int type, string staff_name, string address)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.type = type;
            this.staff_name = staff_name;
            this.address = address;
        }

        private void Statement_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select* from staff_m where staff_code = " + staff_id + ";";　//担当者名取得用
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
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    docuNum = reader["document_number"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(docuNum))
            {
                Num = docuNum.Substring(1, 5);       //伝票番号の数字部分
            }
            else
            {
                docuNum = "F00000";
                Num = docuNum.Substring(1, 5);       //伝票番号の数字部分
            }

            number = int.Parse(Num) + 1;
            documentNumberTextBox.Text = "F" + number.ToString().PadLeft(5, '0');       //Fを追加
            #endregion

            #region"納品書の管理番号"
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
            #endregion

            //担当者ごとの大分類の初期値を先頭に
            string sql_str2 = "select * from main_category_m where invalid = 0 order by main_category_code = " + itemMainCategoryCode + "asc;";
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

            #region "大分類"
            //大分類ヘッダー
            //1行目
            //計算書
            mainCategoryComboBox0.DataSource = dt;
            mainCategoryComboBox0.DisplayMember = "main_category_name";
            mainCategoryComboBox0.ValueMember = "main_category_code";
            mainCategoryComboBox0.SelectedIndex = 0;//担当者ごとの初期値設定
                                                    //納品書
            DataTable deliverydt = new DataTable();
            deliverydt = dt.Copy();
            mainCategoryComboBox00.DataSource = deliverydt;
            mainCategoryComboBox00.DisplayMember = "main_category_name";
            mainCategoryComboBox00.ValueMember = "main_category_code";
            mainCategoryComboBox00.SelectedIndex = 0;//担当者ごとの初期値設定

            //2行目
            //計算書
            DataTable dt100 = new DataTable();
            dt100 = dt.Copy();
            mainCategoryComboBox1.DataSource = dt100;
            mainCategoryComboBox1.DisplayMember = "main_category_name";
            mainCategoryComboBox1.ValueMember = "main_category_code";
            mainCategoryComboBox1.SelectedIndex = 0;
            //納品書
            DataTable deliverydt100 = new DataTable();
            deliverydt100 = dt.Copy();
            mainCategoryComboBox01.DataSource = deliverydt100;
            mainCategoryComboBox01.DisplayMember = "main_category_name";
            mainCategoryComboBox01.ValueMember = "main_category_code";
            mainCategoryComboBox01.SelectedIndex = 0;

            //3行目
            //計算書
            DataTable dt101 = new DataTable();
            dt101 = dt.Copy();
            mainCategoryComboBox2.DataSource = dt101;
            mainCategoryComboBox2.DisplayMember = "main_category_name";
            mainCategoryComboBox2.ValueMember = "main_category_code";
            mainCategoryComboBox2.SelectedIndex = 0;
            //納品書
            DataTable deliverydt101 = new DataTable();
            deliverydt101 = dt.Copy();
            mainCategoryComboBox02.DataSource = deliverydt101;
            mainCategoryComboBox02.DisplayMember = "main_category_name";
            mainCategoryComboBox02.ValueMember = "main_category_code";
            mainCategoryComboBox02.SelectedIndex = 0;

            //4行目
            //計算書
            DataTable dt102 = new DataTable();
            dt102 = dt.Copy();
            mainCategoryComboBox3.DataSource = dt102;
            mainCategoryComboBox3.DisplayMember = "main_category_name";
            mainCategoryComboBox3.ValueMember = "main_category_code";
            mainCategoryComboBox3.SelectedIndex = 0;
            //納品書
            DataTable deliverydt102 = new DataTable();
            deliverydt102 = dt.Copy();
            mainCategoryComboBox03.DataSource = deliverydt102;
            mainCategoryComboBox03.DisplayMember = "main_category_name";
            mainCategoryComboBox03.ValueMember = "main_category_code";
            mainCategoryComboBox03.SelectedIndex = 0;

            //5行目
            //計算書
            DataTable dt103 = new DataTable();
            dt103 = dt.Copy();
            mainCategoryComboBox4.DataSource = dt103;
            mainCategoryComboBox4.DisplayMember = "main_category_name";
            mainCategoryComboBox4.ValueMember = "main_category_code";
            mainCategoryComboBox4.SelectedIndex = 0;
            //納品書
            DataTable deliverydt103 = new DataTable();
            deliverydt103 = dt.Copy();
            mainCategoryComboBox04.DataSource = deliverydt103;
            mainCategoryComboBox04.DisplayMember = "main_category_name";
            mainCategoryComboBox04.ValueMember = "main_category_code";
            mainCategoryComboBox04.SelectedIndex = 0;

            //6行目
            //計算書
            DataTable dt104 = new DataTable();
            dt104 = dt.Copy();
            mainCategoryComboBox5.DataSource = dt104;
            mainCategoryComboBox5.DisplayMember = "main_category_name";
            mainCategoryComboBox5.ValueMember = "main_category_code";
            mainCategoryComboBox5.SelectedIndex = 0;
            //納品書
            DataTable deliverydt104 = new DataTable();
            deliverydt104 = dt.Copy();
            mainCategoryComboBox05.DataSource = deliverydt104;
            mainCategoryComboBox05.DisplayMember = "main_category_name";
            mainCategoryComboBox05.ValueMember = "main_category_code";
            mainCategoryComboBox05.SelectedIndex = 0;

            //7行目
            //計算書
            DataTable dt105 = new DataTable();
            dt105 = dt.Copy();
            mainCategoryComboBox6.DataSource = dt105;
            mainCategoryComboBox6.DisplayMember = "main_category_name";
            mainCategoryComboBox6.ValueMember = "main_category_code";
            mainCategoryComboBox6.SelectedIndex = 0;
            //納品書
            DataTable deliverydt105 = new DataTable();
            deliverydt105 = dt.Copy();
            mainCategoryComboBox06.DataSource = deliverydt105;
            mainCategoryComboBox06.DisplayMember = "main_category_name";
            mainCategoryComboBox06.ValueMember = "main_category_code";
            mainCategoryComboBox06.SelectedIndex = 0;

            //8行目
            //計算書
            DataTable dt106 = new DataTable();
            dt106 = dt.Copy();
            mainCategoryComboBox7.DataSource = dt106;
            mainCategoryComboBox7.DisplayMember = "main_category_name";
            mainCategoryComboBox7.ValueMember = "main_category_code";
            mainCategoryComboBox7.SelectedIndex = 0;
            //納品書
            DataTable deliverydt106 = new DataTable();
            deliverydt106 = dt.Copy();
            mainCategoryComboBox07.DataSource = deliverydt106;
            mainCategoryComboBox07.DisplayMember = "main_category_name";
            mainCategoryComboBox07.ValueMember = "main_category_code";
            mainCategoryComboBox07.SelectedIndex = 0;

            //9行目
            //計算書
            DataTable dt107 = new DataTable();
            dt107 = dt.Copy();
            mainCategoryComboBox8.DataSource = dt107;
            mainCategoryComboBox8.DisplayMember = "main_category_name";
            mainCategoryComboBox8.ValueMember = "main_category_code";
            mainCategoryComboBox8.SelectedIndex = 0;
            //納品書
            DataTable deliverydt107 = new DataTable();
            deliverydt107 = dt.Copy();
            mainCategoryComboBox08.DataSource = deliverydt107;
            mainCategoryComboBox08.DisplayMember = "main_category_name";
            mainCategoryComboBox08.ValueMember = "main_category_code";
            mainCategoryComboBox08.SelectedIndex = 0;

            //10行目
            //計算書
            DataTable dt108 = new DataTable();
            dt108 = dt.Copy();
            mainCategoryComboBox9.DataSource = dt108;
            mainCategoryComboBox9.DisplayMember = "main_category_name";
            mainCategoryComboBox9.ValueMember = "main_category_code";
            mainCategoryComboBox9.SelectedIndex = 0;
            //納品書
            DataTable deliverydt108 = new DataTable();
            deliverydt108 = dt.Copy();
            mainCategoryComboBox09.DataSource = deliverydt108;
            mainCategoryComboBox09.DisplayMember = "main_category_name";
            mainCategoryComboBox09.ValueMember = "main_category_code";
            mainCategoryComboBox09.SelectedIndex = 0;

            //11行目
            //計算書
            DataTable dt109 = new DataTable();
            dt109 = dt.Copy();
            mainCategoryComboBox10.DataSource = dt109;
            mainCategoryComboBox10.DisplayMember = "main_category_name";
            mainCategoryComboBox10.ValueMember = "main_category_code";
            mainCategoryComboBox10.SelectedIndex = 0;
            //納品書
            DataTable deliverydt109 = new DataTable();
            deliverydt109 = dt.Copy();
            mainCategoryComboBox010.DataSource = deliverydt109;
            mainCategoryComboBox010.DisplayMember = "main_category_name";
            mainCategoryComboBox010.ValueMember = "main_category_code";
            mainCategoryComboBox010.SelectedIndex = 0;

            //12行目
            //計算書
            DataTable dt110 = new DataTable();
            dt110 = dt.Copy();
            mainCategoryComboBox11.DataSource = dt110;
            mainCategoryComboBox11.DisplayMember = "main_category_name";
            mainCategoryComboBox11.ValueMember = "main_category_code";
            mainCategoryComboBox11.SelectedIndex = 0;
            //納品書
            DataTable deliverydt110 = new DataTable();
            deliverydt110 = dt.Copy();
            mainCategoryComboBox011.DataSource = deliverydt110;
            mainCategoryComboBox011.DisplayMember = "main_category_name";
            mainCategoryComboBox011.ValueMember = "main_category_code";
            mainCategoryComboBox011.SelectedIndex = 0;

            //13行目
            //計算書
            DataTable dt111 = new DataTable();
            dt111 = dt.Copy();
            mainCategoryComboBox12.DataSource = dt111;
            mainCategoryComboBox12.DisplayMember = "main_category_name";
            mainCategoryComboBox12.ValueMember = "main_category_code";
            mainCategoryComboBox12.SelectedIndex = 0;
            //納品書
            DataTable deliverydt112 = new DataTable();
            deliverydt112 = dt.Copy();
            mainCategoryComboBox012.DataSource = deliverydt112;
            mainCategoryComboBox012.DisplayMember = "main_category_name";
            mainCategoryComboBox012.ValueMember = "main_category_code";
            mainCategoryComboBox012.SelectedIndex = 0;
            #endregion

            #region "品名"
            //品名ヘッダー 初期表示

            //1行目
            //計算書
            itemComboBox0.DataSource = dt2;
            itemComboBox0.DisplayMember = "item_name";
            itemComboBox0.ValueMember = "item_code";
            //納品書
            deliverydt200 = dt2.Copy();
            itemComboBox00.DataSource = deliverydt200;
            itemComboBox00.DisplayMember = "item_name";
            itemComboBox00.ValueMember = "item_code";

            //2行目
            //計算書
            dt200 = dt2.Copy();
            itemComboBox1.DataSource = dt200;
            itemComboBox1.DisplayMember = "item_name";
            itemComboBox1.ValueMember = "item_code";
            //納品書
            deliverydt201 = dt2.Copy();
            itemComboBox01.DataSource = deliverydt201;
            itemComboBox01.DisplayMember = "item_name";
            itemComboBox01.ValueMember = "item_code";

            //3行目
            //計算書
            dt201 = dt2.Copy();
            itemComboBox2.DataSource = dt201;
            itemComboBox2.DisplayMember = "item_name";
            itemComboBox2.ValueMember = "item_code";
            //納品書
            deliverydt202 = dt2.Copy();
            itemComboBox02.DataSource = deliverydt202;
            itemComboBox02.DisplayMember = "item_name";
            itemComboBox02.ValueMember = "item_code";

            //4行目
            //計算書
            dt202 = dt2.Copy();
            itemComboBox3.DataSource = dt202;
            itemComboBox3.DisplayMember = "item_name";
            itemComboBox3.ValueMember = "item_code";
            //納品書
            deliverydt203 = dt2.Copy();
            itemComboBox03.DataSource = deliverydt203;
            itemComboBox03.DisplayMember = "item_name";
            itemComboBox03.ValueMember = "item_code";

            //5行目
            //計算書
            dt203 = dt2.Copy();
            itemComboBox4.DataSource = dt203;
            itemComboBox4.DisplayMember = "item_name";
            itemComboBox4.ValueMember = "item_code";
            //納品書
            deliverydt204 = dt2.Copy();
            itemComboBox04.DataSource = deliverydt204;
            itemComboBox04.DisplayMember = "item_name";
            itemComboBox04.ValueMember = "item_code";

            //6行目
            //計算書
            dt204 = dt2.Copy();
            itemComboBox5.DataSource = dt204;
            itemComboBox5.DisplayMember = "item_name";
            itemComboBox5.ValueMember = "item_code";
            //納品書
            deliverydt205 = dt2.Copy();
            itemComboBox05.DataSource = deliverydt205;
            itemComboBox05.DisplayMember = "item_name";
            itemComboBox05.ValueMember = "item_code";

            //7行目
            //計算書
            dt205 = dt2.Copy();
            itemComboBox6.DataSource = dt205;
            itemComboBox6.DisplayMember = "item_name";
            itemComboBox6.ValueMember = "item_code";
            //納品書
            deliverydt206 = dt2.Copy();
            itemComboBox06.DataSource = deliverydt206;
            itemComboBox06.DisplayMember = "item_name";
            itemComboBox06.ValueMember = "item_code";

            //8行目
            //計算書
            dt206 = dt2.Copy();
            itemComboBox7.DataSource = dt206;
            itemComboBox7.DisplayMember = "item_name";
            itemComboBox7.ValueMember = "item_code";
            //納品書
            deliverydt207 = dt2.Copy();
            itemComboBox07.DataSource = deliverydt207;
            itemComboBox07.DisplayMember = "item_name";
            itemComboBox07.ValueMember = "item_code";

            //9行目
            //計算書
            dt207 = dt2.Copy();
            itemComboBox8.DataSource = dt207;
            itemComboBox8.DisplayMember = "item_name";
            itemComboBox8.ValueMember = "item_code";
            //納品書
            deliverydt208 = dt2.Copy();
            itemComboBox08.DataSource = deliverydt208;
            itemComboBox08.DisplayMember = "item_name";
            itemComboBox08.ValueMember = "item_code";

            //10行目
            //計算書
            dt208 = dt2.Copy();
            itemComboBox9.DataSource = dt208;
            itemComboBox9.DisplayMember = "item_name";
            itemComboBox9.ValueMember = "item_code";
            //納品書
            deliverydt209 = dt2.Copy();
            itemComboBox09.DataSource = deliverydt209;
            itemComboBox09.DisplayMember = "item_name";
            itemComboBox09.ValueMember = "item_code";

            //11行目
            //計算書
            dt209 = dt2.Copy();
            itemComboBox10.DataSource = dt209;
            itemComboBox10.DisplayMember = "item_name";
            itemComboBox10.ValueMember = "item_code";
            //納品書
            deliverydt210 = dt2.Copy();
            itemComboBox010.DataSource = deliverydt210;
            itemComboBox010.DisplayMember = "item_name";
            itemComboBox010.ValueMember = "item_code";

            //12行目
            //計算書
            dt210 = dt2.Copy();
            itemComboBox11.DataSource = dt210;
            itemComboBox11.DisplayMember = "item_name";
            itemComboBox11.ValueMember = "item_code";
            //納品書
            deliverydt211 = dt2.Copy();
            itemComboBox011.DataSource = deliverydt211;
            itemComboBox011.DisplayMember = "item_name";
            itemComboBox011.ValueMember = "item_code";

            //13行目
            //計算書
            dt211 = dt2.Copy();
            itemComboBox12.DataSource = dt211;
            itemComboBox12.DisplayMember = "item_name";
            itemComboBox12.ValueMember = "item_code";
            //納品書
            deliverydt212 = dt2.Copy();
            itemComboBox012.DataSource = deliverydt212;
            itemComboBox012.DisplayMember = "item_name";
            itemComboBox012.ValueMember = "item_code";
            #endregion


            //左の入力項目処理
            //string itemDisplay = "item_name";
            //string itemValue = "item_code";
            //地金
            DataTable dt300 = new DataTable();
            string str_sql_metal = "select* from item_m where main_category_code = 100";
            adapter = new NpgsqlDataAdapter(str_sql_metal, conn);
            adapter.Fill(dt300);

            //ダイヤ
            DataTable dt400 = new DataTable();
            string str_sql_diamond = "select* from item_m where main_category_code = 101";

            adapter = new NpgsqlDataAdapter(str_sql_diamond, conn);
            adapter.Fill(dt400);

            //ブランド
            DataTable dt500 = new DataTable();
            string str_sql_brand = "select* from item_m where main_category_code = 102";
            adapter = new NpgsqlDataAdapter(str_sql_brand, conn);
            adapter.Fill(dt500);

            //製品・ジュエリー
            DataTable dt600 = new DataTable();
            string str_sql_jewelry = "select* from item_m where main_category_code = 103";
            adapter = new NpgsqlDataAdapter(str_sql_jewelry, conn);
            adapter.Fill(dt600);

            //その他
            DataTable dt700 = new DataTable();
            string str_sql_other = "select* from item_m where main_category_code = 104";
            adapter = new NpgsqlDataAdapter(str_sql_other, conn);
            adapter.Fill(dt700);

            //単価の欄に初期表示
            unitPriceTextBox0.Text = "単価 -> 重量 or 数量";
            unitPriceTextBox00.Text = "単価 -> 重量 or 数量";

            //タブのサイズ変更
            tabControl1.ItemSize = new Size(300, 40);

            //デフォルトで税込み表示
            comboBox11.SelectedIndex = 0;

            //デフォルトで円表示
            CoinComboBox.SelectedIndex = 0;

            if (count != 0)
            {
                if (type == 0)
                {
                    //顧客情報 法人
                    #region "計算書"
                    DataTable clientDt = new DataTable();
                    string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + staff_name + "' and address = '" + address + "';";
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

                    typeTextBox.Text = "法人";                    //種別
                    companyTextBox.Text = companyNmae;              //会社名   
                    registerDateTextBox2.Text = antique_license;              //古物商許可証
                    shopNameTextBox.Text = shopName;                    //店舗名
                    clientNameTextBox.Text = Staff_name;                //担当名
                    registerDateTextBox.Text = register_date;           //登録日
                    clientRemarksTextBox.Text = remarks;                //備考
                    #endregion

                    #region "納品書"
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
                    string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + staff_name + "' and address = '" + address + "';";
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
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt2);
                itemComboBox0.DataSource = dt2;
                itemComboBox0.DisplayMember = "item_name";
                itemComboBox0.ValueMember = "item_code";
                mainCategoryCode0 = int.Parse("main_category_code");
                itemCode0 = int.Parse("item_code");

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
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt200);
                itemComboBox1.DataSource = dt200;
                itemComboBox1.DisplayMember = "item_name";
                itemComboBox1.ValueMember = "item_code";
                mainCategoryCode1 = int.Parse("main_category_code");
                itemCode1 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //大分類によって品名変更 3行目
            if (a > 1)
            {

                int codeNum = (int)mainCategoryComboBox2.SelectedValue;
                dt201.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt201);
                itemComboBox2.DataSource = dt201;
                itemComboBox2.DisplayMember = "item_name";
                itemComboBox2.ValueMember = "item_code";
                mainCategoryCode2 = int.Parse("main_category_code");
                itemCode2 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 4行目
            if (a > 1)
            {

                int codeNum = (int)mainCategoryComboBox3.SelectedValue;
                dt202.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt202);
                itemComboBox3.DataSource = dt202;
                itemComboBox3.DisplayMember = "item_name";
                itemComboBox3.ValueMember = "item_code";
                mainCategoryCode3 = int.Parse("main_category_code");
                itemCode3 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 5行目
            if (a > 1)
            {

                int codeNum = (int)mainCategoryComboBox4.SelectedValue;
                dt203.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt203);
                itemComboBox4.DataSource = dt203;
                itemComboBox4.DisplayMember = "item_name";
                itemComboBox4.ValueMember = "item_code";
                mainCategoryCode4 = int.Parse("main_category_code");
                itemCode4 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 6行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox5.SelectedValue;
                dt204.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt204);
                itemComboBox5.DataSource = dt204;
                itemComboBox5.DisplayMember = "item_name";
                itemComboBox5.ValueMember = "item_code";
                mainCategoryCode5 = int.Parse("main_category_code");
                itemCode5 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 7行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox6.SelectedValue;
                dt205.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt205);
                itemComboBox6.DataSource = dt205;
                itemComboBox6.DisplayMember = "item_name";
                itemComboBox6.ValueMember = "item_code";
                mainCategoryCode6 = int.Parse("main_category_code");
                itemCode6 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 8行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox7.SelectedValue;
                dt206.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt206);
                itemComboBox7.DataSource = dt206;
                itemComboBox7.DisplayMember = "item_name";
                itemComboBox7.ValueMember = "item_code";
                mainCategoryCode7 = int.Parse("main_category_code");
                itemCode7 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 9行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox8.SelectedValue;
                dt207.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt207);
                itemComboBox8.DataSource = dt207;
                itemComboBox8.DisplayMember = "item_name";
                itemComboBox8.ValueMember = "item_code";
                mainCategoryCode8 = int.Parse("main_category_code");
                itemCode8 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 10行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox9.SelectedValue;
                dt208.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt208);
                itemComboBox9.DataSource = dt208;
                itemComboBox9.DisplayMember = "item_name";
                itemComboBox9.ValueMember = "item_code";
                mainCategoryCode9 = int.Parse("main_category_code");
                itemCode9 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 11行目
            if (a > 1)
            {
                int codeNum = (int)mainCategoryComboBox10.SelectedValue;
                dt209.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt209);
                itemComboBox10.DataSource = dt209;
                itemComboBox10.DisplayMember = "item_name";
                itemComboBox10.ValueMember = "item_code";
                mainCategoryCode10 = int.Parse("main_category_code");
                itemCode10 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 12行目
            if (a > 1)
            {

                int codeNum = (int)mainCategoryComboBox11.SelectedValue;
                dt210.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt210);
                itemComboBox11.DataSource = dt210;
                itemComboBox11.DisplayMember = "item_name";
                itemComboBox11.ValueMember = "item_code";
                mainCategoryCode11 = int.Parse("main_category_code");
                itemCode11 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 13行目
            if (a > 1)
            {

                int codeNum = (int)mainCategoryComboBox12.SelectedValue;
                dt211.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt211);
                itemComboBox12.DataSource = dt211;
                itemComboBox12.DisplayMember = "item_name";
                itemComboBox12.ValueMember = "item_code";
                mainCategoryCode12 = int.Parse("main_category_code");
                itemCode12 = int.Parse("item_code");

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
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt200);
                itemComboBox00.DataSource = deliverydt200;
                itemComboBox00.DisplayMember = "item_name";
                itemComboBox00.ValueMember = "item_code";
                mainCategoryCode00 = int.Parse("main_category_code");
                itemCode0 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }

        }

        private void mainCategoryComboBox01_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 2行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox01.SelectedValue;
                deliverydt201.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt201);
                itemComboBox01.DataSource = deliverydt201;
                itemComboBox01.DisplayMember = "item_name";
                itemComboBox01.ValueMember = "item_code";
                mainCategoryCode01 = int.Parse("main_category_code");
                itemCode1 = int.Parse("item_code");

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
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt202);
                itemComboBox02.DataSource = deliverydt202;
                itemComboBox02.DisplayMember = "item_name";
                itemComboBox02.ValueMember = "item_code";
                mainCategoryCode02 = int.Parse("main_category_code");
                itemCode2 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox03_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 4行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox03.SelectedValue;
                deliverydt203.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt203);
                itemComboBox03.DataSource = deliverydt203;
                itemComboBox03.DisplayMember = "item_name";
                itemComboBox03.ValueMember = "item_code";
                mainCategoryCode03 = int.Parse("main_category_code");
                itemCode3 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox04_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 5行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox04.SelectedValue;
                deliverydt204.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt204);
                itemComboBox04.DataSource = deliverydt204;
                itemComboBox04.DisplayMember = "item_name";
                itemComboBox04.ValueMember = "item_code";
                mainCategoryCode04 = int.Parse("main_category_code");
                itemCode4 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox05_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 6行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox05.SelectedValue;
                deliverydt205.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt205);
                itemComboBox05.DataSource = deliverydt205;
                itemComboBox05.DisplayMember = "item_name";
                itemComboBox05.ValueMember = "item_code";
                mainCategoryCode05 = int.Parse("main_category_code");
                itemCode5 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox06_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 7行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox06.SelectedValue;
                deliverydt206.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt206);
                itemComboBox06.DataSource = deliverydt206;
                itemComboBox06.DisplayMember = "item_name";
                itemComboBox06.ValueMember = "item_code";
                mainCategoryCode06 = int.Parse("main_category_code");
                itemCode6 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox07_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 8行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox07.SelectedValue;
                deliverydt207.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt207);
                itemComboBox07.DataSource = deliverydt207;
                itemComboBox07.DisplayMember = "item_name";
                itemComboBox07.ValueMember = "item_code";
                mainCategoryCode07 = int.Parse("main_category_code");
                itemCode7 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox08_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 9行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox08.SelectedValue;
                deliverydt208.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt208);
                itemComboBox08.DataSource = deliverydt208;
                itemComboBox08.DisplayMember = "item_name";
                itemComboBox08.ValueMember = "item_code";
                mainCategoryCode08 = int.Parse("main_category_code");
                itemCode8 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox09_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 10行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox09.SelectedValue;
                deliverydt209.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt209);
                itemComboBox09.DataSource = deliverydt209;
                itemComboBox09.DisplayMember = "item_name";
                itemComboBox09.ValueMember = "item_code";
                mainCategoryCode09 = int.Parse("main_category_code");
                itemCode9 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox010_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 11行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox010.SelectedValue;
                deliverydt210.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt210);
                itemComboBox010.DataSource = deliverydt210;
                itemComboBox010.DisplayMember = "item_name";
                itemComboBox010.ValueMember = "item_code";
                mainCategoryCode010 = int.Parse("main_category_code");
                itemCode10 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox011_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 12行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox011.SelectedValue;
                deliverydt211.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt211);
                itemComboBox011.DataSource = deliverydt211;
                itemComboBox011.DisplayMember = "item_name";
                itemComboBox011.ValueMember = "item_code";
                mainCategoryCode011 = int.Parse("main_category_code");
                itemCode11 = int.Parse("item_code");

                conn.Close();
            }
            else
            {

            }
        }

        private void mainCategoryComboBox012_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 12行目
            if (b > 1)
            {
                int codeNum = (int)mainCategoryComboBox012.SelectedValue;
                deliverydt212.Clear();
                conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt212);
                itemComboBox012.DataSource = deliverydt212;
                itemComboBox012.DisplayMember = "item_name";
                itemComboBox012.ValueMember = "item_code";
                mainCategoryCode012 = int.Parse("main_category_code");
                itemCode12 = int.Parse("item_code");

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
            using (client_search search2 = new client_search(mainMenu, staff_id, type))
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
                    companyNmae = row["company_name"].ToString();
                    shopName = row["shop_name"].ToString();
                    staff_name = row["staff_name"].ToString();
                    address = row["address"].ToString();
                    register_date = row["registration_date"].ToString();
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
                    string name= row["name"].ToString();
                    address = row["address"].ToString();
                    remarks = row["remarks"].ToString();

                    typeTextBox.Text = "個人";
                    clientNameTextBox.Text = name;
                    antiqueLicenceTextBox.Text = address;
                    clientRemarksTextBox.Text = remarks;
                }
            }
        }

        private void clientSelectButton_Click(object sender, EventArgs e)//顧客選択メニュー（納品書）
        {
            using (client_search search2 = new client_search(mainMenu, staff_id, type))
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

            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            string DocumentNumber = documentNumberTextBox.Text;
            decimal TotalWeight = weisum;
            int Amount = countsum;
            decimal SubTotal = subSum;
            decimal Total = subSum;
            string SettlementDate = settlementBox.Text;
            string DeliveryDate = deliveryDateBox.Text;
            string DeliveryMethod = deliveryComboBox.Text;
            string PaymentMethod = paymentMethodsComboBox.Text;
            int TYPE = 0;
            int AntiqueNumber = 0;
            int ID_Number = 0;
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
                CompanyName = companyNmae;
                ShopName = shopName;
                StaffName = staff_name;

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
                        NUMBER = @"select antique_number, phone_number from client_m_corporate where company_name ='" + companyNmae + "' and shop_name ='" + shopName + "' and staff_name ='" + staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
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
                            string SQL_STR = @"update client_m_corporate set aol_financial_shareholder='" + AolFinancialShareholder + "' where company_name ='" + companyNmae + "' and shop_name ='" + shopName + "' and staff_name ='" + staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
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
                            string SQL_STR = @"update client_m_corporate set tax_certificate='" + TaxCertification + "' where company_name ='" + companyNmae + "' and shop_name ='" + shopName + "' and staff_name ='" + staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
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
                            string SQL_STR = @"update client_m_corporate set seal_certification='" + SealCertification + "' where company_name ='" + companyNmae + "' and shop_name ='" + shopName + "' and staff_name ='" + staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
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
                            string SQL_STR = @"update client_m_corporate set (residence_card, period_stay) =('" + ResidenceCard + "','" + ResidencePeriod + "') where company_name ='" + companyNmae + "' and shop_name ='" + shopName + "' and staff_name ='" + staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
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
                            string SQL_STR = @"update client_m_corporate set antique_license ='" + AntiqueLicence + "' where company_name ='" + companyNmae + "' and shop_name ='" + shopName + "' and staff_name ='" + staff_name + "' and registration_date='" + registerDateTextBox.Text + "';";
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

            string sql_str = "Insert into statement_data (antique_number, id_number, staff_code, total_weight, total_amount, sub_total, tax_amount, total, delivery_method, payment_method, settlement_date, delivery_date, document_number, company_name, shop_name, staff_name, name, type, birthday, phone_number, occupation) VALUES ('" + AntiqueNumber + "','" + ID_Number + "' , '" + staff_id + "' , '" + TotalWeight + "' ,  '" + Amount + "' , '" + SubTotal + "', '" + TaxAmount + "' , '" + Total + "' , '" + DeliveryMethod + "' , '" + PaymentMethod + "' , '" + SettlementDate + "' , '" + DeliveryDate + "', '" + DocumentNumber + "','" + CompanyName + "','" + ShopName + "','" + StaffName + "','" + Name + "','" + TYPE + "','" + Birthday + "','" + TEL + "','" + Work + "');";

            conn.Close();

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

        }
        #endregion

        #region"計算書・納品書　計算処理"

        #region　"計算書　単価を入力したら重量、数値入力可"
        private void unitPriceTextBox0_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
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
        private void unitPriceTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox1.Text))
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
        private void unitPriceTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox2.Text))
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
        private void unitPriceTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox3.Text))
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
        private void unitPriceTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox4.Text))
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
        private void unitPriceTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox5.Text))
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
        private void unitPriceTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox6.Text))
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
        private void unitPriceTextBox7_TextChanged(object sender, EventArgs e)
        {
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
        private void unitPriceTextBox8_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox8.Text))
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
        private void unitPriceTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox9.Text))
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
        private void unitPriceTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox10.Text))
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
        private void unitPriceTextBox11_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox11.Text))
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
        private void unitPriceTextBox12_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox12.Text))
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

        #region "計算書　フォーカス時初期状態ならnull"
        private void unitPriceTextBox0_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox0.Text.ToString() == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox0.Text = "";
            }
        }

        private void unitPriceTextBox1_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox1.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox1.Text = "";
            }
        }
        private void unitPriceTextBox2_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox2.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox2.Text = "";
            }
        }
        private void unitPriceTextBox3_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox3.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox3.Text = "";
            }
        }
        private void unitPriceTextBox4_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox4.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox4.Text = "";
            }
        }
        private void unitPriceTextBox5_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox5.Text.ToString() == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox5.Text = "";
            }
        }
        private void unitPriceTextBox6_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox6.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox6.Text = "";
            }
        }
        private void unitPriceTextBox7_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox7.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox7.Text = "";
            }
        }
        private void unitPriceTextBox8_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox8.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox8.Text = "";
            }
        }
        private void unitPriceTextBox9_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox9.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox9.Text = "";
            }
        }
        private void unitPriceTextBox10_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox10.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox10.Text = "";
            }
        }
        private void unitPriceTextBox11_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox11.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox11.Text = "";
            }
        }
        private void unitPriceTextBox12_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox12.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox12.Text = "";
            }
        }
        #endregion

        #region"計算書　単価３桁区切り＋フォーカスが外れた時"
        private void unitPriceTextBox0_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
            {
                unitPriceTextBox0.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox1.ReadOnly = false;
                unitPriceTextBox1.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox0.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox0.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox1.Text))
            {
                unitPriceTextBox1.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox2.ReadOnly = false;
                unitPriceTextBox2.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox1.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox1.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox2.Text))
            {
                unitPriceTextBox2.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox3.ReadOnly = false;
                unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox2.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox2.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox3.Text))
            {
                unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox4.ReadOnly = false;
                unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox3.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox3.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox4.Text))
            {
                unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox5.ReadOnly = false;
                unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox4.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox4.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox5.Text))
            {
                unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox6.ReadOnly = false;
                unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox5.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox5.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox6.Text))
            {
                unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox7.ReadOnly = false;
                unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox6.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox6.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox7.Text))
            {
                unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox8.ReadOnly = false;
                unitPriceTextBox8.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox7.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox7.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox8_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox8.Text))
            {
                unitPriceTextBox8.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox9.ReadOnly = false;
                unitPriceTextBox9.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox8.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox8.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox9_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox9.Text))
            {
                unitPriceTextBox9.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox10.ReadOnly = false;
                unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox9.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox9.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox10.Text))
            {
                unitPriceTextBox10.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox11.ReadOnly = false;
                unitPriceTextBox11.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox10.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox10.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox11_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox11.Text))
            {
                unitPriceTextBox11.Text = "単価 -> 重量 or 数量";
            }
            else
            {
                unitPriceTextBox12.ReadOnly = false;
                unitPriceTextBox12.Text = "単価 -> 重量 or 数量";
                unitPriceTextBox11.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox11.Text, System.Globalization.NumberStyles.Number));
            }
        }

        private void unitPriceTextBox12_Leave(object sender, EventArgs e)
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
        #endregion

        #region"計算書　金額が入力されたら次の単価が入力可＋総重量 or 総数計算、自動計算"
        private void moneyTextBox0_TextChanged(object sender, EventArgs e)
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
            countsum = int.Parse(countTextBox0.Text);
            weisum = decimal.Parse(weightTextBox0.Text);
            subSum = money0;        //計算書は税込み
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox1_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox1.Text);
            weisum += decimal.Parse(weightTextBox1.Text);
            subSum += money1;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox2_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox2.Text);
            weisum += decimal.Parse(weightTextBox2.Text);
            subSum += money2;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox3_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox3.Text);
            weisum += decimal.Parse(weightTextBox3.Text);
            subSum += money3;     //増やしていく
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox4_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox4.Text);
            weisum += decimal.Parse(weightTextBox4.Text);
            subSum += money4;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox5_TextChanged(object sender, EventArgs e)
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

            countsum += int.Parse(countTextBox5.Text);
            weisum += decimal.Parse(weightTextBox5.Text);
            subSum += money5;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox6_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox6.Text);
            weisum += decimal.Parse(weightTextBox6.Text);
            subSum += money6;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox7_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox7.Text);
            weisum += decimal.Parse(weightTextBox7.Text);
            subSum += money7;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox8_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox8.Text);
            weisum += decimal.Parse(weightTextBox8.Text);
            subSum += money8;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox9_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox9.Text);
            weisum += decimal.Parse(weightTextBox9.Text);
            subSum += money9;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox10_TextChanged(object sender, EventArgs e)
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

            countsum += int.Parse(countTextBox10.Text);
            weisum += decimal.Parse(weightTextBox10.Text);
            subSum += money10;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox11_TextChanged(object sender, EventArgs e)
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
            countsum += int.Parse(countTextBox11.Text);
            weisum += decimal.Parse(weightTextBox11.Text);
            subSum += money11;
            TaxAmount = subSum * Tax / 100;

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
        private void moneyTextBox12_TextChanged(object sender, EventArgs e)
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

            countsum += int.Parse(countTextBox12.Text);
            weisum += decimal.Parse(weightTextBox12.Text);
            subSum += money12;
            TaxAmount = subSum * Tax / 100;

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
        #endregion

        #region　"納品書　単価を入力したら重量、数値入力可"
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

        #region"納品書　フォーカス時初期状態ならnull"
        private void unitPriceTextBox00_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox00.Text.ToString() == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox00.Text = "";
            }
        }

        private void unitPriceTextBox01_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox01.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox01.Text = "";
            }
        }
        private void unitPriceTextBox02_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox02.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox02.Text = "";
            }
        }
        private void unitPriceTextBox03_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox03.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox03.Text = "";
            }
        }
        private void unitPriceTextBox04_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox04.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox04.Text = "";
            }
        }
        private void unitPriceTextBox05_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox05.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox05.Text = "";
            }
        }
        private void unitPriceTextBox06_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox06.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox06.Text = "";
            }
        }
        private void unitPriceTextBox07_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox07.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox07.Text = "";
            }
        }
        private void unitPriceTextBox08_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox08.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox08.Text = "";
            }
        }
        private void unitPriceTextBox09_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox09.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox09.Text = "";
            }
        }
        private void unitPriceTextBox010_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox010.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox010.Text = "";
            }
        }
        private void unitPriceTextBox011_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox011.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox011.Text = "";
            }
        }
        private void unitPriceTextBox012_Enter(object sender, EventArgs e)
        {
            if (unitPriceTextBox012.Text == "単価 -> 重量 or 数量")
            {
                unitPriceTextBox012.Text = "";
            }
        }
        #endregion

        #region"納品書　数量を入力したら重量を入力不可"

        private void countTextBox00_TextChanged(object sender, EventArgs e)
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

        private void countTextBox01_TextChanged(object sender, EventArgs e)
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

        private void countTextBox02_TextChanged(object sender, EventArgs e)
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

        private void countTextBox03_TextChanged(object sender, EventArgs e)
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

        private void countTextBox04_TextChanged(object sender, EventArgs e)
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

        private void countTextBox05_TextChanged(object sender, EventArgs e)
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

        private void countTextBox06_TextChanged(object sender, EventArgs e)
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

        private void countTextBox07_TextChanged(object sender, EventArgs e)
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

        private void countTextBox08_TextChanged(object sender, EventArgs e)
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

        private void countTextBox09_TextChanged(object sender, EventArgs e)
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

        private void countTextBox010_TextChanged(object sender, EventArgs e)
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

        private void countTextBox011_TextChanged(object sender, EventArgs e)
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

        private void countTextBox012_TextChanged(object sender, EventArgs e)
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

        #region"納品書　単価３桁区切り＋フォーカスが外れた時"
        private void unitPriceTextBox00_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox01_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox02_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox03_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox04_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox05_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox06_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox07_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox08_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox09_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox010_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox011_Leave(object sender, EventArgs e)
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

        private void unitPriceTextBox012_Leave(object sender, EventArgs e)
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
        #endregion

        #region"計算書　重量×単価"
        private void weightTextBox0_Leave(object sender, EventArgs e)
        {

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
            money0 = sub;       //計算書は税込み
            moneyTextBox0.Text = string.Format("{0:C}", sub);
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
            money1 = sub;
            moneyTextBox1.Text = string.Format("{0:C}", sub);
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
            money2 = sub;
            moneyTextBox2.Text = string.Format("{0:C}", sub);
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
            money3 = sub;
            moneyTextBox3.Text = string.Format("{0:C}", sub);
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
            money4 = sub;
            moneyTextBox4.Text = string.Format("{0:C}", sub);
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
            money5 = sub;
            moneyTextBox5.Text = string.Format("{0:C}", sub);
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
            money6 = sub;
            moneyTextBox6.Text = string.Format("{0:C}", sub);
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
            money7 = sub;
            moneyTextBox7.Text = string.Format("{0:C}", sub);
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
            money8 = sub;
            moneyTextBox8.Text = string.Format("{0:C}", sub);
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
            money9 = sub;
            moneyTextBox9.Text = string.Format("{0:C}", sub);
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
            money10 = sub;
            moneyTextBox10.Text = string.Format("{0:C}", sub);
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
            money11 = sub;
            moneyTextBox11.Text = string.Format("{0:C}", sub);
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
            money12 = sub;
            moneyTextBox12.Text = string.Format("{0:C}", sub);
        }
        #endregion

        #region"計算書　数量×単価"
        private void countTextBox0_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox0.Text) && !(countTextBox0.Text == "0"))
            {
                sub = decimal.Parse(countTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
                sub += sub * Tax / 100;
                money0 = sub;
                moneyTextBox0.Text = string.Format("{0:C}", sub);
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
                money1 = sub;
                moneyTextBox1.Text = string.Format("{0:C}", sub);
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
                money2 = sub;
                moneyTextBox2.Text = string.Format("{0:C}", sub);
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
                money3 = sub;
                moneyTextBox3.Text = string.Format("{0:C}", sub);
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
                money4 = sub;
                moneyTextBox4.Text = string.Format("{0:C}", sub);
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
                money5 = sub;
                moneyTextBox5.Text = string.Format("{0:C}", sub);
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
                money6 = sub;
                moneyTextBox6.Text = string.Format("{0:C}", sub);
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
                money7 = sub;
                moneyTextBox7.Text = string.Format("{0:C}", sub);
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
                money8 = sub;
                moneyTextBox8.Text = string.Format("{0:C}", sub);
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
                money9 = sub;
                moneyTextBox9.Text = string.Format("{0:C}", sub);
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
                money10 = sub;
                moneyTextBox10.Text = string.Format("{0:C}", sub);
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
                money11 = sub;
                moneyTextBox11.Text = string.Format("{0:C}", sub);
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
                money12 = sub;
                moneyTextBox12.Text = string.Format("{0:C}", sub);
                weightTextBox12.ReadOnly = true;
            }
            else
            {
                weightTextBox12.ReadOnly = false;
            }
        }
        #endregion

        #region "納品書　重量×単価"
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
            moneyTextBox00.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox01.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox02.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox03.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox04.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox05.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox06.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox07.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox08.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox09.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox010.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox011.Text = string.Format("{0:C}", sub1);
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
            moneyTextBox012.Text = string.Format("{0:C}", sub1);
        }
        #endregion

        #region　"納品書　数量×単価"
        private void countTextBox00_Leave(object sender, EventArgs e)
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
            moneyTextBox00.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox01_Leave(object sender, EventArgs e)
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
            moneyTextBox01.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox02_Leave(object sender, EventArgs e)
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
            moneyTextBox02.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox03_Leave(object sender, EventArgs e)
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
            moneyTextBox03.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox04_Leave(object sender, EventArgs e)
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
            moneyTextBox04.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox05_Leave(object sender, EventArgs e)
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
            moneyTextBox05.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox06_Leave(object sender, EventArgs e)
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
            moneyTextBox06.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox07_Leave(object sender, EventArgs e)
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
            moneyTextBox07.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox08_Leave(object sender, EventArgs e)
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
            moneyTextBox08.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox09_Leave(object sender, EventArgs e)
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
            moneyTextBox09.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox010_Leave(object sender, EventArgs e)
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
            moneyTextBox010.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox011_Leave(object sender, EventArgs e)
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
            moneyTextBox011.Text = string.Format("{0:C}", sub1);
        }
        private void countTextBox012_Leave(object sender, EventArgs e)
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
            moneyTextBox012.Text = string.Format("{0:C}", sub1);
        }
        #endregion

        #region"納品書　金額が入力されたら総重量 or 総数計算、自動計算"
        private void moneyTextBox00_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox01_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox02_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox03_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox04_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox05_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox06_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox07_TextChanged(object sender, EventArgs e)
        {
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox08_TextChanged(object sender, EventArgs e)
        {
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox09_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox010_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox011_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
        }
        private void moneyTextBox012_TextChanged(object sender, EventArgs e)
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
            TaxAmount = subSum * Tax / 100;
            sum = subSum + TaxAmount;

            totalWeight2.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
            totalCount2.Text = string.Format("{0:#,0}", countsum);
            subTotal2.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
            taxAmount2.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
            sumTextBox2.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
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
            string sql_str = "Insert into delivery_m (control_number, staff_code, sub_total, vat, vat_rate, vat_amount, total, name, hororific_title, type, order_date, delivery_date, settlement_date, seaal_print, payment_method, account_payable, currency, remark2, total_count, total_weight) VALUES ( '" + ControlNumber + "','" + staff_id + " ', '" + SubTotal + "','" + vat + "','" + vat_rate + "','" + TaxAmount + "' , '" + Total + "','" + Name + "' ,'" + Title + "','" + Type + "', '" + OrderDate + "' , '" + DeliveryDate + "','" + SettlementDate + "' ,'" + seaal + "' '" + PaymentMethod + "' , '" + payee + "','" + coin + "','" + remark + "','" + Amount + "','" + TotalWeight + "');";

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

            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
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
        }

        #endregion

        #region"画像登録"
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
                op.InitialDirectory = @"C:\Users";
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
                op.InitialDirectory = @"C:\Users";
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
                op.InitialDirectory = @"C:\Users";
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
                op.InitialDirectory = @"C:\Users";
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
                op.InitialDirectory = @"C:\Users";
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
                op.InitialDirectory = @"C:\Users";
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
            pd.Print();
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
            e.Graphics.DrawString("：" + paymentMethodComboBox.Text, font, brush, new PointF(600 + d3, 180));    //決済方法

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
            e.Graphics.DrawString("：" + paymentMethodComboBox.Text, font, brush, new PointF(570 + d3, 990));    //決済方法
            e.Graphics.DrawString("：", font3, brush, new PointF(450 + d, 1050));   //署名

            e.Graphics.DrawRectangle(p, 470, 1070, 250, 0.1f);            //署名下の下線
            e.Graphics.DrawRectangle(p, 730 - d2, 1000 - d2, d2 * 2 + 10, d2 * 2 + 10);         //印の枠
            #endregion

            #region"ページ上のお客様情報"
            //法人の場合
            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (type == 0)
            {
                DataTable clientDt1 = new DataTable();
                string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + staff_name + "' and address = '" + address + "';";
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
                string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + staff_name + "' and address = '" + address + "';";

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

            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            if (type == 0)
            {
                DataTable clientDt1 = new DataTable();
                string str_sql_corporate = "select * from client_m_corporate where invalid = 0 and type = 0 and staff_name = '" + staff_name + "' and address = '" + address + "';";
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
                string str_sql_individual = "select * from client_m_individual where invalid = 0 and type = 1 and name = '" + staff_name + "' and address = '" + address + "';";

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
            RecordList recordList = new RecordList(this, staff_id, staff_name, type, documentNumberTextBox.Text);

            this.Hide();
            recordList.Show();
        }

        #endregion

        private void Button9_Click(object sender, EventArgs e)
        {
            DeliveryPreview deliveryPreview = new DeliveryPreview(mainMenu, staff_id, type);
            this.Close();
            deliveryPreview.Show();
        }
    }
}
