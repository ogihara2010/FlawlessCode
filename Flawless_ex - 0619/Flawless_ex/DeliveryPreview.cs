﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Drawing.Printing;

namespace Flawless_ex
{
    public partial class DeliveryPreview : Form
    {
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        int a; // record_numberの数
        int b; // 2巡目
        int staff_id;
        int type;
        string staff_name;
        string address;
        decimal Total;
        string access_auth;
        string document;
        int control;
        string data;
        int print;
        #region "買取販売履歴"
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
        string pass;
        int grade;
        public DeliveryPreview(MainMenu mainMenu, int id, int type, int control, string access_auth, string pass)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            staff_id = id;
            this.type = type;
            this.access_auth = access_auth;
            this.pass = pass;
            this.control = control;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
            this.Close();
            statement.Show();
        }

        private void DeliveryPreview_Load(object sender, EventArgs e)
        {
            if (type == 0)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                //NpgsqlDataAdapter adapter2;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number =" + control + ";";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);

                /*string sql_str2 = "select * from delivery_calc where invalid = 0 and ;";
                adapter2 = new NpgsqlDataAdapter(sql_str2, conn);
                adapter2.Fill(dt2);
                */
                conn.Close();

                DataRow row;
                row = dt.Rows[0];
                int controlNumber = (int)row["control_number"];
            }
            if (type == 1)
            {
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str = "select * from delivery_m where control_number = " + control + ";";
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);

                conn.Close();
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            printPreviewDialog1.Document = pd;
            DialogResult dr = printPreviewDialog1.ShowDialog();
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            dt.Clear();
            dt2.Clear();
            dt6.Clear();

            Font font = new Font("MS Pゴシック", 10.5f);
            Font font1 = new Font("メイリオ", 36f);
            Brush brush = new SolidBrush(Color.Black);

            #region "納品書 下の部分"
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select * from delivery_m where control_number =" + control + ";";
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            conn.Open();

            DataRow row;
            row = dt.Rows[0];
            string method = row["payment_method"].ToString();
            string orderDate = row["order_date"].ToString();
            string deliveryDate = row["delivery_date"].ToString();
            string settlementDate = ((DateTime)row["settlement_date"]).ToString("yyyy/MM/dd");
            string bank = row["account_payble"].ToString();
            string vat = row["vat"].ToString();
            string vat_rate = row["vat_rate"].ToString();
            string vat_amount = row["vat_amount"].ToString();
            string sub_total = row["sub_total"].ToString();
            string total = row["total"].ToString();
            string customer = row["honorific_title"].ToString();
            string name = row["name"].ToString();
            string sealPrint = row["seaal_print"].ToString();
            string TotalWeight = row["total_weight"].ToString();
            string TotalCount = row["total_count"].ToString();
            int types = (int)row["types1"];
            string kind = row["type"].ToString();
            int ClientCode = (int)row["code"];

            #endregion

            #region "納品書　表の部分"
            NpgsqlConnection conn2 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter2;
            conn2 = postgre.connection();
            //conn2.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str2 = "select * from delivery_calc where control_number =" + control + ";";
            adapter2 = new NpgsqlDataAdapter(sql_str2, conn2);
            adapter2.Fill(dt2);

            int a = dt2.Rows.Count;

            if (print >= 1)
            {
                a = a / (print + 1);
            }
            else { }

            for (int b = 1; b <= a; b++)
            {
                #region "再度開く"
                NpgsqlConnection conn5 = new NpgsqlConnection();
                NpgsqlDataAdapter adapter5;
                conn5 = postgre.connection();
                //conn5.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str5 = "select * from delivery_calc where control_number =" + control + " and record_number = " + b + ";";
                adapter5 = new NpgsqlDataAdapter(sql_str5, conn5);
                adapter5.Fill(dt5);

                int c = b - 1;
                DataRow row5;
                row5 = dt5.Rows[c];
                string weight = row5["weight"].ToString();
                string count = row5["count"].ToString();
                string remark = row5["remarks"].ToString();
                string unit_price = row5["unit_price"].ToString();
                string amount = row5["amount"].ToString();
                string category = row5["main_category_code"].ToString();
                string item = row5["item_code"].ToString();
                #endregion

                #region "大分類"
                NpgsqlConnection conn3 = new NpgsqlConnection();
                NpgsqlDataAdapter adapter3;
                conn3 = postgre.connection();
                //conn3.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str3 = "select * from main_category_m where main_category_code =" + category + ";";
                adapter3 = new NpgsqlDataAdapter(sql_str3, conn3);
                adapter3.Fill(dt3);

                conn3.Close();
                DataRow row3;
                row3 = dt3.Rows[c];
                string categoryName = row3["main_category_name"].ToString();
                #endregion
                #region "品名"
                NpgsqlConnection conn4 = new NpgsqlConnection();
                NpgsqlDataAdapter adapter4;
                conn4 = postgre.connection();
                //conn4.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str4 = "select * from item_m where item_code =" + item + ";";
                adapter4 = new NpgsqlDataAdapter(sql_str4, conn4);
                adapter4.Fill(dt4);
                conn4.Close();

                #endregion
                #region "表の中身"
                if (b == 1)
                {
                    DataRow row4;
                    row4 = dt4.Rows[0];
                    string itemName1 = row4["item_name"].ToString();
                    #region "納品書1行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 430));
                    e.Graphics.DrawString(itemName1, font, brush, new PointF(200, 430));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 430));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 430));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 430));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 430));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 430));
                    #endregion
                }
                else if (b == 2)
                {
                    DataRow row4;
                    row4 = dt4.Rows[1];
                    string itemName2 = row4["item_name"].ToString();
                    #region "納品書2行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 480));
                    e.Graphics.DrawString(itemName2, font, brush, new PointF(200, 480));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 480));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 480));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 480));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 480));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 480));
                    #endregion
                }
                else if (b == 3)
                {
                    DataRow row4;
                    row4 = dt4.Rows[2];
                    string itemName3 = row4["item_name"].ToString();
                    #region "納品書3行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 530));
                    e.Graphics.DrawString(itemName3, font, brush, new PointF(200, 530));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 530));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 530));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 530));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 530));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 530));
                    #endregion
                }
                if (b == 4)
                {
                    DataRow row4;
                    row4 = dt4.Rows[0];
                    string itemName4 = row4["item_name"].ToString();
                    #region "納品書4行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 580));
                    e.Graphics.DrawString(itemName4, font, brush, new PointF(200, 580));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 580));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 580));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 580));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 580));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 580));
                    #endregion
                }
                else if (b == 5)
                {
                    DataRow row4;
                    row4 = dt4.Rows[0];
                    string itemName5 = row4["item_name"].ToString();
                    #region "納品書5行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 630));
                    e.Graphics.DrawString(itemName5, font, brush, new PointF(200, 630));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 630));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 630));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 630));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 630));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 630));
                    #endregion
                }
                else if (b == 6)
                {
                    DataRow row4;
                    row4 = dt4.Rows[0];
                    string itemName6 = row4["item_name"].ToString();
                    #region "納品書6行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 680));
                    e.Graphics.DrawString(itemName6, font, brush, new PointF(200, 680));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 680));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 680));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 680));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 680));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 680));
                    #endregion
                }
                if (b == 7)
                {
                    DataRow row4;
                    row4 = dt4.Rows[0];
                    string itemName7 = row4["item_name"].ToString();
                    #region "納品書7行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 730));
                    e.Graphics.DrawString(itemName7, font, brush, new PointF(200, 730));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 730));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 730));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 730));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 730));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 730));
                    #endregion
                }
                else if (b == 8)
                {
                    DataRow row4;
                    row4 = dt4.Rows[0];
                    string itemName8 = row4["item_name"].ToString();
                    #region "納品書8行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 780));
                    e.Graphics.DrawString(itemName8, font, brush, new PointF(200, 780));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 780));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 780));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 780));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 780));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 780));
                    #endregion
                }
                else if (b == 9)
                {
                    DataRow row4;
                    row4 = dt4.Rows[0];
                    string itemName9 = row4["item_name"].ToString();
                    #region "納品書9行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 830));
                    e.Graphics.DrawString(itemName9, font, brush, new PointF(200, 830));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 830));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 830));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 830));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 830));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 830));
                    #endregion
                }
                if (b == 10)
                {
                    DataRow row4;
                    row4 = dt4.Rows[0];
                    string itemName10 = row4["item_name"].ToString();
                    #region "納品書10行目"
                    e.Graphics.DrawString(categoryName, font, brush, new PointF(30, 880));
                    e.Graphics.DrawString(itemName10, font, brush, new PointF(200, 880));
                    e.Graphics.DrawString(weight, font, brush, new PointF(330, 880));
                    e.Graphics.DrawString(count, font, brush, new PointF(490, 880));
                    e.Graphics.DrawString(unit_price, font, brush, new PointF(390, 880));
                    e.Graphics.DrawString(amount, font, brush, new PointF(550, 880));
                    e.Graphics.DrawString(remark, font, brush, new PointF(690, 880));
                    #endregion
                }
                else
                {

                }
                #endregion

            }
            #region "顧客情報"
            if (types == 0)
            {
                //NpgsqlConnection connA = new NpgsqlConnection();
                //NpgsqlDataAdapter adapterA;
                //connA = postgre.connection();
                ////connA.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                //string sql_antique = "select * from delivery_m where staff_code =" + staff_id + ";";
                //adapterA = new NpgsqlDataAdapter(sql_antique, connA);
                //adapterA.Fill(dt);

                //DataRow rowA;
                //rowA = dt.Rows[0];
                //string AntiqueNumber = rowA["antique_number"].ToString();

                NpgsqlDataAdapter adapter6;
                //conn6.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str6 = "select * from client_m where code = '" + ClientCode + "';";
                adapter6 = new NpgsqlDataAdapter(sql_str6, conn);
                adapter6.Fill(dt6);

                DataRow row6;
                row6 = dt6.Rows[0];
                string address = row6["address"].ToString();
                string phoneNumber = row6["phone_number"].ToString();
                string faxNumber = row6["fax_number"].ToString();
                string PostalCode1 = row6["postal_code1"].ToString();
                string PostalCode2 = row6["postal_code2"].ToString();
                #region"個人情報記入"
                e.Graphics.DrawString("〒" + PostalCode1 + PostalCode2, font, brush, new PointF(360, 80));
                e.Graphics.DrawString(address, font, brush, new PointF(360, 100));
                e.Graphics.DrawString("(TEL)" + phoneNumber, font, brush, new PointF(360, 120));
                e.Graphics.DrawString("(FAX)" + faxNumber, font, brush, new PointF(520, 120));
                #endregion
            }
            if (types == 1)
            {
                NpgsqlDataAdapter adapter6;
                //conn6.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                string sql_str6 = "select * from client_m where code = '" + ClientCode + "';";
                adapter6 = new NpgsqlDataAdapter(sql_str6, conn);
                adapter6.Fill(dt6);

                DataRow row6;
                row6 = dt6.Rows[0];
                string address = row6["address"].ToString();
                string phoneNumber = row6["phone_number"].ToString();
                string faxNumber = row6["fax_number"].ToString();
                string PostalCode1 = row6["postal_code1"].ToString();
                string PostalCode2 = row6["postal_code2"].ToString();
                #region"個人情報記入"
                e.Graphics.DrawString("〒" + PostalCode1 + PostalCode2, font, brush, new PointF(360, 80));
                e.Graphics.DrawString(address, font, brush, new PointF(360, 100));
                e.Graphics.DrawString("(TEL)" + phoneNumber, font, brush, new PointF(360, 120));
                e.Graphics.DrawString("(FAX)" + faxNumber, font, brush, new PointF(520, 120));
                #endregion
            }

            #endregion
            #endregion

            #region "タイトル"
            e.Graphics.DrawString("納品書", font, brush, new PointF(40, 120));
            e.Graphics.DrawString("管理番号", font, brush, new PointF(40, 220));
            e.Graphics.DrawString("注文日", font, brush, new PointF(540, 220));
            e.Graphics.DrawString(kind, font, brush, new PointF(540, 270));
            e.Graphics.DrawString("決済日", font, brush, new PointF(540, 320));
            e.Graphics.DrawString("印鑑を印刷する", font, brush, new PointF(350, 200));
            e.Graphics.DrawString("品名", font, brush, new PointF(60, 380));
            e.Graphics.DrawString("品物詳細", font, brush, new PointF(180, 380));
            e.Graphics.DrawString("重量", font, brush, new PointF(310, 380));
            e.Graphics.DrawString("単価", font, brush, new PointF(390, 380));
            e.Graphics.DrawString("数量", font, brush, new PointF(470, 380));
            e.Graphics.DrawString("金額", font, brush, new PointF(550, 380));
            e.Graphics.DrawString("備考", font, brush, new PointF(690, 380));
            e.Graphics.DrawString("総重量", font, brush, new PointF(200, 910));
            e.Graphics.DrawString("総数", font, brush, new PointF(390, 910));
            e.Graphics.DrawString("お支払方法", font, brush, new PointF(35, 950));
            e.Graphics.DrawString("振込先", font, brush, new PointF(50, 1050));
            e.Graphics.DrawString("小計", font, brush, new PointF(310, 950));
            e.Graphics.DrawString("消費税区分", font, brush, new PointF(310, 990));
            e.Graphics.DrawString("消費税率", font, brush, new PointF(310, 1030));
            e.Graphics.DrawString("消費税額", font, brush, new PointF(310, 1070));
            e.Graphics.DrawString("合計金額", font, brush, new PointF(310, 1110));
            #endregion
            #region "記入事項"
            e.Graphics.DrawString(name, font, brush, new PointF(70, 50));
            e.Graphics.DrawString(customer, font, brush, new PointF(260, 50));
            e.Graphics.DrawString(control.ToString(), font, brush, new PointF(200, 220));
            e.Graphics.DrawString(orderDate, font, brush, new PointF(620, 220));
            e.Graphics.DrawString(deliveryDate, font, brush, new PointF(620, 270));
            e.Graphics.DrawString(settlementDate, font, brush, new PointF(620, 320));
            e.Graphics.DrawString(method, font, brush, new PointF(180, 950));
            e.Graphics.DrawString(sub_total, font, brush, new PointF(690, 950));
            e.Graphics.DrawString(bank, font, brush, new PointF(180, 1050));
            e.Graphics.DrawString(vat, font, brush, new PointF(690, 990));
            e.Graphics.DrawString(vat_rate + "％", font, brush, new PointF(690, 1030));
            e.Graphics.DrawString(vat_amount, font, brush, new PointF(690, 1070));
            e.Graphics.DrawString(total, font, brush, new PointF(690, 1110));
            e.Graphics.DrawString(TotalWeight, font, brush, new PointF(330, 910));
            e.Graphics.DrawString(TotalCount, font, brush, new PointF(480, 910));
            if (sealPrint == "する")
            {
                e.Graphics.DrawString("✓", font1, brush, new PointF(360, 200));
            }
            else { }
            //e.Graphics.DrawString(remark, font, brush, new PointF(690, 430));

            #endregion
            Pen p = new Pen(Color.Black);
            e.Graphics.DrawRectangle(p, new Rectangle(20, 30, 200, 50)); // 座標(20,100)に幅200，高さ50の四角形
            e.Graphics.DrawRectangle(p, new Rectangle(220, 30, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(20, 100, 300, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(20, 200, 100, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(120, 200, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(380, 220, 30, 30));
            #region "注文日"
            e.Graphics.DrawRectangle(p, new Rectangle(600, 200, 170, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(600, 250, 170, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(600, 300, 170, 50));
            #endregion
            #region"枠"
            #region "見出し"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 370, 120, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 370, 150, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 370, 80, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 370, 80, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 370, 80, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 370, 80, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 370, 150, 30));
            #endregion
            #region "1行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 400, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 400, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 400, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 400, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 400, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 400, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 400, 150, 50));
            #endregion
            #region "2行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 450, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 450, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 450, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 450, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 450, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 450, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 450, 150, 50));
            #endregion
            #region "3行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 500, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 500, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 500, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 500, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 500, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 500, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 500, 150, 50));
            #endregion
            #region "4行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 550, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 550, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 550, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 550, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 550, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 550, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 550, 150, 50));
            #endregion
            #region "5行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 600, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 600, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 600, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 600, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 600, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 600, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 600, 150, 50));
            #endregion
            #region "6行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 650, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 650, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 650, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 650, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 650, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 650, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 650, 150, 50));
            #endregion
            #region "7行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 700, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 700, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 700, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 700, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 700, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 700, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 700, 150, 50));
            #endregion
            #region "8行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 750, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 750, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 750, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 750, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 750, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 750, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 750, 150, 50));
            #endregion
            #region "9行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 800, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 800, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 800, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 800, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 800, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 800, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 800, 150, 50));
            #endregion
            #region "10行目"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 850, 120, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 850, 150, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 850, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 850, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 850, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 850, 80, 50));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 850, 150, 50));
            #endregion
            #region "総数"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 900, 270, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 900, 80, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(370, 900, 80, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(450, 900, 80, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 900, 80, 30));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 900, 150, 30));
            #endregion
            #region　"お支払方法"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 930, 120, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 930, 150, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 930, 240, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(530, 930, 80, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 930, 150, 40));
            #endregion
            #region "振込先"
            e.Graphics.DrawRectangle(p, new Rectangle(20, 970, 120, 160));
            e.Graphics.DrawRectangle(p, new Rectangle(140, 970, 150, 160));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 970, 320, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 970, 150, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 1010, 320, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 1010, 150, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 1050, 320, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 1050, 150, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(290, 1090, 320, 40));
            e.Graphics.DrawRectangle(p, new Rectangle(610, 1090, 150, 40));
            #endregion
            #endregion
            #region "ロゴ"
            Image newImage = Image.FromFile(@"\\192.168.152.164\Flawless_test\Flawlessロゴ.png");
            Rectangle destRect = new Rectangle(350, 20, 400, 50);
            e.Graphics.DrawImage(newImage, destRect);
            #endregion
            #region "印鑑"
            Image newImage1 = Image.FromFile(@"\\192.168.152.164\Flawless_test\印鑑.png");
            Rectangle destRect2 = new Rectangle(580, -50, 300, 300);
            e.Graphics.DrawImage(newImage1, destRect2);
            #endregion
            print++;
        }
    }
}