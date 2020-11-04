using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;
using System.Text;
using System.IO.Packaging;

namespace Flawless_ex
{
    public partial class ClientMaster_UPD : Form
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        public int type;
        public string name;
        public string address;
        public int staff_code;
        public string access_auth;
        public string path;
        public string pass;
        public string kana;
        int ClientCode;
        InputControl InputControl = new InputControl();

        NpgsqlTransaction transaction;
        NpgsqlCommand cmd;

        private FileServer fileServer = new FileServer();

        public ClientMaster_UPD(MasterMaintenanceMenu master, int type, int code, int staff_code, string access_auth, string Pass)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
            this.type = type;  //  法人・個人
            this.access_auth = access_auth;
            this.pass = Pass;
            ClientCode = code;              //顧客番号
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        #region "法人　更新"
        private void Button20_Click(object sender, EventArgs e)
        {
            #region "起こりうるミス"
            #region "必須項目"
            if (string.IsNullOrEmpty(companyNameTextBox.Text))
            {
                MessageBox.Show("会社名を入力してください。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(companyKanaTextBox.Text))
            {
                MessageBox.Show("会社名のフリガナを入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(companyKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("会社名カナにはカタカナのみ入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //else if (System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(postalUpCodeTextBox.Text, @"^[a-zA-Z]+$"))
            //{
            //    MessageBox.Show("正しく入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox.Text, @"^[a-zA-Z]+$"))
            //{
            //    MessageBox.Show("正しく入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            else if (string.IsNullOrEmpty(postalDownCodeTextBox.Text) || string.IsNullOrEmpty(postalUpCodeTextBox.Text))
            {
                MessageBox.Show("郵便番号を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //else if (int.Parse(postalDownCodeTextBox.Text) >= 10000 || int.Parse(postalUpCodeTextBox.Text) >= 1000)
            //{
            //    MessageBox.Show("郵便番号を正しく入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            else if (string.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("住所を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(addressKanaTextBox.Text))
            {
                MessageBox.Show("住所のフリガナを入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //else if (!System.Text.RegularExpressions.Regex.IsMatch(addressKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            //{
            //    MessageBox.Show("住所カナにはカタカナのみ入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            else if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("担当者名義を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(telTextBox.Text))
            {
                MessageBox.Show("電話番号を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(registerCopyTextBox.Text))
            {
                MessageBox.Show("登記簿謄本を選んで下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(antiqueLicenseTextBox.Text))
            {
                MessageBox.Show("古物商許可証を選んで下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(antiqueNumberTextBox.Text))
            {
                MessageBox.Show("古物番号を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(antiqueNumberTextBox.Text, @"^\p{N}+$"))
            {
                MessageBox.Show("古物番号には数字を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(photoIDTextBox.Text))
            {
                MessageBox.Show("身分証明書もしくは顔つき身分証明証を選択して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(reasonTextBox.Text))
            {
                MessageBox.Show("変更する理由を記入してください", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion
            #region "その他の項目"
            else
            {
                if (!string.IsNullOrEmpty(shopKanaTextBox.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(shopKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                    {
                        MessageBox.Show("店舗名カナにはカタカナのみ入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(accountKanaTextBox.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(accountKanaTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                    {
                        MessageBox.Show("口座名義カナにはカタカナのみ入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                //if (!string.IsNullOrEmpty(textBox29.Text) && string.IsNullOrEmpty(textBox46.Text))
                //{
                //    MessageBox.Show("在留期限を入力して下さい。");
                //    return;
                //}
                //if (!string.IsNullOrEmpty(textBox46.Text) && string.IsNullOrEmpty(textBox29.Text))
                //{
                //    MessageBox.Show("正しく入力して下さい。");
                //    return;
                //}

                //if (!string.IsNullOrEmpty(faxTextBox.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(faxTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(faxTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(faxTextBox.Text, @"^[a-zA-Z]+$"))
                //    {
                //        MessageBox.Show("fax 番号に正しく入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //if (!string.IsNullOrEmpty(mailTextBox.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(mailTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(mailTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(mailTextBox.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                //    {
                //        MessageBox.Show("メールアドレスを正しく入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}
                //if (!string.IsNullOrEmpty(urlTextBox.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(urlTextBox.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(urlTextBox.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(urlTextBox.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                //    {
                //        MessageBox.Show("URL を正しく入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}
            }
            #endregion
            #endregion
            DialogResult dr = MessageBox.Show("更新しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }
            #region "旧データ"
            NpgsqlConnection conn1 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter1;
            PostgreSQL postgre = new PostgreSQL();
            conn1 = postgre.connection();
            //conn1.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            string sql_old = "select * from client_m where code = '" + ClientCode + "';";
            conn1.Open();
            adapter1 = new NpgsqlDataAdapter(sql_old, conn1);
            adapter1.Fill(dt2);
            DataRow row1;
            row1 = dt2.Rows[0];
            string RegistrationDate2_old = ((DateTime)row1["registration_date"]).ToString("yyyy/MM/dd");

            string PostalCode1_old = row1["postal_code1"].ToString();
            string PostalCode2_old = row1["postal_code2"].ToString();
            string Address_old = row1["address"].ToString();
            string AddressKana_old = row1["address_kana"].ToString();
            string CompanyName_old = row1["company_name"].ToString();
            string CompanyNameKana_old = row1["company_kana"].ToString();
            string ShopName_old = row1["shop_name"].ToString();
            string ShopNameKana_old = row1["shop_name_kana"].ToString();
            string Antique_old = row1["antique_number"].ToString();
            string PhoneNumber_old = row1["phone_number"].ToString();
            string FaxNumber_old = row1["fax_number"].ToString();
            string URL_old = row1["url_infor"].ToString();
            string ClientStaffName_old = row1["name"].ToString();
            string Position_old = row1["position"].ToString();
            string EmailAddress_old = row1["email_address"].ToString();
            string BankName_old = row1["bank_name"].ToString();
            string DepositType_old = row1["deposit_type"].ToString();
            string AccountName_old = row1["account_name"].ToString();
            string BranchName_old = row1["branch_name"].ToString();
            string AccountNumber_old = row1["account_number"].ToString();
            string AccountNameKana_old = row1["account_name_kana"].ToString();
            string RegisterCopy_old = row1["register_copy"].ToString();
            string Antiquelicense_old = row1["antique_license"].ToString();
            string ID_old = row1["id"].ToString();
            string PeriodStay_old = ((DateTime)row1["period_stay"]).ToString("yyyy/MM/dd");
            string SealCertification_old = row1["seal_certification"].ToString();
            string TaxCertification_old = row1["tax_certificate"].ToString();
            string Remarks_old = row1["remarks"].ToString();
            string ResidenceCard_old = row1["residence_card"].ToString();
            string AolFinancialShareholder_old = row1["aol_financial_shareholder"].ToString();
            DateTime dat1 = DateTime.Now;

            #endregion
            #region "更新するパラメータ"
            string RegistrationDate = this.dateTimePicker1.Value.ToString("yyyy/MM/dd");

            string CompanyName = this.companyNameTextBox.Text;
            string CompanyNameKana = this.companyKanaTextBox.Text;
            string PostalCode1 = this.postalUpCodeTextBox.Text;
            string PostalCode2 = this.postalDownCodeTextBox.Text;
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
            string BranchName = this.branchTextBox.Text;
            string AccountNumber = this.accountNumberTextBox.Text;
            string AccountNameKana = this.accountKanaTextBox.Text;

            #region"画像が変更されているか確認"
            string RegisterCopy = "";
            string Antiquelicense = "";
            string ID = "";
            string SealCertification = "";
            string TaxCertification = "";
            string ResidenceCard = "";
            string AolFinancialShareholder = "";

            if (registerCopyTextBox.Text != RegisterCopy_old) 
            {
                RegisterCopy = fileServer.UploadImage(this.registerCopyTextBox.Text, FileServer.Filetype.RegisterCopy);
            }
            else
            {
                RegisterCopy = RegisterCopy_old;
            }

            if (Antiquelicense_old != antiqueLicenseTextBox.Text)
            {
                Antiquelicense = fileServer.UploadImage(this.antiqueLicenseTextBox.Text, FileServer.Filetype.Antiquelicense);
            }
            else
            {
                Antiquelicense = Antiquelicense_old;
            }

            if (photoIDTextBox.Text != ID_old)
            {
                ID = fileServer.UploadImage(this.photoIDTextBox.Text, FileServer.Filetype.ID);
            }
            else
            {
                ID = ID_old;
            }
            if (SealCertification_old != sealCertificationTextBox.Text)
            {
                SealCertification = fileServer.UploadImage(this.sealCertificationTextBox.Text, FileServer.Filetype.SealCertification);
            }
            else
            {
                SealCertification = SealCertification_old;
            }

            if (TaxCertification_old != taxCertificationTextBox.Text)
            {
                TaxCertification = fileServer.UploadImage(this.taxCertificationTextBox.Text, FileServer.Filetype.TaxCertification);
            }
            else
            {
                TaxCertification = TaxCertification_old;
            }

            if (ResidenceCard_old != residenceCardTextBox.Text)
            {
                ResidenceCard = fileServer.UploadImage(this.residenceCardTextBox.Text, FileServer.Filetype.ResidenceCard);
            }
            else
            {
                ResidenceCard = ResidenceCard_old;
            }

            if (AolFinancialShareholder_old != aolFinancialShareholderTextBox.Text)
            {
                AolFinancialShareholder = fileServer.UploadImage(this.aolFinancialShareholderTextBox.Text, FileServer.Filetype.AolFinancialShareholder);
            }
            else
            {
                AolFinancialShareholder = AolFinancialShareholder_old;
            }
            #endregion

            string AntiqueNumber = antiqueNumberTextBox.Text;
            string Remarks = this.remarksTextBox.Text;
            string PeriodStay = periodStayCompanyDateTimePicker.Value.ToString("yyyy/MM/dd");
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            string reason1 = this.reasonTextBox.Text;
            #endregion
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;


            string sql_str = "UPDATE client_m SET type = " + 0 + " ,registration_date = '" + RegistrationDate + "' ,company_name =  '" + CompanyName + "' ,company_kana = '" + CompanyNameKana + "' ,shop_name =  '" + ShopName + "' ,shop_name_kana = '" + ShopNameKana + "',address =  '" + Address + "' ,address_kana = '" + AddressKana + "' ,phone_number = '" + PhoneNumber + "' ,fax_number = '" + FaxNumber + "' ,position = '" + Position + "' , name = '" + ClientStaffName +
                "' ,email_address = '" + EmailAddress + "',url_infor = '" + URLinfor + "',bank_name = '" + BankName + "' ,branch_name = '" + BranchName + "' ,deposit_type = '" + DepositType + "' ,account_number = '" + AccountNumber + "' ,account_name_kana = '" + AccountNameKana + "' ,account_name = '" + AccountName + "' ,remarks = '" + Remarks + "' ,id = '" + ID + "' ,register_date = '" + b + "',antique_license = '" + Antiquelicense + "',tax_certificate = '" + TaxCertification + "',residence_card = '" + ResidenceCard + "',period_stay = '" + PeriodStay + "',seal_certification = '" + SealCertification +
                "',invalid = " + 0 + ",aol_financial_shareholder = '" + AolFinancialShareholder + "',register_copy = '" + RegisterCopy + "',insert_name = " + staff_code + ",postal_code1 = " + PostalCode1 + ",postal_code2 = '" + PostalCode2 + "',reason = '" + reason1 + "' where code = " + ClientCode + "; ";

            conn = postgre.connection();
            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            using (transaction = conn.BeginTransaction())
            {
                cmd = new NpgsqlCommand(sql_str, conn);
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }

            #region "履歴へ"
            NpgsqlDataAdapter adapter2;
            #region "絶対入力する項目"
            #region "登記簿謄本登録日"
            if (RegistrationDate2_old != RegistrationDate)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + RegistrationDate2_old + "' , '" + RegistrationDate + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "住所変更"
            if (Address_old != Address)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code)" +
                    " VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + Address_old + "' , '" + Address + "' ,'" + reason1 + "', '" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "住所フリガナ変更(住所変更に伴う)"
            if (AddressKana_old != AddressKana)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + AddressKana_old + "' , '" + AddressKana + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "郵便番号変更"
            if ((PostalCode1_old != PostalCode1) || (PostalCode2_old != PostalCode2))
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code)" +
                    " VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + PostalCode1_old + "-" + PostalCode2_old + "' , '" + PostalCode1 + "-" + PostalCode2 + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "担当者名義変更"
            if (ClientStaffName_old != ClientStaffName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + ClientStaffName_old + "' , '" + ClientStaffName + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion            
            #region "電話番号変更"
            if (PhoneNumber_old != PhoneNumber)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + PhoneNumber_old + "' , '" + PhoneNumber + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "登記簿謄本変更"
            if (RegisterCopy_old != RegisterCopy)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + RegisterCopy_old + "' , '" + RegisterCopy + "' ,'" + reason1 + "', '" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "身分証明書変更"
            if (ID_old != ID)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + ID_old + "' , '" + ID + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "古物商許可証変更"
            if (Antiquelicense_old != Antiquelicense)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + Antiquelicense_old + "' , '" + Antiquelicense + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "会社名変更"
            if (CompanyName_old != CompanyName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + CompanyName_old + "' , '" + CompanyName + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "会社名フリガナ変更(会社名変更に伴う)"
            if (CompanyNameKana_old != CompanyNameKana)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + CompanyNameKana_old + "' , '" + CompanyNameKana + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region"古物番号"
            if (Antique_old != AntiqueNumber)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + Antique_old + "' , '" + AntiqueNumber + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #endregion
            #region "必須項目以外"            
            #region "店舗名変更"
            if (ShopName_old != ShopName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + ShopName_old + "' , '" + ShopName + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "店舗名フリガナ変更(店舗名変更に伴う)"
            if (ShopNameKana_old != ShopNameKana)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + ShopNameKana_old + "' , '" + ShopNameKana + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "FAX番号変更"
            if (FaxNumber_old != FaxNumber)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + FaxNumber_old + "' , '" + FaxNumber + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "URL変更"
            if (URL_old != URLinfor)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + URL_old + "' , '" + URLinfor + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "役職変更"
            if (Position_old != Position)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + Position_old + "' , '" + Position + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "メールアドレス変更"
            if (EmailAddress_old != EmailAddress)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + EmailAddress_old + "' , '" + EmailAddress + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "銀行名変更"
            if (BankName_old != BankName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code)" +
                " VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + BankName_old + "' , '" + BankName + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "預金種別変更"
            if (DepositType_old != DepositType)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + DepositType_old + "','" + DepositType + "','" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "口座名義変更"
            if (AccountName_old != AccountName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + AccountName_old + "' , '" + AccountName + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "支店名変更"
            if (BranchName_old != BranchName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "'," + staff_code + ",'" + ResidenceCard_old + "','" + ResidenceCard + "','" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "口座番号変更"
            if (AccountNumber_old != AccountNumber)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "'," + staff_code + ",'" + AccountNumber_old + "','" + AccountNumber + "','" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "口座名義人フリガナ変更(口座名義人変更に伴う)"
            if (AccountNameKana_old != AccountNameKana)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "'," + staff_code + ",'" + AccountNameKana_old + "','" + AccountNameKana + "','" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "在留期限変更"
            if (PeriodStay_old != PeriodStay)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + PeriodStay_old + "' , '" + PeriodStay + "' ,'" + reason1 + "', '" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "印鑑証明変更"
            if (SealCertification_old != SealCertification)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + SealCertification_old + "' , '" + SealCertification + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "納税証明書変更"
            if (TaxCertification_old != TaxCertification)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + TaxCertification_old + "' , '" + TaxCertification + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "在留カード"
            if (ResidenceCard_old != ResidenceCard)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "'," + staff_code + ",'" + ResidenceCard_old + "','" + ResidenceCard + "','" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "定款等変更"
            if (AolFinancialShareholder_old != AolFinancialShareholder)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                "VALUES (" + 4 + ",'" + dat1 + "'," + staff_code + ",'" + AolFinancialShareholder_old + "','" + AolFinancialShareholder + "','" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region"備考"
            if (Remarks_old != Remarks)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 4 + ",'" + dat1 + "' ," + staff_code + ",'" + Remarks_old + "' , '" + Remarks + "' ,'" + reason1 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #endregion
            #endregion
            MessageBox.Show("更新しました。");

            this.Close();
        }
        #endregion

        #region "法人　無効"
        private void Button19_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("無効にしますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }
            else if (result == DialogResult.Yes)
            {

                NpgsqlConnection conn;
                //NpgsqlDataAdapter adapter;
                //NpgsqlDataAdapter adapter2;
                //NpgsqlCommandBuilder builder;
                //NpgsqlCommandBuilder builder2;
                DateTime dat = DateTime.Now;

                PostgreSQL postgre = new PostgreSQL();
                conn = postgre.connection();
                //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                string remove_sql = "update client_m set invalid = 1 where code = '" + ClientCode + "';";
                string revisions = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "values (" + 4 + ",'" + dat + "'," + staff_code + ",'" + "有効" + "','" + "無効" + "','" + reasonTextBox.Text + "','" + ClientCode + "');";

                using (transaction = conn.BeginTransaction())
                {
                    cmd = new NpgsqlCommand(remove_sql, conn);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }

                cmd = new NpgsqlCommand(revisions, conn);
                cmd.ExecuteNonQuery();

                //adapter = new NpgsqlDataAdapter(remove_sql, conn);
                //builder = new NpgsqlCommandBuilder(adapter);
                //adapter.Fill(dt);
                //adapter.Update(dt);
                //adapter2 = new NpgsqlDataAdapter(revisions, conn);
                //builder2 = new NpgsqlCommandBuilder(adapter2);
                //adapter2.Fill(dt4);

                MessageBox.Show("無効にしました。");
                return;
            }
            else
            {
                return;
            }
        }
        #endregion

        private void ClientMaster_UPD_Load(object sender, EventArgs e)
        {
            if (type == 0)
            {
                tabControl1.SelectedTab = tabPage1;
                tabControl1.TabPages.Remove(tabPage2);
            }
            else if (type == 1)
            {
                tabControl1.SelectedTab = tabPage2;
                tabControl1.TabPages.Remove(tabPage1);
            }
            else { }

            if (access_auth == "C")
            {
                this.button6.Enabled = false;
                this.button19.Enabled = false;
                this.button5.Enabled = true;
                this.button20.Enabled = true;
            }

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            DataRow row;

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            if (type == 0)
            {
                //法人
                string sql_str = "select * from client_m where  code = '" + ClientCode + "';";
                conn.Open();
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                row = dt.Rows[0];
                #region "入力データ"
                string RegistrationDate1;
                string CompanyName;
                string CompanyNameKana;
                string PostalCode1;
                string PostalCode2;
                string Address;
                string AddressKana;
                string ShopName;
                string ShopNameKana;
                string PhoneNumber;
                string FaxNumber;
                string Position;
                string StaffName;
                string EmailAddress;
                string BankName;
                string DepositType;
                string AccountName;
                string URLinfor;
                string BranchName;
                string AccountNumber;
                string AccountNameKana;
                string RegisterCopy;
                string Antiquelicense;
                string AntiqueNumber;
                string ID;
                string PeriodStay;
                string SealCertification;
                string TaxCertification;
                string Remarks;
                string ResidenceCard;
                string AolFinancialShareholder;
                #endregion
                #region"データベース"
                RegistrationDate1 = row["registration_date"].ToString();
                CompanyName = row["company_name"].ToString();
                CompanyNameKana = row["company_kana"].ToString();

                PostalCode1 = row["postal_code1"].ToString();
                PostalCode2 = row["postal_code2"].ToString();
                
                Address = row["address"].ToString();
                AddressKana = row["address_kana"].ToString();
                ShopName = row["shop_name"].ToString();
                ShopNameKana = row["shop_name_kana"].ToString();
                PhoneNumber = row["phone_number"].ToString();
                FaxNumber = row["fax_number"].ToString();
                Position = row["position"].ToString();
                StaffName = row["name"].ToString();
                EmailAddress = row["email_address"].ToString();
                BankName = row["bank_name"].ToString();
                DepositType = row["deposit_type"].ToString();
                AccountName = row["account_name"].ToString();
                URLinfor = row["url_infor"].ToString();
                BranchName = row["branch_name"].ToString();
                AccountNumber = row["account_number"].ToString();
                AccountNameKana = row["account_name_kana"].ToString();
                
                RegisterCopy = row["register_copy"].ToString();
                
                Antiquelicense = row["antique_license"].ToString();
                AntiqueNumber = row["antique_number"].ToString();
                ID = row["id"].ToString();
                PeriodStay = row["period_stay"].ToString();
                SealCertification = row["seal_certification"].ToString();
                TaxCertification = row["tax_certificate"].ToString();
                Remarks = row["remarks"].ToString();
                ResidenceCard = row["residence_card"].ToString();
                AolFinancialShareholder = row["aol_financial_shareholder"].ToString();
                #endregion
                #region "出力データ"
                this.dateTimePicker1.Text = RegistrationDate1;

                this.companyNameTextBox.Text = CompanyName;
                this.companyKanaTextBox.Text = CompanyNameKana;
                this.postalUpCodeTextBox.Text = PostalCode1;
                this.postalDownCodeTextBox.Text = PostalCode2;                
                this.addressTextBox.Text = Address;
                this.addressKanaTextBox.Text = AddressKana;
                this.shopNameTextBox.Text = ShopName;
                this.shopKanaTextBox.Text = ShopNameKana;
                this.telTextBox.Text = PhoneNumber;
                this.faxTextBox.Text = FaxNumber;
                this.positionTextBox.Text = Position;
                this.nameTextBox.Text = StaffName;
                this.mailTextBox.Text = EmailAddress;
                this.bankNameTextBox.Text = BankName;
                this.depositTypeTextBox.Text = DepositType;
                this.accountNameTextBox.Text = AccountName;
                this.urlTextBox.Text = URLinfor;
                this.branchTextBox.Text = BranchName;
                this.accountNumberTextBox.Text = AccountNumber;
                this.accountKanaTextBox.Text = AccountNameKana;
                this.registerCopyTextBox.Text = RegisterCopy;
                this.antiqueLicenseTextBox.Text = Antiquelicense;
                this.antiqueNumberTextBox.Text = AntiqueNumber.ToString();
                this.photoIDTextBox.Text = ID;
                this.aolFinancialShareholderTextBox.Text = AolFinancialShareholder;

                periodStayCompanyDateTimePicker.Text = PeriodStay;

                this.sealCertificationTextBox.Text = SealCertification;
                this.taxCertificationTextBox.Text = TaxCertification;
                this.remarksTextBox.Text = Remarks;
                this.residenceCardTextBox.Text = ResidenceCard;
                #endregion
            }
            else if (type == 1)
            {
                //個人
                string sql_str = "select * from client_m where code = '" + ClientCode + "';";
                conn.Open();
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                row = dt.Rows[0];
                #region "入力データ"
                string RegistrationDate2;
                string Name;
                string NameKana;
                string Birthday;
                string PostalCode1;
                string PostalCode2;
                string Address;
                string AddressKana;
                string PhoneNumber;
                string FaxNumber;
                string Occupation;
                string EmailAddress;
                string BankName;
                string DepositType;
                string AccountName;
                string BranchName;
                string AccountNumber;
                string AccountNameKana;
                string RegisterCopy;
                string Antiquelicense;
                string PhotoID;
                string ID;
                string PeriodStay;
                string SealCertification;
                string TaxCertification;
                string Remarks;
                string ResidenceCard;
                string AolFinancialShareholder;
                #endregion
                #region"データベース"
                RegistrationDate2 = row["registration_date"].ToString();
                PostalCode1 = row["postal_code1"].ToString();
                PostalCode2 = row["postal_code2"].ToString();
                Address = row["address"].ToString();
                AddressKana = row["address_kana"].ToString();
                Name = row["name"].ToString();
                NameKana = row["name_kana"].ToString();

                Birthday = row["birthday"].ToString();
                
                PhoneNumber = row["phone_number"].ToString();
                FaxNumber = row["fax_number"].ToString();
                Occupation = row["occupation"].ToString();
                EmailAddress = row["email_address"].ToString();
                BankName = row["bank_name"].ToString();
                DepositType = row["deposit_type"].ToString();
                AccountName = row["account_name"].ToString();
                BranchName = row["branch_name"].ToString();
                AccountNumber = row["account_number"].ToString();
                AccountNameKana = row["account_name_kana"].ToString();
                RegisterCopy = row["register_copy"].ToString();
                Antiquelicense = row["antique_license"].ToString();
                PhotoID = row["id"].ToString();
                ID = row["id_number"].ToString();
                PeriodStay = row["period_stay"].ToString();
                SealCertification = row["seal_certification"].ToString();
                TaxCertification = row["tax_certificate"].ToString();
                Remarks = row["remarks"].ToString();
                ResidenceCard = row["residence_card"].ToString();
                AolFinancialShareholder = row["aol_financial_shareholder"].ToString();
                #endregion
                #region "出力データ"
                this.deliveryDateBox.Text = RegistrationDate2;

                this.nameTextBox1.Text = Name;
                this.nameKanaTextBox1.Text = NameKana;

                birthdayDateTimePicker.Text = Birthday;

                this.postalUpCodeTextBox1.Text = PostalCode1.ToString();
                this.postalDownCodeTextBox1.Text = PostalCode2;
                this.addressTextBox1.Text = Address;
                this.addressKanaTextBox1.Text = AddressKana;
                this.telTextBox1.Text = PhoneNumber;
                this.faxTextBox1.Text = FaxNumber;
                this.occupationTextBox1.Text = Occupation;
                this.mailTextBox1.Text = EmailAddress;
                this.bankNameTextBox1.Text = BankName;
                this.depositTypeTextBox1.Text = DepositType;
                this.accountNameTextBox1.Text = AccountName;
                this.branchNameTextBox1.Text = BranchName;
                this.accountNumberTextBox1.Text = AccountNumber;
                this.accountKanaTextBox1.Text = AccountNameKana;
                this.registerCopyTextBox1.Text = RegisterCopy;
                this.antiqueLicenseTextBox1.Text = Antiquelicense;
                this.photoIDTextBox1.Text = PhotoID;
                this.idNumberTextBox1.Text = ID;
                this.aolFinancialShareholderTextBox1.Text = AolFinancialShareholder;

                //this.textBox46.Text = PeriodStay;
                periodStayindividualDateTimePicker.Value = DateTime.Parse(PeriodStay);

                this.sealCertificationTextBox1.Text = SealCertification;
                this.taxCertificationTextBox1.Text = TaxCertification;
                this.remarksTextBox1.Text = Remarks;
                this.residenceCardTextBox1.Text = ResidenceCard;
                #endregion
            }
            conn.Close();
        }

        #region "個人 更新"
        private void Button5_Click(object sender, EventArgs e)
        {
            string Year;
            string Month;
            string Day;

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
            //    MessageBox.Show("正しく入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //else if (System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox1.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(postalDownCodeTextBox1.Text, @"^[a-zA-Z]+$"))
            //{
            //    MessageBox.Show("正しく入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (string.IsNullOrEmpty(idNumberTextBox1.Text))
            {
                MessageBox.Show("身分証番号を入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //else if (!System.Text.RegularExpressions.Regex.IsMatch(idNumberTextBox1.Text, @"^\p{N}+$"))
            //{
            //    MessageBox.Show("数字を入力して下さい。");
            //    return;
            //}
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
                        MessageBox.Show("口座名義カナにカタカナのみ入力して下さい。", "入力確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                //if (!string.IsNullOrEmpty(mailTextBox1.Text))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(mailTextBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(mailTextBox1.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(mailTextBox1.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                //    {
                //        MessageBox.Show("正しく入力して下さい。");
                //        return;
                //    }
                //}
            }
            #endregion
            #endregion
            DialogResult dr = MessageBox.Show("個人情報を変更しますか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No)
            {
                return;
            }

            #region "旧データ"
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter1;
            string sql_old = "select * from client_m where code = '" + ClientCode + "';";

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            
            conn.Open();
            adapter1 = new NpgsqlDataAdapter(sql_old, conn);
            adapter1.Fill(dt2);
            DataRow row1;
            row1 = dt2.Rows[0];

            DateTime RegistrationDate2_old = (DateTime)row1["registration_date"];
            
            string PostalCode1_old = row1["postal_code1"].ToString();
            string PostalCode2_old = row1["postal_code2"].ToString();
            
            string Address_old = row1["address"].ToString();
            string AddressKana_old = row1["address_kana"].ToString();
            string Name_old = row1["name"].ToString();
            string NameKana_old = row1["name_kana"].ToString();

            string Birthday_old = row1["birthday"].ToString();
            
            string PhoneNumber_old = row1["phone_number"].ToString();
            string FaxNumber_old = row1["fax_number"].ToString();
            string Occupation_old = row1["occupation"].ToString();
            string EmailAddress_old = row1["email_address"].ToString();
            string BankName_old = row1["bank_name"].ToString();
            string DepositType_old = row1["deposit_type"].ToString();
            string AccountName_old = row1["account_name"].ToString();
            string BranchName_old = row1["branch_name"].ToString();
            string AccountNumber_old = row1["account_number"].ToString();
            string AccountNameKana_old = row1["account_name_kana"].ToString();
            string RegisterCopy_old = row1["register_copy"].ToString();
            string Antiquelicense_old = row1["antique_license"].ToString();
            string PhotoID_old = row1["id"].ToString();
            string ID_old = row1["id_number"].ToString();

            string PeriodStay_old = ((DateTime)row1["period_stay"]).ToString("yyyy/MM/dd");
            
            string SealCertification_old = row1["seal_certification"].ToString();
            string TaxCertification_old = row1["tax_certificate"].ToString();
            string Remarks_old = row1["remarks"].ToString();
            string ResidenceCard_old = row1["residence_card"].ToString();
            string AolFinancialShareholder_old = row1["aol_financial_shareholder"].ToString();
            DateTime dat1 = DateTime.Now;
            #endregion
            #region "更新するパラメータ"
            DateTime RegistrationDate = DateTime.Parse(deliveryDateBox.Value.ToString());

            string Name = this.nameTextBox1.Text;
            string NameKana = this.nameKanaTextBox1.Text;

            string Birthday = birthdayDateTimePicker.Value.ToLongDateString();            
            
            string PostalCode1 = this.postalUpCodeTextBox1.Text;
            string PostalCode2 = this.postalDownCodeTextBox1.Text;            
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
            string RegisterCopy = "";
            string Antiquelicense = "";
            string PhotoID = "";
            string SealCertification = "";
            string TaxCertification = "";
            string ResidenceCard = "";
            string AolFinancialShareholder = "";

            #region"画像が変更されているかの確認"
            if (RegisterCopy_old != registerCopyTextBox1.Text)
            {
                RegisterCopy = fileServer.UploadImage(this.registerCopyTextBox1.Text, FileServer.Filetype.RegisterCopy);
            }
            else
            {
                RegisterCopy = RegisterCopy_old;
            }

            if (Antiquelicense_old != antiqueLicenseTextBox1.Text)
            {
                Antiquelicense = fileServer.UploadImage(this.antiqueLicenseTextBox1.Text, FileServer.Filetype.Antiquelicense);
            }
            else
            {
                Antiquelicense = Antiquelicense_old;
            }

            if (PhotoID_old != photoIDTextBox1.Text)
            {
                PhotoID = fileServer.UploadImage(this.photoIDTextBox1.Text, FileServer.Filetype.ID);
            }
            else
            {
                PhotoID = PhotoID_old;
            }

            if (SealCertification_old != sealCertificationTextBox1.Text)
            {
                SealCertification = fileServer.UploadImage(this.sealCertificationTextBox1.Text, FileServer.Filetype.SealCertification);
            }
            else
            {
                SealCertification = SealCertification_old;
            }
            
            if (TaxCertification_old != taxCertificationTextBox1.Text)
            {
                TaxCertification = fileServer.UploadImage(this.taxCertificationTextBox1.Text, FileServer.Filetype.TaxCertification);
            }
            else
            {
                TaxCertification = TaxCertification_old;
            }

            if (ResidenceCard_old != residenceCardTextBox1.Text)
            {
                ResidenceCard = fileServer.UploadImage(this.residenceCardTextBox1.Text, FileServer.Filetype.ResidenceCard);
            }
            else
            {
                ResidenceCard = ResidenceCard_old;
            }

            if (AolFinancialShareholder_old != aolFinancialShareholderTextBox1.Text)
            {
                AolFinancialShareholder = fileServer.UploadImage(this.aolFinancialShareholderTextBox1.Text, FileServer.Filetype.AolFinancialShareholder);
            }
            else
            {
                AolFinancialShareholder = AolFinancialShareholder_old;
            }
            #endregion

            string ID = idNumberTextBox1.Text;
            string PeriodStay = periodStayindividualDateTimePicker.Value.ToString("yyyy/MM/dd");
            string Remarks = this.remarksTextBox1.Text;


            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            string reason2 = this.reasonText2.Text;
            #endregion
            NpgsqlDataAdapter adapter;

            string sql_str = "UPDATE client_m SET type = " + 1 + " , registration_date = '" + RegistrationDate + "' , name = '" + Name + "' ,name_kana = '" + NameKana + "' ,birthday = '" + Birthday + "' ,address = '" + Address + "' ,address_kana = '" + AddressKana + "' ,phone_number = '" + PhoneNumber + "' ,fax_number = '" + FaxNumber + "' ,email_address = '" + EmailAddress + "',occupation = '" + Occupation +
               "' ,bank_name = '" + BankName + "' ,branch_name = '" + BranchName + "' ,deposit_type = '" + DepositType + "' ,account_number = '" + AccountNumber + "' ,account_name = '" + AccountName + "' ,account_name_kana = '" + AccountNameKana + "' ,remarks = '" + Remarks + "',register_copy = '" + RegisterCopy + "' ,antique_license = '" + Antiquelicense + "', id = '" + PhotoID + "' ,tax_certificate = '" + TaxCertification + "',residence_card = '" + ResidenceCard + "',period_stay = '" + PeriodStay + "',seal_certification = '" + SealCertification + "',invalid = " +
              0 + ",aol_financial_shareholder = '" + AolFinancialShareholder + "',register_date = '" + b + "',insert_name = " + staff_code + ",postal_code1 = " + PostalCode1 + ",postal_code2 = '" + PostalCode2 + "',reason = '" + reason2 + "'where code = " + ClientCode + ";";

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);

            #region "履歴へ"
            NpgsqlDataAdapter adapter2;
            #region "絶対入力する項目"
            #region "登記簿謄本登録日"
            if (RegistrationDate2_old != RegistrationDate)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + RegistrationDate2_old + "' , '" + RegistrationDate + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "住所変更"
            if (Address_old != Address)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + Address_old + "' , '" + Address + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "住所フリガナ変更(住所変更に伴う)"
            if (AddressKana_old != AddressKana)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + AddressKana_old + "' , '" + AddressKana + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "郵便番号変更"
            if ((PostalCode1_old != PostalCode1) || (PostalCode2_old != PostalCode2))
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + PostalCode1_old + "-" + PostalCode2_old + "' , '" + PostalCode1 + "-" + PostalCode2 + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion                      
            #region "電話番号変更"
            if (PhoneNumber_old != PhoneNumber)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + PhoneNumber_old + "' , '" + PhoneNumber + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "登記簿謄本変更"
            if (RegisterCopy_old != RegisterCopy)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + RegisterCopy_old + "' , '" + RegisterCopy + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "身分証番号変更"
            if (ID_old != ID)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + ID_old + "' , '" + ID + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "古物商許可証変更"
            if (Antiquelicense_old != Antiquelicense)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + Antiquelicense_old + "' , '" + Antiquelicense + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "氏名変更"
            if (Name_old != Name)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + Name_old + "' , '" + Name + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "氏名フリガナ変更(氏名変更に伴う)"
            if (NameKana_old != NameKana)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + NameKana_old + "' , '" + NameKana + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "職業変更"
            if (Occupation_old != Occupation)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + Occupation_old + "' , '" + Occupation + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "生年月日変更(氏名変更に伴う)"
            if (Birthday_old != Birthday)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + Birthday_old + "' , '" + Birthday + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "顔写真変更"
            if (PhotoID_old != PhotoID)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + PhotoID_old + "' , '" + PhotoID + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #endregion
            #region "必須項目以外"
            #region "FAX番号変更"
            if (FaxNumber_old != FaxNumber)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + FaxNumber_old + "' , '" + FaxNumber + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion                        
            #region "メールアドレス変更"
            if (EmailAddress_old != EmailAddress)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + EmailAddress_old + "' , '" + EmailAddress + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "銀行名変更"
            if (BankName_old != BankName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + BankName_old + "' , '" + BankName + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "預金種別変更"
            if (DepositType_old != DepositType)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + DepositType_old + "','" + DepositType + "','" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "口座名義変更"
            if (AccountName_old != AccountName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + AccountName_old + "' , '" + AccountName + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "支店名変更"
            if (BranchName_old != BranchName)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "'," + staff_code + ",'" + ResidenceCard_old + "','" + ResidenceCard + "','" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "口座番号変更"
            if (AccountNumber_old != AccountNumber)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "'," + staff_code + ",'" + AccountNumber_old + "','" + AccountNumber + "','" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "口座名義人フリガナ変更(口座名義人変更に伴う)"
            if (AccountNameKana_old != AccountNameKana)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "'," + staff_code + ",'" + AccountNameKana_old + "','" + AccountNameKana + "','" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "在留期限変更"
            if (PeriodStay_old != PeriodStay)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + PeriodStay_old + "' , '" + PeriodStay + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "印鑑証明変更"
            if (SealCertification_old != SealCertification)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + SealCertification_old + "' , '" + SealCertification + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "納税証明書変更"
            if (TaxCertification_old != TaxCertification)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "' ," + staff_code + ",'" + TaxCertification_old + "' , '" + TaxCertification + "' ,'" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "在留カード"
            if (ResidenceCard_old != ResidenceCard)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "'," + staff_code + ",'" + ResidenceCard_old + "','" + ResidenceCard + "','" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region "定款等変更"
            if (AolFinancialShareholder_old != AolFinancialShareholder)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "'," + staff_code + ",'" + AolFinancialShareholder_old + "','" + AolFinancialShareholder + "','" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #region"備考変更"
            if (Remarks_old != Remarks)
            {
                string sql_in = "Insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "VALUES (" + 5 + ",'" + dat1 + "'," + staff_code + ",'" + Remarks_old + "','" + Remarks + "','" + reason2 + "','" + ClientCode + "');";

                adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                adapter2.Fill(dt3);
            }
            #endregion
            #endregion
            #endregion

            MessageBox.Show("更新しました。");

            this.Close();
        }
        #endregion
        #region "個人　無効"
        private void Button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("無効にしますか？", "確認", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }
            else if (result == DialogResult.Yes)
            {
                PostgreSQL postgre = new PostgreSQL();
                NpgsqlConnection conn = postgre.connection();
                
                conn.Open();
                #region "旧データ（コメントアウト）"
                //NpgsqlDataAdapter adapter1;
                //string sql_old = "select * from client_m where code = '" + ClientCode + "';";
                //adapter1 = new NpgsqlDataAdapter(sql_old, conn);
                //adapter1.Fill(dt2);
                //DataRow row1;
                //row1 = dt2.Rows[0];
                //string RegistrationDate2_old = row1["registration_date"].ToString();
                //int PostalCode1_old = int.Parse(row1["postal_code1"].ToString());
                //string PostalCode2_old = row1["postal_code2"].ToString();
                //string Address_old = row1["address"].ToString();
                //string AddressKana_old = row1["address_kana"].ToString();
                //string Name_old = row1["name"].ToString();
                //string NameKana_old = row1["name_kana"].ToString();
                //string Birthday_old = row1["birthday"].ToString();
                //string PhoneNumber_old = row1["phone_number"].ToString();
                //string FaxNumber_old = row1["fax_number"].ToString();
                //string Occupation_old = row1["occupation"].ToString();
                //string EmailAddress_old = row1["email_address"].ToString();
                //string BankName_old = row1["bank_name"].ToString();
                //string DepositType_old = row1["deposit_type"].ToString();
                //string AccountName_old = row1["account_name"].ToString();
                //string BranchName_old = row1["branch_name"].ToString();
                //string AccountNumber_old = row1["account_number"].ToString();
                //string AccountNameKana_old = row1["account_name_kana"].ToString();
                //string RegisterCopy_old = row1["register_copy"].ToString();
                //string Antiquelicense_old = row1["antique_license"].ToString();
                //string PhotoID_old = row1["photo_id"].ToString();
                //string ID_old = row1["id_number"].ToString();
                //string PeriodStay_old = row1["period_stay"].ToString();
                //string SealCertification_old = row1["seal_certification"].ToString();
                //string TaxCertification_old = row1["tax_certificate"].ToString();
                //string Remarks_old = row1["remarks"].ToString();
                //string ResidenceCard_old = row1["residence_card"].ToString();
                //string AolFinancialShareholder_old = row1["aol_financial_shareholder"].ToString();
                //DateTime dat1 = DateTime.Now;
                //string c = dat1.ToString("yyyy/MM/dd");
                //#region "履歴へ"
                //NpgsqlDataAdapter adapter2;
                //string sql_in = "Insert into revisions VALUES (" + 1 + " , '" + RegistrationDate2_old + "' , '" + Name_old + "' ,'" + NameKana_old + "' , '" + Birthday_old + "' , '" + Address_old + "' , '" + AddressKana_old + "' , '" + PhoneNumber_old + "' , '" + FaxNumber_old + "' , '" + EmailAddress_old + "', '" + Occupation_old +
                //   "' , '" + BankName_old + "' , '" + BranchName_old + "' , '" + DepositType_old + "' , '" + AccountNumber_old + "' , '" + AccountName_old + "' , '" + AccountNameKana_old + "' , '" + ID_old + "' , '" + Remarks_old + "','" + RegisterCopy_old + "' , '" + Antiquelicense_old + "','" + PhotoID_old + "' , '" + TaxCertification_old + "','" + ResidenceCard_old + "','" + PeriodStay_old + "','" + SealCertification_old + "'," +
                //  1 + ",'" + AolFinancialShareholder_old + "','" + c + "'," + staff_code + "," + PostalCode1_old + ",'" + PostalCode2_old + "');";


                //adapter2 = new NpgsqlDataAdapter(sql_in, conn);
                //adapter2.Fill(dt3);
                //#endregion
                #endregion
                //NpgsqlDataAdapter adapter;
                //NpgsqlDataAdapter adapter3;
                //NpgsqlCommandBuilder builder;
                //NpgsqlCommandBuilder builder2;
                DateTime dat = DateTime.Now;

                string remove_sql = "update client_m set invalid = 1 where code = '" + ClientCode + "'";
                string revisions = "insert into revisions (data, upd_date, insert_code, before_data, after_data, reason, upd_code) " +
                    "values (" + 4 + ",'" + dat + "'," + staff_code + ",'" + "有効" + "','" + "無効" + "','" + reasonText2.Text + "','" + ClientCode + "');";

                using (transaction = conn.BeginTransaction())
                {
                    cmd = new NpgsqlCommand(remove_sql, conn);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }

                cmd = new NpgsqlCommand(revisions, conn);
                cmd.ExecuteNonQuery();

                //adapter = new NpgsqlDataAdapter(remove_sql, conn);
                //builder = new NpgsqlCommandBuilder(adapter);
                //adapter.Fill(dt);
                //adapter.Update(dt);
                //adapter3 = new NpgsqlDataAdapter(revisions, conn);
                //builder2 = new NpgsqlCommandBuilder(adapter3);
                //adapter3.Fill(dt5);
                //adapter3.Update(dt5);

                conn.Close();

                MessageBox.Show("無効にしました。");
            }
            else
            {
                return;
            }
            //ClientMaster clientmaster = new ClientMaster(master, staff_code);
            //this.Close();
            //clientmaster.Show();
        }
        #endregion

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
        #region "身分証または顔つき身分証"
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
        #region  "古物商許可証"
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
        #region "顔つき身分証"
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
                sealCertificationTextBox1.Text = path;
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

        private void ClientMaster_UPD_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth, pass);
            clientmaster.Show();
        }

        #region"画像の表示"
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

        private void textBox37_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(registerCopyTextBox1.Text))
            {
                pictureBox2.ImageLocation = registerCopyTextBox1.Text;
            }
        }

        private void textBox33_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(aolFinancialShareholderTextBox1.Text))
            {
                pictureBox2.ImageLocation = aolFinancialShareholderTextBox1.Text;
            }
        }

        private void textBox35_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(photoIDTextBox1.Text))
            {
                pictureBox2.ImageLocation = photoIDTextBox1.Text;
            }
        }

        private void textBox36_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(antiqueLicenseTextBox1.Text))
            {
                pictureBox2.ImageLocation = antiqueLicenseTextBox1.Text;
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
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            companyKanaTextBox.Text = InputControl.Kana(companyKanaTextBox.Text);
            companyKanaTextBox.Select(companyKanaTextBox.Text.Length, 0);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            shopKanaTextBox.Text = InputControl.Kana(shopKanaTextBox.Text);
            shopKanaTextBox.Select(shopKanaTextBox.Text.Length, 0);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            addressKanaTextBox.Text = InputControl.Kana(addressKanaTextBox.Text);
            addressKanaTextBox.Select(addressKanaTextBox.Text.Length, 0);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            accountKanaTextBox.Text = InputControl.Kana(accountKanaTextBox.Text);
            accountKanaTextBox.Select(accountKanaTextBox.Text.Length, 0);
        }

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
        #endregion

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                //MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

    }
}
