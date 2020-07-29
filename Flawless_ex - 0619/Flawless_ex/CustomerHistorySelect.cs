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
        MainMenu mainMenu;
        int staff_id;
        string data;
        public CustomerHistorySelect(MainMenu main, int id, string data)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            data = "S";
            CustomerHistory customerHistory = new CustomerHistory(mainMenu, staff_id, data);
            this.Hide();
            customerHistory.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            data = "D";
            CustomerHistory customerHistory = new CustomerHistory(mainMenu, staff_id, data);
            this.Hide();
            customerHistory.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }
    }
}
