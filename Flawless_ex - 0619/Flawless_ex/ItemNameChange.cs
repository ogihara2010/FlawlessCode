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
    public partial class ItemNameChange : Form
    {
        Statement statement;
        RecordList recordList;
        int Grade;
        int staff_id;
        string Staff_Name;
        string SlipNumber;
        int type;
        int ChangeCount;
        #region"品名変更時"
        string REASON;
        int ItemCategoryCode;
        int MainCategoryCode;
        #endregion
        DataTable dt = new DataTable();
        #region"品名変更時のデータテイブル"
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        DataTable dt9 = new DataTable();
        DataTable dt10 = new DataTable();
        DataTable dt11 = new DataTable();
        DataTable dt12 = new DataTable();
        DataTable dt13 = new DataTable();
        #endregion

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        NpgsqlDataAdapter adapter;
        NpgsqlTransaction transaction;

        public ItemNameChange(RecordList record, int grade, int staff_id, string slipNumber)
        {
            InitializeComponent();

            this.recordList = record;
            this.Grade = grade;
            this.staff_id = staff_id;
            this.SlipNumber = slipNumber;
        }

        #region"品名変更完了"
        private void ItemNameChangeButton_Click(object sender, EventArgs e)
        {
            #region"品名変更理由確認"
            if (!string.IsNullOrEmpty(BeforeChangeTextBox1.Text)&&string.IsNullOrEmpty(ChangeReasonTextBox1.Text))
            {
                MessageBox.Show("１行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox2.Text) && string.IsNullOrEmpty(ChangeReasonTextBox2.Text))
            {
                MessageBox.Show("２行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox3.Text) && string.IsNullOrEmpty(ChangeReasonTextBox3.Text))
            {
                MessageBox.Show("３行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox4.Text) && string.IsNullOrEmpty(ChangeReasonTextBox4.Text))
            {
                MessageBox.Show("４行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox5.Text) && string.IsNullOrEmpty(ChangeReasonTextBox5.Text))
            {
                MessageBox.Show("５行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox6.Text) && string.IsNullOrEmpty(ChangeReasonTextBox6.Text))
            {
                MessageBox.Show("６行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox7.Text) && string.IsNullOrEmpty(ChangeReasonTextBox7.Text))
            {
                MessageBox.Show("７行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox8.Text) && string.IsNullOrEmpty(ChangeReasonTextBox8.Text))
            {
                MessageBox.Show("８行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox9.Text) && string.IsNullOrEmpty(ChangeReasonTextBox9.Text))
            {
                MessageBox.Show("９行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox10.Text) && string.IsNullOrEmpty(ChangeReasonTextBox10.Text))
            {
                MessageBox.Show("１０行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox11.Text) && string.IsNullOrEmpty(ChangeReasonTextBox11.Text))
            {
                MessageBox.Show("１１行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox12.Text) && string.IsNullOrEmpty(ChangeReasonTextBox12.Text))
            {
                MessageBox.Show("１２行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(BeforeChangeTextBox13.Text) && string.IsNullOrEmpty(ChangeReasonTextBox13.Text))
            {
                MessageBox.Show("１３行目の品名を変更する理由を記入してください", "記入漏れ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();
            string SQL = "";

            for (int i = 0; i < ChangeCount; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 0:
                        ItemCategoryCode = (int)AfterChangeComboBox1.SelectedValue;
                        REASON = ChangeReasonTextBox1.Text;
                        SQL = "select main_category_code from item_m where item_code = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "',item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 1 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"２行目"
                    case 1:
                        ItemCategoryCode = (int)AfterChangeComboBox2.SelectedValue;
                        REASON = ChangeReasonTextBox2.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 2 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"３行目"
                    case 2:
                        ItemCategoryCode = (int)AfterChangeComboBox3.SelectedValue;
                        REASON = ChangeReasonTextBox3.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 3 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"４行目"
                    case 3:
                        ItemCategoryCode = (int)AfterChangeComboBox4.SelectedValue;
                        REASON = ChangeReasonTextBox4.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 4 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"５行目"
                    case 4:
                        ItemCategoryCode = (int)AfterChangeComboBox5.SelectedValue;
                        REASON = ChangeReasonTextBox5.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 5 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"６行目"
                    case 5:
                        ItemCategoryCode = (int)AfterChangeComboBox6.SelectedValue;
                        REASON = ChangeReasonTextBox6.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 6 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"７行目"
                    case 6:
                        ItemCategoryCode = (int)AfterChangeComboBox7.SelectedValue;
                        REASON = ChangeReasonTextBox7.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 7 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"８行目"
                    case 7:
                        ItemCategoryCode = (int)AfterChangeComboBox8.SelectedValue;
                        REASON = ChangeReasonTextBox8.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 8 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"９行目"
                    case 8:
                        ItemCategoryCode = (int)AfterChangeComboBox9.SelectedValue;
                        REASON = ChangeReasonTextBox9.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 9 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"１０行目"
                    case 9:
                        ItemCategoryCode = (int)AfterChangeComboBox10.SelectedValue;
                        REASON = ChangeReasonTextBox10.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 10 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"１１行目"
                    case 10:
                        ItemCategoryCode = (int)AfterChangeComboBox11.SelectedValue;
                        REASON = ChangeReasonTextBox11.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 11 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"１２行目"
                    case 11:
                        ItemCategoryCode = (int)AfterChangeComboBox12.SelectedValue;
                        REASON = ChangeReasonTextBox12.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 12 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                    #region"１３行目"
                    case 12:
                        ItemCategoryCode = (int)AfterChangeComboBox13.SelectedValue;
                        REASON = ChangeReasonTextBox13.Text;
                        SQL = "select main_category_code from item_m where item_m = '" + ItemCategoryCode + "';";
                        cmd = new NpgsqlCommand(SQL, conn);
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainCategoryCode = (int)reader["main_category_code"];
                            }
                        }

                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + 13 + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        break;
                    #endregion
                }
            }
            MessageBox.Show("品名を変更しました", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();

            recordList = new RecordList(statement, staff_id, Staff_Name, type, SlipNumber, Grade);
            this.Close();
            recordList.Show();
        }
        #endregion

        #region"戻る"
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            recordList = new RecordList(statement, staff_id, Staff_Name, type, SlipNumber, Grade);

            this.Close();
            recordList.Show();
        }

        private void ItemNameChange_FormClosed(object sender, FormClosedEventArgs e)
        {
            recordList = new RecordList(statement, staff_id, Staff_Name, type, SlipNumber, Grade);

            this.Close();
            recordList.Show();
        }
        #endregion

        private void ItemNameChange_Load(object sender, EventArgs e)
        {
            SlipNumberTextBox.Text = SlipNumber;
            GradeNumberTextBox.Text = Grade.ToString();
            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            conn.Open();

            string SQL = "select * from staff_m where staff_code = '" + staff_id + "';";
            cmd = new NpgsqlCommand(SQL, conn);
            string StaffName = "";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    StaffName = reader["staff_name"].ToString();
                }
            }
            StaffNameTextBox.Text = StaffName;

            string sql_str = "select count(*) from list_result2 where document_number = 'F" + Grade + "' and item_name_change = '1';";
            cmd = new NpgsqlCommand(sql_str, conn);

            //成績入力画面でチェックした変更する品名数取得
            ChangeCount = int.Parse(cmd.ExecuteScalar().ToString());        //カウントは１から
            MessageBox.Show(ChangeCount.ToString());

            #region"左の表"
            sql_str = "select * from list_result2 inner join item_m on item_m.item_code = list_result2.item_code where document_number = '" + SlipNumber + "';";
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            
            for (int i = 0; i < ChangeCount; i++) 
            {
                DataRow row = dt.Rows[i];

                switch(i){
                    case 0:
                        BeforeChangeTextBox1.Text = row["item_name"].ToString();
                        break;
                    case 1:
                        BeforeChangeTextBox2.Text = row["item_name"].ToString();
                        break;
                    case 2:
                        BeforeChangeTextBox3.Text = row["item_name"].ToString();
                        break;
                    case 3:
                        BeforeChangeTextBox4.Text = row["item_name"].ToString();
                        break;
                    case 4:
                        BeforeChangeTextBox5.Text = row["item_name"].ToString();
                        break;
                    case 5:
                        BeforeChangeTextBox6.Text = row["item_name"].ToString();
                        break;
                    case 6:
                        BeforeChangeTextBox7.Text = row["item_name"].ToString();
                        break;
                    case 7:
                        BeforeChangeTextBox8.Text = row["item_name"].ToString();
                        break;
                    case 8:
                        BeforeChangeTextBox9.Text = row["item_name"].ToString();
                        break;
                    case 9:
                        BeforeChangeTextBox10.Text = row["item_name"].ToString();
                        break;
                    case 10:
                        BeforeChangeTextBox11.Text = row["item_name"].ToString();
                        break;
                    case 11:
                        BeforeChangeTextBox12.Text = row["item_name"].ToString();
                        break;
                    case 12:
                        BeforeChangeTextBox13.Text = row["item_name"].ToString();
                        break;
                }
            }
            #endregion

            #region"右の表"

            #region"品名出力"
            sql_str = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code;";
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt1);
            for (int i = 0; i < ChangeCount; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 0:
                        AfterChangeComboBox1.DataSource = dt1;
                        AfterChangeComboBox1.DisplayMember = "item_name";
                        AfterChangeComboBox1.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"２行目"
                    case 1:
                        dt2 = dt1.Copy();
                        AfterChangeComboBox2.DataSource = dt2;
                        AfterChangeComboBox2.DisplayMember = "item_name";
                        AfterChangeComboBox2.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"３行目"
                    case 2:
                        dt3 = dt1.Copy();
                        AfterChangeComboBox3.DataSource = dt3;
                        AfterChangeComboBox3.DisplayMember = "item_name";
                        AfterChangeComboBox3.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"４行目"
                    case 3:
                        dt4 = dt1.Copy();
                        AfterChangeComboBox4.DataSource = dt4;
                        AfterChangeComboBox4.DisplayMember = "item_name";
                        AfterChangeComboBox4.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"５行目"
                    case 4:
                        dt5 = dt1.Copy();
                        AfterChangeComboBox5.DataSource = dt5;
                        AfterChangeComboBox5.DisplayMember = "item_name";
                        AfterChangeComboBox5.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"６行目"
                    case 5:
                        dt6 = dt1.Copy();
                        AfterChangeComboBox6.DataSource = dt6;
                        AfterChangeComboBox6.DisplayMember = "item_name";
                        AfterChangeComboBox6.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"７行目"
                    case 6:
                        dt7 = dt1.Copy();
                        AfterChangeComboBox7.DataSource = dt7;
                        AfterChangeComboBox7.DisplayMember = "item_name";
                        AfterChangeComboBox7.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"８行目"
                    case 7:
                        dt8 = dt1.Copy();
                        AfterChangeComboBox8.DataSource = dt8;
                        AfterChangeComboBox8.DisplayMember = "item_name";
                        AfterChangeComboBox8.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"９行目"
                    case 8:
                        dt9 = dt1.Copy();
                        AfterChangeComboBox9.DataSource = dt9;
                        AfterChangeComboBox9.DisplayMember = "item_name";
                        AfterChangeComboBox9.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"１０行目"
                    case 9:
                        dt10 = dt1.Copy();
                        AfterChangeComboBox10.DataSource = dt10;
                        AfterChangeComboBox10.DisplayMember = "item_name";
                        AfterChangeComboBox10.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"１１行目"
                    case 10:
                        dt11 = dt1.Copy();
                        AfterChangeComboBox11.DataSource = dt11;
                        AfterChangeComboBox11.DisplayMember = "item_name";
                        AfterChangeComboBox11.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"１２行目"
                    case 11:
                        dt12 = dt1.Copy();
                        AfterChangeComboBox12.DataSource = dt12;
                        AfterChangeComboBox12.DisplayMember = "item_name";
                        AfterChangeComboBox12.ValueMember = "item_code";
                        break;
                    #endregion
                    #region"１３行目"
                    case 12:
                        dt13 = dt1.Copy();
                        AfterChangeComboBox13.DataSource = dt13;
                        AfterChangeComboBox13.DisplayMember = "item_name";
                        AfterChangeComboBox13.ValueMember = "item_code";
                        break;
                        #endregion
                }
            }
            #endregion

            #endregion

            conn.Close();
        }
    }
}
