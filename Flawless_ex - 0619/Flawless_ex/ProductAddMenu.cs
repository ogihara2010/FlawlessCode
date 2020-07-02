using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class ProductAddMenu : Form　//品名新規登録メニュー
    {
        MasterMaintenanceMenu master;
        DataTable dt;
        DataTable maindt = new DataTable();//大分類
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter;
        int code;//品名コード
        int staff_code;//ログイン者のコード

        public ProductAddMenu(DataTable dt, MasterMaintenanceMenu master, int staff_code)
        {
            InitializeComponent();

            this.master = master;
            this.dt = dt;
            this.staff_code = staff_code;
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            ItemMaster product = new ItemMaster(master, staff_code);

            this.Close();
            product.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {


            NpgsqlCommandBuilder builder;

            string productName = productNameTextBox.Text;
            int mainCode = (int)mainCategoryComboBox.SelectedValue;
            DateTime dat = DateTime.Now;

            string sql_str = "insert into item_m values(" + mainCode + ", '" + productName + "', " + code + ", '" + dat + "', " + 0 + ")";

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            builder = new NpgsqlCommandBuilder(adapter);

            adapter.Fill(dt);
            adapter.Update(dt);

            MessageBox.Show("登録完了");

            //履歴
            string sql_item_m_revisions = "insert into item_m_revisions values(" + code + ", '" + dat + "', " + staff_code + ")";
            NpgsqlCommand cmd = new NpgsqlCommand(sql_item_m_revisions, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            conn.Close();

            ItemMaster product = new ItemMaster(master, staff_code);
            this.Close();
            product.Show();

        }

        private void ProductAddMenu_Load(object sender, EventArgs e)
        {
            string sql_main_name = "select* from main_category_m where invalid = 0 order by main_category_code";
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            conn.Open();

            //大分類
            adapter = new NpgsqlDataAdapter(sql_main_name, conn);
            adapter.Fill(maindt);

            //品名コード
            string item_sql = "select item_code from item_m order by item_code desc";
            DataTable itemCode = new DataTable();
            adapter = new NpgsqlDataAdapter(item_sql, conn);
            adapter.Fill(itemCode);

            conn.Close();

            mainCategoryComboBox.DataSource = maindt;
            mainCategoryComboBox.DisplayMember = "main_category_name";
            mainCategoryComboBox.ValueMember = "main_category_code";
            mainCategoryComboBox.SelectedIndex = 0;

            //品名コード最大値取得＆新規番号作成
            DataRow row;
            row = itemCode.Rows[0];
            code = (int)row["item_code"];
            code++;
            itemCodeTextBox.Text = code.ToString();

        }
    }
}
