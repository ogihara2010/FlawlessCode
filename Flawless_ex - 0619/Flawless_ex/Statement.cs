using Npgsql;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace Flawless_ex
{
    public partial class Statement : Form //計算書/納品書作成メニュー
    {
        int a = 0;//大分類クリックカウント数（計算書）
        int b = 1;//大分類クリックカウント数（納品書）
        int staff_id;
        int itemMainCategoryCode;
        int Tax;        //計算用の税（表示用を別に用意）
        decimal weisum;     //総重量計算用
        decimal countsum;   //総数計算用
        decimal subSum;     //税抜き時の合計金額用
        decimal sub;    //重量×単価 or 数量×単価
        decimal sum;    //税込み時の合計金額用
        decimal TaxAmount;  //税額
        public DataTable clientDt = new DataTable();//顧客情報
        int type = 1;
        public int count = 0;
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
        string staff_name;
        string address;
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
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定


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

            //税率取得
            string sql_str5 = "select vat_rate from vat_m;";        //税率取得
            cmd = new NpgsqlCommand(sql_str5, conn);

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string vat_rate = reader["vat_rate"].ToString();
                    tax.Text = vat_rate;
                }
            }
            Tax = int.Parse(tax.Text.ToString());
            tax.Text = string.Format("{0:P}", Tax);

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


            groupBox1.Hide();//200万以上の取引情報

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

            this.button13.Enabled = false;
            this.previewButton.Enabled = false;
            this.button9.Enabled = false;

            //単価の欄に初期表示
            unitPriceTextBox0.Text = "単価 -> 重量 or 数量";

            //タブのサイズ変更
            tabControl1.ItemSize = new Size(300, 40);
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
                    int type = (int)row2["type"];

                    string companyNmae = row2["company_name"].ToString();
                    string shopName = row2["shop_name"].ToString();
                    string Staff_name = row2["staff_name"].ToString();
                    //string Address = row2["address"].ToString();
                    string register_date = row2["register_date"].ToString();
                    string remarks = row2["remarks"].ToString();
                    string antique_license = row2["antique_license"].ToString();

                    typeTextBox.Text = "法人";
                    companyTextBox.Text = companyNmae;
                    textBox302.Text = antique_license;
                    shopNameTextBox.Text = shopName;
                    clientNameTextBox.Text = Staff_name;
                    //addressTextBox.Text = Address;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                    #endregion
                    #region "納品書"
                    typeTextBox2.Text = "法人";
                    companyTextBox2.Text = companyNmae;
                    shopNameTextBox2.Text = shopName;
                    clientNameTextBox2.Text = Staff_name;
                    //addressTextBox2.Text = Address;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                    textBox1.Text = antique_license;
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
                    int type = (int)row2["type"];

                    string name = row2["name"].ToString();
                    //string Address = row2["address"].ToString();
                    string register_date = row2["register_date"].ToString();
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
                    textBox302.Text = antique_license;
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
                    //addressTextBox2.Text = Address;
                    registerDateTextBox2.Text = register_date;
                    clientRemarksTextBox2.Text = remarks;
                    textBox1.Text = antique_license;
                    label36.Visible = false;
                    registerDateTextBox2.Visible = false;
                    #endregion
                }
                
            }
        
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
            if (dr == System.Windows.Forms.DialogResult.OK)
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
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
                    string companyNmae = row["company_name"].ToString();
                    string shopName = row["shop_name"].ToString();
                    string staff_name = row["staff_name"].ToString();
                    string address = row["address"].ToString();
                    string register_date = row["register_date"].ToString();
                    string remarks = row["remarks"].ToString();

                    typeTextBox.Text = "法人";
                    companyTextBox.Text = companyNmae;
                    shopNameTextBox.Text = shopName;
                    clientNameTextBox.Text = staff_name;
                    //addressTextBox.Text = address;
                    registerDateTextBox.Text = register_date;
                    clientRemarksTextBox.Text = remarks;
                }
                else if (type == 1)
                {
                    string name = row["name"].ToString();
                    string address = row["address"].ToString();
                    string remarks = row["remarks"].ToString();

                    typeTextBox.Text = "個人";
                    clientNameTextBox.Text = name;
                    //addressTextBox.Text = address;
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
                    //addressTextBox2.Text = address;
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
                    //addressTextBox2.Text = address;
                    clientRemarksTextBox2.Text = remarks;
                }
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }
            
            this.documentNumberTextBox.Text = "101";
            //int AntiqueNumber = int.Parse(this.textBox302.Text);
            int TotalWeight = int.Parse(this.totalWeight.Text);
            int Amount = int.Parse(this.totalCount.Text);
            decimal SubTotal = decimal.Parse(this.subTotal.Text);
            decimal TaxAmount = decimal.Parse(this.taxAmount.Text);
            decimal Total = decimal.Parse(this.sumTextBox.Text);
            string SettlementDate = this.settlementBox.Text;
            string DeliveryDate = this.deliveryDateBox.Text;
            string DeliveryMethod = this.deliveryTextBox.Text;
            string PaymentMethod = this.paymentMethodsBox.Text;
            decimal Weight = decimal.Parse(this.weightTextBox0.Text);
            int Count = int.Parse(this.countTextBox0.Text);
            decimal UnitPrice = decimal.Parse(this.unitPriceTextBox0.Text);
            decimal amount = decimal.Parse(this.moneyTextBox0.Text);
            string Remarks = this.remarks0.Text;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            DataTable dt = new DataTable();
            string sql_str = "Insert into statement_data VALUES ( " + staff_id + " , " +  staff_id  + " , " + staff_id + " , " + TotalWeight + " ,  " + Amount + " , " + SubTotal + ", " + TaxAmount + " , " + Total + " , '" + DeliveryMethod + "' , '" + PaymentMethod  + "' , '" + SettlementDate + "' , '" + DeliveryDate +  "' , '" + staff_id + "');";

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            conn.Close();

            DataTable dt2 = new DataTable();
            string sql_str2 = "Insert into statement_calc_data VALUES ( " + staff_id + " , " + staff_id + " , " +  Weight + " ,  " + Count + " , " + UnitPrice + " , " + amount +  " , '" + Remarks + "' , '" + staff_id + "');";

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);
            conn.Close();
            MessageBox.Show("登録しました。");
            this.button13.Enabled = true;
            this.previewButton.Enabled = true;
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            string path = " ";
            textBox1.Text = " ";
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;
            }
            else if (dialog == DialogResult.Cancel) { }

            textBox1.Text = path;
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            string path = " ";
            textBox302.Text = " ";
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;
            }
            else if (dialog == DialogResult.Cancel) { }

            textBox302.Text = path;
        }

        #region　"計算書　単価を入力したら重量、数値入力可"
        private void unitPriceTextBox0_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(unitPriceTextBox0.Text))
            {
                weightTextBox0.ReadOnly = true;
                countTextBox0.ReadOnly = true;
                unitPriceTextBox1.ReadOnly = true;
            }
            else if(!string.IsNullOrEmpty(unitPriceTextBox0.Text)&&!(unitPriceTextBox0.Text.ToString()== "単価 -> 重量 or 数量"))        //単価がnullでなく、初期状態でもないとき
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
                unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox8.Text = "単価から重量 or 数量";
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
                unitPriceTextBox9.Text = "単価から重量 or 数量";
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
                unitPriceTextBox10.Text = "単価から重量 or 数量";
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
                unitPriceTextBox11.Text = "単価から重量 or 数量";
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

        #region"計算書　重量を入力したら数量を入力不可"
        private void weightTextBox0_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox0.Text))
            {
                countTextBox0.ReadOnly = true;
            }
            else
            {
                countTextBox0.ReadOnly = false;
            }
        }

        private void weightTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox1.Text))
            {
                countTextBox1.ReadOnly = true;
            }
            else
            {
                countTextBox1.ReadOnly = false;
            }
        }

        private void weightTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox2.Text))
            {
                countTextBox2.ReadOnly = true;
            }
            else
            {
                countTextBox2.ReadOnly = false;
            }
        }

        private void weightTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox3.Text))
            {
                countTextBox3.ReadOnly = true;
            }
            else
            {
                countTextBox3.ReadOnly = false;
            }
        }

        private void weightTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox4.Text))
            {
                countTextBox4.ReadOnly = true;
            }
            else
            {
                countTextBox4.ReadOnly = false;
            }
        }

        private void weightTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox5.Text))
            {
                countTextBox5.ReadOnly = true;
            }
            else
            {
                countTextBox5.ReadOnly = false;
            }
        }

        private void weightTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox6.Text))
            {
                countTextBox6.ReadOnly = true;
            }
            else
            {
                countTextBox6.ReadOnly = false;
            }
        }

        private void weightTextBox7_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox7.Text))
            {
                countTextBox7.ReadOnly = true;
            }
            else
            {
                countTextBox7.ReadOnly = false;
            }
        }

        private void weightTextBox8_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox8.Text))
            {
                countTextBox8.ReadOnly = true;
            }
            else
            {
                countTextBox8.ReadOnly = false;
            }
        }

        private void weightTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox9.Text))
            {
                countTextBox9.ReadOnly = true;
            }
            else
            {
                countTextBox9.ReadOnly = false;
            }
        }

        private void weightTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox10.Text))
            {
                countTextBox10.ReadOnly = true;
            }
            else
            {
                countTextBox10.ReadOnly = false;
            }
        }

        private void weightTextBox11_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox11.Text))
            {
                countTextBox11.ReadOnly = true;
            }
            else
            {
                countTextBox11.ReadOnly = false;
            }
        }

        private void weightTextBox12_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox12.Text))
            {
                countTextBox12.ReadOnly = true;
            }
            else
            {
                countTextBox12.ReadOnly = false;
            }
        }

        #endregion

        #region"計算書　数量を入力したら重量を入力不可"

        private void countTextBox0_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox0.Text))
            {
                weightTextBox0.ReadOnly = true;
            }
            else
            {
                weightTextBox0.ReadOnly = false;
            }
        }

        private void countTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox1.Text))
            {
                weightTextBox1.ReadOnly = true;
            }
            else
            {
                weightTextBox1.ReadOnly = false;
            }
        }

        private void countTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox2.Text))
            {
                weightTextBox2.ReadOnly = true;
            }
            else
            {
                weightTextBox2.ReadOnly = false;
            }
        }

        private void countTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox3.Text))
            {
                weightTextBox3.ReadOnly = true;
            }
            else
            {
                weightTextBox3.ReadOnly = false;
            }
        }

        private void countTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox4.Text))
            {
                weightTextBox4.ReadOnly = true;
            }
            else
            {
                weightTextBox4.ReadOnly = false;
            }
        }

        private void countTextBox5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox5.Text))
            {
                weightTextBox5.ReadOnly = true;
            }
            else
            {
                weightTextBox5.ReadOnly = false;
            }
        }

        private void countTextBox6_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox6.Text))
            {
                weightTextBox6.ReadOnly = true;
            }
            else
            {
                weightTextBox6.ReadOnly = false;
            }
        }

        private void countTextBox7_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox7.Text))
            {
                weightTextBox7.ReadOnly = true;
            }
            else
            {
                weightTextBox7.ReadOnly = false;
            }
        }

        private void countTextBox8_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox8.Text))
            {
                weightTextBox8.ReadOnly = true;
            }
            else
            {
                weightTextBox8.ReadOnly = false;
            }
        }

        private void countTextBox9_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox9.Text))
            {
                weightTextBox9.ReadOnly = true;
            }
            else
            {
                weightTextBox9.ReadOnly = false;
            }
        }

        private void countTextBox10_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox10.Text))
            {
                weightTextBox10.ReadOnly = true;
            }
            else
            {
                weightTextBox10.ReadOnly = false;
            }
        }

        private void countTextBox11_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox11.Text))
            {
                weightTextBox11.ReadOnly = true;
            }
            else
            {
                weightTextBox11.ReadOnly = false;
            }
        }

        private void countTextBox12_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(countTextBox12.Text))
            {
                weightTextBox12.ReadOnly = true;
            }
            else
            {
                weightTextBox12.ReadOnly = false;
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

        #region"計算書　金額が入力されたら次の単価が入力可"＋総重量 or 総数計算、自動計算
        private void moneyTextBox0_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!(string.IsNullOrEmpty(moneyTextBox0.Text) )|| !(moneyTextBox0.Text == 0.ToString()))
            {
                countTextBox0.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox0.Text);
                weisum = decimal.Parse(weightTextBox0.Text);
                subSum = money0;
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox0.Text))
            {
                weightTextBox0.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox0.Text);
                weisum = decimal.Parse(weightTextBox0.Text);
                subSum = money0;
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox1_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox1.Text) )
            {
                countTextBox1.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox1.Text);
                weisum = decimal.Parse(weightTextBox1.Text);
                subSum = money0 + money1;
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox1.Text))
            {
                weightTextBox1.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox1.Text);
                weisum = decimal.Parse(weightTextBox1.Text);
                subSum = money0 + money1;
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox2_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox2.Text))
            {
                countTextBox2.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox2.Text);
                weisum = decimal.Parse(weightTextBox2.Text);
                subSum = money0 + money1 + money2;
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox2.Text))
            {
                weightTextBox2.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox2.Text);
                weisum = decimal.Parse(weightTextBox2.Text);
                subSum = money0 + money1 + money2;
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox3_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox3.Text))
            {
                countTextBox3.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox3.Text);
                weisum = decimal.Parse(weightTextBox3.Text);
                subSum = money0 + money1 + money2 + money3;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox3.Text))
            {
                weightTextBox3.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox3.Text);
                weisum = decimal.Parse(weightTextBox3.Text);
                subSum = money0 + money1 + money2 + money3;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox4_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox4.Text))
            {
                countTextBox4.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox4.Text);
                weisum = decimal.Parse(weightTextBox4.Text);
                subSum = money0 + money1 + money2 + money3 + money4;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox4.Text))
            {
                weightTextBox4.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox4.Text);
                weisum = decimal.Parse(weightTextBox4.Text);
                subSum = money0 + money1 + money2 + money3 + money4;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox5_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox5.Text))
            {
                countTextBox5.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox5.Text);
                weisum = decimal.Parse(weightTextBox5.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox5.Text))
            {
                weightTextBox5.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox5.Text);
                weisum = decimal.Parse(weightTextBox5.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox6_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox6.Text))
            {
                countTextBox6.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox6.Text);
                weisum = decimal.Parse(weightTextBox6.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox6.Text))
            {
                weightTextBox6.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox6.Text);
                weisum = decimal.Parse(weightTextBox6.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox7_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox7.Text))
            {
                countTextBox7.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox7.Text);
                weisum = decimal.Parse(weightTextBox7.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox7.Text))
            {
                weightTextBox7.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox7.Text);
                weisum = decimal.Parse(weightTextBox7.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox8_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox8.Text))
            {
                countTextBox8.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox8.Text);
                weisum = decimal.Parse(weightTextBox8.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox8.Text))
            {
                weightTextBox8.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox8.Text);
                weisum = decimal.Parse(weightTextBox8.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox9_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox9.Text))
            {
                countTextBox9.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox9.Text);
                weisum = decimal.Parse(weightTextBox9.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8 + money9;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox9.Text))
            {
                weightTextBox9.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox9.Text);
                weisum = decimal.Parse(weightTextBox9.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8 + money9;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox10_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox10.Text))
            {
                countTextBox10.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox10.Text);
                weisum = decimal.Parse(weightTextBox10.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8 + money9 + money10;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox10.Text))
            {
                weightTextBox10.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox10.Text);
                weisum = decimal.Parse(weightTextBox10.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8 + money9 + money10;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox11_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox11.Text))
            {
                countTextBox11.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox11.Text);
                weisum = decimal.Parse(weightTextBox11.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8 + money9 + money10 + money11;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox11.Text))
            {
                weightTextBox11.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox11.Text);
                weisum = decimal.Parse(weightTextBox11.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8 + money9 + money10 + money11;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        private void moneyTextBox12_TextChanged(object sender, EventArgs e)
        {
            //重量×単価
            if (!string.IsNullOrEmpty(weightTextBox12.Text))
            {
                countTextBox12.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox12.Text);
                weisum = decimal.Parse(weightTextBox12.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8 + money9 + money10 + money11 + money12;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
            //数量×単価
            if (!string.IsNullOrEmpty(countTextBox12.Text))
            {
                weightTextBox12.Text = 0.ToString();
                countsum = decimal.Parse(countTextBox12.Text);
                weisum = decimal.Parse(weightTextBox12.Text);
                subSum = money0 + money1 + money2 + money3 + money4 + money5 + money6 + money7 + money8 + money9 + money10 + money11 + money12;     //増やしていく
                TaxAmount = subSum * Tax / 100;
                sum = subSum + TaxAmount;

                totalWeight.Text = string.Format("{0:#,0}", Math.Round(weisum, 1, MidpointRounding.AwayFromZero));
                totalCount.Text = string.Format("{0:#,0}", countsum);
                subTotal.Text = string.Format("{0:C}", Math.Round(subSum + TaxAmount, MidpointRounding.AwayFromZero));
                taxAmount.Text = string.Format("{0:C}", Math.Round(TaxAmount, MidpointRounding.AwayFromZero));
                sumTextBox.Text = string.Format("{0:C}", Math.Round(sum, MidpointRounding.AwayFromZero));
            }
        }
        #endregion

        #region　"納品書　単価を入力したら重量、数値入力可"　//追加中
        /*
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
                unitPriceTextBox3.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox4.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox5.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox6.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox7.Text = "単価 -> 重量 or 数量";
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
                unitPriceTextBox8.Text = "単価から重量 or 数量";
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
                unitPriceTextBox9.Text = "単価から重量 or 数量";
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
                unitPriceTextBox10.Text = "単価から重量 or 数量";
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
                unitPriceTextBox11.Text = "単価から重量 or 数量";
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
        */
        #endregion

        #region"納品書　フォーカス時初期状態ならnull"   //追加中

        #endregion

        #region"納品書　重量を入力したら数量を入力不可"
        private void weightTextBox00_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox00.Text))
            {
                countTextBox00.ReadOnly = true;
            }
            else
            {
                countTextBox00.ReadOnly = false;
            }
        }

        private void weightTextBox01_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox01.Text))
            {
                countTextBox01.ReadOnly = true;
            }
            else
            {
                countTextBox01.ReadOnly = false;
            }
        }

        private void weightTextBox02_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox02.Text))
            {
                countTextBox02.ReadOnly = true;
            }
            else
            {
                countTextBox02.ReadOnly = false;
            }
        }

        private void weightTextBox03_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox03.Text))
            {
                countTextBox03.ReadOnly = true;
            }
            else
            {
                countTextBox03.ReadOnly = false;
            }
        }

        private void weightTextBox04_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox04.Text))
            {
                countTextBox04.ReadOnly = true;
            }
            else
            {
                countTextBox04.ReadOnly = false;
            }
        }

        private void weightTextBox05_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox05.Text))
            {
                countTextBox05.ReadOnly = true;
            }
            else
            {
                countTextBox05.ReadOnly = false;
            }
        }

        private void weightTextBox06_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox06.Text))
            {
                countTextBox06.ReadOnly = true;
            }
            else
            {
                countTextBox06.ReadOnly = false;
            }
        }

        private void weightTextBox07_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox07.Text))
            {
                countTextBox07.ReadOnly = true;
            }
            else
            {
                countTextBox07.ReadOnly = false;
            }
        }

        private void weightTextBox08_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox08.Text))
            {
                countTextBox08.ReadOnly = true;
            }
            else
            {
                countTextBox08.ReadOnly = false;
            }
        }

        private void weightTextBox09_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox09.Text))
            {
                countTextBox09.ReadOnly = true;
            }
            else
            {
                countTextBox09.ReadOnly = false;
            }
        }

        private void weightTextBox010_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox010.Text))
            {
                countTextBox010.ReadOnly = true;
            }
            else
            {
                countTextBox010.ReadOnly = false;
            }
        }

        private void weightTextBox011_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox011.Text))
            {
                countTextBox011.ReadOnly = true;
            }
            else
            {
                countTextBox011.ReadOnly = false;
            }
        }

        private void weightTextBox012_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(weightTextBox012.Text))
            {
                countTextBox012.ReadOnly = true;
            }
            else
            {
                countTextBox012.ReadOnly = false;
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
        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;";

            string sql_str5 = "select vat_rate from vat_m;";        //税率取得
            cmd = new NpgsqlCommand(sql_str5, conn);

            conn.Open();

            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    tax.Text = reader["vat_rate"].ToString();
                }
            }
            int Tax = int.Parse(tax.Text);

            conn.Close();

            if (comboBox11.SelectedIndex == 0)      //税抜き選択
            {
                decimal subSum = decimal.Parse(moneyTextBox00.Text) + decimal.Parse(moneyTextBox01.Text) + decimal.Parse(moneyTextBox02.Text) + decimal.Parse(moneyTextBox03.Text) + decimal.Parse(moneyTextBox04.Text)
                        + decimal.Parse(moneyTextBox05.Text) + decimal.Parse(moneyTextBox06.Text) + decimal.Parse(moneyTextBox07.Text) + decimal.Parse(moneyTextBox08.Text) + decimal.Parse(moneyTextBox09.Text)
                        + decimal.Parse(moneyTextBox010.Text) + decimal.Parse(moneyTextBox011.Text) + decimal.Parse(moneyTextBox012.Text);

                decimal sum = subSum * Tax;

                subTotal2.Text = string.Format("{0:#,0}", subSum);    //税抜き表記
                sumTextBox2.Text = string.Format("{0:#,0}", sum);
            }
            else        //税込み選択
            {
                decimal subSum = decimal.Parse(moneyTextBox00.Text) + decimal.Parse(moneyTextBox01.Text) + decimal.Parse(moneyTextBox02.Text) + decimal.Parse(moneyTextBox03.Text) + decimal.Parse(moneyTextBox04.Text)
                        + decimal.Parse(moneyTextBox05.Text) + decimal.Parse(moneyTextBox06.Text) + decimal.Parse(moneyTextBox07.Text) + decimal.Parse(moneyTextBox08.Text) + decimal.Parse(moneyTextBox09.Text)
                        + decimal.Parse(moneyTextBox010.Text) + decimal.Parse(moneyTextBox011.Text) + decimal.Parse(moneyTextBox012.Text);

                decimal sum = subSum * Tax;

                subTotal2.Text = string.Format("{0:#,0}", subSum * Tax);    //税込み表記
                sumTextBox2.Text = string.Format("{0:#,0}", sum);
            }
        }
        #endregion

        #region"納品書　単価３桁区切り"＋フォーカスが外れた時
        private void unitPriceTextBox00_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox00.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox00.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox01_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox01.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox01.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox02_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox02.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox02.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox03_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox03.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox03.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox04_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox04.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox04.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox05_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox05.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox05.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox06_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox06.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox06.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox07_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox07.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox07.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox08_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox08.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox08.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox09_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox09.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox09.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox010_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox010.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox010.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox011_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox011.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox011.Text, System.Globalization.NumberStyles.Number));
        }

        private void unitPriceTextBox012_Leave(object sender, EventArgs e)
        {
            unitPriceTextBox012.Text = string.Format("{0:#,0}", decimal.Parse(unitPriceTextBox012.Text, System.Globalization.NumberStyles.Number));
        }



        #endregion

        #region"計算書　重量×単価"
        private void weightTextBox0_Leave(object sender, EventArgs e)
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
            money0 = sub;
            moneyTextBox0.Text = string.Format("{0:C}", sub);
        }

        private void weightTextBox1_Leave(object sender, EventArgs e)
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
            money1 = sub;
            moneyTextBox1.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox2_Leave(object sender, EventArgs e)
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
            money2 = sub;
            moneyTextBox2.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox3_Leave(object sender, EventArgs e)
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
            money3 = sub;
            moneyTextBox3.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox4_Leave(object sender, EventArgs e)
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
            money4 = sub;
            moneyTextBox4.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox5_Leave(object sender, EventArgs e)
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
            money5 = sub;
            moneyTextBox5.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox6_Leave(object sender, EventArgs e)
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
            money6 = sub;
            moneyTextBox6.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox7_Leave(object sender, EventArgs e)
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
            money7 = sub;
            moneyTextBox7.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox8_Leave(object sender, EventArgs e)
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
            money8 = sub;
            moneyTextBox8.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox9_Leave(object sender, EventArgs e)
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
            money9 = sub;
            moneyTextBox9.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox10_Leave(object sender, EventArgs e)
        {
            int j = weightTextBox10.Text.IndexOf(".");
            if (j > -1)     //小数点あり
            {
                //重量の四捨五入
                decimal weight10 = Math.Round(decimal.Parse(weightTextBox10.Text), 1, MidpointRounding.AwayFromZero);
                weightTextBox10.Text = weight10.ToString();

                //重量×単価の切捨て
                sub = Math.Floor(weight10 * decimal.Parse(unitPriceTextBox2.Text));
            }
            else        //小数点なし
            {
                sub = decimal.Parse(weightTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
            }
            money10 = sub;
            moneyTextBox10.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox11_Leave(object sender, EventArgs e)
        {
            int j = weightTextBox11.Text.IndexOf(".");
            if (j > -1)     //小数点あり
            {
                //重量の四捨五入
                decimal weight11 = Math.Round(decimal.Parse(weightTextBox11.Text), 1, MidpointRounding.AwayFromZero);
                weightTextBox11.Text = weight11.ToString();

                //重量×単価の切捨て
                sub = Math.Floor(weight11 * decimal.Parse(unitPriceTextBox2.Text));
            }
            else        //小数点なし
            {
                sub = decimal.Parse(weightTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
            }
            money11 = sub;
            moneyTextBox11.Text = string.Format("{0:C}", sub);
        }
        private void weightTextBox12_Leave(object sender, EventArgs e)
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
            money12 = sub;
            moneyTextBox12.Text = string.Format("{0:C}", sub);
        }
        #endregion

        #region"計算書　数量×単価"
        private void countTextBox0_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox0.Text) * decimal.Parse(unitPriceTextBox0.Text);
            money0 = sub;
            moneyTextBox0.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox1_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox1.Text) * decimal.Parse(unitPriceTextBox1.Text);
            money1 = sub;
            moneyTextBox1.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox2_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox2.Text) * decimal.Parse(unitPriceTextBox2.Text);
            money2 = sub;
            moneyTextBox2.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox3_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox3.Text) * decimal.Parse(unitPriceTextBox3.Text);
            money3 = sub;
            moneyTextBox3.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox4_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox4.Text) * decimal.Parse(unitPriceTextBox4.Text);
            money4 = sub;
            moneyTextBox4.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox5_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox5.Text) * decimal.Parse(unitPriceTextBox5.Text);
            money5 = sub;
            moneyTextBox5.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox6_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox6.Text) * decimal.Parse(unitPriceTextBox6.Text);
            money6 = sub;
            moneyTextBox6.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox7_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox7.Text) * decimal.Parse(unitPriceTextBox7.Text);
            money7 = sub;
            moneyTextBox7.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox8_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox8.Text) * decimal.Parse(unitPriceTextBox8.Text);
            money8 = sub;
            moneyTextBox8.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox9_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox9.Text) * decimal.Parse(unitPriceTextBox9.Text);
            money9 = sub;
            moneyTextBox9.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox10_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox10.Text) * decimal.Parse(unitPriceTextBox10.Text);
            money10 = sub;
            moneyTextBox10.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox11_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox11.Text) * decimal.Parse(unitPriceTextBox11.Text);
            money11 = sub;
            moneyTextBox11.Text = string.Format("{0:C}", sub);
        }
        private void countTextBox12_Leave(object sender, EventArgs e)
        {
            sub = decimal.Parse(countTextBox12.Text) * decimal.Parse(unitPriceTextBox12.Text);
            money12 = sub;
            moneyTextBox12.Text = string.Format("{0:C}", sub);
        }
        #endregion

        #region "納品書　重量×単価"     これから

        #endregion

        #region　"納品書　数量×単価"     これから

        #endregion

        private void Client_searchButton1_Click(object sender, EventArgs e)
        {
            using (client_search search2 = new client_search(mainMenu, staff_id, type))
            {
                this.Hide();
                search2.ShowDialog();
            }
        }
    }
}
