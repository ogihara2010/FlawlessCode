using Npgsql;
using NpgsqlTypes;
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
    public partial class TaxMaster : Form
    {
        MasterMaintenanceMenu master;
        DataTable dt;

        public TaxMaster(MasterMaintenanceMenu master)
        {
            InitializeComponent();
            this.master = master;
        }
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            master.Show();
        }

        private void update_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            int tax = int.Parse(taxPercent.Text);

            NpgsqlDataAdapter adapter;
            NpgsqlCommandBuilder builder;
            dt = new DataTable();
            NpgsqlConnection db = new NpgsqlConnection();
            string sql_str = "insert into vat_m (vat_rate, upd_date) values (" + tax + ",'" + time.ToString("yyyy/MM/dd") + "')";

            db.ConnectionString = @"Server= localhost;Port=5432;User Id=postgres;Password=postgres;Database=master;";
            db.Open();

            adapter = new NpgsqlDataAdapter(sql_str, db);
            builder = new NpgsqlCommandBuilder(adapter);

            adapter.Fill(dt);
            adapter.Update(dt);

            db.Close();

            this.Close();
            MessageBox.Show("更新しました。");

            master.Show();
        }

        private void TaxMaster_Load(object sender, EventArgs e)
        {
            NpgsqlConnection db = new NpgsqlConnection();

            db.ConnectionString = @"Server= localhost;Port = 5432;User Id=postgres;Password=postgres;Database=master;";

            db.Open();

            var cmd = new NpgsqlCommand("SELECT vat_rate FROM vat_m", db);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                taxPercent.Text = reader["vat_rate"].ToString();
            }

            db.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }
    }
}