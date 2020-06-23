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
    public partial class updateSubCategoryMenu : Form
    {

        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        int subCode;
        public updateSubCategoryMenu(MasterMaintenanceMenu master, int code)
        {
            InitializeComponent();

            this.master = master;
            subCode = code;
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            SubCategoryMaster subCategory = new SubCategoryMaster(master);
            this.Close();
            subCategory.Show();
        }

        private void updateSubCategoryMenu_Load(object sender, EventArgs e)
        {

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataRow row;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            //string sql_str = "select* from sub_category_m inner join main_category_m on 
            string sql_str2 = "select sub_category_code, sub_category_name, sub_category_name from sub_category_m inner join main_category_m on sub_category_m.main_category_code = main_category_m.main_category_code;";
            conn.Open();

            

            row = dt.Rows[0];
            this.subCategoryTextBox.Text = row["sub_category_name"].ToString();
            this.mainCategoryComboBox1.Text = row["main_category_name"].ToString();

            
            adapter = new NpgsqlDataAdapter(sql_str2, conn);
            adapter.Fill(dt2);

            conn.Close();

            //コンボボックス表示
            this.mainCategoryComboBox1.DataSource = dt2;
            this.mainCategoryComboBox1.DisplayMember = "sub_category_name";
            this.mainCategoryComboBox1.ValueMember = "main_category_code";

            this.subCategory.Text = row["sub_category_code"].ToString();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {

        }
    }
}
