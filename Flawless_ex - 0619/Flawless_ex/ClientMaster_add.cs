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
    public partial class ClientMaster_add : Form
    {
        MasterMaintenanceMenu master;
        DataTable dt = new DataTable();
        public ClientMaster_add(MasterMaintenanceMenu master)
        {
            InitializeComponent();
            this.master = master;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master);

            this.Close();
            clientmaster.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("登録しますか？","登録確認",MessageBoxButtons.YesNo);

            if (dr == DialogResult.No) {
                return;
            }

            string RegistrationDate = this.textBox1.Text;
            string CompanyName = this.textBox2.Text;
            string CompanyNameKana = this.textBox3.Text;
            string ClientStaff = this.textBox3.Text;
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
            string AntiqueNumber = this.textBox23.Text;
            //string ID = this.textBox24.Text;
            //string RegistrationDate = this.textBox25.Text;
            string SealCertification = this.textBox26.Text;
            string TaxCertification = this.textBox27.Text;
            string Remarks = this.textBox28.Text;
            string ResidenceCard = this.textBox29.Text;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定

            string sql_str = "Insert * into client_m_corporate VALUES (type = 0, company_name = " + RegistrationDate + ",invalid = 0, company_name =" + CompanyName + " company_kana = " + CompanyNameKana + "shop_name =" + ShopName + " shop_name_kana = " + ShopNameKana + 
                ",antique_number = " + AntiqueNumber + " ,postal_code = " + PostalCodeNumber +" , client_staff_name =" + ClientStaff + ", address =" + Address + ",address_kana = " + AddressKana + ",phone_numbe =" + PhoneNumber + " ,fax_number = " + FaxNumber + ",position =" + Position + 
                ",client_staff_name = " + ClientStaffName + ",email_address = " + EmailAddress + " ,url_infor = " + URLinfor + ",bank_name = " + BankName + ",branch_name = " + BranchName + " ,deposit_type = " + DepositType + ",account_number = " + AccountNumber + ", account_name = " + AccountName
                + ",account_name_kana = " + AccountNameKana + ",remarks = " + Remarks +  ",register_copy = " + RegisterCopy + ",antique_license = " + Antiquelicense + ",tax_certificate = " + TaxCertification + ",residence_card =" + ResidenceCard + ",seal_certification = " + SealCertification;
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
        }
    }
}
