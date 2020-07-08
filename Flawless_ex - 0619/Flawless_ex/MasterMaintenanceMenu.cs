using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class MasterMaintenanceMenu : Form //マスタメンテナンスメニュー(A権限者)
    {
        MainMenu mainMenu;
        int staff_code;
        public MasterMaintenanceMenu(MainMenu mainMenu, int staff_code)
        {
            InitializeComponent();

            this.mainMenu = mainMenu;
            this.staff_code = staff_code;

        }

        private void MasterMaintenanceMenu_Load(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }




        private void staffMasterButtonClick(object sender, EventArgs e)
        {
            StaffMaster personMaster = new StaffMaster(this, staff_code);
            this.Hide();
            personMaster.Show();
        }

        private void clientMasterButton_Click(object sender, EventArgs e)
        {
            ClientMaster clientMaster = new ClientMaster(this, staff_code);
            this.Hide();
            clientMaster.Show();
        }

        private void itemMasterButtonClick(object sender, EventArgs e)
        {
            ItemMaster productNameMenu = new ItemMaster(this, staff_code);
            this.Hide();
            productNameMenu.Show();
        }

        private void TaxMaster_Click(object sender, EventArgs e)
        {
            TaxMaster taxMaster = new TaxMaster(this);
            this.Hide();
            taxMaster.Show();
        }
    }
}
