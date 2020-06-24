using System;
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
            ClientMaster clientMaster = new ClientMaster(this);
            this.Hide();
            clientMaster.Show();
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
