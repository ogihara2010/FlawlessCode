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
    public partial class MasterMaintenanceMenu : Form //マスタメンテナンスメニュー(A権限者)
    {
        MainMenu mainMenu;
        
        public MasterMaintenanceMenu(MainMenu mainMenu)
        {
            InitializeComponent();

            this.mainMenu = mainMenu;

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
            StaffMaster personMaster = new StaffMaster(this);
            this.Hide();
            personMaster.Show();
        }

        private void clientMasterButton_Click(object sender, EventArgs e)
        {

        }

        private void itemMasterButtonClick(object sender, EventArgs e)
        {
            ItemMaster productNameMenu = new ItemMaster(this);
            this.Hide();
            productNameMenu.Show();
        }

        private void mainCategoryMasterButton_Click(object sender, EventArgs e)
        {
            MainCategoryMaster mainCategory = new MainCategoryMaster(this);
            this.Hide();
            mainCategory.Show();
        }

        private void subCategoryMasterButton_Click(object sender, EventArgs e)
        {
            SubCategoryMaster subCategory = new SubCategoryMaster(this);
            this.Hide();
            subCategory.Show();
        }

        private void TaxMaster_Click(object sender, EventArgs e)
        {
            TaxMaster taxMaster = new TaxMaster(this);
            this.Hide();
            taxMaster.Show();
        }
    }
}
