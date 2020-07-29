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
    public partial class RecordList : Form
    {
        int staff_id;
        string staff_name;
        int type;
        Statement statement;
        string SlipNumber;

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        public RecordList(Statement statement, int staff_id, string staff_neme, int type, string slipNumber)
        {
            InitializeComponent();
            this.statement = statement;
            this.staff_id = staff_id;
            this.staff_name = staff_neme;
            this.type = type;
            this.SlipNumber = slipNumber;
        }

        private void RecordList_Load(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Server = 192.168.11.30; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "select * from staff_m where staff_code = " + staff_id + ";";              //担当者名取得用
            string sql_str1 = "select * from statement_data where staff_code = " + staff_id + ";";      //伝票番号、合計金額、受け渡し方法、決済方法、決済日、受け渡し日を取得用
            //

            cmd = new NpgsqlCommand(sql_str, conn);

            conn.Open();

            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    StaffNameTextBox.Text = reader["staff_name"].ToString();
                }
            }

            cmd = new NpgsqlCommand(sql_str1, conn);

            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    SlipNumberTextBox.Text = reader["document_number"].ToString();
                    PurchaseTotalTextBox.Text = reader["total"].ToString();

                }
            }
        }


    }
}
