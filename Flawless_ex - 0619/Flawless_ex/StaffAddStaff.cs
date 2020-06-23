using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class StaffAddStaff : Form　//担当者新規登録メニュー
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        MasterMaintenanceMenu master;
        DataTable dt;

        public StaffAddStaff(DataTable dt, MasterMaintenanceMenu master)
        {
            InitializeComponent();

            this.dt = dt;
            this.master = master;
            
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            StaffMaster staffMaster = new StaffMaster(master);

            this.Close();
            staffMaster.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("登録をしますか？", "確認", MessageBoxButtons.YesNo);

            if(result == DialogResult.Yes)
            {
                NpgsqlCommandBuilder builder;

                int staffCode = int.Parse(parsonCodeText.Text);//担当者コード
                string staffName = this.parsonNameText.Text;//担当者名
                string staffNameKana = this.parsonNamt2Text.Text;//担当者名カナ
                int mainCategoryCode = (int)this.mainCategoryComboBox.SelectedValue; //大分類初期値
                string password = this.passwordText.Text;//パスワード
                string rePassword = this.passwordReText.Text;//パスワード再入力
                string access_auth = this.accessButton.Text;//アクセス権限
                DateTime dat = DateTime.Now;
                string d = dat.ToString();

                string sql_str = "insert into staff_m values(" + staffCode + " , '" + staffName + "', '" + staffNameKana + "'," + mainCategoryCode + ",'" + password + "', '" + access_auth + "," +  d + "," + 0 + "')";
                

                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                

                adapter.Fill(dt);
                adapter.Update(dt);

                conn.Close();

                MessageBox.Show("登録完了");


                StaffMaster staffMaster = new StaffMaster(master);
                this.Close();
                staffMaster.Show();
            }
            else { }
            
        }

        private void StaffAddStaff_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            string sql_str2 = "select* from main_category_m";
            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);

            conn.Close();

            mainCategoryComboBox.DataSource = dt2;
            mainCategoryComboBox.DisplayMember = "main_category_name";
            mainCategoryComboBox.ValueMember = "main_category_code";
        }
    }
}
