using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class TaxMaster : Form
    {
        MasterMaintenanceMenu master;
        DataTable dt;
        NpgsqlDataAdapter adapter;
        NpgsqlCommandBuilder builder;
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        int staff_code;
        string Access_auth;
        MainMenu mainMenu;

        public TaxMaster(MasterMaintenanceMenu master, int staff_code, string access_auth)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
            this.Access_auth = access_auth;
        }
        private void back_Click(object sender, EventArgs e)
        {
            master = new MasterMaintenanceMenu(mainMenu, staff_code, Access_auth);
            this.Close();
            master.Show();
        }

        private void update_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("税率を更新しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)     //無記入と小数点に対応
            {
                int letter = 0;
                int.TryParse(taxPercent.Text.ToString(), out letter);

                if (string.IsNullOrEmpty(taxPercent.Text))
                {
                    this.Close();
                    MessageBox.Show("税率が未入力です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (taxPercent.Text.IndexOf(".") > 0)
                {
                    this.Close();
                    MessageBox.Show("小数は入力できません", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } else if (letter == 0) 
                {
                    this.Close();
                    MessageBox.Show("半角の数値のみ入力できます。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DateTime time = DateTime.Now;
                    int tax = int.Parse(taxPercent.Text);

                    dt = new DataTable();
                    NpgsqlConnection db = new NpgsqlConnection();
                    string sql_str = "insert into vat_m (vat_rate, upd_date, upd_name) values (" + tax + ",'" + time.ToString("yyyy/MM/dd") + "'," + staff_code + ")";

                    db.ConnectionString = @"Server= 192.168.152.157;Port = 5432;User Id = postgres;Password = postgres;Database = master;";
                    db.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, db);
                    builder = new NpgsqlCommandBuilder(adapter);
                    
                    adapter.Fill(dt);
                    adapter.Update(dt);
                    
                    db.Close();

                    this.Close();
                    MessageBox.Show("更新しました。");
                }
            }
            master.Show();
        }

        private void TaxMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection db = new NpgsqlConnection();

            db.ConnectionString = @"Server= 192.168.152.157;Port = 5432;User Id=postgres;Password=postgres;Database=master;";

            db.Open();

            cmd = new NpgsqlCommand("SELECT vat_rate FROM vat_m", db);
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taxPercent.Text = reader["vat_rate"].ToString();
                    }
                }
            }catch(Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            db.Close();
        }

        private void taxPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar)) 
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void TaxMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            master = new MasterMaintenanceMenu(mainMenu, staff_code, Access_auth);
            master.Show();
        }
    }
}