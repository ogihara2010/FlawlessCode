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
        int antique;
        int id;
        string Access_auth;
        string Pass;
        bool notLoad = false;
        bool NameChange;
        bool CarryOver;
        bool MonthCatalog;

        string Registration;
        DateTime date;

        #region"変更前の品名コード"
        int BeforeItemCode1;
        int BeforeItemCode2;
        int BeforeItemCode3;
        int BeforeItemCode4;
        int BeforeItemCode5;
        int BeforeItemCode6;
        int BeforeItemCode7;
        int BeforeItemCode8;
        int BeforeItemCode9;
        int BeforeItemCode10;
        int BeforeItemCode11;
        int BeforeItemCode12;
        int BeforeItemCode13;
        #endregion
        #region"品名変更時"
        string REASON;
        int ItemCategoryCode;
        int MainCategoryCode;
        int BeforeItemCode;
        #region"DBに入っている行数"
        int record1;
        int record2;
        int record3;
        int record4;
        int record5;
        int record6;
        int record7;
        int record8;
        int record9;
        int record10;
        int record11;
        int record12;
        int record13;
        #endregion
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
        #region"大分類名のデータテーブル"
        DataTable table1 = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();
        DataTable table4 = new DataTable();
        DataTable table5 = new DataTable();
        DataTable table6 = new DataTable();
        DataTable table7 = new DataTable();
        DataTable table8 = new DataTable();
        DataTable table9 = new DataTable();
        DataTable table10 = new DataTable();
        DataTable table11 = new DataTable();
        DataTable table12 = new DataTable();
        DataTable table13 = new DataTable();
        #endregion
        DataTable data = new DataTable();
        #region"大分類名から品名の絞り込み"
        DataTable data1 = new DataTable();
        DataTable data2 = new DataTable();
        DataTable data3 = new DataTable();
        DataTable data4 = new DataTable();
        DataTable data5 = new DataTable();
        DataTable data6 = new DataTable();
        DataTable data7 = new DataTable();
        DataTable data8 = new DataTable();
        DataTable data9 = new DataTable();
        DataTable data10 = new DataTable();
        DataTable data11 = new DataTable();
        DataTable data12 = new DataTable();
        DataTable data13 = new DataTable();
        #endregion
        #region"品名変更の履歴を登録する用のデータテイブル"
        DataTable dataTable1 = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        DataTable dataTable4 = new DataTable();
        DataTable dataTable5 = new DataTable();
        DataTable dataTable6 = new DataTable();
        DataTable dataTable7 = new DataTable();
        DataTable dataTable8 = new DataTable();
        DataTable dataTable9 = new DataTable();
        DataTable dataTable10 = new DataTable();
        DataTable dataTable11 = new DataTable();
        DataTable dataTable12 = new DataTable();
        DataTable dataTable13 = new DataTable();
        #endregion
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        NpgsqlDataAdapter adapter;
        NpgsqlTransaction transaction;

        public ItemNameChange(RecordList record, int grade, int staff_id, string slipNumber, string pass, string access_auth, bool nameChange, bool carryOver, bool monthCatalog)
        {
            InitializeComponent();

            this.recordList = record;
            this.Grade = grade;
            this.staff_id = staff_id;
            this.SlipNumber = slipNumber;
            this.Pass = pass;
            this.Access_auth = access_auth;
            this.NameChange = nameChange;
            this.CarryOver = carryOver;
            this.MonthCatalog = monthCatalog;
        }

        #region"品名変更ボタン"
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

            date = DateTime.Now;
            //Registration = date.ToLongDateString();

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();
            string SQL = "";
            string sql = "";

            for (int i = 0; i < ChangeCount; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 0:
                        BeforeItemCode = BeforeItemCode1;
                        MainCategoryCode = (int)MainCategoryComboBox1.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox1.SelectedValue;
                        REASON = ChangeReasonTextBox1.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "',item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record1 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record1 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable1);

                        break;
                    #endregion
                    #region"２行目"
                    case 1:
                        BeforeItemCode = BeforeItemCode2;
                        MainCategoryCode = (int)MainCategoryComboBox2.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox2.SelectedValue;
                        REASON = ChangeReasonTextBox2.Text;
                       
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + record2 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record2 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable2);

                        break;
                    #endregion
                    #region"３行目"
                    case 2:
                        BeforeItemCode = BeforeItemCode3;
                        MainCategoryCode = (int)MainCategoryComboBox3.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox3.SelectedValue;
                        REASON = ChangeReasonTextBox3.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + record3 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record3 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable3);

                        break;
                    #endregion
                    #region"４行目"
                    case 3:
                        BeforeItemCode = BeforeItemCode4;
                        MainCategoryCode = (int)MainCategoryComboBox4.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox4.SelectedValue;
                        REASON = ChangeReasonTextBox4.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "' where record_number = '" + record4 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record4 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable4);

                        break;
                    #endregion
                    #region"５行目"
                    case 4:
                        BeforeItemCode = BeforeItemCode5;
                        MainCategoryCode = (int)MainCategoryComboBox5.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox5.SelectedValue;
                        REASON = ChangeReasonTextBox5.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record5 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record5 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable5);

                        break;
                    #endregion
                    #region"６行目"
                    case 5:
                        BeforeItemCode = BeforeItemCode6;
                        MainCategoryCode = (int)MainCategoryComboBox6.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox6.SelectedValue;
                        REASON = ChangeReasonTextBox6.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record6 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record6 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable6);

                        break;
                    #endregion
                    #region"７行目"
                    case 6:
                        BeforeItemCode = BeforeItemCode7;
                        MainCategoryCode = (int)MainCategoryComboBox7.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox7.SelectedValue;
                        REASON = ChangeReasonTextBox7.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record7 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record7 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable7);

                        break;
                    #endregion
                    #region"８行目"
                    case 7:
                        BeforeItemCode = BeforeItemCode8;
                        MainCategoryCode = (int)MainCategoryComboBox8.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox8.SelectedValue;
                        REASON = ChangeReasonTextBox8.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record8 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record8 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable8);

                        break;
                    #endregion
                    #region"９行目"
                    case 8:
                        BeforeItemCode = BeforeItemCode9;
                        MainCategoryCode = (int)MainCategoryComboBox9.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox9.SelectedValue;
                        REASON = ChangeReasonTextBox9.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record9 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record9 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable9);

                        break;
                    #endregion
                    #region"１０行目"
                    case 9:
                        BeforeItemCode = BeforeItemCode10;
                        MainCategoryCode = (int)MainCategoryComboBox10.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox10.SelectedValue;
                        REASON = ChangeReasonTextBox10.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record10 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record10 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable10);

                        break;
                    #endregion
                    #region"１１行目"
                    case 10:
                        BeforeItemCode = BeforeItemCode11;
                        MainCategoryCode = (int)MainCategoryComboBox11.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox11.SelectedValue;
                        REASON = ChangeReasonTextBox11.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record11 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record11 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable11);

                        break;
                    #endregion
                    #region"１２行目"
                    case 11:
                        BeforeItemCode = BeforeItemCode12;
                        MainCategoryCode = (int)MainCategoryComboBox12.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox12.SelectedValue;
                        REASON = ChangeReasonTextBox12.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record12 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record12 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable12);

                        break;
                    #endregion
                    #region"１３行目"
                    case 12:
                        BeforeItemCode = BeforeItemCode13;
                        MainCategoryCode = (int)MainCategoryComboBox13.SelectedValue;
                        ItemCategoryCode = (int)AfterChangeComboBox13.SelectedValue;
                        REASON = ChangeReasonTextBox13.Text;
                        
                        using (transaction = conn.BeginTransaction())
                        {
                            SQL = @"update list_result2 set main_category_code = '" + MainCategoryCode + "', item_code = '" + ItemCategoryCode + "', reason = '" + REASON + "'" +
                                " where record_number = '" + record13 + "' and grade_number = '" + Grade + "';";
                            cmd = new NpgsqlCommand(SQL, conn);
                            cmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        //履歴用の表に登録
                        sql = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code, record)" +
                            " values('" + 7 + "','" + date + "','" + staff_id + "','" + BeforeItemCode + "','" + ItemCategoryCode + "','" + REASON + "', '" + Grade + "','" + record13 + "');";
                        adapter = new NpgsqlDataAdapter(sql, conn);
                        adapter.Fill(dataTable13);

                        break;
                    #endregion
                }
            }
            MessageBox.Show("品名を変更しました。"+"\r\n"+"成績入力画面にてデータを登録してください", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conn.Close();
            NameChange = true;

            this.Close();
        }
        #endregion

        #region"戻る"
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            //戻るときに品名変更理由をDBに更新するかも？
            this.Close();
        }

        private void ItemNameChange_FormClosed(object sender, FormClosedEventArgs e)
        {
            recordList = new RecordList(statement, staff_id, Staff_Name, type, SlipNumber, Grade, antique, id, Access_auth, Pass, NameChange, CarryOver, MonthCatalog);
            recordList.Show();
        }
        #endregion

        private void ItemNameChange_Load(object sender, EventArgs e)
        {
            SlipNumberTextBox.Text = SlipNumber;
            GradeNumberTextBox.Text = Grade.ToString();
            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            conn.Open();

            string SQL = "select * from staff_m where staff_code = '" + staff_id + "';";
            cmd = new NpgsqlCommand(SQL, conn);
            string StaffName = "";
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    StaffName = reader["staff_name"].ToString();
                    MainCategoryCode = (int)reader["main_category_code"];
                }
            }
            StaffNameTextBox.Text = StaffName;

            string sql_str = "select count(*) from list_result2 where document_number = '" + SlipNumber + "' and item_name_change = '1';";
            cmd = new NpgsqlCommand(sql_str, conn);

            //成績入力画面でチェックした変更する品名数取得
            ChangeCount = int.Parse(cmd.ExecuteScalar().ToString());        //カウントは１から

            #region"左の表"
            sql_str = "select * from list_result2 inner join item_m on item_m.item_code = list_result2.item_code where document_number = '" + SlipNumber + "' and item_name_change = 1 order by record_number;";
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            
            for (int i = 0; i < ChangeCount; i++) 
            {
                DataRow row = dt.Rows[i];

                switch(i){
                    case 0:
                        BeforeItemCode1 = (int)row["item_code"];
                        BeforeChangeTextBox1.Text = row["item_name"].ToString();
                        record1 = (int)row["record_number"];
                        break;
                    case 1:
                        BeforeItemCode2 = (int)row["item_code"];
                        BeforeChangeTextBox2.Text = row["item_name"].ToString();
                        record2 = (int)row["record_number"];
                        break;
                    case 2:
                        BeforeItemCode3 = (int)row["item_code"];
                        BeforeChangeTextBox3.Text = row["item_name"].ToString();
                        record3 = (int)row["record_number"];
                        break;
                    case 3:
                        BeforeItemCode4 = (int)row["item_code"];
                        BeforeChangeTextBox4.Text = row["item_name"].ToString();
                        record4 = (int)row["record_number"];
                        break;
                    case 4:
                        BeforeItemCode5 = (int)row["item_code"];
                        BeforeChangeTextBox5.Text = row["item_name"].ToString();
                        record5 = (int)row["record_number"];
                        break;
                    case 5:
                        BeforeItemCode6 = (int)row["item_code"];
                        BeforeChangeTextBox6.Text = row["item_name"].ToString();
                        record6 = (int)row["record_number"];
                        break;
                    case 6:
                        BeforeItemCode7 = (int)row["item_code"];
                        BeforeChangeTextBox7.Text = row["item_name"].ToString();
                        record7 = (int)row["record_number"];
                        break;
                    case 7:
                        BeforeItemCode8 = (int)row["item_code"];
                        BeforeChangeTextBox8.Text = row["item_name"].ToString();
                        record8 = (int)row["record_number"];
                        break;
                    case 8:
                        BeforeItemCode9 = (int)row["item_code"];
                        BeforeChangeTextBox9.Text = row["item_name"].ToString();
                        record9 = (int)row["record_number"];
                        break;
                    case 9:
                        BeforeItemCode10 = (int)row["item_code"];
                        BeforeChangeTextBox10.Text = row["item_name"].ToString();
                        record10 = (int)row["record_number"];
                        break;
                    case 10:
                        BeforeItemCode11 = (int)row["item_code"];
                        BeforeChangeTextBox11.Text = row["item_name"].ToString();
                        record11 = (int)row["record_number"];
                        break;
                    case 11:
                        BeforeItemCode12 = (int)row["item_code"];
                        BeforeChangeTextBox12.Text = row["item_name"].ToString();
                        record12 = (int)row["record_number"];
                        break;
                    case 12:
                        BeforeItemCode13 = (int)row["item_code"];
                        BeforeChangeTextBox13.Text = row["item_name"].ToString();
                        record13 = (int)row["record_number"];
                        break;
                }
            }
            #endregion

            #region"右の表"

            //担当者ごとの大分類の初期値を先頭に
            #region"大分類出力"
            sql_str = "select * from main_category_m where invalid = 0 order by main_category_code;";
            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(table1);
            for(int i = 1; i <= ChangeCount; i++)
            {
                switch (i)
                {
                    #region"１行目"
                    case 1:
                        MainCategoryComboBox1.DataSource = table1;
                        MainCategoryComboBox1.DisplayMember = "main_category_name";
                        MainCategoryComboBox1.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"２行目"
                    case 2:
                        table2 = table1.Copy();
                        MainCategoryComboBox2.DataSource = table2;
                        MainCategoryComboBox2.DisplayMember = "main_category_name";
                        MainCategoryComboBox2.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"３行目"
                    case 3:
                        table3 = table1.Copy();
                        MainCategoryComboBox3.DataSource = table3;
                        MainCategoryComboBox3.DisplayMember = "main_category_name";
                        MainCategoryComboBox3.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"４行目"
                    case 4:
                        table4 = table1.Copy();
                        MainCategoryComboBox4.DataSource = table4;
                        MainCategoryComboBox4.DisplayMember = "main_category_name";
                        MainCategoryComboBox4.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"５行目"
                    case 5:
                        table5 = table1.Copy();
                        MainCategoryComboBox5.DataSource = table5;
                        MainCategoryComboBox5.DisplayMember = "main_category_name";
                        MainCategoryComboBox5.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"６行目"
                    case 6:
                        table6 = table1.Copy();
                        MainCategoryComboBox6.DataSource = table6;
                        MainCategoryComboBox6.DisplayMember = "main_category_name";
                        MainCategoryComboBox6.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"７行目"
                    case 7:
                        table7 = table1.Copy();
                        MainCategoryComboBox7.DataSource = table7;
                        MainCategoryComboBox7.DisplayMember = "main_category_name";
                        MainCategoryComboBox7.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"８行目"
                    case 8:
                        table8 = table1.Copy();
                        MainCategoryComboBox8.DataSource = table8;
                        MainCategoryComboBox8.DisplayMember = "main_category_name";
                        MainCategoryComboBox8.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"９行目"
                    case 9:
                        table9 = table1.Copy();
                        MainCategoryComboBox9.DataSource = table9;
                        MainCategoryComboBox9.DisplayMember = "main_category_name";
                        MainCategoryComboBox9.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"１０行目"
                    case 10:
                        table10 = table1.Copy();
                        MainCategoryComboBox10.DataSource = table10;
                        MainCategoryComboBox10.DisplayMember = "main_category_name";
                        MainCategoryComboBox10.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"１１行目"
                    case 11:
                        table11 = table1.Copy();
                        MainCategoryComboBox11.DataSource = table11;
                        MainCategoryComboBox11.DisplayMember = "main_category_name";
                        MainCategoryComboBox11.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"１２行目"
                    case 12:
                        table12 = table1.Copy();
                        MainCategoryComboBox12.DataSource = table12;
                        MainCategoryComboBox12.DisplayMember = "main_category_name";
                        MainCategoryComboBox12.ValueMember = "main_category_code";
                        break;
                    #endregion
                    #region"１３行目"
                    case 13:
                        table13 = table1.Copy();
                        MainCategoryComboBox13.DataSource = table13;
                        MainCategoryComboBox13.DisplayMember = "main_category_name";
                        MainCategoryComboBox13.ValueMember = "main_category_code";
                        break;
                        #endregion
                }
            }
            #endregion

            //品名検索用
            string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
            adapter = new NpgsqlDataAdapter(sql_str3, conn);
            adapter.Fill(data);

            #region"品名出力"
            sql_str = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where main_category_m.main_category_code = '"+MainCategoryCode+"'" +
                " order by main_category_m.main_category_code;";
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

            notLoad = true;
        }

        #region"大分類名から品名を絞り込み"
        private void MainCategoryComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox1.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data1);
                AfterChangeComboBox1.DataSource = data1;
                AfterChangeComboBox1.DisplayMember = "item_name";
                AfterChangeComboBox1.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox2.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data2);
                AfterChangeComboBox2.DataSource = data2;
                AfterChangeComboBox2.DisplayMember = "item_name";
                AfterChangeComboBox2.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox3.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data3);
                AfterChangeComboBox3.DataSource = data3;
                AfterChangeComboBox3.DisplayMember = "item_name";
                AfterChangeComboBox3.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox4.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data4);
                AfterChangeComboBox4.DataSource = data4;
                AfterChangeComboBox4.DisplayMember = "item_name";
                AfterChangeComboBox4.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox5.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data5);
                AfterChangeComboBox5.DataSource = data5;
                AfterChangeComboBox5.DisplayMember = "item_name";
                AfterChangeComboBox5.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox6.SelectedValue;

                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data6);
                AfterChangeComboBox6.DataSource = data6;
                AfterChangeComboBox6.DisplayMember = "item_name";
                AfterChangeComboBox6.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox7.SelectedValue;

                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data7);
                AfterChangeComboBox7.DataSource = data7;
                AfterChangeComboBox7.DisplayMember = "item_name";
                AfterChangeComboBox7.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox8.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data8);
                AfterChangeComboBox8.DataSource = data8;
                AfterChangeComboBox8.DisplayMember = "item_name";
                AfterChangeComboBox8.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox9.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data9);
                AfterChangeComboBox9.DataSource = data9;
                AfterChangeComboBox9.DisplayMember = "item_name";
                AfterChangeComboBox9.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox10.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data10);
                AfterChangeComboBox10.DataSource = data10;
                AfterChangeComboBox10.DisplayMember = "item_name";
                AfterChangeComboBox10.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox11.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data11);
                AfterChangeComboBox11.DataSource = data11;
                AfterChangeComboBox11.DisplayMember = "item_name";
                AfterChangeComboBox11.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox12.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data12);
                AfterChangeComboBox12.DataSource = data12;
                AfterChangeComboBox12.DisplayMember = "item_name";
                AfterChangeComboBox12.ValueMember = "item_code";
                conn.Close();
            }
        }

        private void MainCategoryComboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notLoad)
            {
                MainCategoryCode = (int)MainCategoryComboBox13.SelectedValue;
                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();

                conn.Open();
                //品名検索用
                string sql_str3 = "select * from item_m inner join main_category_m on item_m.main_category_code = main_category_m.main_category_code where item_m.main_category_code = " + MainCategoryCode + ";";
                adapter = new NpgsqlDataAdapter(sql_str3, conn);
                adapter.Fill(data13);
                AfterChangeComboBox13.DataSource = data13;
                AfterChangeComboBox13.DisplayMember = "item_name";
                AfterChangeComboBox13.ValueMember = "item_code";
                conn.Close();
            }
        }
        #endregion
    }
}
