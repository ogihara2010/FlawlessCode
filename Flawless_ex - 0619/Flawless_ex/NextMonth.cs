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
    public partial class NextMonth : Form
    {
        int staff_id;
        MainMenu mainMenu;
        public NextMonth(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Return4_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }
    }
}
