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
            client_search client_Search = new client_search(statement, staff_id, type, staff_name, address, Total, control, document, access_auth, pass);
            screan = false;
            this.Close();
            this.data = client_Search.data;
            statement.AddOwnedForm(client_Search);
            client_Search.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            client_search client_Search = new client_search(statement, staff_id, type, staff_name, address, Total, control, document, access_auth, pass);
            screan = false;
            this.Close();
            this.data = client_Search.data;
            client_Search.Show();
        }
        #region "法人　登録"
        private void Button5_Click(object sender, EventArgs e)
        {
            #region"必須項目"
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("会社名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("会社名のカタカナを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox23.Text))
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

            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("住所を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("住所のカタカナを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("電話番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox12.Text))
            {
                MessageBox.Show("担当者名義を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox24.Text))
            {
                MessageBox.Show("身分証明書を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox21.Text))
            {
                MessageBox.Show("登記簿謄本を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox22.Text))
            {
                MessageBox.Show("古物商許可証を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #endregion

            string RegistrationDate = this.deliveryDateBox.Text;
            string CompanyName = this.textBox2.Text;
            string CompanyNameKana = this.textBox3.Text;
            
            string PostalUPCoreNumber = PostalUpCordTextBox.Text;
            string PostalDownCordNumber = PostalDownCordTextBox.Text;
            
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

            string PeriodStay = periodStayDateTimePicker.Text;
            
            string SealCertification = this.textBox26.Text;
            string TaxCertification = this.textBox27.Text;
            string Remarks = this.textBox28.Text;
            string ResidenceCard = this.textBox29.Text;
            string AolFinancialShareholder = textBox25.Text;
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");

            if (string.IsNullOrEmpty(ResidenceCard))
            {
                PeriodStay = "";
            }

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
                " VALUES (" + 0 + " , '" + RegistrationDate + "' , '" + CompanyName + "' ,'" + CompanyNameKana + "' ," +
                " '" + ShopName + "' ,  '" + ShopNameKana + " ', '" + AntiqueNumber + "' , '" + Address + "' , '" + AddressKana + "' ," +
                " '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + Position + "' , '" + ClientStaffName + "' , '" + EmailAddress + "', '" + URLinfor + "'," +
                " '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' ," +
                " '" + Remarks + "' , '" + ID + "' , '" + b + "','" + Antiquelicense + "','" + TaxCertification + "','" + ResidenceCard + "','" + PeriodStay + "'," +
                "'" + SealCertification + "'," + 0 + ",'" + AolFinancialShareholder + "','" + RegisterCopy + "'," + staff_id + ",'" + PostalUPCoreNumber + "'," +
                "'" + PostalDownCordNumber + "','" + ClientCode + "');";

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
            type = 0;
            staff_name = ClientStaffName;
            address = Address;
            Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1,
                phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
            this.Close();
            statement.Show();
        }
        #endregion
        #region "個人　登録"
        private void Button18_Click(object sender, EventArgs e)
        {
            #region"必須項目"
            if (string.IsNullOrEmpty(textBox56.Text))
            {
                MessageBox.Show("氏名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox55.Text))
            {
                MessageBox.Show("氏名のカタカナを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox50.Text))
            {
                MessageBox.Show("生年月日を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (string.IsNullOrEmpty(textBox53.Text))
            {
                MessageBox.Show("住所を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox52.Text))
            {
                MessageBox.Show("住所のカタカナを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox49.Text))
            {
                MessageBox.Show("電話番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox41.Text))
            {
                MessageBox.Show("職業を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox34.Text))
            {
                MessageBox.Show("身分証番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox35.Text))
            {
                MessageBox.Show("顔つき身分証を登録してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            string RegistrationDate = this.dateTimePicker1.Text;
            string Name = this.textBox56.Text;
            string NameKana = this.textBox55.Text;

            string Birthday = this.textBox50.Text;

            string PostalCodeUpNumber = this.PostalUpCordTextBox2.Text;
            string PostalCodeDownNumber = PostalDownCordTextBox2.Text;
            
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

            string PeriodStay = periodStayDateTimePicker1.Text;
            
            string SealCertification = this.textBox32.Text;
            string TaxCertification = this.textBox31.Text;
            string Remarks = this.textBox58.Text;
            string ResidenceCard = this.textBox30.Text;
            string AolFinancialShareholder = textBox33.Text;

            if (string.IsNullOrEmpty(ResidenceCard))
            {
                PeriodStay = "";
            }

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
                " period_stay, seal_certification, invalid, aol_financial_shareholder, postal_code1, postal_code2, code, insert_name )" +
                " VALUES (" + 1 + " , '" + RegistrationDate + "' , '" + Name + "' ,'" + NameKana + "' , '" + Birthday + "' , '" + Address + "' , '" + AddressKana + "' ," +
                " '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + EmailAddress + "', '" + Occupation + "' , '" + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , " +
                "'" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + ID + "' , '" + Remarks + "','" + RegisterCopy + "' , '" + Antiquelicense + "'," +
                "'" + PhotoID + "' , '" + TaxCertification + "','" + ResidenceCard + "','" + PeriodStay + "','" + SealCertification + "'," + 0 + ",'" + AolFinancialShareholder + "'," +
                "'"+PostalCodeUpNumber+"','"+PostalCodeDownNumber+"','"+ClientCode+"','"+staff_id+"');";

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
            type = 1;
            staff_name = Name;
            address = Address;
            statement.ClientCode = ClientCode;
            //Statement statement = new Statement(mainMenu, staff_id, type, staff_name, address, access_auth, Total, pass, document, control, data, name1, phoneNumber1, addresskana1, code1, item1, date1, date2, method1, amountA, amountB, antiqueNumber, documentNumber, address1, grade);
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
                textBox24.Text = path;
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
                textBox22.Text = path;
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
                textBox21.Text = path;
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
                textBox27.Text = path;
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
                textBox25.Text = path;
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
                textBox29.Text = path;
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
                textBox26.Text = path;
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
                textBox37.Text = path;
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
                textBox36.Text = path;
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
                textBox35.Text = path;
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
                textBox31.Text = path;
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
                textBox33.Text = path;
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
                textBox30.Text = path;
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
                textBox32.Text = path;
            }
            else if (dialog == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}
