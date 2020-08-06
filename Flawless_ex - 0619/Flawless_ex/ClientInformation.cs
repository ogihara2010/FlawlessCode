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

namespace Flawless_ex
{
    public partial class ClientInformation : Form
    {
        RecordList RecordList;
        Statement Statement;
        int Staff_id;
        string Staff_Name;
        int Type;
        string SlipNumber;
        int AntiqueNumber = 0;
        int ID_Number = 0;
        int Grade;
        int invalid;
        string Access_auth;

        NpgsqlConnection conn = new NpgsqlConnection();
        NpgsqlCommand cmd;
        NpgsqlCommand cmd1;
        NpgsqlDataReader reader;

        public ClientInformation(RecordList recordList, int staff_id, string staff_name, int type, string slipnumber, int antique, int id)
        {
            InitializeComponent();

            this.RecordList = recordList;
            this.Staff_id = staff_id;
            this.Staff_Name = staff_name;
            this.Type = type;
            this.SlipNumber = slipnumber;
            this.AntiqueNumber = antique;
            this.ID_Number = id;
        }

        private void ClientInformation_Load(object sender, EventArgs e)
        {
            StaffNameTextBox.Text = Staff_Name;
            SlipNumberTextBox.Text = SlipNumber;
            GradeNumberTextBox.Text = SlipNumber.Trim('F');

            conn.ConnectionString = @"Server = 192.168.152.157; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            string sql = "select * from statement_data where document_number = '" + SlipNumber + "';";
            cmd = new NpgsqlCommand(sql, conn);
            using (reader = cmd.ExecuteReader())
            {
                if (Type == 0)
                {
                    while (reader.Read())
                    {
                        AntiqueNumber = (int)reader["antique_number"];
                    }
                }
                else if (Type == 1)
                {
                    while (reader.Read())
                    {
                        ID_Number = (int)reader["id_number"];
                    }
                }
            }
            string SQL = "";
            if (Type == 0)
            {
                TypeTextBox.Text = "法人";
                AntiqueNumberTextBox.Text = AntiqueNumber.ToString();
                 
                SQL = "select * from client_m_corporate inner join staff_m on staff_m.staff_code = client_m_corporate.insert_name where antique_number = '" + AntiqueNumber + "';";
                cmd = new NpgsqlCommand(SQL, conn);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RegistrationDateTextBox.Text = reader["registration_date"].ToString();
                        invalid = (int)reader["invalid"];
                        if (invalid == 0)
                        {
                            InvalidTextBox.Text = "有効";
                        }
                        else if (invalid == 1)
                        {
                            InvalidTextBox.Text = "無効";
                        }
                        CompanyNameTextBox.Text = reader["company_name"].ToString();
                        ComPanyKanaTextBox.Text = reader["company_kana"].ToString();
                        ShopNameTextBox.Text = reader["shop_name"].ToString();
                        ShopKanaTextBox.Text = reader["shop_name_kana"].ToString();
                        PostalCodeTextBox.Text = reader["postal_code"].ToString();
                        AddressTextBox.Text = reader["address"].ToString();
                        AddressKanaTextBox.Text = reader["address_kana"].ToString();
                        TelTextBox.Text = reader["phone_number"].ToString();
                        FaxTextBox.Text = reader["fax_number"].ToString();
                        PositionTextBox.Text = reader["position"].ToString();
                        ClientStaffNameTextBox.Text = reader["staff_name"].ToString();
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
                        RegisterDateTextBox.Text = (DateTime.Parse(reader["register_date"].ToString())).ToLongDateString();
                        AntiqueLicenseTextBox.Text = reader["antique_license"].ToString();
                        AolFinancialShareholderTextBox.Text = reader["aol_financial_shareholder"].ToString();
                        TaxCertificateTextBox.Text = reader["tax_certificate"].ToString();
                        ResidenceCardTextBox.Text = reader["residence_card"].ToString();
                        PeriodStayTextBox.Text = reader["period_stay"].ToString();
                        SealCetificationTextBox.Text = reader["seal_certification"].ToString();
                        InsertNameTextBox.Text = reader["staff_name"].ToString();
                        ReasonTextBox.Text = reader["reason"].ToString();
                    }
                }
            }
            //個人のとき
            else if (Type == 1)
            {
                TypeTextBox.Text = "個人";

                #region"Label名変更"
                CompanyNameLabel.Text = "氏名";
                CompanyKanaLabel.Text = "氏名" + "\r\n" + "（カタカナ）";
                ShopNameLabel.Text = "生年月日";
                ShopNameKanaLabel.Text = "郵便番号";
                AntiqueLabel.Text = "住所";
                PositionLabel.Text = "住所"+ "\r\n"+"（カタカナ）";
                AddressLabel.Text = "電話番号";
                AddressKaneLabel.Text = "FAX 番号";
                TelLabel.Text = "メールアドレス";
                FaxLabel.Text = "職業";
                PositionLabel.Text = "金融機関名";
                ClientLabel.Text = "支店名";
                MailLabel.Text = "預金種別";
                UrlLabel.Text = "口座番号";
                BankNameLabel.Text = "口座名義";
                BranchLabel.Text = "口座名義" + "\r\n" + "（カタカナ）";
                DepositLabel.Text = "身分証番号";
                AccountNumberLabel.Text = "備考";
                AccountNameLabel.Text = "登記簿謄本";
                AccountNameKanaLabel.Text = "古物商許可証";
                RemarkLabel.Text = "顔つき身分証";
                IdLabel.Text = "定款、決算書、" + "\r\n" + "株主構成";
                RegisterCopyLabel.Text = "納税証明書";
                RegisterDateLabel.Text = "在留カード";
                AntiqueLabel.Text = "在留期限";
                AolLabel.Text = "印鑑証明書";
                TaxCertificateLabel.Text = "更新・変更者";
                ResidenceCardLabel.Text = "更新・変更理由";
                PeriodStayLabel.Text = "更新・変更日";
                #endregion
                #region"非表示"
                SealCertificateLabel.Visible = false;
                SealCetificationTextBox.Visible = false;
                InsertLabel.Visible = false;
                InsertNameTextBox.Visible = false;
                ReasonLabel.Visible = false;
                ReasonTextBox.Visible = false;
                #endregion

                SQL = "selest * from client_m_individual inner join staff_m on staff_m.staff_code = client_m_corporate.insert_name where id_number = '" + ID_Number + "';";
                cmd = new NpgsqlCommand(SQL, conn);
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RegistrationDateTextBox.Text = reader["registration_date"].ToString();
                        invalid = (int)reader["invalid"];
                        if (invalid == 0)
                        {
                            InvalidTextBox.Text = "有効";
                        }
                        else if (invalid == 1)
                        {
                            InvalidTextBox.Text = "無効";
                        }
                        CompanyNameTextBox.Text = reader["name"].ToString();
                        ComPanyKanaTextBox.Text = reader["name_kana"].ToString();
                        ShopNameTextBox.Text = reader["birthday"].ToString();
                        ShopKanaTextBox.Text = reader["postal_code"].ToString();
                        AntiqueNumberTextBox.Text = reader["address"].ToString();
                        PostalCodeTextBox.Text = reader["address_kana"].ToString();
                        AddressTextBox.Text = reader["phone_number"].ToString();
                        AddressKanaTextBox.Text = reader["fax_number"].ToString();
                        TelTextBox.Text = reader["email_address"].ToString();
                        FaxTextBox.Text = reader["occupation"].ToString();
                        PositionTextBox.Text = reader["bank_name"].ToString();
                        ClientStaffNameTextBox.Text = reader["branch_name"].ToString();
                        MailAddressTextBox.Text = reader["deposit_type"].ToString();
                        UrlTextBox.Text = reader["account_number"].ToString();
                        BankNameTextBox.Text = reader["account_name"].ToString();
                        BranchNameTextBox.Text = reader["account_name_kana"].ToString();
                        DepositTypeTextBox.Text = reader["id_number"].ToString();
                        AccountNumberTextBox.Text = reader["remarks"].ToString();
                        AccountNameTextBox.Text = reader["register_copy"].ToString();
                        AccountNameKanaTextBox.Text = reader["antique_license"].ToString();
                        RemarksTextBox.Text = reader["photo_id"].ToString();
                        IdTextBox.Text = reader["aol_financial_shareholder"].ToString();
                        RegisterCopyTextBox.Text = reader["tax_certificate"].ToString();
                        RegisterDateTextBox.Text = reader["residence_card"].ToString();
                        AntiqueLicenseTextBox.Text = reader["period_stay"].ToString();
                        AolFinancialShareholderTextBox.Text = reader["seal_certification"].ToString();
                        TaxCertificateTextBox.Text = reader["staff_name"].ToString();
                        ResidenceCardTextBox.Text = reader["reason"].ToString();
                        PeriodStayTextBox.Text = (DateTime.Parse(reader["register_date"].ToString())).ToLongDateString();
                    }
                }
            }
        }

        #region"戻る"
        private void ClientInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            RecordList = new RecordList(Statement, Staff_id, Staff_Name, Type, SlipNumber, Grade, AntiqueNumber, ID_Number, Access_auth);

            RecordList.Show();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            RecordList = new RecordList(Statement, Staff_id, Staff_Name, Type, SlipNumber, Grade, AntiqueNumber, ID_Number, Access_auth);
            this.Close();
            RecordList.Show();
        }
        #endregion
    }
}
