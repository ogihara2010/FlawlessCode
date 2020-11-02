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
        NpgsqlCommand cmd;
        NpgsqlTransaction transaction;

        ItemMaster productNameMenu;
        MasterMaintenanceMenu master;
        public int puroductCode;
        DataTable dt;
        public int staff_code;//ログイン者の担当者コード
        public int mCode;//大分類コード履歴
        public string item_name;//品名履歴
        public string Access_auth;
        public string Pass;
        public int MainCode;
        public string reason;
        string mName;           //画面遷移した時の大分類名
        string mNameAfter;      //変更した大分類名


        public ProductChangeDeleteMenu(ItemMaster nameMenu, MasterMaintenanceMenu master, int code, int staff_code, string access_auth, string pass, int maincode)
        {
            InitializeComponent();
            this.productNameMenu = nameMenu;
            this.master = master;
            puroductCode = code;
            this.staff_code = staff_code;
            this.Access_auth = access_auth;
            this.Pass = pass;
            this.MainCode = maincode;
        }

        private void ProductChangeDeleteMenu_Load(object sender, EventArgs e)   //画面起動時
        {
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            
            productCodeTextBox.Text = puroductCode.ToString();

            string sql_item = "select * from item_m where item_code ='" + puroductCode + "';";
            DataTable itemdt = new DataTable();

            string sql_mainCategory = "select * from main_category_m order by main_category_code = '" + MainCode + "' desc, main_category_code asc;";
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

            DataRow row1 = maindt.Rows[0];
            mName = row1["main_category_name"].ToString();

            mainCategoryComboBox.DataSource = maindt;
            mainCategoryComboBox.DisplayMember = "main_category_name";
            mainCategoryComboBox.ValueMember = "main_category_code";
            mainCategoryComboBox.SelectedIndex = 0;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            this.Close();
        }

        private void removeButton_Click(object sender, EventArgs e)//無効
        {
            if (string.IsNullOrEmpty(reasonText.Text))
            {
                MessageBox.Show("理由を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult result = MessageBox.Show("無効にしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            // Yes かつ 理由あり
            DataTable item_mdt = new DataTable();

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();

            using (transaction = conn.BeginTransaction())
            {
                string remove_sql = "update item_m set invalid = 1 where item_code = " + puroductCode + ";";
                cmd = new NpgsqlCommand(remove_sql, conn);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }

            //履歴
            reason = reasonText.Text;
            DateTime dat = DateTime.Now;
            string sql_remove_invalid = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code)" +
                " values(" + 2 + ", '" + dat + "'," + staff_code + ", '" + "有効" + "', '" + "無効" + "','" + reason + "', '" + puroductCode + "');";
            cmd = new NpgsqlCommand(sql_remove_invalid, conn);
            cmd.ExecuteNonQuery();
            
            conn.Close();
            MessageBox.Show("選択した品名を無効にしました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); ;
            this.Close();
        }

        private void updateButton_Click(object sender, EventArgs e)     //更新時
        {
            if (string.IsNullOrEmpty(productNameTextBox.Text))
            {
                MessageBox.Show("品名を入力してください。", "品名未入力", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(reasonText.Text))
            {
                MessageBox.Show("理由を入力してください。", "理由未入力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            DateTime dat = DateTime.Now;
            string iname = productNameTextBox.Text;//品名
            int mcode = (int)mainCategoryComboBox.SelectedValue;//大分類コード
            mNameAfter = mainCategoryComboBox.Text;             //大分類名

            DataTable dt = new DataTable();
            reason = reasonText.Text;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            
            conn.Open();

            //大分類コード履歴と品名履歴
            if ((MainCode != mcode) && (productNameTextBox.Text != item_name))
            {
                //大分類・品名両方変更
                string sql_item_mCode_revisions = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code)" +
                    "values(" + 2 + ",'" + dat + "'," + staff_code + ",'" + mName + "', '" + mNameAfter + "','" + reason + "', '" + puroductCode + "');";
                cmd = new NpgsqlCommand(sql_item_mCode_revisions, conn);
                cmd.ExecuteNonQuery();

                string sql_itemName = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code)" +
                    "values(" + 2 + ",'" + dat + "'," + staff_code + ",'" + item_name + "', '" + iname + "', '" + reason + "','" + puroductCode + "');";
                cmd = new NpgsqlCommand(sql_itemName, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("大分類名・品名を変更しました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((MainCode == mcode) && (productNameTextBox.Text != item_name))
            {
                ////品名のみ変更 or 再入力
                //DialogResult dialogResult = MessageBox.Show("大分類名が変更されておりません" + "\r\n" + "大分類名を変更しないでもよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dialogResult == DialogResult.Yes)
                //{
                //MessageBox.Show("品名のみ変更します", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                string sql_itemName = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code)" +
                    " values(" + 2 + ",'" + dat + "'," + staff_code + ",'" + item_name + "', '" + iname + "','" + reason + "', '" + puroductCode + "');";
                cmd = new NpgsqlCommand(sql_itemName, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("品名を変更しました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //}

                //if (dialogResult == DialogResult.No)
                //{
                //    MessageBox.Show("大分類名を変更してください", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    conn.Close();
                //    return;
                //}
            }
            else if ((MainCode != mcode) && (productNameTextBox.Text == item_name))
            {
                //大分類名のみ変更
                //DialogResult dialogResult = MessageBox.Show("品名が変更されておりません" + "\r\n" + "品名を変更しないでもよろしいですか？", "入力エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                //if (dialogResult == DialogResult.Yes)
                //{
                //    MessageBox.Show("大分類名のみ変更します", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //例：ダイヤ->地金                    
                string sql_item_mCode_revisions = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code)" +
                    " values(" + 2 + ",'" + dat + "','" + staff_code + "','" + mName + "', '" + mNameAfter + "','" + reason + "','" + puroductCode + "');";
                cmd = new NpgsqlCommand(sql_item_mCode_revisions, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("大分類名を変更しました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                
                //}
                ////if (dialogResult == DialogResult.No)
                ////{
                //    MessageBox.Show("品名を変更してください", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    conn.Close();
                //    return;
                //}
            }
            else
            {
                MessageBox.Show("変更・無効画面に戻ります", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conn.Close();
                return;
            }

            using (transaction = conn.BeginTransaction())
            {
                string sql_item_m = "update item_m set main_category_code = " + mcode + ", item_name = '" + iname + "' where item_code =" + puroductCode + "";
                cmd = new NpgsqlCommand(sql_item_m, conn);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }

            MessageBox.Show("品名マスタ画面に戻ります", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            conn.Close();
            this.Close();
        }

        private void ProductChangeDeleteMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            ItemMaster productNameMenu = new ItemMaster(master, staff_code, Access_auth, Pass);
            productNameMenu.Show();
        }
    }
}
