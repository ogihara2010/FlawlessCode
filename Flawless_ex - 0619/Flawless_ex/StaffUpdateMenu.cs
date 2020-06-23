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
    public partial class StaffUpdateMenu : Form　//担当者更新メニュー
    {
        
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable(); //選択した担当者コード全ての情報
        
        int staffCode;//担当者コード
        string staffName;//担当者名
        string staffNameKana;//担当者名カナ
        string password;//パスワード
        string rePassword;//パスワード再入力
        string access_auth;//アクセス権限
        
        public StaffUpdateMenu(MasterMaintenanceMenu master,int code)
        {
            InitializeComponent();

           
            this.master = master;
            this.staffCode = code;
        }


        private void ReturnButton(object sender, EventArgs e)
        {
            StaffMaster personMaster = new StaffMaster(master);

            this.Close();
            personMaster.Show();
        }

        private void removeButton_Click(object sender, EventArgs e)//削除ボタン
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            NpgsqlCommandBuilder builder;
            

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "delete from staff_m where staff_code = " + staffCode +"";
            conn.Open();


            rePassword = this.passwordReText.Text; //変更予定


            if (password == rePassword)
            {
                adapter = new NpgsqlDataAdapter(sql_str,conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);
                MessageBox.Show("削除完了");
            }
            else
            {
                MessageBox.Show("削除失敗");
            }


            
            conn.Close();

            StaffMaster staffMaster = new StaffMaster(master);
            this.Close();
            staffMaster.Show();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            NpgsqlCommandBuilder builder;
            DataTable dt = new DataTable();

            staffName = this.parsonNameText.Text;//担当者名
            staffNameKana = this.parsonNamt2Text.Text;//担当者名カナ
            password = this.passwordText.Text;//パスワード
            rePassword = this.passwordReText.Text;//パスワード再入力
            access_auth = this.accessButton.Text;//アクセス権限

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();
            string sql_str = " update staff_m set  staff_name = '" + staffName + "', staff_name_kana = '" + staffNameKana + "', password = '" + password + "', access_auth = '" + access_auth + "' where staff_code =" + staffCode +" ";



            if (string.IsNullOrEmpty(password))//パスワード未入力
            {
                if (string.IsNullOrEmpty(rePassword))
                {
                    MessageBox.Show("パスワードを入力してください");
                    return;
                }
            }
            else { }
           

            if(this.passwordText.Text == this.passwordReText.Text)
            {
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                builder = new NpgsqlCommandBuilder(adapter);

                adapter.Fill(dt);
                adapter.Update(dt);
                MessageBox.Show("更新完了");
            }
            else
            {
                MessageBox.Show("更新失敗");
            }


            StaffMaster staffMaster = new StaffMaster(master);

            this.Close();
            staffMaster.Show();

        }

        private void UpdateMenu_Load(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            DataRow row;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select* from staff_m where staff_code = " + staffCode + "";

            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            row = dt.Rows[0];
            staffName = row["staff_name"].ToString();
            staffNameKana = row["staff_name_kana"].ToString();
            password = row["password"].ToString();
            access_auth = row["access_auth"].ToString();

            this.parsonCodeText.Text = staffCode.ToString();
            this.parsonNameText.Text = staffName;
            this.parsonNamt2Text.Text = staffNameKana ;
            this.passwordText.Text = password;
            this.accessButton.Text = access_auth;

            conn.Close();
        }
    }
}
