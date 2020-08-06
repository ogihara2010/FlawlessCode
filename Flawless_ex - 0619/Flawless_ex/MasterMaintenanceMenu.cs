using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class MasterMaintenanceMenu : Form //マスタメンテナンスメニュー(A権限者)
    {
        MainMenu mainMenu;
        int staff_code;
        string access_auth;
        public MasterMaintenanceMenu(MainMenu mainMenu, int staff_code, string access_auth)
        {
            InitializeComponent();

            this.mainMenu = mainMenu;
            this.staff_code = staff_code;
            this.access_auth = access_auth;
        }

        private void MasterMaintenanceMenu_Load(object sender, EventArgs e)
        {
            if (access_auth == "B")
            {
                this.staffMasterButton.Visible= false;
            }
            else if (access_auth == "C")
            {
                this.staffMasterButton.Visible = false;
            }
            else { }
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
            ClientMaster clientMaster = new ClientMaster(this, staff_code, access_auth);
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
            TaxMaster taxMaster = new TaxMaster(this, staff_code, access_auth);
            this.Hide();
            taxMaster.Show();
        }
    }
}
