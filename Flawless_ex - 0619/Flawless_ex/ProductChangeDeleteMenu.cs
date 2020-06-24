using System;
using System.Data;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class ProductChangeDeleteMenu : Form //品名削除・更新メニュー
    {
        ItemMaster productNameMenu;
        MasterMaintenanceMenu master;
        int puroductCode;
        DataTable dt;
        int staff_code;//ログイン者の担当者コード

        public ProductChangeDeleteMenu(ItemMaster nameMenu, MasterMaintenanceMenu master, int code,int staff_code)
        {
            InitializeComponent();
            this.productNameMenu = nameMenu;
            this.master = master;
            puroductCode = code;
            this.staff_code = staff_code;
        }

        private void ProductChangeDeleteMenu_Load(object sender, EventArgs e)
        {
            productCodeTextBox.Text = puroductCode.ToString();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            ItemMaster productNameMenu = new ItemMaster(master,staff_code);
            this.Close();
            productNameMenu.Show();
        }
    }
}
