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
        NpgsqlTransaction transaction;
        NpgsqlDataReader reader;
        NpgsqlCommand cmd;

        int code;//品名コード
        int staff_code;//ログイン者のコード
        string Access_auth;
        string Pass;
        string ItemName;

        public ProductAddMenu(DataTable dt, MasterMaintenanceMenu master, int staff_code, string access_auth, string pass)
        {
            InitializeComponent();

            this.master = master;
            this.dt = dt;
            this.staff_code = staff_code;
            this.Access_auth = access_auth;
            this.Pass = pass;
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("登録をしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes && string.IsNullOrEmpty(productNameTextBox.Text))
            {
                MessageBox.Show("品名が未入力です。", "品名エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (result == DialogResult.Yes && !(string.IsNullOrEmpty(productNameTextBox.Text)))
            {
                NpgsqlCommandBuilder builder;

                string productName = productNameTextBox.Text;
                int mainCode = (int)mainCategoryComboBox.SelectedValue;
                DateTime dat = DateTime.Now;

                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                conn.Open();

                using (transaction = conn.BeginTransaction())
                {
                    string SQL = "select * from item_m;";
                    cmd = new NpgsqlCommand(SQL, conn);
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemName = reader["item_name"].ToString();
                            if (ItemName == productName)
                            {
                                MessageBox.Show("既に登録されている品名です", "登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                conn.Close();
                                productNameTextBox.Text = "";
                                return;
                            }
                        }
                    }
                }

                string sql_str = "insert into item_m values(" + mainCode + ", '" + productName + "', " + code + ", '" + dat + "', " + 0 + ",'" + staff_code + "')";

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);

                MessageBox.Show("品名の新規登録が完了しました", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) ;

                conn.Close();
                this.Close();
            }
        }

        private void ProductAddMenu_Load(object sender, EventArgs e)
        {
            string sql_main_name = "select* from main_category_m where invalid = 0 order by main_category_code";
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

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

        private void ProductAddMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            ItemMaster product = new ItemMaster(master, staff_code, Access_auth, Pass);
            product.Show();
        }
    }
}
