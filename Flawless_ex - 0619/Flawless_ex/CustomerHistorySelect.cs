﻿using System;
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
        string Pass;
        bool screan = true;
        public CustomerHistorySelect(MainMenu main, int id, string data, string pass)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
            this.data = data;
            this.Pass = pass;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            data = "S";
            CustomerHistory customerHistory = new CustomerHistory(mainMenu, staff_id, data, Pass);
            screan = false;
            this.Hide();
            customerHistory.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            data = "D";
            CustomerHistory customerHistory = new CustomerHistory(mainMenu, staff_id, data, Pass);
            screan = false;
            this.Hide();
            customerHistory.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            screan = false;
            this.Close();
            mainMenu.Show();
        }

        private void CustomerHistorySelect_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (screan)
            {
                mainMenu.Show();
            }
        }
    }
}
