using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
namespace Flawless_ex
{
    public partial class ProductChangeDeleteMenu : Form //品名削除・更新メニュー
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter;
        NpgsqlCommandBuilder builder;
        ItemMaster productNameMenu;
        MasterMaintenanceMenu master;
        int puroductCode;
        DataTable dt;
        int staff_code;//ログイン者の担当者コード
        int mCode;//大分類コード履歴
        string item_name;//品名履歴

        public ProductChangeDeleteMenu(ItemMaster nameMenu, MasterMaintenanceMenu master, int code, int staff_code)
        {
            InitializeComponent();
            this.productNameMenu = nameMenu;
            this.master = master;
            puroductCode = code;
            this.staff_code = staff_code;
        }

        private void ProductChangeDeleteMenu_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            productCodeTextBox.Text = puroductCode.ToString();

            string sql_item = "select* from item_m where item_code =" + puroductCode + "";
            DataTable itemdt = new DataTable();

            string sql_mainCategory = "select* from main_category_m";
            DataTable maindt = new DataTable();

            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_item, conn);
            adapter.Fill(itemdt);

            adapter = new NpgsqlDataAdapter(sql_mainCategory, conn);
            adapter.Fill(maindt);

            conn.Close();

            DataRow row;
            row = itemdt.Rows[0];
            item_name = row["item_name"].ToString();
            productNameTextBox.Text = item_name;

            mCode = (int)row["main_category_code"];

            mainCategoryComboBox.DataSource = maindt;
            mainCategoryComboBox.DisplayMember = "main_category_name";
            mainCategoryComboBox.ValueMember = "main_category_code";
            mainCategoryComboBox.SelectedIndex = 0;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            ItemMaster productNameMenu = new ItemMaster(master, staff_code);
            this.Close();
            productNameMenu.Show();
        }

        private void removeButton_Click(object sender, EventArgs e)//無効
        {
            DialogResult result = MessageBox.Show("無効をしますか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DataTable item_mdt = new DataTable();
                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                string remove_sql = "update item_m set invalid = 1 where item_code = " + puroductCode + "";
                adapter = new NpgsqlDataAdapter(remove_sql, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(item_mdt);
                adapter.Update(item_mdt);

                //履歴
                DateTime dat = DateTime.Now;
                string sql_remove_invalid = "insert into item_m_invalid_revisions values(" + puroductCode + ", '" + dat + "', " + staff_code + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(sql_remove_invalid, conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                conn.Close();
                MessageBox.Show("無効完了");
                ItemMaster itemMaster = new ItemMaster(master, staff_code);
                this.Close();
                itemMaster.Show();
            }
            else { }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {


            DialogResult result = MessageBox.Show("更新をしますか？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                DateTime dat = DateTime.Now;
                string iname = productNameTextBox.Text;//品名
                int mcode = (int)mainCategoryComboBox.SelectedValue;//大分類コード
                DataTable dt = new DataTable();

                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                string sql_item_m = "update item_m set main_category_code = " + mcode + ", item_name = '" + iname + "' where item_code =" + puroductCode + "";
                adapter = new NpgsqlDataAdapter(sql_item_m, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);
                MessageBox.Show("更新完了");

                //履歴
                //大分類コード履歴
                if ((int)mainCategoryComboBox.SelectedValue != mCode)
                {
                    string sql_item_mCode_revisions = "insert into item_m_main_category_code_revisions values(" + mCode + ", " + mcode + "," + puroductCode + ",'" + dat + "', " + staff_code + ")";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql_item_mCode_revisions, conn);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                }
                //品名履歴
                else if (productNameTextBox.Text != item_name)
                {
                    string sql_itemName = "insert into item_m_item_name_revisions values('" + iname + "', '" + item_name + "', " + puroductCode + ", '" + dat + "', " + staff_code + ")";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql_itemName, conn);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                }

            }
            else { }
            conn.Close();

            ItemMaster itemMaster = new ItemMaster(master, staff_code);
            this.Close();
            itemMaster.Show();

        }
    }
}
