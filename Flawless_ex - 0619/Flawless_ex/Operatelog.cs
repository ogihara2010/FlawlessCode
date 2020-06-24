using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class Operatelog : Form
    {

        int staff_id;
        MainMenu mainMenu;
        public Operatelog(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;

        }

        private void Return1_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }
    }
}
