using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;
namespace Flawless_ex
{
    public partial class ClientMaster_UPD : Form
    {
        NpgsqlConnection conn = new NpgsqlConnection();
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        int type;
        string name;
        string address;
        int staff_code;
        string access_auth;
        public ClientMaster_UPD(MasterMaintenanceMenu master, int type, string name, string address, int staff_code, string access_auth)
        {
            InitializeComponent();
            this.master = master;
            this.staff_code = staff_code;
            this.name = name;　//担当者名または氏名
            this.address = address; // 住所
            this.type = type;  //  法人・個人
            this.access_auth = access_auth;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth);

            this.Close();
            clientmaster.Show();
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth);

            this.Close();
            clientmaster.Show();
        }

        private void Button18_Click_1(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth);

            this.Close();
            clientmaster.Show();
        }
        #region "法人　更新"
        private void Button20_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("更新しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            string RegistrationDate = this.dateTimePicker1.Text;
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
            string PeriodStay = this.textBox6.Text;
            string SealCertification = this.textBox26.Text;
            string TaxCertification = this.textBox27.Text;
            string Remarks = this.textBox28.Text;
            string ResidenceCard = this.textBox29.Text;
            string AolFinancialShareholder = textBox25.Text;
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            string reason1 = this.reasonText1.Text;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;


            string sql_str = "Insert into client_m_corporate VALUES (" + 0 + " , '" + RegistrationDate + "' , '" + CompanyName + "' ,'" + CompanyNameKana + "' , '" + ShopName + "' ,  '" + ShopNameKana + " ', '" + AntiqueNumber + "' , '" + PostalCodeNumber + "', '" + Address + "' , '" + AddressKana + "' , '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + Position + "' , '" + ClientStaffName +
                "' , '" + EmailAddress + "', '" + URLinfor + "', '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + Remarks + "' , '" + ID + "' , '" + b + "','" + Antiquelicense + "','" + TaxCertification + "','" + ResidenceCard + "','" + PeriodStay + "','" + SealCertification + "'," +
               0 + ",'" + AolFinancialShareholder + "','" + RegisterCopy + "'," + staff_code + ",'" + reason1 +"');";

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("更新しました。");
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth);

            this.Close();
            clientmaster.Show();
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


                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
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

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
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
                string PostalCodeNumber;
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
                PostalCodeNumber = row["postal_code"].ToString();
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
                this.textBox4.Text = PostalCodeNumber.ToString();
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
                string PostalCodeNumber;
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
                PostalCodeNumber = row["postal_code"].ToString();
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
                this.textBox54.Text = PostalCodeNumber;
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
            DialogResult dr = MessageBox.Show("更新しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            string RegistrationDate = this.deliveryDateBox.Text;
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
            string PeriodStay = this.textBox46.Text;
            string SealCertification = this.textBox32.Text;
            string TaxCertification = this.textBox31.Text;
            string Remarks = this.textBox58.Text;
            string ResidenceCard = this.textBox30.Text;
            string AolFinancialShareholder = textBox33.Text;
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            string reason2 = this.reasonText2.Text;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;

            string sql_str = "Insert into client_m_individual VALUES (" + 1 + " , '" + RegistrationDate + "' , '" + Name + "' ,'" + NameKana + "' , '" + Birthday + "' , '" + PostalCodeNumber + "', '" + Address + "' , '" + AddressKana + "' , '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + EmailAddress + "', '" + Occupation +
               "' , '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + ID + "' , '" + Remarks + "','" + RegisterCopy + "' , '" + Antiquelicense + "','" + PhotoID + "' , '" + TaxCertification + "','" + ResidenceCard + "','" + PeriodStay + "','" + SealCertification + "'," +
              0 + ",'" + AolFinancialShareholder + "'," + staff_code + ",'" + reason2 + "');";

            conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("更新しました。");
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth);

            this.Close();
            clientmaster.Show();
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
                NpgsqlConnection conn = new NpgsqlConnection();
                NpgsqlDataAdapter adapter;
                NpgsqlCommandBuilder builder;


                conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
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

        private void ClientMaster_UPD_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth);
            clientmaster.Show();
        }
    }
}
