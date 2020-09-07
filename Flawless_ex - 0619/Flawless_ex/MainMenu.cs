using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class MainMenu : Form　//メインメニュー
    {
        int type;
        string staff_name;
        string address;
        TopMenu top = new TopMenu();
        string access_auth;
        int staff_id;
        string data;
        string slipNumber;
        decimal Total;
        int control;
        #region "買取販売履歴"
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
        string Pass;
        int grade;
        #region "納品書の引数"
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
        #region "計算書の引数"
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
        bool CarryOver;
        bool MonthCatalog;
        bool screan = true;

        NpgsqlConnection conn;
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;

        public MainMenu(TopMenu topMenu, int id, string pass, string access_auth)
        {
            InitializeComponent();

            this.access_auth = access_auth;
            staff_id = id;
            this.Pass = pass;
            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MasterMainte_Click(object sender, EventArgs e)//権限によって
        {
            MasterMaintenanceMenu masterMenu = new MasterMaintenanceMenu(this, staff_id, access_auth, Pass);
            screan = false;
            this.Close();
            masterMenu.Show();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection();
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str2 = "select* from staff_m where staff_code = " + staff_id + " and password = '" + Pass + "'";
            cmd = new NpgsqlCommand(sql_str2, conn);
            conn.Open();
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    label1.Text = (string.Format("{0}:{1}", reader["staff_code"].ToString(), reader["staff_name"]));
                }
            }
            conn.Close();

        }
        #region "計算書・納品書"
        private void Statement_DeliveryButton_Click(object sender, EventArgs e)
        {
            Statement statement = new Statement(this, staff_id, type, staff_name, address, access_auth, Total, Pass, slipNumber, control, data, search1, search2, search3, search4, search5, search6, search7, search8, search9, search10, search11, search12, amount00, amount01, amount02, amount03, amount04, amount05, amount06, amount07, amount08, amount09, amount010, amount011, amount012, amount10, amount11, amount12, amount13, amount14, amount15, amount16, amount17, amount18, amount19, amount110, amount111, amount112, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
            screan = false;
            this.Close();
            statement.Show();
        }
        #endregion
        #region"操作履歴ボタン"
        private void Operatelog_Click(object sender, EventArgs e)
        {
            Operatelog operatelog = new Operatelog(this, staff_id, access_auth, Pass);
            screan = false;
            this.Close();
            operatelog.Show();
        }
        #endregion
        #region "買取販売データ検索ボタン"
        private void CustomerHistoriButton_Click(object sender, EventArgs e)
        {
            CustomerHistorySelect customerHistorySelect = new CustomerHistorySelect(this, staff_id, data, Pass, access_auth);
            screan = false;
            this.Close();
            customerHistorySelect.Show();
        }
        #endregion
        #region "月間成績表一覧"
        private void MonResults_Click(object sender, EventArgs e)
        {
            MonResult monresult = new MonResult(this, staff_id, access_auth, staff_name, type, slipNumber, Pass, grade, CarryOver, MonthCatalog);
            screan = false;
            this.Close();
            monresult.Show();
        }
        #endregion
        #region"未入力・次月持ち越し"
        private void Button3_Click(object sender, EventArgs e)
        {
            NextMonth nextmonth = new NextMonth(this, staff_id, Pass, access_auth);
            screan = false;
            this.Close();
            nextmonth.Show();
        }
        #endregion

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                Application.Exit();
            }
            else
            {
                screan = true;
            }
        }
    }
}
