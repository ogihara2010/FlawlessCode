﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using Npgsql;

namespace Flawless_ex
{
    public partial class ClientInformation : Form
    {
        RecordList RecordList;
        Statement Statement;
        public int Staff_id;
        public string Staff_Name;
        public int Type;
        public string SlipNumber;
        public int AntiqueNumber = 0;
        public int ID_Number = 0;
        public int Grade;
        public int invalid;
        public string Access_auth;
        public string Pass;
        bool NameChange;
        bool CarryOver;
        string PostalUpCode;
        string PostalDownCode;
        bool MonthCatalog;
        int ClientCode;

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;

        public ClientInformation(RecordList recordList, int staff_id, string staff_name, int type, string slipnumber, int antique, int id, string pass, int grade, string access_auth, bool monthCatalog, bool carryOver)
        {
            InitializeComponent();

            this.RecordList = recordList;
            this.Staff_id = staff_id;
            this.Staff_Name = staff_name;
            this.Type = type;
            this.SlipNumber = slipnumber;
            this.AntiqueNumber = antique;
            this.ID_Number = id;
            this.Pass = pass;
            this.Grade = grade;
            this.Access_auth = access_auth;
            this.MonthCatalog = monthCatalog;
            this.CarryOver = carryOver;
        }

        private void ClientInformation_Load(object sender, EventArgs e)
        {
            SlipNumberTextBox.Text = SlipNumber;
            GradeNumberTextBox.Text = SlipNumber.Trim('F');

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            conn.Open();

            string sql = "select * from staff_m where staff_code = " + Staff_id + ";";
            cmd = new NpgsqlCommand(sql, conn);
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    StaffNameTextBox.Text = reader["staff_name"].ToString();
                }
            }

            sql = "";

            sql = "select * from statement_data where document_number = '" + SlipNumber + "';";
            cmd = new NpgsqlCommand(sql, conn);
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ClientCode = (int)reader["code"];
                }
            }

            string SQL = "";
            if (Type == 0)
            {
                tabControl1.SelectedIndex = 0;
                tabControl1.TabPages.Remove(tabPage2);
                AntiqueNumberTextBox.Text = AntiqueNumber.ToString();

                SQL = "select * from client_m where code = '" + ClientCode + "';";
                cmd = new NpgsqlCommand(SQL, conn);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RegistrationDateTextBox.Text = ((DateTime)reader["registration_date"]).ToString("yyyy/MM/dd");
                        CompanyNameTextBox.Text = reader["company_name"].ToString();
                        ComPanyKanaTextBox.Text = reader["company_kana"].ToString();
                        ShopNameTextBox.Text = reader["shop_name"].ToString();
                        ShopKanaTextBox.Text = reader["shop_name_kana"].ToString();

                        PostalUpCode = reader["postal_code1"].ToString();
                        PostalDownCode = reader["postal_code2"].ToString();
                        PostalCodeTextBox.Text = PostalUpCode + PostalDownCode;
                        
                        AddressTextBox.Text = reader["address"].ToString();
                        AddressKanaTextBox.Text = reader["address_kana"].ToString();
                        TelTextBox.Text = reader["phone_number"].ToString();
                        FaxTextBox.Text = reader["fax_number"].ToString();
                        PositionTextBox.Text = reader["position"].ToString();
                        ClientStaffNameTextBox.Text = reader["name"].ToString();
                        MailAddressTextBox.Text = reader["email_address"].ToString();
                        UrlTextBox.Text = reader["url_infor"].ToString();
                        BankNameTextBox.Text = reader["bank_name"].ToString();
                        BranchNameTextBox.Text = reader["branch_name"].ToString();
                        DepositTypeTextBox.Text = reader["deposit_type"].ToString();
                        AccountNumberTextBox.Text = reader["account_number"].ToString();
                        AccountNameTextBox.Text = reader["account_name"].ToString();
                        AccountNameKanaTextBox.Text = reader["account_name_kana"].ToString();
                        RemarksTextBox.Text = reader["remarks"].ToString();
                        IdTextBox.Text = reader["id"].ToString();
                        RegisterCopyTextBox.Text = reader["register_copy"].ToString();

                        AntiqueLicenseTextBox.Text = reader["antique_license"].ToString();
                        AolFinancialShareholderTextBox.Text = reader["aol_financial_shareholder"].ToString();
                        TaxCertificateTextBox.Text = reader["tax_certificate"].ToString();
                        ResidenceCardTextBox.Text = reader["residence_card"].ToString();
                        PeriodStayTextBox.Text = reader["period_stay"].ToString();
                        SealCetificationTextBox.Text = reader["seal_certification"].ToString();
                    }
                }

                if (string.IsNullOrEmpty(ResidenceCardTextBox.Text))
                {
                    PeriodStayTextBox.Text = "";
                }
            }
            //個人のとき
            else if (Type == 1)
            {
                tabControl1.SelectedIndex = 1;
                tabControl1.TabPages.Remove(tabPage1);
                IdTextBox2.Text = ID_Number.ToString();

                SQL = "select * from client_m where code = '" + ClientCode + "';";
                cmd = new NpgsqlCommand(SQL, conn);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RegistrationDateTextBox2.Text = ((DateTime)reader["registration_date"]).ToString("yyyy/MM/dd");
                        NameTextBox2.Text = reader["name"].ToString();
                        NameKanaTextBox2.Text = reader["name_kana"].ToString();
                        BirthdayTextBox2.Text = reader["birthday"].ToString();

                        PostalUpCode = reader["postal_code1"].ToString();
                        PostalDownCode = reader["postal_code2"].ToString();
                        PostalCodeTextBox2.Text = PostalUpCode + PostalDownCode;

                        AddressTextBox2.Text = reader["address"].ToString();
                        AddressKanaTextBox2.Text = reader["address_kana"].ToString();
                        TelTextBox2.Text = reader["phone_number"].ToString();
                        FaxTextBox2.Text = reader["fax_number"].ToString();
                        MailTextBox2.Text = reader["email_address"].ToString();
                        OccupationTextBox2.Text = reader["occupation"].ToString();
                        BankNameTextBox2.Text = reader["bank_name"].ToString();
                        BranchNameTextBox2.Text = reader["branch_name"].ToString();
                        DepositTypeTextBox2.Text = reader["deposit_type"].ToString();
                        AccountNumberTextBox2.Text = reader["account_number"].ToString();
                        AccountNameTextBox2.Text = reader["account_name"].ToString();
                        AccountNameKanaTextBox2.Text = reader["account_name_kana"].ToString();
                        RegisterCopyTextBox2.Text = reader["register_copy"].ToString();
                        AntiqueLicenseTextBox2.Text = reader["antique_license"].ToString();
                        PhotoIdTextBox2.Text = reader["id"].ToString();
                        AolFinancialShareholderTextBox2.Text = reader["aol_financial_shareholder"].ToString();
                        TaxCertificateTextBox2.Text = reader["tax_certificate"].ToString();
                        ResidenceCardTextBox2.Text = reader["residence_card"].ToString();
                        PeriodStayTextBox2.Text = reader["period_stay"].ToString();
                        SealCetificationTextBox2.Text = reader["seal_certification"].ToString();
                        textBox1.Text = reader["remarks"].ToString();
                    }
                }
                if (string.IsNullOrEmpty(RegisterCopyTextBox2.Text))
                {
                    RegistrationDateTextBox2.Text = "";
                }

                if (string.IsNullOrEmpty(ResidenceCardTextBox2.Text))
                {
                    PeriodStayTextBox2.Text = "";
                }
            }

            conn.Close();
        }

        #region"戻る"
        private void ClientInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            RecordList = new RecordList(Statement, Staff_id, Staff_Name, Type, SlipNumber, Grade, AntiqueNumber, ID_Number, Access_auth, Pass, NameChange, CarryOver, MonthCatalog);
            RecordList.Show();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region"画像の表示　法人"
        private void ResidenceCardTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ResidenceCardTextBox.Text))
            {
                pictureBox2.ImageLocation = ResidenceCardTextBox.Text;
            }
            else
            {
                MessageBox.Show("在留カードが登録されていません。", "在留カード表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void RegisterCopyTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RegisterCopyTextBox.Text))
            {
                pictureBox2.ImageLocation = RegisterCopyTextBox.Text;
            }
            else
            {
                MessageBox.Show("登記簿謄本が登録されていません。", "登記簿謄本表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void IdTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IdTextBox.Text))
            {
                pictureBox2.ImageLocation = IdTextBox.Text;
            }
            else
            {
                MessageBox.Show("身分証明書または顔つき身分証が登録されていません。", "身分証明書表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AntiqueLicenseTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AntiqueLicenseTextBox.Text))
            {
                pictureBox2.ImageLocation = AntiqueLicenseTextBox.Text;
            }
            else
            {
                MessageBox.Show("古物商許可証が登録されていません。", "古物商許可証表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AolFinancialShareholderTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AolFinancialShareholderTextBox.Text))
            {
                pictureBox2.ImageLocation = AolFinancialShareholderTextBox.Text;
            }
            else
            {
                MessageBox.Show("定款、決算書、株主構成が登録されていません。", "定款、決算書、株主構成表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void TaxCertificateTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TaxCertificateTextBox.Text))
            {
                pictureBox2.ImageLocation = TaxCertificateTextBox.Text;
            }
            else
            {
                MessageBox.Show("納税証明書が登録されていません。", "納税証明書表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SealCetificationTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SealCetificationTextBox.Text))
            {
                pictureBox2.ImageLocation = SealCetificationTextBox.Text;
            }
            else
            {
                MessageBox.Show("印鑑証明書が登録されていません。", "印鑑証明書表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region"画像の表示　個人"

        private void RegisterCopyTextBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RegisterCopyTextBox2.Text))
            {
                pictureBox1.ImageLocation = RegisterCopyTextBox2.Text;
            }
            else
            {
                MessageBox.Show("登記簿謄本が登録されていません。", "登記簿謄本表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AntiqueLicenseTextBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AntiqueLicenseTextBox2.Text))
            {
                pictureBox1.ImageLocation = AntiqueLicenseTextBox2.Text;
            }
            else
            {
                MessageBox.Show("古物商許可証が登録されていません。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PhotoIdTextBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(PhotoIdTextBox2.Text))
            {
                pictureBox1.ImageLocation = PhotoIdTextBox2.Text;
            }
            else
            {
                MessageBox.Show("身分証明書または顔つき身分証が登録されていません。", "身分証明書表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void TaxCertificateTextBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TaxCertificateTextBox2.Text))
            {
                pictureBox1.ImageLocation = TaxCertificateTextBox2.Text;
            }
            else
            {
                MessageBox.Show("納税証明書が登録されていません。", "納税証明書表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AolFinancialShareholderTextBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AolFinancialShareholderTextBox2.Text))
            {
                pictureBox1.ImageLocation = AolFinancialShareholderTextBox2.Text;
            }
            else
            {
                MessageBox.Show("定款、決算書、株主構成が登録されていません。", "定款、決算書、株主構成表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ResidenceCardTextBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ResidenceCardTextBox2.Text))
            {
                pictureBox1.ImageLocation = ResidenceCardTextBox2.Text;
            }
            else
            {
                MessageBox.Show("在留カードが登録されていません。", "在留カード表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SealCetificationTextBox2_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SealCetificationTextBox2.Text))
            {
                pictureBox1.ImageLocation = SealCetificationTextBox2.Text;
            }
            else
            {
                MessageBox.Show("印鑑証明書が登録されていません。", "印鑑証明書表示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion
    }
}
