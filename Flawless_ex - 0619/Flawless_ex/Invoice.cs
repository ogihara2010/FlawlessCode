using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class Invoice : Form
    {
        int staff_id;
        MainMenu mainMenu;
        public Invoice(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Return5_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }
    }
}
