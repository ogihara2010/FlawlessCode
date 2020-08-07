﻿using System;
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
    public partial class client_add : Form
    {
        //Statement statement;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        int staff_id;
        int type;
        string staff_name;
        string address;
        decimal Total;
        string access_auth;
        string document;
        int control;
        string data;
        string search1;
        string search2;
        string search3;
        string pass;
        public client_add(MainMenu mainMenu, int id, int type)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            //this.statement = statement;
            staff_id = id;
            this.type = type;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            client_search client_Search = new client_search(mainMenu, staff_id, type, staff_name, address, Total);
            this.Close();
            client_Search.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            client_search client_Search = new client_search(mainMenu, staff_id, type, staff_name, address, Total);
            this.Close();
            client_Search.Show();
        }
        #region "法人　登録"
        private void Button5_Click(object sender, EventArgs e)
        {
            string RegistrationDate = this.deliveryDateBox.Text;
            string CompanyName = this.textBox2.Text;
            string CompanyNameKana = this.textBox3.Text;
            string PostalCodeNumber = this.textBox4.Text;
            string Address = this.textBox5.Text;
            string AddressKana = this.textBox6.Text;
            string ShopName = this.textBox7.Text;
            string ShopNameKana = this.textBox8.Text;
            string PhoneNumber = this.textBox9.Text;
            string FaxNumber = this.textBox10.Text;
            string Position = this.textBox11.Text;
            string ClientStaffName = this.textBox12.Text;
            string EmailAddress = this.textBox13.Text;
            string BankName = this.textBox14.Text;
            string DepositType = this.textBox15.Text;
            string AccountName = this.textBox16.Text;
            string URLinfor = this.textBox17.Text;
            string BranchName = this.textBox18.Text;
            string AccountNumber = this.textBox19.Text;
            string AccountNameKana = this.textBox20.Text;
            string RegisterCopy = this.textBox21.Text;
            string Antiquelicense = this.textBox22.Text;
            int AntiqueNumber = int.Parse(this.textBox23.Text);
            string ID = this.textBox24.Text;
            string PeriodStay = this.textBox46.Text;
            string SealCertification = this.textBox26.Text;
            string TaxCertification = this.textBox27.Text;
            string Remarks = this.textBox28.Text;
            string ResidenceCard = this.textBox29.Text;
            string AolFinancialShareholder = textBox25.Text;
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;


            string sql_str = "Insert into client_m_corporate VALUES (" + 0 + " , '" + RegistrationDate + "' , '" + CompanyName + "' ,'" + CompanyNameKana + "' , '" + ShopName + "' ,  '" + ShopNameKana + " ', '" + AntiqueNumber + "' , '" + PostalCodeNumber + "', '" + Address + "' , '" + AddressKana + "' , '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + Position + "' , '" + ClientStaffName +
                "' , '" + EmailAddress + "', '" + URLinfor + "', '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + Remarks + "' , '" + ID + "' , '" + b + "','" + Antiquelicense + "','" + TaxCertification + "','" + ResidenceCard + "','" + PeriodStay + "','" + SealCertification + "'," +
               0 + ",'" + AolFinancialShareholder + "','" + RegisterCopy + "'," + staff_id + ");";

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
            type = 0;
            staff_name = ClientStaffName;
            address = Address;
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, search1, search2, search3);
            this.Close();
            statement.Show();
        }
        #endregion
        #region "個人　登録"
        private void Button18_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            string RegistrationDate = this.dateTimePicker1.Text;
            string Name = this.textBox56.Text;
            string NameKana = this.textBox55.Text;
            string Birthday = this.textBox50.Text;
            string PostalCodeNumber = this.textBox54.Text;
            string Address = this.textBox53.Text;
            string AddressKana = this.textBox52.Text;
            string PhoneNumber = this.textBox49.Text;
            string FaxNumber = this.textBox48.Text;
            string Occupation = this.textBox41.Text;
            string EmailAddress = this.textBox45.Text;
            string BankName = this.textBox44.Text;
            string DepositType = this.textBox43.Text;
            string AccountName = this.textBox42.Text;
            string BranchName = this.textBox40.Text;
            string AccountNumber = this.textBox39.Text;
            string AccountNameKana = this.textBox38.Text;
            string RegisterCopy = this.textBox37.Text;
            string Antiquelicense = this.textBox36.Text;
            string PhotoID = this.textBox35.Text;
            string ID = this.textBox34.Text;
            string PeriodStay = this.textBox47.Text;
            string SealCertification = this.textBox32.Text;
            string TaxCertification = this.textBox31.Text;
            string Remarks = this.textBox58.Text;
            string ResidenceCard = this.textBox30.Text;
            string AolFinancialShareholder = textBox33.Text;
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            string sql_str = "Insert into client_m_individual VALUES (" + 1 + " , '" + RegistrationDate + "' , '" + Name + "' ,'" + NameKana + "' , '" + Birthday + "' , '" + PostalCodeNumber + "', '" + Address + "' , '" + AddressKana + "' , '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + EmailAddress + "', '" + Occupation +
               "' , '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + ID + "' , '" + Remarks + "','" + RegisterCopy + "' , '" + Antiquelicense + "','" + PhotoID + "' , '" + TaxCertification + "','" + ResidenceCard + "','" + PeriodStay + "','" + SealCertification + "'," +
              0 + ",'" + AolFinancialShareholder + "');";

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
            type = 1;
            staff_name = Name;
            address = Address;
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, search1, search2, search3);
            this.Close();
            statement.Show();
        }
        #endregion

        private void Client_add_Load(object sender, EventArgs e)
        {
            this.textBox50.Text = "　年　月　日";
        }
    }
}
