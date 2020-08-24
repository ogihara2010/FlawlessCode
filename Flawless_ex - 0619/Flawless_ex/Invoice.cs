using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class Invoice : Form
    {
        int staff_id;
        MainMenu mainMenu;
        string Access_auth;
        string Pass;

        public Invoice(MainMenu main, int id, string access_auth, string pass)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.Access_auth = access_auth;
            this.Pass = pass;
        }

        private void Return5_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }
    }
}
