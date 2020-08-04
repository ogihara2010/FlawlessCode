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

        DataTable dt = new DataTable();

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        NpgsqlDataAdapter adapter;

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

        }
        #endregion

        #region"戻る"
        private void ReturnButton_Click(object sender, EventArgs e)
        {
            recordList = new RecordList(statement, staff_id, Staff_Name, type, SlipNumber, Grade);

            this.Hide();
            recordList.Show();
        }

        private void ItemNameChange_FormClosed(object sender, FormClosedEventArgs e)
        {
            recordList = new RecordList(statement, staff_id, Staff_Name, type, SlipNumber, Grade);

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
            adapter.Fill(dt);
            #region"１行目"
            AfterChangeComboBox1.DataSource = dt;
            AfterChangeComboBox1.DisplayMember = "item_name";
            AfterChangeComboBox1.ValueMember = "item_code";
            #endregion
            #region"２行目"
            AfterChangeComboBox2.DataSource = dt;
            AfterChangeComboBox2.DisplayMember = "item_name";
            AfterChangeComboBox2.ValueMember = "item_code";
            #endregion
            #region"３行目"
            AfterChangeComboBox3.DataSource = dt;
            AfterChangeComboBox3.DisplayMember = "item_name";
            AfterChangeComboBox3.ValueMember = "item_code";
            #endregion
            #region"４行目"
            AfterChangeComboBox4.DataSource = dt;
            AfterChangeComboBox4.DisplayMember = "item_name";
            AfterChangeComboBox4.ValueMember = "item_code";
            #endregion
            #region"５行目"
            AfterChangeComboBox5.DataSource = dt;
            AfterChangeComboBox5.DisplayMember = "item_name";
            AfterChangeComboBox5.ValueMember = "item_code";
            #endregion
            #region"６行目"
            AfterChangeComboBox6.DataSource = dt;
            AfterChangeComboBox6.DisplayMember = "item_name";
            AfterChangeComboBox6.ValueMember = "item_code";
            #endregion
            #region"７行目"
            AfterChangeComboBox7.DataSource = dt;
            AfterChangeComboBox7.DisplayMember = "item_name";
            AfterChangeComboBox7.ValueMember = "item_code";
            #endregion
            #region"８行目"
            AfterChangeComboBox8.DataSource = dt;
            AfterChangeComboBox8.DisplayMember = "item_name";
            AfterChangeComboBox8.ValueMember = "item_code";
            #endregion
            #region"９行目"
            AfterChangeComboBox9.DataSource = dt;
            AfterChangeComboBox9.DisplayMember = "item_name";
            AfterChangeComboBox9.ValueMember = "item_code";
            #endregion
            #region"１０行目"
            AfterChangeComboBox10.DataSource = dt;
            AfterChangeComboBox10.DisplayMember = "item_name";
            AfterChangeComboBox10.ValueMember = "item_code";
            #endregion
            #region"１１行目"
            AfterChangeComboBox11.DataSource = dt;
            AfterChangeComboBox11.DisplayMember = "item_name";
            AfterChangeComboBox11.ValueMember = "item_code";
            #endregion
            #region"１２行目"
            AfterChangeComboBox12.DataSource = dt;
            AfterChangeComboBox12.DisplayMember = "item_name";
            AfterChangeComboBox12.ValueMember = "item_code";
            #endregion
            #region"１３行目"
            AfterChangeComboBox13.DataSource = dt;
            AfterChangeComboBox13.DisplayMember = "item_name";
            AfterChangeComboBox13.ValueMember = "item_code";
            #endregion
            #endregion

            #endregion
            conn.Close();
        }
    }
}
