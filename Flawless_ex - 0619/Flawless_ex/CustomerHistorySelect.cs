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
    public partial class CustomerHistorySelect : Form
    {
        TopMenu topMenu;
        MainMenu mainMenu;
        public int staff_id;
        public string data;
        public string Pass;
        public string Access_auth;
        bool screan = true;

        public CustomerHistorySelect(MainMenu main, int id, string data, string pass, string access_auth)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.data = data;
            this.Pass = pass;
            this.Access_auth = access_auth;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            data = "S";
            CustomerHistory customerHistory = new CustomerHistory(mainMenu, staff_id, data, Pass, Access_auth);
            screan = false;
            this.Close();
            this.data = customerHistory.data;
            customerHistory.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            data = "D";
            CustomerHistory customerHistory = new CustomerHistory(mainMenu, staff_id, data, Pass, Access_auth);
            screan = false;
            this.Close();
            this.data = customerHistory.data;
            customerHistory.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu(topMenu, staff_id, Pass, Access_auth);
            screan = false;
            this.Close();
            mainMenu.Show();
        }

        private void CustomerHistorySelect_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                mainMenu = new MainMenu(topMenu, staff_id, Pass, Access_auth);
                mainMenu.Show();
            }
            else
            {
                screan = true;
            }
        }
    }
}
