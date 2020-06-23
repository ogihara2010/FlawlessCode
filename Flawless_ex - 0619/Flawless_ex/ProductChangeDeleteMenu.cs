using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class ProductChangeDeleteMenu : Form //品名削除・更新メニュー
    {
        ItemMaster productNameMenu;
        MasterMaintenanceMenu master;
        int puroductCode;
        DataTable dt;

        public ProductChangeDeleteMenu(ItemMaster nameMenu, MasterMaintenanceMenu master, int code)
        {
            InitializeComponent();
            this.productNameMenu = nameMenu;
            this.master = master;
            puroductCode = code;
        }

        private void ProductChangeDeleteMenu_Load(object sender, EventArgs e)
        {
            productCodeTextBox.Text = puroductCode.ToString();
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            ItemMaster productNameMenu = new ItemMaster(master);
            this.Close();
            productNameMenu.Show();
        }
    }
}
