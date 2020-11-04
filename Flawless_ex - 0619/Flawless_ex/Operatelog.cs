using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class Operatelog : Form
    {

        public int staff_id;
        public string Access_auth;
        public string Pass;
        TopMenu topMenu;
        MainMenu mainMenu;
        NpgsqlConnection conn ;
        NpgsqlDataAdapter adapter;
        PostgreSQL postgre = new PostgreSQL();
        string sql = "";

        DataTable dt = new DataTable();                 //変更内容
        DataTable dt2 = new DataTable();                //変更者
        DataTable dt3 = new DataTable();                //操作履歴の表

        public Operatelog(MainMenu main, int id, string access_auth, string pass)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            Access_auth = access_auth;
            Pass = pass;
        }

        private void Return1_Click(object sender, EventArgs e)
        {
            MainMenu main = new MainMenu(topMenu, staff_id, Pass, Access_auth);
            this.Close();
            main.Show();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            dt3.Clear();
            conn = postgre.connection();
            #region"検索パラメータ"
            //string changeTable = "table_name = '" + changeTableComboBox.Text + "' and ";
            string changeContent = "";
            string changeTarget = "";
            string changer = "";
            string changeConditions = "";
            string tableJoin = "";
            string changeDate = "";
            string changeID = "";
            string changeBefore = "";
            string changeAfter = "";
            string changeReason;

            changeDate = changeDateDateTimePicker.Value.ToShortDateString().Replace('/', '-');

            #region"変更内容・対象・条件"
            if ((int)changeTableComboBox.SelectedValue == 1)
            {
                changeConditions = "data =  '" + 1 + "' ";
            }
            if ((int)changeTableComboBox.SelectedValue == 2)
            {
                changeConditions = "data =  '" + 2 + "' ";
            }
            if ((int)changeTableComboBox.SelectedValue == 3)
            {
                changeConditions = "data =  '" + 3 + "' ";
            }
            if ((int)changeTableComboBox.SelectedValue == 4)
            {
                changeConditions = "data =  '" + 4 + "' ";
            }
            if ((int)changeTableComboBox.SelectedValue == 5)
            {
                changeConditions = "data =  '" + 5 + "' ";
            }
            if ((int)changeTableComboBox.SelectedValue == 6)
            {
                changeConditions = "data =  '" + 6 + "' ";
            }
            if ((int)changeTableComboBox.SelectedValue == 7)
            {
                changeConditions = "data =  '" + 7 + "' ";
            }
            if ((int)changeTableComboBox.SelectedValue == 8)
            {
                changeConditions = "data =  '" + 8 + "' ";
            }
            //else
            //{
            //    changeTarget
            //}
            #endregion
            #region"変更者"
            if (changerComboBox.SelectedIndex == -1)
            {
                changer = "";
            }
            else
            {
                changer = "insert_code = '" + changerComboBox.SelectedValue + "' and ";
            }
            #endregion
            #region"変更 ID"
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                changeID = "";
            }
            else
            {
                changeID = "upd_code like '%" + idTextBox.Text + "%' and ";
            }
            #endregion
            #region"変更前"
            if (string.IsNullOrEmpty(changeBeforeTextBox.Text))
            {
                changeBefore = "";
            }
            else
            {
                changeBefore = "before_data like '%" + changeBeforeTextBox.Text + "%' and ";
            }
            #endregion
            #region"変更後"
            if (string.IsNullOrEmpty(changeAfterTextBox.Text))
            {
                changeAfter = "";
            }
            else
            {
                changeAfter = "after_data like '%" + changeAfterTextBox.Text + "%' and ";
            }
            #endregion
            #region"変更理由"
            if (string.IsNullOrEmpty(changeReasonTextBox.Text))
            {
                changeReason = "";
            }
            else
            {
                changeReason = "reason like '%" + changeReasonTextBox.Text + "%' and ";
            }
            #endregion
            #endregion

            sql = "select table_name, change_target, upd_code, upd_date, staff_name, before_data, after_data, reason " +
                "from revisions inner join change_table on data = data_type inner join staff_m on staff_code = insert_code" +
                " where " + changer + changeID + changeBefore + changeAfter + changeReason + changeConditions + " and cast(upd_date as text) like '%" + changeDate + "%' ;";
            conn.Open();
            adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(dt3);

            dataGridView1.DataSource = dt3;
            dataGridView1.Columns[0].HeaderText = "変更テーブル";
            dataGridView1.Columns[1].HeaderText = "変更対象";
            dataGridView1.Columns[2].HeaderText = "ID";
            dataGridView1.Columns[3].HeaderText = "変更日時";
            dataGridView1.Columns[4].HeaderText = "変更者";
            dataGridView1.Columns[5].HeaderText = "変更前";
            dataGridView1.Columns[6].HeaderText = "変更後";
            dataGridView1.Columns[7].HeaderText = "変更理由";

            conn.Close();
        }

        private void changeTableComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //担当者
            if (changeTableComboBox.SelectedIndex == 0)
            {
                changeTargetComboBox.SelectedIndex = 0;
            }
            //品名
            if (changeTableComboBox.SelectedIndex == 1)
            {
                changeTargetComboBox.SelectedIndex = 1;
            }
            //大分類
            if (changeTableComboBox.SelectedIndex == 2)
            {
                changeTargetComboBox.SelectedIndex = 2;
            }
            //顧客
            if (changeTableComboBox.SelectedIndex == 3)
            {
                changeTargetComboBox.SelectedIndex = 3;
            }
            //計算書
            if (changeTableComboBox.SelectedIndex == 4)
            {
                changeTargetComboBox.SelectedIndex = 4;
            }
            //納品書
            if (changeTableComboBox.SelectedIndex == 5)
            {
                changeTargetComboBox.SelectedIndex = 5;
            }
            //成績表
            if (changeTableComboBox.SelectedIndex == 6)
            {
                changeTargetComboBox.SelectedIndex = 6;
            }
            //消費税
            if (changeTableComboBox.SelectedIndex == 7)
            {
                changeTargetComboBox.SelectedIndex = 7;
                idTextBox.ReadOnly = true;
                idTextBox.Text = "";
            }
        }

        private void Operatelog_Load(object sender, EventArgs e)
        {
            dt.Clear();
            conn = postgre.connection();

            conn.Open();
            sql = "select * from change_table;";
            adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(dt);
            changeTableComboBox.DataSource = dt;
            changeTableComboBox.DisplayMember = "table_name";
            changeTableComboBox.ValueMember = "data_type";

            sql = "select * from staff_m;";
            adapter = new NpgsqlDataAdapter(sql, conn);
            adapter.Fill(dt2);
            changerComboBox.DataSource = dt2;
            changerComboBox.DisplayMember = "staff_name";
            changerComboBox.ValueMember = "staff_code";
            conn.Close();

            //changeTableComboBox.SelectedIndex = -1;
        }

    }
}
