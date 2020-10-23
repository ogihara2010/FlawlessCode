﻿using System;
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
        public int staff_code;
        public string access_auth;
        public string path;
        public int type;
        public bool screan = true;
        public string Pass;
        public string kana;

        private FileServer fileServer = new FileServer();

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
                if (!string.IsNullOrEmpty(textBox29.Text) &&  string.IsNullOrEmpty(textBox46.Text))
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
            DialogResult dr = MessageBox.Show("登録しますか？","登録確認",MessageBoxButtons.YesNo);

            if (dr == DialogResult.No) {
                return;
            }

            string RegistrationDate = this.deliveryDateBox.Text;
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
            string RegisterCopy = fileServer.UploadImage(textBox21.Text,FileServer.Filetype.RegisterCopy);
            string Antiquelicense = fileServer.UploadImage(this.textBox22.Text,FileServer.Filetype.Antiquelicense);
            int AntiqueNumber = int.Parse(this.textBox23.Text);
            string ID = fileServer.UploadImage(this.textBox24.Text,FileServer.Filetype.ID);
            
            string PeriodStay = this.textBox46.Text;
            
            string SealCertification = fileServer.UploadImage(this.textBox26.Text, FileServer.Filetype.SealCertification);
            string TaxCertification = fileServer.UploadImage(this.textBox27.Text,FileServer.Filetype.TaxCertification);
            string Remarks = this.textBox28.Text;
            string ResidenceCard = fileServer.UploadImage(this.textBox29.Text,FileServer.Filetype.ResidenceCard);
            string AolFinancialShareholder = fileServer.UploadImage(textBox25.Text,FileServer.Filetype.AolFinancialShareholder);
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
            

            string sql_str = "Insert into client_m (type, registration_date, company_name, company_kana, shop_name, shop_name_kana, antique_number," +
                " address, address_kana, phone_number, fax_number, position, name, email_address, url_infor, bank_name, branch_name, deposit_type," +
                " account_number, account_name, account_name_kana, remarks, id, register_date, antique_license, tax_certificate, residence_card, period_stay," +
                " seal_certification, invalid, aol_financial_shareholder, register_copy, insert_name, postal_code1, postal_code2, code) " +
                "VALUES (" + 0 + " , '"  + RegistrationDate + "' , '" +  CompanyName + "' ,'" + CompanyNameKana + "' , '" + ShopName + "' ,  '" + ShopNameKana + " ', '" + AntiqueNumber + "' , '" + Address + "' ," +
                " '" + AddressKana + "' , '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + Position + "' , '" + ClientStaffName +"' , '" + EmailAddress + "', '" + URLinfor + "', '" + BankName + "' , " +
                "'" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + Remarks +  "' , '" + ID + "' , '" + b  +  "','" + Antiquelicense + "'," +
                "'" + TaxCertification + "','" + ResidenceCard + "','" + PeriodStay + "','" + SealCertification + "'," + 0 +  ",'" + AolFinancialShareholder + "','" + RegisterCopy + "'," + staff_code + "," +
                "" + PostalCode1 + ",'" + PostalCode2 + "');";

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = localhost; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("法人の顧客を登録しました。", "登録完了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk) ;
            ClientMaster clientmaster = new ClientMaster(master, staff_code, access_auth, Pass);
            screan = false;
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
            DialogResult dr = MessageBox.Show("登録しますか？", "登録確認", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return;
            }

            string RegistrationDate = this.dateTimePicker1.Text;
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
            string RegisterCopy = fileServer.UploadImage(this.textBox37.Text,FileServer.Filetype.RegisterCopy);
            string Antiquelicense = fileServer.UploadImage(this.textBox36.Text,FileServer.Filetype.Antiquelicense);
            string PhotoID = this.textBox35.Text;
            string ID = fileServer.UploadImage(this.textBox34.Text,FileServer.Filetype.ID);
            string PeriodStay = this.textBox47.Text;
            string SealCertification = fileServer.UploadImage(this.textBox32.Text,FileServer.Filetype.SealCertification);
            string TaxCertification = fileServer.UploadImage(this.textBox31.Text,FileServer.Filetype.TaxCertification);
            string Remarks = this.textBox58.Text;
            string ResidenceCard = fileServer.UploadImage(this.textBox30.Text,FileServer.Filetype.ResidenceCard);
            string AolFinancialShareholder = fileServer.UploadImage(textBox33.Text,FileServer.Filetype.AolFinancialShareholder);
            DateTime dat = DateTime.Now;
            string b = dat.ToString("yyyy/MM/dd");
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlDataAdapter adapter;
           
            string sql_str = "Insert into client_m_individual VALUES (" + 1 + " , '" + RegistrationDate + "' , '" + Name + "' ,'" + NameKana +  "' , '" + Birthday + "' , '"  + Address + "' , '" + AddressKana + "' , '" + PhoneNumber + "' , '" + FaxNumber + "' , '" + EmailAddress + "', '" + Occupation + 
               "' , '"  + BankName + "' , '" + BranchName + "' , '" + DepositType + "' , '" + AccountNumber + "' , '" + AccountName + "' , '" + AccountNameKana + "' , '" + ID +  "' , '" + Remarks + "','" + RegisterCopy + "' , '" + Antiquelicense + "','" + PhotoID + "' , '"  + TaxCertification + "','" + ResidenceCard + "','" + PeriodStay + "','" + SealCertification + "'," +
              0 + ",'" + AolFinancialShareholder +  "','" + b +"'," + staff_code + "," + PostalCode1 +",'" + PostalCode2 +"');";

            PostgreSQL postgre = new PostgreSQL();
            conn = postgre.connection();
            //conn.ConnectionString = @"Server = 192.168.152.43; Port = 5432; User Id = postgres; Password = postgres; Database = master;"; //変更予定
            conn.Open();

            adapter = new NpgsqlDataAdapter(sql_str, conn);
            adapter.Fill(dt);
            MessageBox.Show("登録しました。");
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
                this.ActiveControl = this.textBox2;
            }
            else if (type == 1)
            {
                tabControl1.SelectedTab = tabPage2;
                tabControl1.TabPages.Remove(tabPage1);
                this.textBox50.Text = "　年　月　日";
                this.ActiveControl = this.textBox56;
            }
            else { }
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
                textBox36.Text = path;
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

                textBox31.Text = path;

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

        #region"画像の表示　法人"
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

        #region"画像の表示　個人"

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

        #region"値の検証"
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

        private void textBox54_KeyPress(object sender, KeyPressEventArgs e)
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

        private void label34_Click(object sender, EventArgs e)
        {

        }
    }
}
