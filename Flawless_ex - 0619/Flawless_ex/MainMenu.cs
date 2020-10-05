using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class MainMenu : Form　//メインメニュー
    {
        public int type;
        public string staff_name;
        public string address;
        TopMenu top = new TopMenu();
        public string access_auth;
        public int staff_id;
        public string data;
        public string slipNumber;
        decimal Total;
        public int control;
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
        public string Pass;
        public int grade;
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
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

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
            Statement statement = new Statement(this, staff_id, type, staff_name, address, access_auth, Total, Pass, slipNumber, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
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
