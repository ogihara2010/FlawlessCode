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
        string kana;
        int grade;

        public client_add(MainMenu mainMenu, int id, int type, string Access_auth, string Pass)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
            //this.statement = statement;
            staff_id = id;
            this.access_auth = Access_auth;
            this.pass = Pass;
            this.type = type;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            client_search client_Search = new client_search(mainMenu, staff_id, type, staff_name, address, Total, access_auth, pass);
            this.Close();
            client_Search.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            client_search client_Search = new client_search(mainMenu, staff_id, type, staff_name, address, Total, access_auth, pass);
            this.Close();
            client_Search.Show();
        }
        #region "法人　登録"
        private void Button5_Click(object sender, EventArgs e)
        {
            string RegistrationDate = this.deliveryDateBox.Text;
            string CompanyName = this.textBox2.Text;
            string CompanyNameKana = this.textBox3.Text;
            
            string PostalCodeNumber = this.PostalUpCordTextBox.Text;
            
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

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
            type = 0;
            staff_name = ClientStaffName;
            address = Address;
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, search1, search2, search3, grade);
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

            string PostalCodeNumber = this.PostalUpCordTextBox2.Text;
            
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

            conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
            type = 1;
            staff_name = Name;
            address = Address;
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, search1, search2, search3, grade);
            this.Close();
            statement.Show();
        }
        #endregion

        private void Client_add_Load(object sender, EventArgs e)
        {
            this.textBox50.Text = "　年　月　日";
        }
        
        #region"個人　値の検証"
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

        private void PostalUpCordTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void PostalDownCordTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        #endregion

        #region"法人　値の検証"
        private void PostalUpCordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
        }

        private void PostalDownCordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && !Char.IsControl(e.KeyChar))
            {
                MessageBox.Show("半角の数値しか入力できません。", "数値エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
            }
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
        #endregion

        #region"法人　画像確認"
        private void textBox24_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox24.Text))
            {
                pictureBox1.ImageLocation = textBox24.Text;
            }
        }

        private void textBox21_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox21.Text))
            {
                pictureBox1.ImageLocation = textBox21.Text;
            }
        }

        private void textBox22_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox22.Text))
            {
                pictureBox1.ImageLocation = textBox22.Text;
            }
        }

        private void textBox25_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox25.Text))
            {
                pictureBox1.ImageLocation = textBox25.Text;
            }
        }

        private void textBox27_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox27.Text))
            {
                pictureBox1.ImageLocation = textBox27.Text;
            }
        }

        private void textBox29_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox29.Text))
            {
                pictureBox1.ImageLocation = textBox29.Text;
            }
        }

        private void textBox26_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox26.Text))
            {
                pictureBox1.ImageLocation = textBox26.Text;
            }
        }
        #endregion

        #region"個人　画像確認"
        private void textBox37_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox37.Text))
            {
                pictureBox2.ImageLocation = textBox37.Text;
            }
        }

        private void textBox36_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox36.Text))
            {
                pictureBox2.ImageLocation = textBox36.Text;
            }
        }

        private void textBox35_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox35.Text))
            {
                pictureBox2.ImageLocation = textBox35.Text;
            }
        }

        private void textBox33_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox33.Text))
            {
                pictureBox2.ImageLocation = textBox33.Text;
            }
        }

        private void textBox31_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox31.Text))
            {
                pictureBox2.ImageLocation = textBox31.Text;
            }
        }

        private void textBox30_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox30.Text))
            {
                pictureBox2.ImageLocation = textBox30.Text;
            }
        }

        private void textBox32_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox32.Text))
            {
                pictureBox2.ImageLocation = textBox32.Text;
            }
        }
        #endregion

        //法人　メールアドレス
        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox13.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox13.Text = stringBuilder.ToString();
            textBox13.Select(textBox13.Text.Length, 0);
        }

        //法人　URL
        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            kana = Microsoft.VisualBasic.Strings.StrConv(textBox17.Text, Microsoft.VisualBasic.VbStrConv.Katakana | Microsoft.VisualBasic.VbStrConv.Narrow, 0x411);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(kana);

            textBox17.Text = stringBuilder.ToString();
            textBox17.Select(textBox17.Text.Length, 0);
        }
    }
}
