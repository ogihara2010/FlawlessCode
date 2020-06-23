using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace Flawless_ex
{
    public partial class Statement : Form //計算書/納品書作成メニュー
    {
        int a = 0;//大分類クリックカウント数（計算書）
        int b = 1;//大分類クリックカウント数（納品書）
        int con = 0; //追加カウント数
        int com = 0;　// 納品書　追加カウント
        int staff_id;
        int itemMainCategoryCode;
        
        MainMenu mainMenu;

        DataTable dt = new DataTable();//大分類

        DataTable dt2 = new DataTable();//品名と大分類関連付け
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
        

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataAdapter adapter;
        public Statement(MainMenu main,int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Statement_Load(object sender, EventArgs e)
        {

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定



            string sql_str = "select* from staff_m where staff_code = " + staff_id + ";";　//担当者名取得用
            cmd = new NpgsqlCommand(sql_str, conn);

            conn.Open();
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    label2.Text = (string.Format("{0}", reader["staff_name"]));
                    itemMainCategoryCode = (int)reader["main_category_code"];
                }
            }

            //担当者ごとの大分類の初期値を先頭に
            string sql_str2 = "select* from main_category_m order by main_category_code = " + itemMainCategoryCode + "desc;";
            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt);

            //品名検索用
            string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + itemMainCategoryCode + ";";
            adapter = new NpgsqlDataAdapter(sql_str3, conn);
            adapter.Fill(dt2);

            string sql_str4 = "select main_category_name from main_category_m where main_category_code = " + itemMainCategoryCode + ";";
            NpgsqlDataAdapter adapter1;
            adapter1 = new NpgsqlDataAdapter(sql_str4, conn);
            adapter1.Fill(dt3);

            DataRow row;
            row = dt3.Rows[0];
            string MainName = row["main_category_name"].ToString();

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
            mainCategoryComboBox07.DataSource =deliverydt106;
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
            #region "2行目以降"
            //２行目以降非表示
            mainCategoryComboBox1.Hide();
            itemComboBox1.Hide();
            weightTextBox1.Hide();
            countTextBox1.Hide();
            unitPriceTextBox1.Hide();
            moneyTextBox1.Hide();
            remarks1.Hide();
            mainCategoryComboBox01.Hide();
            itemComboBox01.Hide();
            weightTextBox01.Hide();
            countTextBox01.Hide();
            unitPriceTextBox01.Hide();
            moneyTextBox01.Hide();
            remarks01.Hide();

            mainCategoryComboBox2.Hide();
            itemComboBox2.Hide();
            weightTextBox2.Hide();
            countTextBox2.Hide();
            unitPriceTextBox2.Hide();
            moneyTextBox2.Hide();
            remarks2.Hide();
            mainCategoryComboBox02.Hide();
            itemComboBox02.Hide();
            weightTextBox02.Hide();
            countTextBox02.Hide();
            unitPriceTextBox02.Hide();
            moneyTextBox02.Hide();
            remarks02.Hide();

            mainCategoryComboBox3.Hide();
            itemComboBox3.Hide();
            weightTextBox3.Hide();
            countTextBox3.Hide();
            unitPriceTextBox3.Hide();
            moneyTextBox3.Hide();
            remarks3.Hide();
            mainCategoryComboBox03.Hide();
            itemComboBox03.Hide();
            weightTextBox03.Hide();
            countTextBox03.Hide();
            unitPriceTextBox03.Hide();
            moneyTextBox03.Hide();
            remarks03.Hide();

            mainCategoryComboBox4.Hide();
            itemComboBox4.Hide();
            weightTextBox4.Hide();
            countTextBox4.Hide();
            unitPriceTextBox4.Hide();
            moneyTextBox4.Hide();
            remarks4.Hide();
            mainCategoryComboBox04.Hide();
            itemComboBox04.Hide();
            weightTextBox04.Hide();
            countTextBox04.Hide();
            unitPriceTextBox04.Hide();
            moneyTextBox04.Hide();
            remarks04.Hide();

            mainCategoryComboBox5.Hide();
            itemComboBox5.Hide();
            weightTextBox5.Hide();
            countTextBox5.Hide();
            unitPriceTextBox5.Hide();
            moneyTextBox5.Hide();
            remarks5.Hide();
            mainCategoryComboBox05.Hide();
            itemComboBox05.Hide();
            weightTextBox05.Hide();
            countTextBox05.Hide();
            unitPriceTextBox05.Hide();
            moneyTextBox05.Hide();
            remarks05.Hide();

            mainCategoryComboBox6.Hide();
            itemComboBox6.Hide();
            weightTextBox6.Hide();
            countTextBox6.Hide();
            unitPriceTextBox6.Hide();
            moneyTextBox6.Hide();
            remarks6.Hide();
            mainCategoryComboBox06.Hide();
            itemComboBox06.Hide();
            weightTextBox06.Hide();
            countTextBox06.Hide();
            unitPriceTextBox06.Hide();
            moneyTextBox06.Hide();
            remarks06.Hide();

            mainCategoryComboBox7.Hide();
            itemComboBox7.Hide();
            weightTextBox7.Hide();
            countTextBox7.Hide();
            unitPriceTextBox7.Hide();
            moneyTextBox7.Hide();
            remarks7.Hide();
            mainCategoryComboBox07.Hide();
            itemComboBox07.Hide();
            weightTextBox07.Hide();
            countTextBox07.Hide();
            unitPriceTextBox07.Hide();
            moneyTextBox07.Hide();
            remarks07.Hide();

            mainCategoryComboBox8.Hide();
            itemComboBox8.Hide();
            weightTextBox8.Hide();
            countTextBox8.Hide();
            unitPriceTextBox8.Hide();
            moneyTextBox8.Hide();
            remarks8.Hide();
            mainCategoryComboBox08.Hide();
            itemComboBox08.Hide();
            weightTextBox08.Hide();
            countTextBox08.Hide();
            unitPriceTextBox08.Hide();
            moneyTextBox08.Hide();
            remarks08.Hide();

            mainCategoryComboBox9.Hide();
            itemComboBox9.Hide();
            weightTextBox9.Hide();
            countTextBox9.Hide();
            unitPriceTextBox9.Hide();
            moneyTextBox9.Hide();
            remarks9.Hide();
            mainCategoryComboBox09.Hide();
            itemComboBox09.Hide();
            weightTextBox09.Hide();
            countTextBox09.Hide();
            unitPriceTextBox09.Hide();
            moneyTextBox09.Hide();
            remarks09.Hide();

            mainCategoryComboBox10.Hide();
            itemComboBox10.Hide();
            weightTextBox10.Hide();
            countTextBox10.Hide();
            unitPriceTextBox10.Hide();
            moneyTextBox10.Hide();
            remarks10.Hide();
            mainCategoryComboBox010.Hide();
            itemComboBox010.Hide();
            weightTextBox010.Hide();
            countTextBox010.Hide();
            unitPriceTextBox010.Hide();
            moneyTextBox010.Hide();
            remarks010.Hide();

            mainCategoryComboBox11.Hide();
            itemComboBox11.Hide();
            weightTextBox11.Hide();
            countTextBox11.Hide();
            unitPriceTextBox11.Hide();
            moneyTextBox11.Hide();
            remarks11.Hide();
            mainCategoryComboBox011.Hide();
            itemComboBox011.Hide();
            weightTextBox011.Hide();
            countTextBox011.Hide();
            unitPriceTextBox011.Hide();
            moneyTextBox011.Hide();
            remarks011.Hide();

            mainCategoryComboBox12.Hide();
            itemComboBox12.Hide();
            weightTextBox12.Hide();
            countTextBox12.Hide();
            unitPriceTextBox12.Hide();
            moneyTextBox12.Hide();
            remarks12.Hide();
            mainCategoryComboBox012.Hide();
            itemComboBox012.Hide();
            weightTextBox012.Hide();
            countTextBox012.Hide();
            unitPriceTextBox012.Hide();
            moneyTextBox012.Hide();
            remarks012.Hide();
            #endregion

            groupBox1.Hide();//200万以上の取引情報

            //左の入力項目処理
            string itemDisplay = "item_name";
            string itemValue = "item_code";
            #region "計算書　左の項目"
            //地金
            DataTable dt300 = new DataTable();
            string str_sql_metal = "select* from item_m where main_category_code = 100";
            adapter = new NpgsqlDataAdapter(str_sql_metal, conn);
            adapter.Fill(dt300);
            //1行目
            
            metalComboBox0.DataSource = dt300;
            metalComboBox0.DisplayMember = itemDisplay;
            metalComboBox0.ValueMember = itemValue; ;
            metalComboBox0.SelectedIndex = -1;

            //2行目
            DataTable dt301 = new DataTable();
            dt301 = dt300.Copy();
            metalComboBox1.DataSource = dt301;
            metalComboBox1.DisplayMember = itemDisplay;
            metalComboBox1.ValueMember = itemValue ;
            metalComboBox1.SelectedIndex = -1;

            //3行目
            DataTable dt302 = new DataTable();
            dt302 = dt300.Copy();
            metalComboBox2.DataSource = dt302;
            metalComboBox2.DisplayMember = itemDisplay;
            metalComboBox2.ValueMember = itemValue;
            metalComboBox2.SelectedIndex = -1;

            //4行目
            DataTable dt303 = new DataTable();
            dt303 = dt300.Copy();
            metalComboBox3.DataSource = dt303;
            metalComboBox3.DisplayMember = itemDisplay;
            metalComboBox3.ValueMember = itemValue;
            metalComboBox3.SelectedIndex = -1;

            //5行目
            DataTable dt304 = new DataTable();
            dt304 = dt300.Copy();
            metalComboBox4.DataSource = dt304;
            metalComboBox4.DisplayMember = itemDisplay;
            metalComboBox4.ValueMember = itemValue;
            metalComboBox4.SelectedIndex = -1;


            //ダイヤ
            DataTable dt400 = new DataTable();
            string str_sql_diamond = "select* from item_m where main_category_code = 101";
            
            adapter = new NpgsqlDataAdapter(str_sql_diamond, conn);
            adapter.Fill(dt400);
            //1行目
            diamondComboBox0.DataSource = dt400;
            diamondComboBox0.DisplayMember = itemDisplay;
            diamondComboBox0.ValueMember = itemValue;
            diamondComboBox0.SelectedIndex = -1;

            //2行目
            DataTable dt401 = new DataTable();
            dt401 = dt400.Copy();
            diamondComboBox1.DataSource = dt401;
            diamondComboBox1.DisplayMember = itemDisplay;
            diamondComboBox1.ValueMember = itemValue;
            diamondComboBox1.SelectedIndex = -1;

            //3行目
            DataTable dt402 = new DataTable();
            dt402 = dt400.Copy();
            diamondComboBox2.DataSource = dt402;
            diamondComboBox2.DisplayMember = itemDisplay;
            diamondComboBox2.ValueMember = itemValue;
            diamondComboBox2.SelectedIndex = -1;

            //4行目
            DataTable dt403 = new DataTable();
            dt403 = dt400.Copy();
            diamondComboBox3.DataSource = dt403;
            diamondComboBox3.DisplayMember = itemDisplay;
            diamondComboBox3.ValueMember = itemValue;
            diamondComboBox3.SelectedIndex = -1;

            //5行目
            DataTable dt404 = new DataTable();
            dt404 = dt400.Copy();
            diamondComboBox4.DataSource = dt404;
            diamondComboBox4.DisplayMember = itemDisplay;
            diamondComboBox4.ValueMember = itemValue;
            diamondComboBox4.SelectedIndex = -1;

            //ブランド
            DataTable dt500 = new DataTable();
            string str_sql_brand = "select* from item_m where main_category_code = 102";
            adapter = new NpgsqlDataAdapter(str_sql_brand, conn);
            adapter.Fill(dt500);

            //1行目
            brandComboBox0.DataSource = dt500;
            brandComboBox0.DisplayMember = itemDisplay;
            brandComboBox0.ValueMember = itemValue;
            brandComboBox0.SelectedIndex = -1;

            //2行目
            DataTable dt501 = new DataTable();
            dt501 = dt500.Copy();
            brandComboBox1.DataSource = dt501;
            brandComboBox1.DisplayMember = itemDisplay;
            brandComboBox1.ValueMember = itemValue;
            brandComboBox1.SelectedIndex = -1;

            //3行目
            DataTable dt502 = new DataTable();
            dt502 = dt500.Copy();
            brandComboBox2.DataSource = dt502;
            brandComboBox2.DisplayMember = itemDisplay;
            brandComboBox2.ValueMember = itemValue;
            brandComboBox2.SelectedIndex = -1;

            //4行目
            DataTable dt503 = new DataTable();
            dt503 = dt500.Copy();
            brandComboBox3.DataSource = dt503;
            brandComboBox3.DisplayMember = itemDisplay;
            brandComboBox3.ValueMember = itemValue;
            brandComboBox3.SelectedIndex = -1;

            //5行目
            DataTable dt504 = new DataTable();
            dt504 = dt500.Copy();
            brandComboBox4.DataSource = dt504;
            brandComboBox4.DisplayMember = itemDisplay;
            brandComboBox4.ValueMember = itemValue;
            brandComboBox4.SelectedIndex = -1;

            //製品・ジュエリー
            DataTable dt600 = new DataTable();
            string str_sql_jewelry = "select* from item_m where main_category_code = 103";
            adapter = new NpgsqlDataAdapter(str_sql_jewelry, conn);
            adapter.Fill(dt600);

            //1行目
            jewelryComboBox0.DataSource = dt600;
            jewelryComboBox0.DisplayMember = itemDisplay;
            jewelryComboBox0.ValueMember = itemValue;
            jewelryComboBox0.SelectedIndex = -1;

            //2行目
            DataTable dt601 = new DataTable();
            dt601 = dt600.Copy();
            jewelryComboBox1.DataSource = dt601;
            jewelryComboBox1.DisplayMember = itemDisplay;
            jewelryComboBox1.ValueMember = itemValue;
            jewelryComboBox1.SelectedIndex = -1;

            //3行目
            DataTable dt602 = new DataTable();
            dt602 = dt600.Copy();
            jewelryComboBox2.DataSource = dt602;
            jewelryComboBox2.DisplayMember = itemDisplay;
            jewelryComboBox2.ValueMember = itemValue;
            jewelryComboBox2.SelectedIndex = -1;

            //4行目
            DataTable dt603 = new DataTable();
            dt603 = dt600.Copy();
            jewelryComboBox3.DataSource = dt603;
            jewelryComboBox3.DisplayMember = itemDisplay;
            jewelryComboBox3.ValueMember = itemValue;
            jewelryComboBox3.SelectedIndex = -1;

            //5行目
            DataTable dt604 = new DataTable();
            dt604 = dt600.Copy();
            jewelryComboBox4.DataSource = dt604;
            jewelryComboBox4.DisplayMember = itemDisplay;
            jewelryComboBox4.ValueMember = itemValue;
            jewelryComboBox4.SelectedIndex = -1;

            //その他
            DataTable dt700 = new DataTable();
            string str_sql_other = "select* from item_m where main_category_code = 104";
            adapter = new NpgsqlDataAdapter(str_sql_other, conn);
            adapter.Fill(dt700);

            //1行目
            otherComboBox0.DataSource = dt700;
            otherComboBox0.DisplayMember = itemDisplay;
            otherComboBox0.ValueMember = itemValue;
            otherComboBox0.SelectedIndex = -1;

            //2行目
            DataTable dt701 = new DataTable();
            dt701 = dt700.Copy();
            otherComboBox1.DataSource = dt701;
            otherComboBox1.DisplayMember = itemDisplay;
            otherComboBox1.ValueMember = itemValue;
            otherComboBox1.SelectedIndex = -1;

            //3行目
            DataTable dt702 = new DataTable();
            dt702 = dt700.Copy();
            otherComboBox2.DataSource = dt702;
            otherComboBox2.DisplayMember = itemDisplay;
            otherComboBox2.ValueMember = itemValue;
            otherComboBox2.SelectedIndex = -1;

            //4行目
            DataTable dt703 = new DataTable();
            dt703 = dt700.Copy();
            otherComboBox3.DataSource = dt703;
            otherComboBox3.DisplayMember = itemDisplay;
            otherComboBox3.ValueMember = itemValue;
            otherComboBox3.SelectedIndex = -1;

            //5行目
            DataTable dt704 = new DataTable();
            dt704 = dt700.Copy();
            otherComboBox4.DataSource = dt704;
            otherComboBox4.DisplayMember = itemDisplay;
            otherComboBox4.ValueMember = itemValue;
            otherComboBox4.SelectedIndex = -1;
            #endregion
            #region "納品書　左の項目"
            //地金
            //1行目
            DataTable deliverydt300 = new DataTable();
            deliverydt300 = dt300.Copy();
            metalComboBox00.DataSource = deliverydt300;
            metalComboBox00.DisplayMember = itemDisplay;
            metalComboBox00.ValueMember = itemValue; ;
            metalComboBox00.SelectedIndex = -1;

            //2行目
            DataTable deliverydt301 = new DataTable();
            deliverydt301 = dt300.Copy();
            metalComboBox01.DataSource = deliverydt301;
            metalComboBox01.DisplayMember = itemDisplay;
            metalComboBox01.ValueMember = itemValue;
            metalComboBox01.SelectedIndex = -1;

            //3行目
            DataTable deliverydt302 = new DataTable();
            deliverydt302 = dt300.Copy();
            metalComboBox02.DataSource = deliverydt302;
            metalComboBox02.DisplayMember = itemDisplay;
            metalComboBox02.ValueMember = itemValue;
            metalComboBox02.SelectedIndex = -1;

            //4行目
            DataTable deliverydt303 = new DataTable();
            deliverydt303 = dt300.Copy();
            metalComboBox03.DataSource = deliverydt303;
            metalComboBox03.DisplayMember = itemDisplay;
            metalComboBox03.ValueMember = itemValue;
            metalComboBox03.SelectedIndex = -1;

            //5行目
            DataTable deliverydt304 = new DataTable();
            deliverydt304 = dt300.Copy();
            metalComboBox04.DataSource = deliverydt304;
            metalComboBox04.DisplayMember = itemDisplay;
            metalComboBox04.ValueMember = itemValue;
            metalComboBox04.SelectedIndex = -1;


            //ダイヤ
            //1行目
            DataTable deliverydt400 = new DataTable();
            deliverydt400 = dt400.Copy();
            diamondComboBox00.DataSource = deliverydt400;
            diamondComboBox00.DisplayMember = itemDisplay;
            diamondComboBox00.ValueMember = itemValue;
            diamondComboBox00.SelectedIndex = -1;

            //2行目
            DataTable deliverydt401 = new DataTable();
            deliverydt401 = dt400.Copy();
            diamondComboBox01.DataSource = deliverydt401;
            diamondComboBox01.DisplayMember = itemDisplay;
            diamondComboBox01.ValueMember = itemValue;
            diamondComboBox01.SelectedIndex = -1;

            //3行目
            DataTable deliverydt402 = new DataTable();
            deliverydt402 = dt400.Copy();
            diamondComboBox02.DataSource = deliverydt402;
            diamondComboBox02.DisplayMember = itemDisplay;
            diamondComboBox02.ValueMember = itemValue;
            diamondComboBox02.SelectedIndex = -1;

            //4行目
            DataTable deliverydt403 = new DataTable();
            deliverydt403 = dt400.Copy();
            diamondComboBox03.DataSource = deliverydt403;
            diamondComboBox03.DisplayMember = itemDisplay;
            diamondComboBox03.ValueMember = itemValue;
            diamondComboBox03.SelectedIndex = -1;

            //5行目
            DataTable deliverydt404 = new DataTable();
            deliverydt404 = dt400.Copy();
            diamondComboBox04.DataSource = deliverydt404;
            diamondComboBox04.DisplayMember = itemDisplay;
            diamondComboBox04.ValueMember = itemValue;
            diamondComboBox04.SelectedIndex = -1;

            //ブランド
            //1行目
            DataTable deliverydt500 = new DataTable();
            deliverydt500 = dt500.Copy();
            brandComboBox00.DataSource = deliverydt500;
            brandComboBox00.DisplayMember = itemDisplay;
            brandComboBox00.ValueMember = itemValue;
            brandComboBox00.SelectedIndex = -1;

            //2行目
            DataTable deliverydt501 = new DataTable();
            deliverydt501 = dt500.Copy();
            brandComboBox01.DataSource = deliverydt501;
            brandComboBox01.DisplayMember = itemDisplay;
            brandComboBox01.ValueMember = itemValue;
            brandComboBox01.SelectedIndex = -1;

            //3行目
            DataTable deliverydt502 = new DataTable();
            deliverydt502 = dt500.Copy();
            brandComboBox02.DataSource = deliverydt502;
            brandComboBox02.DisplayMember = itemDisplay;
            brandComboBox02.ValueMember = itemValue;
            brandComboBox02.SelectedIndex = -1;

            //4行目
            DataTable deliverydt503 = new DataTable();
            deliverydt503 = dt500.Copy();
            brandComboBox03.DataSource = deliverydt503;
            brandComboBox03.DisplayMember = itemDisplay;
            brandComboBox03.ValueMember = itemValue;
            brandComboBox03.SelectedIndex = -1;

            //5行目
            DataTable deliverydt504 = new DataTable();
            deliverydt504 = dt500.Copy();
            brandComboBox04.DataSource = deliverydt504;
            brandComboBox04.DisplayMember = itemDisplay;
            brandComboBox04.ValueMember = itemValue;
            brandComboBox04.SelectedIndex = -1;

            //製品・ジュエリー
            //1行目
            DataTable deliverydt600 = new DataTable();
            deliverydt600 = dt600.Copy();
            jewelryComboBox00.DataSource = deliverydt600;
            jewelryComboBox00.DisplayMember = itemDisplay;
            jewelryComboBox00.ValueMember = itemValue;
            jewelryComboBox00.SelectedIndex = -1;

            //2行目
            DataTable deliverydt601 = new DataTable();
            deliverydt601 = dt600.Copy();
            jewelryComboBox01.DataSource = deliverydt601;
            jewelryComboBox01.DisplayMember = itemDisplay;
            jewelryComboBox01.ValueMember = itemValue;
            jewelryComboBox01.SelectedIndex = -1;

            //3行目
            DataTable deliverydt602 = new DataTable();
            deliverydt602 = dt600.Copy();
            jewelryComboBox02.DataSource = deliverydt602;
            jewelryComboBox02.DisplayMember = itemDisplay;
            jewelryComboBox02.ValueMember = itemValue;
            jewelryComboBox02.SelectedIndex = -1;

            //4行目
            DataTable deliverydt603 = new DataTable();
            deliverydt603 = dt600.Copy();
            jewelryComboBox03.DataSource = deliverydt603;
            jewelryComboBox03.DisplayMember = itemDisplay;
            jewelryComboBox03.ValueMember = itemValue;
            jewelryComboBox03.SelectedIndex = -1;

            //5行目
            DataTable deliverydt604 = new DataTable();
            deliverydt604 = dt600.Copy();
            jewelryComboBox04.DataSource = deliverydt604;
            jewelryComboBox04.DisplayMember = itemDisplay;
            jewelryComboBox04.ValueMember = itemValue;
            jewelryComboBox04.SelectedIndex = -1;

            //その他
            //1行目
            DataTable deliverydt700 = new DataTable();
            deliverydt700 = dt700.Copy();
            otherComboBox00.DataSource = deliverydt700;
            otherComboBox00.DisplayMember = itemDisplay;
            otherComboBox00.ValueMember = itemValue;
            otherComboBox00.SelectedIndex = -1;

            //2行目
            DataTable deliverydt701 = new DataTable();
            deliverydt701 = dt700.Copy();
            otherComboBox01.DataSource = deliverydt701;
            otherComboBox01.DisplayMember = itemDisplay;
            otherComboBox01.ValueMember = itemValue;
            otherComboBox01.SelectedIndex = -1;

            //3行目
            DataTable deliverydt702 = new DataTable();
            deliverydt702 = dt700.Copy();
            otherComboBox02.DataSource = deliverydt702;
            otherComboBox02.DisplayMember = itemDisplay;
            otherComboBox02.ValueMember = itemValue;
            otherComboBox02.SelectedIndex = -1;

            //4行目
            DataTable deliverydt703 = new DataTable();
            deliverydt703 = dt700.Copy();
            otherComboBox03.DataSource = deliverydt703;
            otherComboBox03.DisplayMember = itemDisplay;
            otherComboBox03.ValueMember = itemValue;
            otherComboBox03.SelectedIndex = -1;

            //5行目
            DataTable deliverydt704 = new DataTable();
            deliverydt704 = dt700.Copy();
            otherComboBox04.DataSource = deliverydt704;
            otherComboBox04.DisplayMember = itemDisplay;
            otherComboBox04.ValueMember = itemValue;
            otherComboBox04.SelectedIndex = -1;
            #endregion
        }


        private void button1_Click(object sender, EventArgs e) //200万以上取引時の表示
        {
            groupBox1.Show();
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

        private void button4_Click(object sender, EventArgs e)//ファイルを選択
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if(dr == System.Windows.Forms.DialogResult.OK)
            {
                articlesTextBox.Text = openFileDialog1.FileName;
            }
        }
        #region "計算書　大分類変更"
        private void mainCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e) //大分類選択分岐
        {
            

             //大分類によって品名変更　1行目
            if (a > 1)
            {
                

                int codeNum = (int)mainCategoryComboBox0.SelectedValue;
                dt2.Clear();
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt2);
                itemComboBox0.DataSource = dt2;
                itemComboBox0.DisplayMember = "item_name";
                itemComboBox0.ValueMember = "item_code";

                conn.Close();
            }
            else
            {
                
            }
            
        }

        private void mainCategoryComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 2行目
            if (a > 1)
            {


                int codeNum = (int)mainCategoryComboBox1.SelectedValue;
                dt200.Clear();
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt200);
                itemComboBox1.DataSource = dt200;
                itemComboBox1.DisplayMember = "item_name";
                itemComboBox1.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt201);
                itemComboBox2.DataSource = dt201;
                itemComboBox2.DisplayMember = "item_name";
                itemComboBox2.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt202);
                itemComboBox3.DataSource = dt202;
                itemComboBox3.DisplayMember = "item_name";
                itemComboBox3.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt203);
                itemComboBox4.DataSource = dt203;
                itemComboBox4.DisplayMember = "item_name";
                itemComboBox4.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt204);
                itemComboBox5.DataSource = dt204;
                itemComboBox5.DisplayMember = "item_name";
                itemComboBox5.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt205);
                itemComboBox6.DataSource = dt205;
                itemComboBox6.DisplayMember = "item_name";
                itemComboBox6.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt206);
                itemComboBox7.DataSource = dt206;
                itemComboBox7.DisplayMember = "item_name";
                itemComboBox7.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt207);
                itemComboBox8.DataSource = dt207;
                itemComboBox8.DisplayMember = "item_name";
                itemComboBox8.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt208);
                itemComboBox9.DataSource = dt208;
                itemComboBox9.DisplayMember = "item_name";
                itemComboBox9.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt209);
                itemComboBox10.DataSource = dt209;
                itemComboBox10.DisplayMember = "item_name";
                itemComboBox10.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt210);
                itemComboBox11.DataSource = dt210;
                itemComboBox11.DisplayMember = "item_name";
                itemComboBox11.ValueMember = "item_code";

                conn.Close();
            }
            else
            {
                
            }
        }

        private void mainCategoryComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            //大分類によって品名変更 12行目
            if (a > 1)
            {


                int codeNum = (int)mainCategoryComboBox12.SelectedValue;
                dt211.Clear();
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(dt211);
                itemComboBox12.DataSource = dt211;
                itemComboBox12.DisplayMember = "item_name";
                itemComboBox12.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt200);
                itemComboBox00.DataSource = deliverydt200;
                itemComboBox00.DisplayMember = "item_name";
                itemComboBox00.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt201);
                itemComboBox01.DataSource = deliverydt201;
                itemComboBox01.DisplayMember = "item_name";
                itemComboBox01.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt202);
                itemComboBox02.DataSource = deliverydt202;
                itemComboBox02.DisplayMember = "item_name";
                itemComboBox02.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt203);
                itemComboBox03.DataSource = deliverydt203;
                itemComboBox03.DisplayMember = "item_name";
                itemComboBox03.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt204);
                itemComboBox04.DataSource = deliverydt204;
                itemComboBox04.DisplayMember = "item_name";
                itemComboBox04.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt205);
                itemComboBox05.DataSource = deliverydt205;
                itemComboBox05.DisplayMember = "item_name";
                itemComboBox05.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt206);
                itemComboBox06.DataSource = deliverydt206;
                itemComboBox06.DisplayMember = "item_name";
                itemComboBox06.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt207);
                itemComboBox07.DataSource = deliverydt207;
                itemComboBox07.DisplayMember = "item_name";
                itemComboBox07.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt208);
                itemComboBox08.DataSource = deliverydt208;
                itemComboBox08.DisplayMember = "item_name";
                itemComboBox08.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt209);
                itemComboBox09.DataSource = deliverydt209;
                itemComboBox09.DisplayMember = "item_name";
                itemComboBox09.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt210);
                itemComboBox010.DataSource = deliverydt210;
                itemComboBox010.DisplayMember = "item_name";
                itemComboBox010.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt211);
                itemComboBox011.DataSource = deliverydt211;
                itemComboBox011.DisplayMember = "item_name";
                itemComboBox011.ValueMember = "item_code";

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + codeNum + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(deliverydt212);
                itemComboBox012.DataSource = deliverydt212;
                itemComboBox012.DisplayMember = "item_name";
                itemComboBox012.ValueMember = "item_code";

                conn.Close();
            }
            else
            {
                b++;
            }
        }
        #endregion
        #region "計算書　計算ボタン"
        private void calcButton_Click(object sender, EventArgs e)//計算ボタン
        {
            decimal sub;
            #region "計算開始"
            //計算開始
            for (int i = 0; i <= con; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(weightTextBox0.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox0.Text) && !string.IsNullOrEmpty(unitPriceTextBox0.Text))
                            {
                                sub = decimal.Parse(weightTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
                                moneyTextBox0.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox0.Text) && !string.IsNullOrEmpty(unitPriceTextBox0.Text))
                            {
                                sub = decimal.Parse(countTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
                                moneyTextBox0.Text = sub.ToString();
                            }
                        }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(weightTextBox1.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox1.Text) && !string.IsNullOrEmpty(unitPriceTextBox1.Text))
                            {
                                sub = decimal.Parse(weightTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
                                moneyTextBox1.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox1.Text) && !string.IsNullOrEmpty(unitPriceTextBox1.Text))
                            {
                                sub = decimal.Parse(countTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
                                moneyTextBox1.Text = sub.ToString();
                            }
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(weightTextBox2.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox2.Text) && !string.IsNullOrEmpty(unitPriceTextBox2.Text))
                            {
                                sub = decimal.Parse(weightTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
                                moneyTextBox2.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox2.Text) && !string.IsNullOrEmpty(unitPriceTextBox2.Text))
                            {
                                sub = decimal.Parse(countTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
                                moneyTextBox2.Text = sub.ToString();
                            }
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(weightTextBox3.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox3.Text) && !string.IsNullOrEmpty(unitPriceTextBox3.Text))
                            {
                                sub = decimal.Parse(weightTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
                                moneyTextBox3.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox3.Text) && !string.IsNullOrEmpty(unitPriceTextBox3.Text))
                            {
                                sub = decimal.Parse(countTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
                                moneyTextBox3.Text = sub.ToString();
                            }
                        }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(weightTextBox4.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox4.Text) && !string.IsNullOrEmpty(unitPriceTextBox4.Text))
                            {
                                sub = decimal.Parse(weightTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
                                moneyTextBox4.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox4.Text) && !string.IsNullOrEmpty(unitPriceTextBox4.Text))
                            {
                                sub = decimal.Parse(countTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
                                moneyTextBox4.Text = sub.ToString();
                            }
                        }
                        break;
                    case 5:
                        if (!string.IsNullOrEmpty(weightTextBox5.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox5.Text) && !string.IsNullOrEmpty(unitPriceTextBox5.Text))
                            {
                                sub = decimal.Parse(weightTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
                                moneyTextBox5.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox5.Text) && !string.IsNullOrEmpty(unitPriceTextBox5.Text))
                            {
                                sub = decimal.Parse(countTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
                                moneyTextBox5.Text = sub.ToString();
                            }
                        }
                        break;
                    case 6:
                        if (!string.IsNullOrEmpty(weightTextBox6.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox6.Text) && !string.IsNullOrEmpty(unitPriceTextBox6.Text))
                            {
                                sub = decimal.Parse(weightTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
                                moneyTextBox6.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox6.Text) && !string.IsNullOrEmpty(unitPriceTextBox6.Text))
                            {
                                sub = decimal.Parse(countTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
                                moneyTextBox6.Text = sub.ToString();
                            }
                        }
                        break;
                    case 7:
                        if (!string.IsNullOrEmpty(weightTextBox7.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox7.Text) && !string.IsNullOrEmpty(unitPriceTextBox7.Text))
                            {
                                sub = decimal.Parse(weightTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
                                moneyTextBox7.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox7.Text) && !string.IsNullOrEmpty(unitPriceTextBox7.Text))
                            {
                                sub = decimal.Parse(countTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
                                moneyTextBox7.Text = sub.ToString();
                            }
                        }
                        break;
                    case 8:
                        if (!string.IsNullOrEmpty(weightTextBox8.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox8.Text) && !string.IsNullOrEmpty(unitPriceTextBox8.Text))
                            {
                                sub = decimal.Parse(weightTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
                                moneyTextBox8.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox8.Text) && !string.IsNullOrEmpty(unitPriceTextBox8.Text))
                            {
                                sub = decimal.Parse(countTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
                                moneyTextBox8.Text = sub.ToString();
                            }
                        }
                        break;
                    case 9:
                        if (!string.IsNullOrEmpty(weightTextBox9.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox9.Text) && !string.IsNullOrEmpty(unitPriceTextBox9.Text))
                            {
                                sub = decimal.Parse(weightTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
                                moneyTextBox9.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox9.Text) && !string.IsNullOrEmpty(unitPriceTextBox9.Text))
                            {
                                sub = decimal.Parse(countTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
                                moneyTextBox9.Text = sub.ToString();
                            }
                        }
                        break;
                    case 10:
                        if (!string.IsNullOrEmpty(weightTextBox10.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox10.Text) && !string.IsNullOrEmpty(unitPriceTextBox10.Text))
                            {
                                sub = decimal.Parse(weightTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
                                moneyTextBox10.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox10.Text) && !string.IsNullOrEmpty(unitPriceTextBox10.Text))
                            {
                                sub = decimal.Parse(countTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
                                moneyTextBox10.Text = sub.ToString();
                            }
                        }
                        break;
                    case 11:
                        if (!string.IsNullOrEmpty(weightTextBox11.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox11.Text) && !string.IsNullOrEmpty(unitPriceTextBox11.Text))
                            {
                                sub = decimal.Parse(weightTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
                                moneyTextBox11.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox11.Text) && !string.IsNullOrEmpty(unitPriceTextBox11.Text))
                            {
                                sub = decimal.Parse(countTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
                                moneyTextBox11.Text = sub.ToString();
                            }
                        }
                        break;
                    case 12:
                        if (!string.IsNullOrEmpty(weightTextBox12.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox12.Text) && !string.IsNullOrEmpty(unitPriceTextBox12.Text))
                            {
                                sub = decimal.Parse(weightTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
                                moneyTextBox12.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox12.Text) && !string.IsNullOrEmpty(unitPriceTextBox12.Text))
                            {
                                sub = decimal.Parse(countTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
                                moneyTextBox12.Text = sub.ToString();
                            }
                        }
                        break;
                    default:
                        break;
                }//計算終了
            }
            #endregion
            #region"空欄確認"
            //空欄確認
            for (int i = 0; i <= 14; i++)
            {
                switch (i)
                {
                    case 0:
                        if (string.IsNullOrEmpty(moneyTextBox1.Text))
                        {
                            moneyTextBox1.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(moneyTextBox2.Text))
                        {
                            moneyTextBox2.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(moneyTextBox3.Text))
                        {
                            moneyTextBox3.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 3:
                        if (string.IsNullOrEmpty(moneyTextBox4.Text))
                        {
                            moneyTextBox4.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 4:
                        if (string.IsNullOrEmpty(moneyTextBox5.Text))
                        {
                            moneyTextBox5.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 5:
                        if (string.IsNullOrEmpty(moneyTextBox6.Text))
                        {
                            moneyTextBox6.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 6:
                        if (string.IsNullOrEmpty(moneyTextBox7.Text))
                        {
                            moneyTextBox7.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 7:
                        if(string.IsNullOrEmpty(moneyTextBox8.Text))
                        {
                            moneyTextBox8.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 8:
                        if (string.IsNullOrEmpty(moneyTextBox9.Text))
                        {
                            moneyTextBox9.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 9:
                        if (string.IsNullOrEmpty(moneyTextBox10.Text))
                        {
                            moneyTextBox10.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 10:
                        if (string.IsNullOrEmpty(moneyTextBox11.Text))
                        {
                            moneyTextBox11.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 11:
                        if (string.IsNullOrEmpty(moneyTextBox12.Text))
                        {
                            moneyTextBox12.Text = 0.ToString();
                        }
                        else { }
                        break;
                    default:
                        if (string.IsNullOrEmpty(moneyTextBox0.Text))
                        {
                            moneyTextBox0.Text = 0.ToString();
                        }
                        else { }
                        break;
                }
            }
            #endregion
            #region "重量空欄確認"
            //重量空欄確認
            for (int i = 0; i <= 14; i++)
            {
                switch (i)
                {
                    case 0:
                        if (string.IsNullOrEmpty(weightTextBox1.Text))
                        {
                            weightTextBox1.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(weightTextBox2.Text))
                        {
                            weightTextBox2.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(weightTextBox3.Text))
                        {
                            weightTextBox3.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 3:
                        if (string.IsNullOrEmpty(weightTextBox4.Text))
                        {
                            weightTextBox4.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 4:
                        if (string.IsNullOrEmpty(weightTextBox5.Text))
                        {
                            weightTextBox5.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 5:
                        if (string.IsNullOrEmpty(weightTextBox6.Text))
                        {
                            weightTextBox6.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 6:
                        if (string.IsNullOrEmpty(weightTextBox7.Text))
                        {
                            weightTextBox7.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 7:
                        if (string.IsNullOrEmpty(weightTextBox8.Text))
                        {
                            weightTextBox8.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 8:
                        if (string.IsNullOrEmpty(weightTextBox9.Text))
                        {
                            weightTextBox9.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 9:
                        if (string.IsNullOrEmpty(weightTextBox10.Text))
                        {
                            weightTextBox10.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 10:
                        if (string.IsNullOrEmpty(weightTextBox11.Text))
                        {
                            weightTextBox11.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 11:
                        if (string.IsNullOrEmpty(weightTextBox12.Text))
                        {
                            weightTextBox12.Text = 0.ToString();
                        }
                        else { }
                        break;
                    default:
                        if (string.IsNullOrEmpty(weightTextBox0.Text))
                        {
                            weightTextBox0.Text = 0.ToString();
                        }
                        else { }
                        break;
                }


            }
            #endregion
            #region"総数量確認"
            //総数量確認
            for (int i = 0; i <= 14; i++)
            {
                switch (i)
                {
                    case 0:
                        if (string.IsNullOrEmpty(countTextBox1.Text))
                        {
                            countTextBox1.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(countTextBox2.Text))
                        {
                            countTextBox2.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(countTextBox3.Text))
                        {
                            countTextBox3.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 3:
                        if (string.IsNullOrEmpty(countTextBox4.Text))
                        {
                            countTextBox4.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 4:
                        if (string.IsNullOrEmpty(countTextBox5.Text))
                        {
                            countTextBox5.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 5:
                        if (string.IsNullOrEmpty(countTextBox6.Text))
                        {
                            countTextBox6.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 6:
                        if (string.IsNullOrEmpty(countTextBox7.Text))
                        {
                            countTextBox7.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 7:
                        if (string.IsNullOrEmpty(countTextBox8.Text))
                        {
                            countTextBox8.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 8:
                        if (string.IsNullOrEmpty(countTextBox9.Text))
                        {
                            countTextBox9.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 9:
                        if (string.IsNullOrEmpty(countTextBox10.Text))
                        {
                            countTextBox10.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 10:
                        if (string.IsNullOrEmpty(countTextBox11.Text))
                        {
                            countTextBox11.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 11:
                        if (string.IsNullOrEmpty(countTextBox12.Text))
                        {
                            countTextBox12.Text = 0.ToString();
                        }
                        else { }
                        break;
                    default:
                        if (string.IsNullOrEmpty(countTextBox0.Text))
                        {
                            countTextBox0.Text = 0.ToString();
                        }
                        else { }
                        break;
                }


            }
            #endregion
            #region "総量"
            //総重量計算
            decimal weisum = decimal.Parse(weightTextBox0.Text) + decimal.Parse(weightTextBox1.Text) + decimal.Parse(weightTextBox2.Text) + decimal.Parse(weightTextBox3.Text) + decimal.Parse(weightTextBox4.Text)
                        + decimal.Parse(weightTextBox5.Text) + decimal.Parse(weightTextBox6.Text) + decimal.Parse(weightTextBox7.Text) + decimal.Parse(weightTextBox8.Text) + decimal.Parse(weightTextBox9.Text)
                        + decimal.Parse(weightTextBox10.Text) + decimal.Parse(weightTextBox11.Text) + decimal.Parse(weightTextBox12.Text);

            //総数量計算
            decimal countsum = decimal.Parse(countTextBox0.Text) + decimal.Parse(countTextBox1.Text) + decimal.Parse(countTextBox2.Text) + decimal.Parse(countTextBox3.Text) + decimal.Parse(countTextBox4.Text)
                        + decimal.Parse(countTextBox5.Text) + decimal.Parse(countTextBox6.Text) + decimal.Parse(countTextBox7.Text) + decimal.Parse(countTextBox8.Text) + decimal.Parse(countTextBox9.Text)
                        + decimal.Parse(countTextBox10.Text) + decimal.Parse(countTextBox11.Text) + decimal.Parse(countTextBox12.Text);

            //小計計算
            decimal subSum = decimal.Parse(moneyTextBox0.Text) + decimal.Parse(moneyTextBox1.Text) + decimal.Parse(moneyTextBox2.Text) + decimal.Parse(moneyTextBox3.Text) + decimal.Parse(moneyTextBox4.Text)
                        + decimal.Parse(moneyTextBox5.Text) + decimal.Parse(moneyTextBox6.Text) + decimal.Parse(moneyTextBox7.Text) + decimal.Parse(moneyTextBox8.Text) + decimal.Parse(moneyTextBox9.Text)
                        + decimal.Parse(moneyTextBox10.Text) + decimal.Parse(moneyTextBox11.Text) + decimal.Parse(moneyTextBox12.Text);

            //税額計算 DBから取得？
            decimal tax = (decimal)1.10;
            decimal taxA = subSum * tax;

            //合計計算
            decimal sum = subSum + taxA;

            

            subTotal.Text = subSum.ToString() ;
            totalWeight.Text = weisum.ToString();
            totalCount.Text = countsum.ToString();
            taxAmount.Text = taxA.ToString();
            sumTextBox.Text = sum.ToString();
            #endregion
        }
        #endregion
        #region "納品書　計算ボタン"
        private void calc2Button2_Click(object sender, EventArgs e)
        {
            decimal sub;
            #region "計算開始"
            //計算開始
            for (int i = 0; i <= con; i++)
            {
                switch (i)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(weightTextBox00.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox00.Text) && !string.IsNullOrEmpty(unitPriceTextBox00.Text))
                            {
                                sub = decimal.Parse(weightTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
                                moneyTextBox00.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox00.Text) && !string.IsNullOrEmpty(unitPriceTextBox00.Text))
                            {
                                sub = decimal.Parse(countTextBox00.Text) * decimal.Parse(unitPriceTextBox00.Text);
                                moneyTextBox00.Text = sub.ToString();
                            }
                        }
                        break;
                    case 1:
                        if (!string.IsNullOrEmpty(weightTextBox01.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox01.Text) && !string.IsNullOrEmpty(unitPriceTextBox01.Text))
                            {
                                sub = decimal.Parse(weightTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
                                moneyTextBox01.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox01.Text) && !string.IsNullOrEmpty(unitPriceTextBox01.Text))
                            {
                                sub = decimal.Parse(countTextBox01.Text) * decimal.Parse(unitPriceTextBox01.Text);
                                moneyTextBox01.Text = sub.ToString();
                            }
                        }
                        break;
                    case 2:
                        if (!string.IsNullOrEmpty(weightTextBox02.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox02.Text) && !string.IsNullOrEmpty(unitPriceTextBox02.Text))
                            {
                                sub = decimal.Parse(weightTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
                                moneyTextBox02.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox02.Text) && !string.IsNullOrEmpty(unitPriceTextBox02.Text))
                            {
                                sub = decimal.Parse(countTextBox02.Text) * decimal.Parse(unitPriceTextBox02.Text);
                                moneyTextBox02.Text = sub.ToString();
                            }
                        }
                        break;
                    case 3:
                        if (!string.IsNullOrEmpty(weightTextBox03.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox03.Text) && !string.IsNullOrEmpty(unitPriceTextBox03.Text))
                            {
                                sub = decimal.Parse(weightTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
                                moneyTextBox03.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox03.Text) && !string.IsNullOrEmpty(unitPriceTextBox03.Text))
                            {
                                sub = decimal.Parse(countTextBox03.Text) * decimal.Parse(unitPriceTextBox03.Text);
                                moneyTextBox03.Text = sub.ToString();
                            }
                        }
                        break;
                    case 4:
                        if (!string.IsNullOrEmpty(weightTextBox04.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox04.Text) && !string.IsNullOrEmpty(unitPriceTextBox04.Text))
                            {
                                sub = decimal.Parse(weightTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
                                moneyTextBox04.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox04.Text) && !string.IsNullOrEmpty(unitPriceTextBox04.Text))
                            {
                                sub = decimal.Parse(countTextBox04.Text) * decimal.Parse(unitPriceTextBox04.Text);
                                moneyTextBox04.Text = sub.ToString();
                            }
                        }
                        break;
                    case 5:
                        if (!string.IsNullOrEmpty(weightTextBox05.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox05.Text) && !string.IsNullOrEmpty(unitPriceTextBox05.Text))
                            {
                                sub = decimal.Parse(weightTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
                                moneyTextBox05.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox05.Text) && !string.IsNullOrEmpty(unitPriceTextBox05.Text))
                            {
                                sub = decimal.Parse(countTextBox05.Text) * decimal.Parse(unitPriceTextBox05.Text);
                                moneyTextBox05.Text = sub.ToString();
                            }
                        }
                        break;
                    case 6:
                        if (!string.IsNullOrEmpty(weightTextBox06.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox06.Text) && !string.IsNullOrEmpty(unitPriceTextBox06.Text))
                            {
                                sub = decimal.Parse(weightTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
                                moneyTextBox06.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox06.Text) && !string.IsNullOrEmpty(unitPriceTextBox06.Text))
                            {
                                sub = decimal.Parse(countTextBox06.Text) * decimal.Parse(unitPriceTextBox06.Text);
                                moneyTextBox06.Text = sub.ToString();
                            }
                        }
                        break;
                    case 7:
                        if (!string.IsNullOrEmpty(weightTextBox07.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox07.Text) && !string.IsNullOrEmpty(unitPriceTextBox07.Text))
                            {
                                sub = decimal.Parse(weightTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
                                moneyTextBox07.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox07.Text) && !string.IsNullOrEmpty(unitPriceTextBox07.Text))
                            {
                                sub = decimal.Parse(countTextBox07.Text) * decimal.Parse(unitPriceTextBox07.Text);
                                moneyTextBox07.Text = sub.ToString();
                            }
                        }
                        break;
                    case 8:
                        if (!string.IsNullOrEmpty(weightTextBox08.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox08.Text) && !string.IsNullOrEmpty(unitPriceTextBox08.Text))
                            {
                                sub = decimal.Parse(weightTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
                                moneyTextBox08.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox08.Text) && !string.IsNullOrEmpty(unitPriceTextBox08.Text))
                            {
                                sub = decimal.Parse(countTextBox08.Text) * decimal.Parse(unitPriceTextBox08.Text);
                                moneyTextBox08.Text = sub.ToString();
                            }
                        }
                        break;
                    case 9:
                        if (!string.IsNullOrEmpty(weightTextBox09.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox09.Text) && !string.IsNullOrEmpty(unitPriceTextBox09.Text))
                            {
                                sub = decimal.Parse(weightTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
                                moneyTextBox09.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox09.Text) && !string.IsNullOrEmpty(unitPriceTextBox09.Text))
                            {
                                sub = decimal.Parse(countTextBox09.Text) * decimal.Parse(unitPriceTextBox09.Text);
                                moneyTextBox09.Text = sub.ToString();
                            }
                        }
                        break;
                    case 10:
                        if (!string.IsNullOrEmpty(weightTextBox010.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox010.Text) && !string.IsNullOrEmpty(unitPriceTextBox010.Text))
                            {
                                sub = decimal.Parse(weightTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
                                moneyTextBox010.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox010.Text) && !string.IsNullOrEmpty(unitPriceTextBox010.Text))
                            {
                                sub = decimal.Parse(countTextBox010.Text) * decimal.Parse(unitPriceTextBox010.Text);
                                moneyTextBox010.Text = sub.ToString();
                            }
                        }
                        break;
                    case 11:
                        if (!string.IsNullOrEmpty(weightTextBox011.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox011.Text) && !string.IsNullOrEmpty(unitPriceTextBox011.Text))
                            {
                                sub = decimal.Parse(weightTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
                                moneyTextBox011.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox011.Text) && !string.IsNullOrEmpty(unitPriceTextBox011.Text))
                            {
                                sub = decimal.Parse(countTextBox011.Text) * decimal.Parse(unitPriceTextBox011.Text);
                                moneyTextBox11.Text = sub.ToString();
                            }
                        }
                        break;
                    case 12:
                        if (!string.IsNullOrEmpty(weightTextBox012.Text))
                        {
                            if (string.IsNullOrEmpty(countTextBox012.Text) && !string.IsNullOrEmpty(unitPriceTextBox012.Text))
                            {
                                sub = decimal.Parse(weightTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
                                moneyTextBox012.Text = sub.ToString();
                            }
                            else { }
                        }
                        else//数量入力版
                        {
                            if (!string.IsNullOrEmpty(countTextBox012.Text) && !string.IsNullOrEmpty(unitPriceTextBox012.Text))
                            {
                                sub = decimal.Parse(countTextBox012.Text) * decimal.Parse(unitPriceTextBox012.Text);
                                moneyTextBox012.Text = sub.ToString();
                            }
                        }
                        break;
                    default:
                        break;
                }//計算終了
            }
            #endregion
            #region"空欄確認"
            //空欄確認
            for (int i = 0; i <= 14; i++)
            {
                switch (i)
                {
                    case 0:
                        if (string.IsNullOrEmpty(moneyTextBox01.Text))
                        {
                            moneyTextBox01.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(moneyTextBox02.Text))
                        {
                            moneyTextBox02.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(moneyTextBox03.Text))
                        {
                            moneyTextBox03.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 3:
                        if (string.IsNullOrEmpty(moneyTextBox04.Text))
                        {
                            moneyTextBox04.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 4:
                        if (string.IsNullOrEmpty(moneyTextBox05.Text))
                        {
                            moneyTextBox05.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 5:
                        if (string.IsNullOrEmpty(moneyTextBox06.Text))
                        {
                            moneyTextBox06.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 6:
                        if (string.IsNullOrEmpty(moneyTextBox07.Text))
                        {
                            moneyTextBox07.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 7:
                        if (string.IsNullOrEmpty(moneyTextBox08.Text))
                        {
                            moneyTextBox08.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 8:
                        if (string.IsNullOrEmpty(moneyTextBox09.Text))
                        {
                            moneyTextBox09.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 9:
                        if (string.IsNullOrEmpty(moneyTextBox010.Text))
                        {
                            moneyTextBox010.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 10:
                        if (string.IsNullOrEmpty(moneyTextBox011.Text))
                        {
                            moneyTextBox011.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 11:
                        if (string.IsNullOrEmpty(moneyTextBox012.Text))
                        {
                            moneyTextBox012.Text = 0.ToString();
                        }
                        else { }
                        break;
                    default:
                        if (string.IsNullOrEmpty(moneyTextBox00.Text))
                        {
                            moneyTextBox00.Text = 0.ToString();
                        }
                        else { }
                        break;
                }
            }
            #endregion
            #region "重量空欄確認"
            //重量空欄確認
            for (int i = 0; i <= 14; i++)
            {
                switch (i)
                {
                    case 0:
                        if (string.IsNullOrEmpty(weightTextBox01.Text))
                        {
                            weightTextBox01.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(weightTextBox02.Text))
                        {
                            weightTextBox02.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(weightTextBox03.Text))
                        {
                            weightTextBox03.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 3:
                        if (string.IsNullOrEmpty(weightTextBox04.Text))
                        {
                            weightTextBox04.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 4:
                        if (string.IsNullOrEmpty(weightTextBox05.Text))
                        {
                            weightTextBox05.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 5:
                        if (string.IsNullOrEmpty(weightTextBox06.Text))
                        {
                            weightTextBox06.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 6:
                        if (string.IsNullOrEmpty(weightTextBox07.Text))
                        {
                            weightTextBox07.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 7:
                        if (string.IsNullOrEmpty(weightTextBox08.Text))
                        {
                            weightTextBox08.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 8:
                        if (string.IsNullOrEmpty(weightTextBox09.Text))
                        {
                            weightTextBox09.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 9:
                        if (string.IsNullOrEmpty(weightTextBox010.Text))
                        {
                            weightTextBox010.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 10:
                        if (string.IsNullOrEmpty(weightTextBox011.Text))
                        {
                            weightTextBox011.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 11:
                        if (string.IsNullOrEmpty(weightTextBox012.Text))
                        {
                            weightTextBox012.Text = 0.ToString();
                        }
                        else { }
                        break;
                    default:
                        if (string.IsNullOrEmpty(weightTextBox00.Text))
                        {
                            weightTextBox00.Text = 0.ToString();
                        }
                        else { }
                        break;
                }


            }
            #endregion
            #region"総数量確認"
            //総数量確認
            for (int i = 0; i <= 14; i++)
            {
                switch (i)
                {
                    case 0:
                        if (string.IsNullOrEmpty(countTextBox01.Text))
                        {
                            countTextBox01.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 1:
                        if (string.IsNullOrEmpty(countTextBox02.Text))
                        {
                            countTextBox02.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 2:
                        if (string.IsNullOrEmpty(countTextBox03.Text))
                        {
                            countTextBox03.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 3:
                        if (string.IsNullOrEmpty(countTextBox04.Text))
                        {
                            countTextBox04.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 4:
                        if (string.IsNullOrEmpty(countTextBox05.Text))
                        {
                            countTextBox05.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 5:
                        if (string.IsNullOrEmpty(countTextBox06.Text))
                        {
                            countTextBox06.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 6:
                        if (string.IsNullOrEmpty(countTextBox07.Text))
                        {
                            countTextBox07.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 7:
                        if (string.IsNullOrEmpty(countTextBox08.Text))
                        {
                            countTextBox08.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 8:
                        if (string.IsNullOrEmpty(countTextBox09.Text))
                        {
                            countTextBox09.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 9:
                        if (string.IsNullOrEmpty(countTextBox010.Text))
                        {
                            countTextBox010.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 10:
                        if (string.IsNullOrEmpty(countTextBox011.Text))
                        {
                            countTextBox011.Text = 0.ToString();
                        }
                        else { }
                        break;
                    case 11:
                        if (string.IsNullOrEmpty(countTextBox012.Text))
                        {
                            countTextBox012.Text = 0.ToString();
                        }
                        else { }
                        break;
                    default:
                        if (string.IsNullOrEmpty(countTextBox00.Text))
                        {
                            countTextBox00.Text = 0.ToString();
                        }
                        else { }
                        break;
                }


            }
            #endregion
            #region "総量"
            //総重量計算
            decimal weisum = decimal.Parse(weightTextBox00.Text) + decimal.Parse(weightTextBox01.Text) + decimal.Parse(weightTextBox02.Text) + decimal.Parse(weightTextBox03.Text) + decimal.Parse(weightTextBox04.Text)
                        + decimal.Parse(weightTextBox05.Text) + decimal.Parse(weightTextBox06.Text) + decimal.Parse(weightTextBox07.Text) + decimal.Parse(weightTextBox08.Text) + decimal.Parse(weightTextBox09.Text)
                        + decimal.Parse(weightTextBox010.Text) + decimal.Parse(weightTextBox011.Text) + decimal.Parse(weightTextBox012.Text);

            //総数量計算
            decimal countsum = decimal.Parse(countTextBox00.Text) + decimal.Parse(countTextBox01.Text) + decimal.Parse(countTextBox02.Text) + decimal.Parse(countTextBox03.Text) + decimal.Parse(countTextBox04.Text)
                        + decimal.Parse(countTextBox05.Text) + decimal.Parse(countTextBox06.Text) + decimal.Parse(countTextBox07.Text) + decimal.Parse(countTextBox08.Text) + decimal.Parse(countTextBox09.Text)
                        + decimal.Parse(countTextBox010.Text) + decimal.Parse(countTextBox011.Text) + decimal.Parse(countTextBox012.Text);

            //小計計算
            decimal subSum = decimal.Parse(moneyTextBox00.Text) + decimal.Parse(moneyTextBox01.Text) + decimal.Parse(moneyTextBox02.Text) + decimal.Parse(moneyTextBox03.Text) + decimal.Parse(moneyTextBox04.Text)
                        + decimal.Parse(moneyTextBox05.Text) + decimal.Parse(moneyTextBox06.Text) + decimal.Parse(moneyTextBox07.Text) + decimal.Parse(moneyTextBox08.Text) + decimal.Parse(moneyTextBox09.Text)
                        + decimal.Parse(moneyTextBox010.Text) + decimal.Parse(moneyTextBox011.Text) + decimal.Parse(moneyTextBox012.Text);

            //税額計算 DBから取得？
            decimal tax = (decimal)0.10;
            decimal taxA = subSum * tax;

            //合計計算
            decimal sum = subSum + taxA;



            subTotal2.Text = subSum.ToString();
            totalWeight2.Text = weisum.ToString();
            totalCount2.Text = countsum.ToString();
            taxAmount2.Text = taxA.ToString();
            sumTextBox2.Text = sum.ToString();
            #endregion
        }
        #endregion
        #region "計算書　追加ボタン"
        private void add_Click(object sender, EventArgs e)//追加ボタン
        {
            switch (con)
            {
                case 0:
                    mainCategoryComboBox1.Show();
                    itemComboBox1.Show();
                    weightTextBox1.Show();
                    countTextBox1.Show();
                    unitPriceTextBox1.Show();
                    moneyTextBox1.Show();
                    remarks1.Show();
                    break;
                case 1:
                    mainCategoryComboBox2.Show();
                    itemComboBox2.Show();
                    weightTextBox2.Show();
                    countTextBox2.Show();
                    unitPriceTextBox2.Show();
                    moneyTextBox2.Show();
                    remarks2.Show();
                    break;
                case 2:
                    mainCategoryComboBox3.Show();
                    itemComboBox3.Show();
                    weightTextBox3.Show();
                    countTextBox3.Show();
                    unitPriceTextBox3.Show();
                    moneyTextBox3.Show();
                    remarks3.Show();
                    break;
                case 3:
                    mainCategoryComboBox4.Show();
                    itemComboBox4.Show();
                    weightTextBox4.Show();
                    countTextBox4.Show();
                    unitPriceTextBox4.Show();
                    moneyTextBox4.Show();
                    remarks4.Show();
                    break;
                case 4:
                    mainCategoryComboBox5.Show();
                    itemComboBox5.Show();
                    weightTextBox5.Show();
                    countTextBox5.Show();
                    unitPriceTextBox5.Show();
                    moneyTextBox5.Show();
                    remarks5.Show();
                    break;
                case 5:
                    mainCategoryComboBox6.Show();
                    itemComboBox6.Show();
                    weightTextBox6.Show();
                    countTextBox6.Show();
                    unitPriceTextBox6.Show();
                    moneyTextBox6.Show();
                    remarks6.Show();
                    break;
                case 6:
                    mainCategoryComboBox7.Show();
                    itemComboBox7.Show();
                    weightTextBox7.Show();
                    countTextBox7.Show();
                    unitPriceTextBox7.Show();
                    moneyTextBox7.Show();
                    remarks7.Show();
                    break;
                case 7:
                    mainCategoryComboBox8.Show();
                    itemComboBox8.Show();
                    weightTextBox8.Show();
                    countTextBox8.Show();
                    unitPriceTextBox8.Show();
                    moneyTextBox8.Show();
                    remarks8.Show();
                    break;
                case 8:
                    mainCategoryComboBox9.Show();
                    itemComboBox9.Show();
                    weightTextBox9.Show();
                    countTextBox9.Show();
                    unitPriceTextBox9.Show();
                    moneyTextBox9.Show();
                    remarks9.Show();
                    break;
                case 9:
                    mainCategoryComboBox10.Show();
                    itemComboBox10.Show();
                    weightTextBox10.Show();
                    countTextBox10.Show();
                    unitPriceTextBox10.Show();
                    moneyTextBox10.Show();
                    remarks10.Show();
                    break;
                case 10:
                    mainCategoryComboBox11.Show();
                    itemComboBox11.Show();
                    weightTextBox11.Show();
                    countTextBox11.Show();
                    unitPriceTextBox11.Show();
                    moneyTextBox11.Show();
                    remarks11.Show();
                    break;
                case 11:
                    mainCategoryComboBox12.Show();
                    itemComboBox12.Show();
                    weightTextBox12.Show();
                    countTextBox12.Show();
                    unitPriceTextBox12.Show();
                    moneyTextBox12.Show();
                    remarks12.Show();
                    break;
                default:
                    break;
            }
            con++;
        }
        #endregion
        #region"計算書 クリアボタン"
        private void button3_Click(object sender, EventArgs e)//クリアボタン
        {
            switch(con)
            {
                case 0:
                    mainCategoryComboBox1.Hide();
                itemComboBox1.Hide();
                weightTextBox1.Hide();
                countTextBox1.Hide();
                unitPriceTextBox1.Hide();
                moneyTextBox1.Hide();
                remarks1.Hide();
                break;
                case 1:
                   mainCategoryComboBox2.Hide();
                itemComboBox2.Hide();
                weightTextBox2.Hide();
                countTextBox2.Hide();
                unitPriceTextBox2.Hide();
                moneyTextBox2.Hide();
                remarks2.Hide();
                break;
                case 2:
                    mainCategoryComboBox3.Hide();
                itemComboBox3.Hide();
                weightTextBox3.Hide();
                countTextBox3.Hide();
                unitPriceTextBox3.Hide();
                moneyTextBox3.Hide();
                remarks3.Hide();
                break;
                case 3:
                    mainCategoryComboBox4.Hide();
                itemComboBox4.Hide();
                weightTextBox4.Hide();
                countTextBox4.Hide();
                unitPriceTextBox4.Hide();
                moneyTextBox4.Hide();
                remarks4.Hide();
                break;
                case 4:
                    mainCategoryComboBox5.Hide();
                itemComboBox5.Hide();
                weightTextBox5.Hide();
                countTextBox5.Hide();
                unitPriceTextBox5.Hide();
                moneyTextBox5.Hide();
                remarks5.Hide();
                break;
                case 5:
                    mainCategoryComboBox6.Hide();
                itemComboBox6.Hide();
                weightTextBox6.Hide();
                countTextBox6.Hide();
                unitPriceTextBox6.Hide();
                moneyTextBox6.Hide();
                remarks6.Hide();
                break;
                case 6:
                    mainCategoryComboBox7.Hide();
                itemComboBox7.Hide();
                weightTextBox7.Hide();
                countTextBox7.Hide();
                unitPriceTextBox7.Hide();
                moneyTextBox7.Hide();
                remarks7.Hide();
                break;
                case 7:
                    mainCategoryComboBox8.Hide();
                itemComboBox8.Hide();
                weightTextBox8.Hide();
                countTextBox8.Hide();
                unitPriceTextBox8.Hide();
                moneyTextBox8.Hide();
                remarks8.Hide();
                break;
                case 8:
                    mainCategoryComboBox9.Hide();
                itemComboBox9.Hide();
                weightTextBox9.Hide();
                countTextBox9.Hide();
                unitPriceTextBox9.Hide();
                moneyTextBox9.Hide();
                remarks9.Hide();
                break;
                case 9:
                    mainCategoryComboBox10.Hide();
                itemComboBox10.Hide();
                weightTextBox10.Hide();
                countTextBox10.Hide();
                unitPriceTextBox10.Hide();
                moneyTextBox10.Hide();
                remarks10.Hide();
                break;
                case 10:
                    mainCategoryComboBox11.Hide();
                itemComboBox11.Hide();
                weightTextBox11.Hide();
                countTextBox11.Hide();
                unitPriceTextBox11.Hide();
                moneyTextBox11.Hide();
                remarks11.Hide();
                break;
                case 11:
                    mainCategoryComboBox12.Hide();
                itemComboBox12.Hide();
                weightTextBox12.Hide();
                countTextBox12.Hide();
                unitPriceTextBox12.Hide();
                moneyTextBox12.Hide();
                remarks12.Hide();
                break;
                default:
                    break;
            }
            con--;
        }


        #endregion
        #region "納品書　追加ボタン"
        private void add2_Click(object sender, EventArgs e)
        {
            switch (com)
            {
                case 0:
                    mainCategoryComboBox01.Show();
                    itemComboBox01.Show();
                    weightTextBox01.Show();
                    countTextBox01.Show();
                    unitPriceTextBox01.Show();
                    moneyTextBox01.Show();
                    remarks01.Show();
                    break;
                case 1:
                    mainCategoryComboBox02.Show();
                    itemComboBox02.Show();
                    weightTextBox02.Show();
                    countTextBox02.Show();
                    unitPriceTextBox02.Show();
                    moneyTextBox02.Show();
                    remarks02.Show();
                    break;
                case 2:
                    mainCategoryComboBox03.Show();
                    itemComboBox03.Show();
                    weightTextBox03.Show();
                    countTextBox03.Show();
                    unitPriceTextBox03.Show();
                    moneyTextBox03.Show();
                    remarks03.Show();
                    break;
                case 3:
                    mainCategoryComboBox04.Show();
                    itemComboBox04.Show();
                    weightTextBox04.Show();
                    countTextBox04.Show();
                    unitPriceTextBox04.Show();
                    moneyTextBox04.Show();
                    remarks04.Show();
                    break;
                case 4:
                    mainCategoryComboBox05.Show();
                    itemComboBox05.Show();
                    weightTextBox05.Show();
                    countTextBox05.Show();
                    unitPriceTextBox05.Show();
                    moneyTextBox05.Show();
                    remarks05.Show();
                    break;
                case 5:
                    mainCategoryComboBox06.Show();
                    itemComboBox06.Show();
                    weightTextBox06.Show();
                    countTextBox06.Show();
                    unitPriceTextBox06.Show();
                    moneyTextBox06.Show();
                    remarks06.Show();
                    break;
                case 6:
                    mainCategoryComboBox07.Show();
                    itemComboBox07.Show();
                    weightTextBox07.Show();
                    countTextBox07.Show();
                    unitPriceTextBox07.Show();
                    moneyTextBox07.Show();
                    remarks07.Show();
                    break;
                case 7:
                    mainCategoryComboBox08.Show();
                    itemComboBox08.Show();
                    weightTextBox08.Show();
                    countTextBox08.Show();
                    unitPriceTextBox08.Show();
                    moneyTextBox08.Show();
                    remarks08.Show();
                    break;
                case 8:
                    mainCategoryComboBox09.Show();
                    itemComboBox09.Show();
                    weightTextBox09.Show();
                    countTextBox09.Show();
                    unitPriceTextBox09.Show();
                    moneyTextBox09.Show();
                    remarks09.Show();
                    break;
                case 9:
                    mainCategoryComboBox010.Show();
                    itemComboBox010.Show();
                    weightTextBox010.Show();
                    countTextBox010.Show();
                    unitPriceTextBox010.Show();
                    moneyTextBox010.Show();
                    remarks010.Show();
                    break;
                case 10:
                    mainCategoryComboBox011.Show();
                    itemComboBox011.Show();
                    weightTextBox011.Show();
                    countTextBox011.Show();
                    unitPriceTextBox011.Show();
                    moneyTextBox011.Show();
                    remarks011.Show();
                    break;
                case 11:
                    mainCategoryComboBox012.Show();
                    itemComboBox012.Show();
                    weightTextBox012.Show();
                    countTextBox012.Show();
                    unitPriceTextBox012.Show();
                    moneyTextBox012.Show();
                    remarks012.Show();
                    break;
                default:
                    break;
            }
            com++;
        }
        #endregion
        #region "納品書　クリアボタン"
        private void clear2_Click(object sender, EventArgs e)
        {
            switch (com)
            {
                case 0:
                    mainCategoryComboBox01.Hide();
                    itemComboBox01.Hide();
                    weightTextBox01.Hide();
                    countTextBox01.Hide();
                    unitPriceTextBox01.Hide();
                    moneyTextBox01.Hide();
                    remarks01.Hide();
                    break;
                case 1:
                    mainCategoryComboBox02.Hide();
                    itemComboBox02.Hide();
                    weightTextBox02.Hide();
                    countTextBox02.Hide();
                    unitPriceTextBox02.Hide();
                    moneyTextBox02.Hide();
                    remarks02.Hide();
                    break;
                case 2:
                    mainCategoryComboBox03.Hide();
                    itemComboBox03.Hide();
                    weightTextBox03.Hide();
                    countTextBox03.Hide();
                    unitPriceTextBox03.Hide();
                    moneyTextBox03.Hide();
                    remarks03.Hide();
                    break;
                case 3:
                    mainCategoryComboBox04.Hide();
                    itemComboBox04.Hide();
                    weightTextBox04.Hide();
                    countTextBox04.Hide();
                    unitPriceTextBox04.Hide();
                    moneyTextBox04.Hide();
                    remarks04.Hide();
                    break;
                case 4:
                    mainCategoryComboBox05.Hide();
                    itemComboBox05.Hide();
                    weightTextBox05.Hide();
                    countTextBox05.Hide();
                    unitPriceTextBox05.Hide();
                    moneyTextBox05.Hide();
                    remarks05.Hide();
                    break;
                case 5:
                    mainCategoryComboBox06.Hide();
                    itemComboBox06.Hide();
                    weightTextBox06.Hide();
                    countTextBox06.Hide();
                    unitPriceTextBox06.Hide();
                    moneyTextBox06.Hide();
                    remarks06.Hide();
                    break;
                case 6:
                    mainCategoryComboBox07.Hide();
                    itemComboBox07.Hide();
                    weightTextBox07.Hide();
                    countTextBox07.Hide();
                    unitPriceTextBox07.Hide();
                    moneyTextBox07.Hide();
                    remarks07.Hide();
                    break;
                case 7:
                    mainCategoryComboBox08.Hide();
                    itemComboBox08.Hide();
                    weightTextBox08.Hide();
                    countTextBox08.Hide();
                    unitPriceTextBox08.Hide();
                    moneyTextBox08.Hide();
                    remarks08.Hide();
                    break;
                case 8:
                    mainCategoryComboBox09.Hide();
                    itemComboBox09.Hide();
                    weightTextBox09.Hide();
                    countTextBox09.Hide();
                    unitPriceTextBox09.Hide();
                    moneyTextBox09.Hide();
                    remarks09.Hide();
                    break;
                case 9:
                    mainCategoryComboBox010.Hide();
                    itemComboBox010.Hide();
                    weightTextBox010.Hide();
                    countTextBox010.Hide();
                    unitPriceTextBox010.Hide();
                    moneyTextBox010.Hide();
                    remarks010.Hide();
                    break;
                case 10:
                    mainCategoryComboBox011.Hide();
                    itemComboBox011.Hide();
                    weightTextBox011.Hide();
                    countTextBox011.Hide();
                    unitPriceTextBox011.Hide();
                    moneyTextBox011.Hide();
                    remarks011.Hide();
                    break;
                case 11:
                    mainCategoryComboBox012.Hide();
                    itemComboBox012.Hide();
                    weightTextBox012.Hide();
                    countTextBox012.Hide();
                    unitPriceTextBox012.Hide();
                    moneyTextBox012.Hide();
                    remarks012.Hide();
                    break;
                default:
                    break;
            }
            com--;
        }


        #endregion

 

        private void client_searchButton(object sender, EventArgs e)
        {
            Client_search client_Search = new Client_search(this);

            this.Hide();
            client_Search.Show();
        }

        private void client_Button_Click(object sender, EventArgs e)//顧客選択メニュー（計算書）
        {
            Client_search client_Search = new Client_search(this);
            this.Hide();
            client_Search.Show();
        }

        private void client_searchButton1_Click(object sender, EventArgs e)//顧客選択メニュー（納品書）
        {
            Client_search client_Search = new Client_search(this);

            this.Hide();
            client_Search.Show();
        }
    }
}
