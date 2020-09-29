using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class MasterMaintenanceMenu : Form //マスタメンテナンスメニュー(A権限者)
    {
        MainMenu mainMenu;
        public int staff_code;
        public string access_auth;
        TopMenu top;
        public string Pass;
        bool screan = true;

        public MasterMaintenanceMenu(MainMenu mainMenu, int staff_code, string access_auth, string pass)
        {
            InitializeComponent();

            this.mainMenu = mainMenu;
            this.staff_code = staff_code;
            this.access_auth = access_auth;
            this.Pass = pass;
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
        }

        private void staffMasterButtonClick(object sender, EventArgs e)
        {
            StaffMaster personMaster = new StaffMaster(this, staff_code, Pass, access_auth);
            screan = false;
            this.Close();
            personMaster.Show();
        }

        private void clientMasterButton_Click(object sender, EventArgs e)
        {
            ClientMaster clientMaster = new ClientMaster(this, staff_code, access_auth, Pass);
            screan = false;
            this.Close();
            clientMaster.Show();
        }

        private void itemMasterButtonClick(object sender, EventArgs e)
        {
            ItemMaster productNameMenu = new ItemMaster(this, staff_code, access_auth, Pass);
            screan = false;
            this.Close();
            productNameMenu.Show();
        }

        private void TaxMaster_Click(object sender, EventArgs e)
        {
            TaxMaster taxMaster = new TaxMaster(this, staff_code, access_auth, Pass);
            screan = false;
            this.Close();
            taxMaster.Show();
        }

        private void MasterMaintenanceMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                mainMenu = new MainMenu(top, staff_code, Pass, access_auth);
                mainMenu.Show();
            }
            else
            {
                screan = true;
            }
        }
    }
}
