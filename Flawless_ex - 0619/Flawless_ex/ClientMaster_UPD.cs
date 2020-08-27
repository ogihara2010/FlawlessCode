using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;
using System.Text;

namespace Flawless_ex
{
    public partial class ClientMaster_UPD : Form
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        int type;
        string name;
        string address;
        int staff_code;
        string access_auth;
        string path;
        string pass;
        string kana;

        public ClientMaster_UPD(MasterMaintenanceMenu master, int type, string name, string address, int staff_code, string access_auth, string Pass)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
            this.name = name;　//担当者名または氏名
            this.address = address; // 住所
            this.type = type;  //  法人・個人
            this.access_auth = access_auth;
            this.pass = Pass;
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
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("会社名を入力してください。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("会社名のフリガナを入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("カタカナを入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("郵便番号を入力して下さい。");
                return;
            }
            else if (int.Parse(textBox1.Text) >= 10000 || int.Parse(textBox4.Text) >= 1000)
            {
                MessageBox.Show("郵便番号を正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("住所を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("住所のフリガナを入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("カタカナを入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox12.Text))
            {
                MessageBox.Show("担当者名義を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("電話番号を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox21.Text))
            {
                MessageBox.Show("登記簿謄本を選んで下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox22.Text))
            {
                MessageBox.Show("古物商許可証を選んで下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox23.Text))
            {
                MessageBox.Show("古物番号を入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox23.Text, @"^\p{N}+$"))
            {
                MessageBox.Show("数字を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox24.Text))
            {
                MessageBox.Show("身分証明書もしくは顔つき身分証明証を選択して下さい。");
                return;
            }
            #endregion
            #region "その他の項目"
            else
            {
                if (!string.IsNullOrEmpty(textBox8.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(textBox8.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                    {
                        MessageBox.Show("カタカナを入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(textBox20.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(textBox20.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                    {
                        MessageBox.Show("カタカナを入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(textBox29.Text) && string.IsNullOrEmpty(textBox46.Text))
                {
                    MessageBox.Show("在留期限を入力して下さい。");
                    return;
                }
                if (!string.IsNullOrEmpty(textBox46.Text) && string.IsNullOrEmpty(textBox29.Text))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                if (!string.IsNullOrEmpty(textBox10.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox10.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox10.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox10.Text, @"^[a-zA-Z]+$"))
                    {
                        MessageBox.Show("正しく入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(textBox19.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox19.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox19.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox19.Text, @"^[a-zA-Z]+$"))
                    {
                        MessageBox.Show("正しく入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(textBox13.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox13.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox13.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox13.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                    {
                        MessageBox.Show("正しく入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(textBox17.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox17.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox17.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox17.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                    {
                        MessageBox.Show("正しく入力して下さい。");
                        return;
                    }
                }
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
            conn1.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            string sql_old = "select * from client_m_corporate where invalid = 0 and (staff_name = '" + name + "' and address = '" + address + "');";
            conn1.Open();
            adapter1 = new NpgsqlDataAdapter(sql_old, conn1);
            adapter1.Fill(dt2);
            DataRow row1;
            row1 = dt2.Rows[0];
            string RegistrationDate2_old = row1["registration_date"].ToString();
            int PostalCode1_old = int.Parse(row1["postal_code1"].ToString());
            string PostalCode2_old = row1["postal_code2"].ToString();
            string Address_old = row1["address"].ToString();
            string AddressKana_old = row1["address_kana"].ToString();
            string CompanyName_old = row1["company_name"].ToString();
            string CompanyNameKana_old = row1["company_kana"].ToString();
            string ShopName_old = row1["shop_name"].ToString();
            string ShopNameKana_old = row1["shop_name_kana"].ToString();
            int Antique_old = (int)row1["antique_number"];
            string PhoneNumber_old = row1["phone_number"].ToString();
            string FaxNumber_old = row1["fax_number"].ToString();
            string URL_old = row1["url_infor"].ToString();
            string ClientStaffName_old = row1["staff_name"].ToString();
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
            string PeriodStay_old = row1["period_stay"].ToString();
            string SealCertification_old = row1["seal_certification"].ToString();
            string TaxCertification_old = row1["tax_certificate"].ToString();
            string Remarks_old = row1["remarks"].ToString();
            string ResidenceCard_old = row1["residence_card"].ToString();
            string AolFinancialShareholder_old = row1["aol_financial_shareholder"].ToString();
            DateTime dat1 = DateTime.Now;
            string c = dat1.ToString("yyyy/MM/dd");
            #region "履歴へ"
            NpgsqlConnection conn2 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter2;
            string sql_in = "Insert into client_m_corporate_revisions VALUES (" + 0 + " , '" + RegistrationDate2_old + "' , '" + CompanyName_old + "' ,'" + CompanyNameKana_old + "' , '" + ShopName_old + "' , '" + ShopNameKana_old + "'," + Antique_old + " ,'" + Address_old + "' , '" + AddressKana_old + "' , '" + PhoneNumber_old + "' , '" + FaxNumber_old + "','" + Position_old +
               "' , '" + ClientStaffName_old + "','" + EmailAddress_old + "','" + URL_old + "','" + BankName_old + "' , '" + BranchName_old + "' , '" + DepositType_old + "' , '" + AccountNumber_old + "' , '" + AccountNameKana_old + "' , '" + AccountName_old + "' , '" + Remarks_old + "' , '" + ID_old + "','" + c + "','" + Antiquelicense_old + "' , '" + TaxCertification_old + "','" + ResidenceCard_old + "' , '" + PeriodStay_old + "','" + SealCertification_old + "'," +
              0 + ",'" + AolFinancialShareholder_old + "','" + RegisterCopy_old + "'," + staff_code + "," + PostalCode1_old + ",'" + PostalCode2_old + "');";

            conn2.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn2.Open();

            adapter2 = new NpgsqlDataAdapter(sql_in, conn2);
            adapter2.Fill(dt3);
            #endregion
            #endregion
            #region "更新するパラメータ"
            string RegistrationDate = this.dateTimePicker1.Text;
            string CompanyName = this.textBox2.Text;
            string CompanyNameKana = this.textBox3.Text;

            int PostalCode1 = int.Parse(this.textBox4.Text);
            string PostalCode2 = this.textBox1.Text;
            
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
            string PeriodStay = this.textBox6.Text;
            string SealCertification = this.textBox26.Text;
            string TaxCertification = this.textBox27.Text;
            string Remarks = this.textBox28.Text;
            string ResidenceCard = this.textBox29.Text;
            string AolFinancialShareholder = textBox25.Text;
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            string reason1 = this.reasonText1.Text;
            #endregion
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;


            string sql_str = "UPDATE client_m_corporate SET type = " + 0 + " ,registration_date = '" + RegistrationDate + "' ,company_name =  '" + CompanyName + "' ,company_kana = '" + CompanyNameKana + "' ,shop_name =  '" + ShopName + "' ,shop_name_kana = '" + ShopNameKana + " ',address =  '" + Address + "' ,address_kana = '" + AddressKana + "' ,phone_number = '" + PhoneNumber + "' ,fax_number = '" + FaxNumber + "' ,position = '" + Position + "' ,staff_name = '" + ClientStaffName +
                "' ,email_address = '" + EmailAddress + "',url_infor = '" + URLinfor + "',bank_name = '" + BankName + "' ,branch_name = '" + BranchName + "' ,deposit_type = '" + DepositType + "' ,account_number = '" + AccountNumber + "' ,account_name_kana = '" + AccountNameKana + "' ,account_name = '" + AccountName + "' ,remarks = '" + Remarks + "' ,id = '" + ID + "' ,register_date = '" + b + "',antique_license = '" + Antiquelicense + "',tax_certificate = '" + TaxCertification + "',residence_card = '" + ResidenceCard + "',period_stay = '" + PeriodStay + "',seal_certification = '" + SealCertification +
                "',invalid = " + 0 + ",aol_financial_shareholder = '" + AolFinancialShareholder + "',register_copy = '" + RegisterCopy + "',insert_name = " + staff_code + ",postal_code1 = " + PostalCode1 + ",postal_code2 = '" + PostalCode2 + "',reason = '" + reason1 + "' where antique_number = " + AntiqueNumber + "; ";

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
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

                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlCommandBuilder builder;


                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                string remove_sql = "update client_m_corporate set invalid = 1 where staff_name = '" + name + "'" + "and address = '" + address + "'";

                adapter = new NpgsqlDataAdapter(remove_sql, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);
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
            #region"コメントアウト"
            //#region "旧データ"
            //NpgsqlConnection conn1 = new NpgsqlConnection();
            //NpgsqlDataAdapter adapter1;
            //conn1.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //string sql_old = "select * from client_m_individual where invalid = 0 and (name = '" + name + "' and address = '" + address + "');";
            //conn1.Open();
            //adapter1 = new NpgsqlDataAdapter(sql_old, conn1);
            //adapter1.Fill(dt2);
            //DataRow row1;
            //row1 = dt2.Rows[0];
            //string RegistrationDate2_old = row1["registration_date"].ToString();
            //int PostalCode1_old = int.Parse(row1["postal_code1"].ToString());
            //string PostalCode2_old = row1["postal_code2"].ToString();
            //string Address_old = row1["address"].ToString();
            //string AddressKana_old = row1["address_kana"].ToString();
            //string CompanyName_old = row1["company_name"].ToString();
            //string CompanyNameKana_old = row1["company_kana"].ToString();
            //string ShopName_old = row1["shop_name"].ToString();
            //string ShopNameKana_old = row1["shop_name_kana"].ToString();
            //int Antique_old = (int)row1["antique_number"];
            //string PhoneNumber_old = row1["phone_number"].ToString();
            //string FaxNumber_old = row1["fax_number"].ToString();
            //string URL_old = row1["url_infor"].ToString();
            //string ClientStaffName_old = row1["staff_name"].ToString();
            //string Position_old = row1["position"].ToString();
            //string EmailAddress_old = row1["email_address"].ToString();
            //string BankName_old = row1["bank_name"].ToString();
            //string DepositType_old = row1["deposit_type"].ToString();
            //string AccountName_old = row1["account_name"].ToString();
            //string BranchName_old = row1["branch_name"].ToString();
            //string AccountNumber_old = row1["account_number"].ToString();
            //string AccountNameKana_old = row1["account_name_kana"].ToString();
            //string RegisterCopy_old = row1["register_copy"].ToString();
            //string Antiquelicense_old = row1["antique_license"].ToString();
            //string ID_old = row1["id"].ToString();
            //string PeriodStay_old = row1["period_stay"].ToString();
            //string SealCertification_old = row1["seal_certification"].ToString();
            //string TaxCertification_old = row1["tax_certificate"].ToString();
            //string Remarks_old = row1["remarks"].ToString();
            //string ResidenceCard_old = row1["residence_card"].ToString();
            //string AolFinancialShareholder_old = row1["aol_financial_shareholder"].ToString();
            //DateTime dat1 = DateTime.Now;
            //string c = dat1.ToString("yyyy/MM/dd");
            //#region "履歴へ"
            //NpgsqlConnection conn2 = new NpgsqlConnection();
            //NpgsqlDataAdapter adapter2;
            //string sql_in = "Insert into client_m_corporate_revisions VALUES (" + 0 + " , '" + RegistrationDate2_old + "' , '" + CompanyName_old + "' ,'" + CompanyNameKana_old + "' , '" + ShopName_old + "' , '" + ShopNameKana_old + "'," + Antique_old + " ,'" + Address_old + "' , '" + AddressKana_old + "' , '" + PhoneNumber_old + "' , '" + FaxNumber_old + "','" + Position_old +
            //   "' , '" + ClientStaffName_old + "','" + EmailAddress_old + "','" + URL_old + "','" + BankName_old + "' , '" + BranchName_old + "' , '" + DepositType_old + "' , '" + AccountNumber_old + "' , '" + AccountNameKana_old + "' , '" + AccountName_old + "' , '" + Remarks_old + "' , '" + ID_old + "','" + c + "','" + Antiquelicense_old + "' , '" + TaxCertification_old + "','" + ResidenceCard_old + "' , '" + PeriodStay_old + "','" + SealCertification_old + "'," +
            //  1 + ",'" + AolFinancialShareholder_old + "','" + RegisterCopy_old + "'," + staff_code + "," + PostalCode1_old + ",'" + PostalCode2_old + "');";

            //conn2.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            //conn2.Open();

            //adapter2 = new NpgsqlDataAdapter(sql_in, conn2);
            //adapter2.Fill(dt3);
            //#endregion
            //#endregion
            #endregion"コメントアウト"

            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            DataTable dt = new DataTable();
            DataRow row;

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            if (type == 0)
            {
                //法人
                string sql_str = "select * from client_m_corporate where  address = '" + address + "' and staff_name = '" + name + "';";
                conn.Open();
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                row = dt.Rows[0];
                #region "入力データ"
                string RegistrationDate1;
                string CompanyName;
                string CompanyNameKana;
                int PostalCode1;
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
                int AntiqueNumber;
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

                PostalCode1 = int.Parse(row["postal_code1"].ToString());
                PostalCode2 = row["postal_code2"].ToString();
                
                Address = row["address"].ToString();
                AddressKana = row["address_kana"].ToString();
                ShopName = row["shop_name"].ToString();
                ShopNameKana = row["shop_name_kana"].ToString();
                PhoneNumber = row["phone_number"].ToString();
                FaxNumber = row["fax_number"].ToString();
                Position = row["position"].ToString();
                StaffName = row["staff_name"].ToString();
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
                AntiqueNumber = (int)row["antique_number"];
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
                this.textBox2.Text = CompanyName;
                this.textBox3.Text = CompanyNameKana;

                this.textBox4.Text = PostalCode1.ToString();
                this.textBox1.Text = PostalCode2.ToString();
                
                this.textBox5.Text = Address;
                this.textBox6.Text = AddressKana;
                this.textBox7.Text = ShopName;
                this.textBox8.Text = ShopNameKana;
                this.textBox9.Text = PhoneNumber;
                this.textBox10.Text = FaxNumber;
                this.textBox11.Text = Position;
                this.textBox12.Text = StaffName;
                this.textBox13.Text = EmailAddress;
                this.textBox14.Text = BankName;
                this.textBox15.Text = DepositType;
                this.textBox16.Text = AccountName;
                this.textBox17.Text = URLinfor;
                this.textBox18.Text = BranchName;
                this.textBox19.Text = AccountNumber;
                this.textBox20.Text = AccountNameKana;
                this.textBox21.Text = RegisterCopy;
                this.textBox22.Text = Antiquelicense;
                this.textBox23.Text = AntiqueNumber.ToString();
                this.textBox24.Text = ID;
                this.textBox25.Text = AolFinancialShareholder;

                this.textBox47.Text = PeriodStay;
                
                this.textBox26.Text = SealCertification;
                this.textBox27.Text = TaxCertification;
                this.textBox28.Text = Remarks;
                this.textBox29.Text = ResidenceCard;
                #endregion
            }
            else if (type == 1)
            {
                //個人
                string sql_str = "select * from client_m_individual where name = '" + name + "' and address = '" + address + "';";
                conn.Open();
                adapter = new NpgsqlDataAdapter(sql_str, conn);
                adapter.Fill(dt);
                row = dt.Rows[0];

                #region "入力データ"
                string RegistrationDate2;
                string Name;
                string NameKana;
                string Birthday;
                int PostalCode1;
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
                PostalCode1 = int.Parse(row["postal_code1"].ToString());
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
                PhotoID = row["photo_id"].ToString();
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
                this.textBox56.Text = Name;
                this.textBox55.Text = NameKana;
                this.textBox50.Text = Birthday;
                this.textBox54.Text = PostalCode1.ToString();
                this.textBox51.Text = PostalCode2;
                this.textBox53.Text = Address;
                this.textBox52.Text = AddressKana;
                this.textBox49.Text = PhoneNumber;
                this.textBox48.Text = FaxNumber;
                this.textBox41.Text = Occupation;
                this.textBox45.Text = EmailAddress;
                this.textBox44.Text = BankName;
                this.textBox43.Text = DepositType;
                this.textBox42.Text = AccountName;
                this.textBox40.Text = BranchName;
                this.textBox39.Text = AccountNumber;
                this.textBox38.Text = AccountNameKana;
                this.textBox37.Text = RegisterCopy;
                this.textBox36.Text = Antiquelicense;
                this.textBox35.Text = PhotoID;
                this.textBox34.Text = ID;
                this.textBox33.Text = AolFinancialShareholder;
                this.textBox46.Text = PeriodStay;
                this.textBox32.Text = SealCertification;
                this.textBox31.Text = TaxCertification;
                this.textBox58.Text = Remarks;
                this.textBox30.Text = ResidenceCard;
                #endregion
            }
            conn.Close();
        }
        #region "個人 更新"
        private void Button5_Click(object sender, EventArgs e)
        {
            #region "起こりうるミス"
            #region "必須項目"
            if (string.IsNullOrEmpty(textBox56.Text))
            {
                MessageBox.Show("名前を入力してください。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox55.Text))
            {
                MessageBox.Show("名前のフリガナを入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox55.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("カタカナを入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox54.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox54.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox54.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(textBox51.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox51.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox51.Text, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox51.Text) || string.IsNullOrEmpty(textBox54.Text))
            {
                MessageBox.Show("郵便番号を入力して下さい。");
                return;
            }
            else if (int.Parse(textBox51.Text) >= 10000 || int.Parse(textBox54.Text) >= 1000)
            {
                MessageBox.Show("郵便番号を正しく入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox53.Text))
            {
                MessageBox.Show("住所を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox52.Text))
            {
                MessageBox.Show("住所のフリガナを入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox52.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
            {
                MessageBox.Show("カタカナを入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox49.Text))
            {
                MessageBox.Show("電話番号を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox37.Text))
            {
                MessageBox.Show("登記簿謄本を選んで下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox36.Text))
            {
                MessageBox.Show("古物商許可証を選んで下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox34.Text))
            {
                MessageBox.Show("身分証番号を入力して下さい。");
                return;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox34.Text, @"^\p{N}+$"))
            {
                MessageBox.Show("数字を入力して下さい。");
                return;
            }
            else if (string.IsNullOrEmpty(textBox35.Text))
            {
                MessageBox.Show("顔つき身分証明証を選択して下さい。");
                return;
            }
            #endregion
            #region "その他の項目"
            else
            {
                if (!string.IsNullOrEmpty(textBox38.Text))
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(textBox38.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$"))
                    {
                        MessageBox.Show("カタカナを入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(textBox30.Text) && string.IsNullOrEmpty(textBox47.Text))
                {
                    MessageBox.Show("在留期限を入力して下さい。");
                    return;
                }
                if (!string.IsNullOrEmpty(textBox47.Text) && string.IsNullOrEmpty(textBox30.Text))
                {
                    MessageBox.Show("正しく入力して下さい。");
                    return;
                }
                if (!string.IsNullOrEmpty(textBox39.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox39.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox39.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox39.Text, @"^[a-zA-Z]+$"))
                    {
                        MessageBox.Show("正しく入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(textBox48.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox48.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox48.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox48.Text, @"^[a-zA-Z]+$"))
                    {
                        MessageBox.Show("正しく入力して下さい。");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(textBox45.Text))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox45.Text, @"^[\p{IsKatakana}\u31F0-\u31FF\u3099-\u309C\uFF65-\uFF9F]+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox45.Text, @"^\p{IsHiragana}+$") || System.Text.RegularExpressions.Regex.IsMatch(textBox45.Text, @"^[ａ-ｚＡ-Ｚ]+$"))
                    {
                        MessageBox.Show("正しく入力して下さい。");
                        return;
                    }
                }
            }
            #endregion
            #endregion
            DialogResult dr = MessageBox.Show("個人情報を変更しますか？", "登録確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No)
            {
                return;
            }

            #region "旧データ"
            NpgsqlConnection conn1 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter1;
            string sql_old = "select * from client_m_individual where invalid = 0 and (name = '" + name + "' and address = '" + address + "');";
            conn1.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn1.Open();
            adapter1 = new NpgsqlDataAdapter(sql_old, conn1);
            adapter1.Fill(dt2);
            DataRow row1;
            row1 = dt2.Rows[0];
            string RegistrationDate2_old = row1["registration_date"].ToString();
            
            int PostalCode1_old = int.Parse(row1["postal_code1"].ToString());
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
            string PhotoID_old = row1["photo_id"].ToString();
            string ID_old = row1["id_number"].ToString();
            
            string PeriodStay_old = row1["period_stay"].ToString();
            
            string SealCertification_old = row1["seal_certification"].ToString();
            string TaxCertification_old = row1["tax_certificate"].ToString();
            string Remarks_old = row1["remarks"].ToString();
            string ResidenceCard_old = row1["residence_card"].ToString();
            string AolFinancialShareholder_old = row1["aol_financial_shareholder"].ToString();
            DateTime dat1 = DateTime.Now;
            string c = dat1.ToString("yyyy/MM/dd");
            #region "履歴へ"
            NpgsqlConnection conn2 = new NpgsqlConnection();
            NpgsqlDataAdapter adapter2;
            string sql_in = "Insert into client_m_individual_revisions VALUES (" + 1 + " , '" + RegistrationDate2_old + "' , '" + Name_old + "' ,'" + NameKana_old + "' , '" + Birthday_old + "' , '" + Address_old + "' , '" + AddressKana_old + "' , '" + PhoneNumber_old + "' , '" + FaxNumber_old + "' , '" + EmailAddress_old + "', '" + Occupation_old +
               "' , '" + BankName_old + "' , '" + BranchName_old + "' , '" + DepositType_old + "' , '" + AccountNumber_old + "' , '" + AccountName_old + "' , '" + AccountNameKana_old + "' , '" + ID_old + "' , '" + Remarks_old + "','" + RegisterCopy_old + "' , '" + Antiquelicense_old + "','" + PhotoID_old + "' , '" + TaxCertification_old + "','" + ResidenceCard_old + "','" + PeriodStay_old + "','" + SealCertification_old + "'," +
              0 + ",'" + AolFinancialShareholder_old + "','" + c + "'," + staff_code + "," + PostalCode1_old + ",'" + PostalCode2_old + "');";

            conn2.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn2.Open();

            adapter2 = new NpgsqlDataAdapter(sql_in, conn2);
            adapter2.Fill(dt3);
            #endregion
            #endregion
            #region "更新するパラメータ"
            string RegistrationDate = this.deliveryDateBox.Text;
            string Name = this.textBox56.Text;
            string NameKana = this.textBox55.Text;
            string Birthday = this.textBox50.Text;
            
            int PostalCode1 = int.Parse(this.textBox54.Text);
            string PostalCode2 = this.textBox51.Text;
            
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
            
            string PeriodStay = this.textBox46.Text;
            
            string SealCertification = this.textBox32.Text;
            string TaxCertification = this.textBox31.Text;
            string Remarks = this.textBox58.Text;
            string ResidenceCard = this.textBox30.Text;
            string AolFinancialShareholder = textBox33.Text;
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            string reason2 = this.reasonText2.Text;
            #endregion
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            string sql_str = "UPDATE client_m_individual SET type = " + 1 + " , registration_date = '" + RegistrationDate + "' , name = '" + Name + "' ,name_kana = '" + NameKana + "' ,birthday = '" + Birthday + "' ,address = '" + Address + "' ,address_kana = '" + AddressKana + "' ,phone_number = '" + PhoneNumber + "' ,fax_number = '" + FaxNumber + "' ,email_address = '" + EmailAddress + "',occupation = '" + Occupation +
               "' ,bank_name = '" + BankName + "' ,branch_name = '" + BranchName + "' ,deposit_type = '" + DepositType + "' ,account_number = '" + AccountNumber + "' ,account_name = '" + AccountName + "' ,account_name_kana = '" + AccountNameKana + "' ,remarks = '" + Remarks + "',register_copy = '" + RegisterCopy + "' ,antique_license = '" + Antiquelicense + "',photo_id = '" + PhotoID + "' ,tax_certificate = '" + TaxCertification + "',residence_card = '" + ResidenceCard + "',period_stay = '" + PeriodStay + "',seal_certification = '" + SealCertification + "',invalid = " +
              0 + ",aol_financial_shareholder = '" + AolFinancialShareholder + "',register_date = '" + b + "',insert_name = " + staff_code + ",postal_code1 = " + PostalCode1 + ",postal_code2 = '" + PostalCode2 + "',reason = '" + reason2 + "'where id_number = " + ID + ";";

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
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
                #region "旧データ"
                NpgsqlConnection conn1 = new NpgsqlConnection();
                NpgsqlDataAdapter adapter1;
                string sql_old = "select * from client_m_individual where name = '" + name + "' and address = '" + address + "';";
                conn1.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn1.Open();
                adapter1 = new NpgsqlDataAdapter(sql_old, conn1);
                adapter1.Fill(dt2);
                DataRow row1;
                row1 = dt2.Rows[0];
                string RegistrationDate2_old = row1["registration_date"].ToString();
                int PostalCode1_old = int.Parse(row1["postal_code1"].ToString());
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
                string PhotoID_old = row1["photo_id"].ToString();
                string ID_old = row1["id_number"].ToString();
                string PeriodStay_old = row1["period_stay"].ToString();
                string SealCertification_old = row1["seal_certification"].ToString();
                string TaxCertification_old = row1["tax_certificate"].ToString();
                string Remarks_old = row1["remarks"].ToString();
                string ResidenceCard_old = row1["residence_card"].ToString();
                string AolFinancialShareholder_old = row1["aol_financial_shareholder"].ToString();
                DateTime dat1 = DateTime.Now;
                string c = dat1.ToString("yyyy/MM/dd");
                #region "履歴へ"
                NpgsqlConnection conn2 = new NpgsqlConnection();
                NpgsqlDataAdapter adapter2;
                string sql_in = "Insert into client_m_individual_revisions VALUES (" + 1 + " , '" + RegistrationDate2_old + "' , '" + Name_old + "' ,'" + NameKana_old + "' , '" + Birthday_old + "' , '" + Address_old + "' , '" + AddressKana_old + "' , '" + PhoneNumber_old + "' , '" + FaxNumber_old + "' , '" + EmailAddress_old + "', '" + Occupation_old +
                   "' , '" + BankName_old + "' , '" + BranchName_old + "' , '" + DepositType_old + "' , '" + AccountNumber_old + "' , '" + AccountName_old + "' , '" + AccountNameKana_old + "' , '" + ID_old + "' , '" + Remarks_old + "','" + RegisterCopy_old + "' , '" + Antiquelicense_old + "','" + PhotoID_old + "' , '" + TaxCertification_old + "','" + ResidenceCard_old + "','" + PeriodStay_old + "','" + SealCertification_old + "'," +
                  1 + ",'" + AolFinancialShareholder_old + "','" + c + "'," + staff_code + "," + PostalCode1_old + ",'" + PostalCode2_old + "');";

                conn2.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn2.Open();

                adapter2 = new NpgsqlDataAdapter(sql_in, conn2);
                adapter2.Fill(dt3);
                #endregion
                #endregion
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlCommandBuilder builder;


                conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
                conn.Open();

                string remove_sql = "update client_m_individual set invalid = 1 where name = '" + name + "'" + "and address = '" + address + "'";

                adapter = new NpgsqlDataAdapter(remove_sql, conn);
                builder = new NpgsqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);
                MessageBox.Show("無効にしました。");
                return;
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
                textBox21.Text = path;
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
                textBox22.Text = path;
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
                textBox24.Text = path;
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
                textBox25.Text = path;
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
                textBox26.Text = path;
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
                textBox27.Text = path;
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
                textBox29.Text = path;
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
                textBox37.Text = path;
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
                textBox36.Text = path;
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
                textBox35.Text = path;
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
                textBox33.Text = path;
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
                textBox32.Text = path;
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
                textBox32.Text = path;
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
                textBox30.Text = path;
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

        
        private void textBox24_DoubleClick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox24.Text;
        }

        private void textBox21_DoubleClick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox21.Text;
        }

        private void textBox22_DoubleClick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox22.Text;
        }

        private void textBox25_DoubleClick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox25.Text;
        }

        private void textBox27_DoubleClick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox27.Text;
        }

        private void textBox29_DoubleClick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox29.Text;
        }

        private void textBox26_DoubleClick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox26.Text;
        }

        private void textBox37_DoubleClick(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = textBox37.Text;
        }

        private void textBox33_DoubleClick(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = textBox33.Text;
        }

        private void textBox35_DoubleClick(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = textBox35.Text;
        }

        private void textBox36_DoubleClick(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = textBox36.Text;
        }

        private void textBox31_DoubleClick(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = textBox31.Text;
        }

        private void textBox30_DoubleClick(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = textBox30.Text;
        }

        private void textBox32_DoubleClick(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = textBox32.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox3.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox3.Text = stringBuilder.ToString();
            textBox3.Select(textBox3.Text.Length, 0);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox8.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox8.Text = stringBuilder.ToString();
            textBox8.Select(textBox8.Text.Length, 0);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox6.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox6.Text = stringBuilder.ToString();
            textBox6.Select(textBox6.Text.Length, 0);
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox20.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox20.Text = stringBuilder.ToString();
            textBox20.Select(textBox20.Text.Length, 0);
        }

        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox55.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox55.Text = stringBuilder.ToString();
            textBox55.Select(textBox55.Text.Length, 0);
        }

        private void textBox52_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox52.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox52.Text = stringBuilder.ToString();
            textBox52.Select(textBox52.Text.Length, 0);
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox38.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox38.Text = stringBuilder.ToString();
            textBox38.Select(textBox38.Text.Length, 0);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void textBox51_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void textBox54_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }
    }
}
