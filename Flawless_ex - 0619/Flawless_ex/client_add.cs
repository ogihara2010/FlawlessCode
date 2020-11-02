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
using System.Text.RegularExpressions;

namespace Flawless_ex
{
    public partial class client_add : Form
    {
        Statement statement;
        MainMenu mainMenu;
        DataTable dt = new DataTable();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        int staff_id;
        int type;
        string staff_name;
        string address;
        decimal Total;
        string access_auth;
        string document;
        int control;
        bool screan;
        public string data;
        string path = "";

        int ClientCode;             //顧客コード

        #region "買取販売履歴"
        string name1;
        string phoneNumber1;
        string address1;
        string addresskana1;
        string code1;
        string item1;
        string date1;
        string date2;
        string method1;
        string amountA;
        string amountB;
        string antiqueNumber;
        string documentNumber;
        #endregion
        string pass;
        string kana;
        int grade;

        private FileServer fileServer = new FileServer();
        InputControl inputControl = new InputControl();

        public client_add(Statement statement, int id, int type, string Access_auth, string Pass)
        {
            InitializeComponent();
            //this.mainMenu = mainMenu;
            this.statement = statement;
            staff_id = id;
            this.access_auth = Access_auth;
            this.pass = Pass;
            this.type = type;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region "法人　登録"
        private void Button5_Click(object sender, EventArgs e)
        {
            #region"必須項目"
            if (string.IsNullOrEmpty(companyNameTextBox.Text))
            {
                MessageBox.Show("会社名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(companyKanaTextBox.Text))
            {
                MessageBox.Show("会社名のカタカナを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(antiqueNumberTextBox.Text))
            {
                MessageBox.Show("古物番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(PostalUpCordTextBox.Text))
            {
                MessageBox.Show("郵便番号の上半分を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(PostalDownCordTextBox.Text))
            {
                MessageBox.Show("郵便番号の下半分を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("住所を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(addressKanaTextBox.Text))
            {
                MessageBox.Show("住所のカタカナを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(telTextBox.Text))
            {
                MessageBox.Show("電話番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("担当者名義を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(photoIDTextBox.Text))
            {
                MessageBox.Show("身分証明書を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(registerCopyTextBox.Text))
            {
                MessageBox.Show("登記簿謄本を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(antiqueLicenseTextBox.Text))
            {
                MessageBox.Show("古物商許可証を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #endregion
            //会社名カナ、店舗名カナ、住所カナ、口座名義カナ
            #region"カタカナ入力欄の確認"
            if (!string.IsNullOrEmpty(companyKanaTextBox.Text))
            {
                if (!Regex.IsMatch(companyKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                {
                    MessageBox.Show("会社名カナにカタカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(shopKanaTextBox.Text))
            {
                if (!Regex.IsMatch(shopKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                {
                    MessageBox.Show("店舗名カナにカタカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(addressKanaTextBox.Text))
            {
                if (!Regex.IsMatch(addressKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                {
                    MessageBox.Show("住所カナにカタカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(accountKanaTextBox.Text))
            {
                if (!Regex.IsMatch(accountKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                {
                    MessageBox.Show("口座名義カナにカタカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            #endregion

            //string RegistrationDate = this.deliveryDateBox.Text;
            DateTime registrationDate = DateTime.Parse(deliveryDateBox.Value.ToShortDateString());

            string CompanyName = this.companyNameTextBox.Text;
            string CompanyNameKana = this.companyKanaTextBox.Text;
            
            string PostalUPCoreNumber = PostalUpCordTextBox.Text;
            string PostalDownCordNumber = PostalDownCordTextBox.Text;
            
            string Address = this.addressTextBox.Text;
            string AddressKana = this.addressKanaTextBox.Text;
            string ShopName = this.shopNameTextBox.Text;
            string ShopNameKana = this.shopKanaTextBox.Text;
            string PhoneNumber = this.telTextBox.Text;
            string FaxNumber = this.faxTextBox.Text;
            string Position = this.positionTextBox.Text;
            string ClientStaffName = this.nameTextBox.Text;
            string EmailAddress = this.mailTextBox.Text;
            string BankName = this.bankNameTextBox.Text;
            string DepositType = this.depositTypeTextBox.Text;
            string AccountName = this.accountNameTextBox.Text;
            string URLinfor = this.urlTextBox.Text;
            string BranchName = this.branchNameTextBox.Text;
            string AccountNumber = this.accountNumberTextBox.Text;
            string AccountNameKana = this.accountKanaTextBox.Text;
            string RegisterCopy = fileServer.UploadImage(registerCopyTextBox.Text, FileServer.Filetype.RegisterCopy);
            string Antiquelicense = fileServer.UploadImage(this.antiqueLicenseTextBox.Text, FileServer.Filetype.Antiquelicense);
            decimal AntiqueNumber = decimal.Parse(this.antiqueNumberTextBox.Text);
            string ID = fileServer.UploadImage(this.photoIDTextBox.Text, FileServer.Filetype.ID);

            //string PeriodStay = periodStayDateTimePicker.Text;
            DateTime? periodStay = DateTime.Parse(periodStayDateTimePicker.Value.ToShortTimeString());

            string SealCertification = fileServer.UploadImage(this.sealCertificationTextBox.Text, FileServer.Filetype.SealCertification);
            string TaxCertification = fileServer.UploadImage(this.taxCertificationTextBox.Text, FileServer.Filetype.TaxCertification);
            string Remarks = this.remarksTextBox.Text;
            string ResidenceCard = fileServer.UploadImage(this.residenceCardTextBox.Text, FileServer.Filetype.ResidenceCard);
            string AolFinancialShareholder = fileServer.UploadImage(aolFinancialShareholderTextBox.Text, FileServer.Filetype.AolFinancialShareholder);
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");


            //if (string.IsNullOrEmpty(textBox21.Text)) 
            //{
            //    periodStay = null;
            //}

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            conn.Open();

            string sql = "select code from client_m order by code;";
            cmd = new NpgsqlCommand(sql, conn);

            using(reader=cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ClientCode = (int)reader["code"];
                }
            }

            ClientCode++;

            string sql_str = "Insert into client_m (type, registration_date, company_name, company_kana, shop_name, shop_name_kana, antique_number," +
                " address, address_kana, phone_number, fax_number, position, name, email_address, url_infor, bank_name, branch_name, deposit_type," +
                " account_number, account_name, account_name_kana, remarks, id, register_date, antique_license, tax_certificate, residence_card, period_stay," +
                " seal_certification, invalid, aol_financial_shareholder, register_copy, insert_name, postal_code1, postal_code2, code)" +
                " VALUES (" + 0 + " , '" + registrationDate + "' , '" + CompanyName + "' ,'" + CompanyNameKana + "' ," +
                " '" + ShopName + "' ,  '" + ShopNameKana + " ', '" + AntiqueNumber + "' , '" + Address + "' , '" + AddressKana + "' ," +
                " '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + Position + "' , '" + ClientStaffName + "' , '" + EmailAddress + "', '" + URLinfor + "'," +
                " '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' ," +
                " '" + Remarks + "' , '" + ID + "' , '" + b + "','" + Antiquelicense + "','" + TaxCertification + "','" + ResidenceCard + "','" + periodStay + "'," +
                "'" + SealCertification + "'," + 0 + ",'" + AolFinancialShareholder + "','" + RegisterCopy + "'," + staff_id + ",'" + PostalUPCoreNumber + "'," +
                "'" + PostalDownCordNumber + "','" + ClientCode + "');";

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
            type = 0;
            staff_name = ClientStaffName;
            address = Address;
            statement.ClientCode = ClientCode;
            statement.count = 1;
            statement.type = 0;
            //Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1,
            //    phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
            this.Close();
            statement.Show();
        }
        #endregion

        #region "個人　登録"
        private void Button18_Click(object sender, EventArgs e)
        {
            #region"必須項目"
            if (string.IsNullOrEmpty(nameTextBox1.Text))
            {
                MessageBox.Show("氏名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(nameKanaTextBox1.Text))
            {
                MessageBox.Show("氏名のカタカナを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(PostalUpCordTextBox2.Text))
            {
                MessageBox.Show("郵便番号の上半分を入力しください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(PostalDownCordTextBox2.Text))
            {
                MessageBox.Show("郵便番号の下半分を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(addressTextBox1.Text))
            {
                MessageBox.Show("住所を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(addressKanaTextBox1.Text))
            {
                MessageBox.Show("住所のカタカナを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(telTextBox1.Text))
            {
                MessageBox.Show("電話番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(occupationTextBox1.Text))
            {
                MessageBox.Show("職業を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(idNumberTextBox1.Text))
            {
                MessageBox.Show("身分証番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(photoIDTextBox1.Text))
            {
                MessageBox.Show("顔つき身分証を登録してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            #region"カタカナ入力欄の確認"
            if (!string.IsNullOrEmpty(nameKanaTextBox1.Text))
            {
                if (!Regex.IsMatch(nameKanaTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                {
                    MessageBox.Show("氏名カナにカタカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(accountKanaTextBox1.Text))
            {
                if (!Regex.IsMatch(accountKanaTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                {
                    MessageBox.Show("口座名義カナにカタカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(addressKanaTextBox1.Text))
            {
                if (!Regex.IsMatch(addressKanaTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                {
                    MessageBox.Show("住所カナにカタカナを入力して下さい。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
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

            DateTime Birthday = DateTime.Parse(birthdayDateTimePicker.Value.ToShortDateString());
            //string Birthday = this.textBox50.Text;

            string PostalCodeUpNumber = this.PostalUpCordTextBox2.Text;
            string PostalCodeDownNumber = PostalDownCordTextBox2.Text;
            
            string Address = this.addressTextBox1.Text;
            string AddressKana = this.addressKanaTextBox1.Text;
            string PhoneNumber = this.telTextBox1.Text;
            string FaxNumber = this.faxTextBox1.Text;
            string Occupation = this.occupationTextBox1.Text;
            string EmailAddress = this.mailTextBox1.Text;
            string BankName = this.bankNameTextBox1.Text;
            string DepositType = this.depositTypeTextBox1.Text;
            string AccountName = this.accountNameTextBox1.Text;
            string BranchName = this.branchNameTextBox1.Text;
            string AccountNumber = this.accountNumberTextBox1.Text;
            string AccountNameKana = this.accountKanaTextBox1.Text;
            string RegisterCopy = fileServer.UploadImage(this.registerCopyTextBox1.Text, FileServer.Filetype.RegisterCopy);
            string Antiquelicense = fileServer.UploadImage(this.antiqueLicenseTextBox1.Text, FileServer.Filetype.Antiquelicense);
            decimal ID = decimal.Parse(idNumberTextBox1.Text);
            string PhotoID = fileServer.UploadImage(this.photoIDTextBox1.Text, FileServer.Filetype.ID);

            //string PeriodStay = periodStayDateTimePicker1.Text;
            DateTime? periodStay = DateTime.Parse(periodStayDateTimePicker1.Value.ToShortDateString());

            string SealCertification = SealCertification = fileServer.UploadImage(this.sealCertificationTextBox1.Text, FileServer.Filetype.SealCertification);
            string TaxCertification = TaxCertification = fileServer.UploadImage(this.taxCertificationTextBox1.Text, FileServer.Filetype.TaxCertification);
            string Remarks = this.textBox58.Text;
            string ResidenceCard = fileServer.UploadImage(this.residenceCardTextBox1.Text, FileServer.Filetype.ResidenceCard);
            string AolFinancialShareholder = fileServer.UploadImage(aolFinancialShareholderTextBox1.Text, FileServer.Filetype.AolFinancialShareholder);

            //if (string.IsNullOrEmpty(textBox37.Text))
            //{
            //    registrationDate = null;
            //}

            //if (string.IsNullOrEmpty(textBox30.Text))
            //{
            //    periodStay = null;
            //}

            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();

            conn.Open();

            string sql = "select code from client_m order by code;";
            cmd = new NpgsqlCommand(sql, conn);

            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ClientCode = (int)reader["code"];
                }
            }

            ClientCode++;

            string sql_str = "Insert into client_m (type, registration_date, name, name_kana, birthday, address, address_kana, phone_number, fax_number, email_address, occupation," +
                " bank_name, branch_name, deposit_type, account_number, account_name, account_name_kana, id_number, remarks, register_copy, antique_license, id, tax_certificate, residence_card," +
                " period_stay, seal_certification, invalid, aol_financial_shareholder, postal_code1, postal_code2, code, insert_name, register_date )" +
                " VALUES (" + 1 + " , '" + registrationDate + "' , '" + Name + "' ,'" + NameKana + "' , '" + Birthday + "' , '" + Address + "' , '" + AddressKana + "' ," +
                " '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + EmailAddress + "', '" + Occupation + "' , '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , " +
                "'" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + ID + "' , '" + Remarks + "','" + RegisterCopy + "' , '" + Antiquelicense + "'," +
                "'" + PhotoID + "' , '" + TaxCertification + "','" + ResidenceCard + "','" + periodStay + "','" + SealCertification + "'," + 0 + ",'" + AolFinancialShareholder + "'," +
                "'" + PostalCodeUpNumber + "','" + PostalCodeDownNumber + "','" + ClientCode + "','" + staff_id + "','" + b + "');";

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
            type = 1;
            staff_name = Name;
            address = Address;
            statement.ClientCode = ClientCode;
            statement.count = 1;
            statement.type = 1;
            //Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
            this.Close();
            statement.Show();
        }
        #endregion

        private void Client_add_Load(object sender, EventArgs e)
        {
            
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                //MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        #region"個人　値の検証"
        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            nameKanaTextBox1.Text = inputControl.Kana(nameKanaTextBox1.Text);
            nameKanaTextBox1.Select(nameKanaTextBox1.Text.Length, 0);
        }

        private void textBox52_TextChanged(object sender, EventArgs e)
        {
            addressKanaTextBox1.Text = inputControl.Kana(addressKanaTextBox1.Text);
            addressKanaTextBox1.Select(addressKanaTextBox1.Text.Length, 0);
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            accountKanaTextBox1.Text = inputControl.Kana(accountKanaTextBox1.Text);
            accountKanaTextBox1.Select(accountKanaTextBox1.Text.Length, 0);
        }
        #endregion
        #region"法人　値の検証"
        
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            companyKanaTextBox.Text = inputControl.Kana(companyKanaTextBox.Text);
            companyKanaTextBox.Select(companyKanaTextBox.Text.Length, 0);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            shopKanaTextBox.Text = inputControl.Kana(shopKanaTextBox.Text);
            shopKanaTextBox.Select(shopKanaTextBox.Text.Length, 0);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            addressKanaTextBox.Text = inputControl.Kana(addressKanaTextBox.Text);
            addressKanaTextBox.Select(addressKanaTextBox.Text.Length, 0);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            accountKanaTextBox.Text = inputControl.Kana(accountKanaTextBox.Text);
            accountKanaTextBox.Select(accountKanaTextBox.Text.Length, 0);
        }
        #endregion

        #region"法人　画像確認"
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
        #region"個人　画像確認"
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

        //メールアドレス
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            //mailTextBox.Text = inputControl.Kana(mailTextBox.Text);
            //mailTextBox.Select(mailTextBox.Text.Length, 0);
        }
        private void mailTextBox1_TextChanged(object sender, EventArgs e)
        {
            //mailTextBox1.Text = inputControl.Kana(mailTextBox1.Text);
            //mailTextBox1.Select(mailTextBox1.Text.Length, 0);
        }

        //法人　URL
        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            //urlTextBox.Text = inputControl.Kana(urlTextBox.Text);
            //urlTextBox.Select(urlTextBox.Text.Length, 0);
        }

        #region"画像選択"
        //画像選択（法人）
        private void IdentificationButton_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
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

        private void registeredCopyButton_Click(object sender, EventArgs e)
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

        private void taxCertificateButton_Click(object sender, EventArgs e)
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

        private void aolButton_Click(object sender, EventArgs e)
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

        private void residenceCardButton_Click(object sender, EventArgs e)
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

        private void sealCertificateButton_Click(object sender, EventArgs e)
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

        //画像の選択（個人）
        private void registerCopyButton1_Click(object sender, EventArgs e)
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

        private void antiqueLicenseButton1_Click(object sender, EventArgs e)
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

        private void IdentificationButton1_Click(object sender, EventArgs e)
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

        private void taxCertificateButton1_Click(object sender, EventArgs e)
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

        private void aolButton1_Click(object sender, EventArgs e)
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

        private void residenceButton1_Click(object sender, EventArgs e)
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

        private void sealCertificateButton1_Click(object sender, EventArgs e)
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

        private void client_add_FormClosed(object sender, FormClosedEventArgs e)
        {
            client_search client_Search = new client_search(statement, staff_id, type, staff_name, address, Total, control, document, access_auth, pass);
            this.data = client_Search.data;
            client_Search.ShowDialog();
        }

    }
}
