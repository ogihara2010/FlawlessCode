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
    public partial class MasterMaintenanceMenu_BC : Form//マスタメニュー（B,C権限者）
    {
        MainMenu mainMenu;
        public MasterMaintenanceMenu_BC(MainMenu mainMenu)
        {
            InitializeComponent();
            this.mainMenu = mainMenu;
        }

        private void MasterMaintenanceMenu_BC_Load(object sender, EventArgs e)
        {

        }

        private void clientMasterButton_Click(object sender, EventArgs e)
        {

        }

        private void itemMasterButton_Click(object sender, EventArgs e)
        {
         /*  ProductNameMenu productNameMenu = new ProductNameMenu(this);
            this.Hide();
            productNameMenu.Show();*/
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }
    }
}
