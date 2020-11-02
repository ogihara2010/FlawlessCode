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
        int Tax;
        bool figure = false;
        string Pass;

        public TaxMaster(MasterMaintenanceMenu master, int staff_code, string access_auth, string pass)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
            this.Access_auth = access_auth;
            this.Pass = pass;
        }
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void update_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("税率を変更しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                MessageBox.Show("マスタメインメニューに戻ります", "確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
            }

           if (result == DialogResult.Yes)     //無記入、未変更に対応
            {
                if (string.IsNullOrEmpty(taxPercent.Text))
                {
                    MessageBox.Show("税率が未入力です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    taxPercent.Text = Tax.ToString();
                    return;
                }

                if (int.Parse(taxPercent.Text) == Tax)
                {
                    MessageBox.Show("税率が変更されておりません", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DateTime time = DateTime.Now;
                    int tax = int.Parse(taxPercent.Text);

                    dt = new DataTable();
                    NpgsqlConnection db = new NpgsqlConnection();
                    string sql_str = "insert into vat_m (vat_rate, upd_date, upd_name) values (" + tax + ",'" + time.ToString("yyyy/MM/dd") + "'," + staff_code + ");";

                    PostgreSQL postgre = new PostgreSQL();
                    db = postgre.connection();
                    db.Open();

                    adapter = new NpgsqlDataAdapter(sql_str, db);
                    builder = new NpgsqlCommandBuilder(adapter);

                    adapter.Fill(dt);
                    adapter.Update(dt);

                    sql_str = "insert into revisions (data, upd_date, insert_code, before_data, after_data)" +
                        "values ('" + 8 + "','" + time + "','" + staff_code + "','" + Tax.ToString() + "','" + tax.ToString() + "');";
                    cmd = new NpgsqlCommand(sql_str, db);
                    cmd.ExecuteNonQuery();

                    db.Close();
                    MessageBox.Show("税率を変更しました。", "変更確認", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                }
            }
        }

        private void TaxMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection db = new NpgsqlConnection();


            PostgreSQL postgre = new PostgreSQL();
            db = postgre.connection();

            db.Open();

            cmd = new NpgsqlCommand("SELECT vat_rate FROM vat_m", db);
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tax = (int)reader["vat_rate"];
                    taxPercent.Text = Tax.ToString();
                }
            }
            db.Close();
        }

        private void taxPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                //MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void TaxMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            master = new MasterMaintenanceMenu(mainMenu, staff_code, Access_auth, Pass);
            master.Show();
        }

        private void taxPercent_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(taxPercent.Text))
            {
                if (int.Parse(taxPercent.Text) <= Tax)
                {
                    update.Enabled = false;
                }
                else
                {
                    update.Enabled = true;
                }
            }
        }
    }
}