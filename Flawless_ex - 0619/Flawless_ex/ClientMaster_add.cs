using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using Npgsql;

namespace Flawless_ex
{
    public partial class ClientMaster_add : Form
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        public int staff_code;
        public string access_auth;
        public string path;
        public int type;
        public bool screan = true;
        public string Pass;
        public string kana;
        NpgsqlDataReader dataReader;
        NpgsqlTransaction transaction;

        private FileServer fileServer = new FileServer();
        InputControl InputControl = new InputControl();
        int ClientCode;

        public ClientMaster_add(MasterMaintenanceMenu master, int staff_code, string access_auth, int type, string pass)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
            this.access_auth = access_auth;
            this.type = type;
            this.Pass = pass;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region"法人　登録"
        private void Button5_Click(object sender, EventArgs e)
        {
            #region "起こりうるミス"
            #region "必須項目"
            if (string.IsNullOrEmpty(companyNameTextBox.Text))
            {
                MessageBox.Show("会社名を入力してください。");
                return;
            }
            else if (string.IsNullOrEmpty(companyKanaTextBox.Text))
            {
                MessageBox.Show("会社名のフリガナを入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(companyKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("会社カナにはカタカナのみ入力して下さい。");
                return;
            }
            //else if (System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox.Text, @"^[a-zA-Z]+$"))
            //{
            //    MessageBox.Show("正しく入力して下さい。");
            //    return;
            //}
            //else if (System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox.Text, @"^[a-zA-Z]+$"))
            //{
            //    MessageBox.Show("正しく入力して下さい。");
            //    return;
            //}
            else if (string.IsNullOrEmpty(postalDownCodeTextBox.Text) || string.IsNullOrEmpty(postalUpCodeTextBox.Text))
            {
                MessageBox.Show("郵便番号を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("住所を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(addressKanaTextBox.Text))
            {
                MessageBox.Show("住所のフリガナを入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(addressKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("住所カナにはカタカナのみ入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("担当者名義を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(telTextBox.Text))
            {
                MessageBox.Show("電話番号を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(registerCopyTextBox.Text))
            {
                MessageBox.Show("登記簿謄本を選んで下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(antiqueLicenseTextBox.Text))
            {
                MessageBox.Show("古物商許可証を選んで下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(antiqueNumberTextBox.Text))
            {
                MessageBox.Show("古物番号を入力して下さい。");
                return;
            }
            //else if (!System.Text.RegularExpressions.Regex.IsMatch(antiqueNumberTextBox.Text, @"^\p{N}+$"))
            //{
            //    MessageBox.Show("数字を入力して下さい。");
            //    return;
            //}
            else if (string.IsNullOrEmpty(photoIDTextBox.Text))
            {
                MessageBox.Show("身分証明書もしくは顔つき身分証明証を選択して下さい。");
                return;
            }
            #endregion
            #region "その他の項目"
            else
            {
                if (!string.IsNullOrEmpty(shopNameKanaTextBox.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(shopNameKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                    {
                        MessageBox.Show("カタカナを入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(accountNameKanaTextBox.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(accountNameKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                    {
                        MessageBox.Show("カタカナを入力して下さい。");
                        return;
                    }
                }
                //if (!string.IsNullOrEmpty(faxTextBox.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(faxTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(faxTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(faxTextBox.Text, @"^[a-zA-Z]+$"))
                //    {
                //        MessageBox.Show("正しく入力して下さい。");
                //        return;
                //    }
                //}
                //if (!string.IsNullOrEmpty(accountNumberTextBox.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(accountNumberTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(accountNumberTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(accountNumberTextBox.Text, @"^[a-zA-Z]+$"))
                //    {
                //        MessageBox.Show("正しく入力して下さい。");
                //        return;
                //    }
                //}
                //if (!string.IsNullOrEmpty(emailTextBox.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(emailTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(emailTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(emailTextBox.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                //    {
                //        MessageBox.Show("正しく入力して下さい。");
                //        return;
                //    }
                //}
                //if (!string.IsNullOrEmpty(urlTextBox.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(urlTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(urlTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(urlTextBox.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                //    {
                //        MessageBox.Show("正しく入力して下さい。");
                //        return;
                //    }
                //}
            }
            #endregion
            #endregion
            DialogResult dr = MessageBox.Show("登録しますか？","登録確認",MessageBoxButtons.YesNo);

            if (dr == DialogResult.No) {
                return;
            }

            //string RegistrationDate = this.deliveryDateBox.Text;
            DateTime? registrationDate = DateTime.Parse(deliveryDateBox.Value.ToShortDateString());

            string CompanyName = this.companyNameTextBox.Text;
            string CompanyNameKana = this.companyKanaTextBox.Text;
            
            string PostalCode1 = this.postalUpCodeTextBox.Text;
            string PostalCode2 = this.postalDownCodeTextBox.Text;
            
            string Address = this.addressTextBox.Text;
            string AddressKana = this.addressKanaTextBox.Text;
            string ShopName = this.shopNameTextBox.Text;
            string ShopNameKana = this.shopNameKanaTextBox.Text;
            string PhoneNumber = this.telTextBox.Text;
            string FaxNumber = this.faxTextBox.Text;
            string Position = this.positionTextBox.Text;
            string ClientStaffName = this.nameTextBox.Text;
            string EmailAddress = this.emailTextBox.Text;
            string BankName = this.bankNameTextBox.Text;
            string DepositType = this.depositTypeTextBox.Text;
            string AccountName = this.accountNameTextBox.Text;
            string URLinfor = this.urlTextBox.Text;
            string BranchName = this.branchNameTextBox.Text;
            string AccountNumber = this.accountNumberTextBox.Text;
            string AccountNameKana = this.accountNameKanaTextBox.Text;
            string RegisterCopy = fileServer.UploadImage(registerCopyTextBox.Text,FileServer.Filetype.RegisterCopy);
            string Antiquelicense = fileServer.UploadImage(this.antiqueLicenseTextBox.Text,FileServer.Filetype.Antiquelicense);
            string AntiqueNumber = antiqueNumberTextBox.Text;
            string ID = fileServer.UploadImage(this.photoIDTextBox.Text,FileServer.Filetype.ID);

            DateTime? periodStay = DateTime.Parse(periodStayCompanyDateTimePicker.Value.ToShortTimeString());
            //string PeriodStay = this.textBox46.Text;
            
            string SealCertification = fileServer.UploadImage(this.sealCertificationTextBox.Text, FileServer.Filetype.SealCertification);
            string TaxCertification = fileServer.UploadImage(this.taxCertificationTextBox.Text,FileServer.Filetype.TaxCertification);
            string Remarks = this.remarkTextBox.Text;
            string ResidenceCard = fileServer.UploadImage(this.residenceCardTextBox.Text,FileServer.Filetype.ResidenceCard);
            string AolFinancialShareholder = fileServer.UploadImage(aolFinancialShareholderTextBox.Text,FileServer.Filetype.AolFinancialShareholder);
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");

            //if (string.IsNullOrEmpty(RegisterCopy))
            //{
            //    registrationDate = null;
            //}

            //if (string.IsNullOrEmpty(ResidenceCard))
            //{
            //    periodStay = null;
            //}

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            NpgsqlDataReader reader;
            NpgsqlCommand cmd;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            
            conn.Open();

            string sql_str = "update client_m set (type, registration_date, company_name, company_kana, shop_name, shop_name_kana, antique_number," +
                " address, address_kana, phone_number, fax_number, position, name, email_address, url_infor, bank_name, branch_name, deposit_type," +
                " account_number, account_name, account_name_kana, remarks, id, register_date, antique_license, tax_certificate, residence_card, period_stay," +
                " seal_certification, invalid, aol_financial_shareholder, register_copy, insert_name, postal_code1, postal_code2) " +
                " = (" + 0 + " , '" + registrationDate + "' , '" + CompanyName + "' ,'" + CompanyNameKana + "' , '" + ShopName + "' ,  '" + ShopNameKana + "', '" + AntiqueNumber + "' ," +
                " '" + Address + "' , '" + AddressKana + "' , '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + Position + "' , '" + ClientStaffName + "' , '" + EmailAddress + "', '" + URLinfor + "', " +
                "'" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + Remarks + "' , '" + ID + "' , " +
                "'" + b + "','" + Antiquelicense + "','" + TaxCertification + "','" + ResidenceCard + "','" + periodStay + "','" + SealCertification + "'," + 0 + ",'" + AolFinancialShareholder + "'," +
                "'" + RegisterCopy + "'," + staff_code + "," + PostalCode1 + ",'" + PostalCode2 + "') where code = '" + ClientCode + "';";

            //adapter = new NpgsqlDataAdapter(sql_str, conn);
            //adapter.Fill(dt);
            using (transaction = conn.BeginTransaction())
            {
                cmd = new NpgsqlCommand(sql_str, conn);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }

            MessageBox.Show("法人の顧客を登録しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) ;
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth, Pass);
            screan = false;
            conn.Close();
            this.Close();
            clientmaster.Show();
        }
        #endregion
        private void Button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region "個人　登録"
        private void Button18_Click(object sender, EventArgs e)
        {
            #region "起こりうるミス"
            #region "必須項目"
            if (string.IsNullOrEmpty(nameTextBox1.Text))
            {
                MessageBox.Show("名前を入力してください。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(nameKanaTextBox1.Text))
            {
                MessageBox.Show("名前のフリガナを入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(nameKanaTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("氏名カナにはカタカナのみ入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //else if (System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox1.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox1.Text, @"^[a-zA-Z]+$"))
            //{
            //    MessageBox.Show("正しく入力して下さい。");
            //    return;
            //}
            //else if (System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox1.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox1.Text, @"^[a-zA-Z]+$"))
            //{
            //    MessageBox.Show("正しく入力して下さい。");
            //    return;
            //}
            else if (string.IsNullOrEmpty(postalDownCodeTextBox1.Text) || string.IsNullOrEmpty(postalUpCodeTextBox1.Text))
            {
                MessageBox.Show("郵便番号を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //else if (int.Parse(postalDownCodeTextBox1.Text) >= 10000 || int.Parse(postalUpCodeTextBox1.Text) >= 1000)
            //{
            //    MessageBox.Show("郵便番号を正しく入力して下さい。");
            //    return;
            //}
            else if (string.IsNullOrEmpty(addressTextBox1.Text))
            {
                MessageBox.Show("住所を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(addressKanaTextBox1.Text))
            {
                MessageBox.Show("住所のフリガナを入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(addressKanaTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("住所カナにはカタカナのみ入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(telTextBox1.Text))
            {
                MessageBox.Show("電話番号を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(registerCopyTextBox1.Text))
            {
                MessageBox.Show("登記簿謄本を選んで下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(antiqueLicenseTextBox1.Text))
            {
                MessageBox.Show("古物商許可証を選んで下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(idNumberTextBox1.Text))
            {
                MessageBox.Show("身分証番号を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(photoIDTextBox1.Text))
            {
                MessageBox.Show("顔つき身分証明証を選択して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #region "その他の項目"
            else
            {
                if (!string.IsNullOrEmpty(accountKanaTextBox1.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(accountKanaTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                    {
                        MessageBox.Show("カタカナを入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //if (!string.IsNullOrEmpty(textBox30.Text) && string.IsNullOrEmpty(textBox47.Text))
                //{
                //    MessageBox.Show("在留期限を入力して下さい。");
                //    return;
                //}
                //if (!string.IsNullOrEmpty(textBox47.Text) && string.IsNullOrEmpty(textBox30.Text))
                //{
                //    MessageBox.Show("正しく入力して下さい。");
                //    return;
                //}
                
                //if (!string.IsNullOrEmpty(accountNumberTextBox1.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(accountNumberTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(accountNumberTextBox1.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(accountNumberTextBox1.Text, @"^[a-zA-Z]+$"))
                //    {
                //        MessageBox.Show("正しく入力して下さい。");
                //        return;
                //    }
                //}
                //if (!string.IsNullOrEmpty(faxTextBox1.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(faxTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(faxTextBox1.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(faxTextBox1.Text, @"^[a-zA-Z]+$"))
                //    {
                //        MessageBox.Show("正しく入力して下さい。");
                //        return;
                //    }
                //}
                //if (!string.IsNullOrEmpty(emailTextBox1.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(emailTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(emailTextBox1.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(emailTextBox1.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                //    {
                //        MessageBox.Show("正しく入力して下さい。");
                //        return;
                //    }
                //}
            }
            #endregion
            #endregion
            DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            //string RegistrationDate = this.dateTimePicker1.Text;
            DateTime? registrationDate = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());

            string Name = this.nameTextBox1.Text;
            string NameKana = this.nameKanaTextBox1.Text;

            DateTime birthday = DateTime.Parse(birthdayDateTimePicker.Value.ToShortTimeString());
            //string Birthday = this.textBox50.Text;
            
            string PostalCode1 = this.postalUpCodeTextBox1.Text;
            string PostalCode2 = this.postalDownCodeTextBox1.Text;
            string Address = this.addressTextBox1.Text;
            string AddressKana = this.addressKanaTextBox1.Text;
            string PhoneNumber = this.telTextBox1.Text;
            string FaxNumber = this.faxTextBox1.Text;
            string Occupation = this.occupationTextBox1.Text;
            string EmailAddress = this.emailTextBox1.Text;
            string BankName = this.bankNameTextBox1.Text;
            string DepositType = this.depositTypeTextBox1.Text;
            string AccountName = this.accountNameTextBox1.Text;
            string BranchName = this.branchNameTextBox1.Text;
            string AccountNumber = this.accountNumberTextBox1.Text;
            string AccountNameKana = this.accountKanaTextBox1.Text;
            string RegisterCopy = fileServer.UploadImage(this.registerCopyTextBox1.Text,FileServer.Filetype.RegisterCopy);
            string Antiquelicense = fileServer.UploadImage(this.antiqueLicenseTextBox1.Text,FileServer.Filetype.Antiquelicense);
            string ID = idNumberTextBox1.Text;
            string PhotoID = fileServer.UploadImage(photoIDTextBox1.Text,FileServer.Filetype.ID);

            DateTime? periodStay = DateTime.Parse(periodStayIndividualDateTimePicker.Value.ToShortDateString());
            //string PeriodStay = this.textBox47.Text;
            
            string SealCertification = fileServer.UploadImage(this.sealCertificationTextBox1.Text,FileServer.Filetype.SealCertification);
            string TaxCertification = fileServer.UploadImage(this.taxCertificationTextBox1.Text,FileServer.Filetype.TaxCertification);
            string Remarks = this.remarkTextBox1.Text;
            string ResidenceCard = fileServer.UploadImage(this.residenceCardTextBox1.Text,FileServer.Filetype.ResidenceCard);
            string AolFinancialShareholder = fileServer.UploadImage(aolFinancialShareholderTextBox1.Text,FileServer.Filetype.AolFinancialShareholder);
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");

            //if (string.IsNullOrEmpty(RegisterCopy))
            //{
            //    registrationDate = null;
            //}

            //if (string.IsNullOrEmpty(ResidenceCard))
            //{
            //    periodStay = null;
            //}

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            NpgsqlDataReader reader;
            NpgsqlCommand cmd;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            conn.Open();

            string sql_str = "update client_m set (type, registration_date, name, name_kana, birthday, address, address_kana, phone_number, fax_number, email_address, occupation, " +
                "bank_name, branch_name, deposit_type, account_number, account_name, account_name_kana, id_number, remarks, register_copy, antique_license, id, tax_certificate, residence_card, period_stay," +
                " seal_certification, invalid, aol_financial_shareholder, register_date, insert_name, postal_code1, postal_code2) " +
                " = (" + 1 + " , '" + registrationDate + "' , '" + Name + "' ,'" + NameKana + "' , '" + birthday + "' , '" + Address + "' , '" + AddressKana + "' , '" + PhoneNumber + "' ," +
                " '" + FaxNumber + "' , '" + EmailAddress + "', '" + Occupation + "' , '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' ," +
                " '" + AccountNameKana + "' , '" + ID + "' , '" + Remarks + "','" + RegisterCopy + "', '" + Antiquelicense + "' ,'" + PhotoID + "' , '" + TaxCertification + "','" + ResidenceCard + "','" + periodStay + "'," +
                "'" + SealCertification + "'," + 0 + ",'" + AolFinancialShareholder + "','" + b + "'," + staff_code + "," + PostalCode1 + ",'" + PostalCode2 + "') where code = '" + ClientCode + "';";

            //adapter = new NpgsqlDataAdapter(sql_str, conn);
            //adapter.Fill(dt);

            using (transaction = conn.BeginTransaction())
            {
                cmd = new NpgsqlCommand(sql_str, conn);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }

            MessageBox.Show("個人の顧客を登録しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth, Pass);
            screan = false;
            this.Close();
            clientmaster.Show();
        }
        #endregion

        private void ClientMaster_add_Load(object sender, EventArgs e)
        {
            if (type == 0)
            {
                tabControl1.SelectedTab = tabPage1;
                tabControl1.TabPages.Remove(tabPage2);
                this.ActiveControl = this.companyNameTextBox;
            }
            else if (type == 1)
            {
                tabControl1.SelectedTab = tabPage2;
                tabControl1.TabPages.Remove(tabPage1);
                //this.textBox50.Text = "　年　月　日";
                this.ActiveControl = this.nameTextBox1;
            }
            else { }

            PostgreSQL postgreSQL = new PostgreSQL();
            NpgsqlConnection conn = postgreSQL.connection();
            conn.Open();
            string sql = "select code from client_m order by code;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            using (dataReader = cmd.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    ClientCode = (int)dataReader["code"];
                }
            }

            ClientCode++;

            sql = "insert into client_m (code) values (" + ClientCode + ");";
            cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
            //string sql = "select code from client_m order by code;";
            //cmd = new NpgsqlCommand(sql, conn);
            //using (reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        ClientCode = (int)reader["code"];
            //    }
            //}
            //ClientCode++;


        }

        private void ClientMaster_add_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth, Pass);
                clientmaster.Show();
            }
            else
            {
                screan = true;
            }
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }
        #region "法人"
        #region "登記簿謄本"
        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;
                registerCopyTextBox.Text = path;
            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "古物商許可証"
        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;
                antiqueLicenseTextBox.Text = path;
            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "身分証明書"
        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;
                photoIDTextBox.Text = path;
            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "定款など"
        private void Button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;

                aolFinancialShareholderTextBox.Text = path;

            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "印鑑証明"
        private void Button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;

                sealCertificationTextBox.Text = path;

            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "納税証明書"
        private void Button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;

                taxCertificationTextBox.Text = path;

            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "在留カード"
        private void Button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;

                residenceCardTextBox.Text = path;

            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #endregion
        #region "個人"
        #region "登記簿謄本"
        private void Button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;
                registerCopyTextBox1.Text = path;
            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "古物商許可証"
        private void Button16_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;
                antiqueLicenseTextBox1.Text = path;
            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "顔つき身分証明書"
        private void Button15_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;
                photoIDTextBox1.Text = path;
            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "定款など"
        private void Button14_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;

                aolFinancialShareholderTextBox1.Text = path;

            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "印鑑証明"
        private void Button13_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;

                sealCertificationTextBox1.Text = path;

            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "納税証明書"
        private void Button12_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;

                taxCertificationTextBox1.Text = path;

            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion
        #region "在留カード"
        private void Button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "ファイルを開く";
            op.InitialDirectory = @"C:\Users\fpadmin\Desktop";
            op.Filter = "すべてのファイル(*.*)|*.*";
            op.FilterIndex = 1;

            DialogResult dialog = op.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                path = op.FileName;

                residenceCardTextBox1.Text = path;

            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion

        #endregion

        #region"画像の表示　法人"
        private void textBox24_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(photoIDTextBox.Text))
            {
                pictureBox1.ImageLocation = photoIDTextBox.Text;
            }
        }

        private void textBox21_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(registerCopyTextBox.Text))
            {
                pictureBox1.ImageLocation = registerCopyTextBox.Text;
            }
        }

        private void textBox22_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(antiqueLicenseTextBox.Text))
            {
                pictureBox1.ImageLocation = antiqueLicenseTextBox.Text;
            }
        }

        private void textBox25_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(aolFinancialShareholderTextBox.Text))
            {
                pictureBox1.ImageLocation = aolFinancialShareholderTextBox.Text;
            }
        }

        private void textBox27_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(taxCertificationTextBox.Text))
            {
                pictureBox1.ImageLocation = taxCertificationTextBox.Text;
            }
        }

        private void textBox29_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(residenceCardTextBox.Text))
            {
                pictureBox1.ImageLocation = residenceCardTextBox.Text;
            }
        }

        private void textBox26_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sealCertificationTextBox.Text))
            {
                pictureBox1.ImageLocation = sealCertificationTextBox.Text;
            }
        }
        #endregion

        #region"画像の表示　個人"

        private void textBox37_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(registerCopyTextBox1.Text))
            {
                pictureBox2.ImageLocation = registerCopyTextBox1.Text;
            }
        }

        private void textBox36_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(antiqueLicenseTextBox1.Text))
            {
                pictureBox2.ImageLocation = antiqueLicenseTextBox1.Text;
            }
        }

        private void textBox35_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(photoIDTextBox1.Text))
            {
                pictureBox2.ImageLocation = photoIDTextBox1.Text;
            }
        }

        private void textBox33_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(aolFinancialShareholderTextBox1.Text))
            {
                pictureBox2.ImageLocation = aolFinancialShareholderTextBox1.Text;
            }
        }

        private void textBox31_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(taxCertificationTextBox1.Text))
            {
                pictureBox2.ImageLocation = taxCertificationTextBox1.Text;
            }
        }

        private void textBox30_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(residenceCardTextBox1.Text))
            {
                pictureBox2.ImageLocation = residenceCardTextBox1.Text;
            }
        }

        private void textBox32_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sealCertificationTextBox1.Text))
            {
                pictureBox2.ImageLocation = sealCertificationTextBox1.Text;
            }
        }

        #endregion

        #region"半角"
        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            nameKanaTextBox1.Text = InputControl.Kana(nameKanaTextBox1.Text);
            nameKanaTextBox1.Select(nameKanaTextBox1.Text.Length, 0);
        }

        private void textBox52_TextChanged(object sender, EventArgs e)
        {
            addressKanaTextBox1.Text = InputControl.Kana(addressKanaTextBox1.Text);
            addressKanaTextBox1.Select(addressKanaTextBox1.Text.Length, 0);
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            accountKanaTextBox1.Text = InputControl.Kana(accountKanaTextBox1.Text);
            accountKanaTextBox1.Select(accountKanaTextBox1.Text.Length, 0);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            companyKanaTextBox.Text = InputControl.Kana(companyKanaTextBox.Text);
            companyKanaTextBox.Select(companyKanaTextBox.Text.Length, 0);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            shopNameKanaTextBox.Text = InputControl.Kana(shopNameKanaTextBox.Text);
            shopNameKanaTextBox.Select(shopNameKanaTextBox.Text.Length, 0);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            addressKanaTextBox.Text = InputControl.Kana(addressKanaTextBox.Text);
            addressKanaTextBox.Select(addressKanaTextBox.Text.Length, 0);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            accountNameKanaTextBox.Text = InputControl.Kana(accountNameKanaTextBox.Text);
            accountNameKanaTextBox.Select(accountNameKanaTextBox.Text.Length, 0);
        }
        #endregion

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                //MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }
    }
}
