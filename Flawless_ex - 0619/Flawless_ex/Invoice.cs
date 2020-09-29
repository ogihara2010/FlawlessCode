using System;
using System.Windows.Forms;
using Npgsql;
using System.Data;

namespace Flawless_ex
{
    public partial class Invoice : Form
    {
        int staff_id;
        MainMenu mainMenu;
        TopMenu topMenu;    
        string access_auth;
        string pass;                
                
        public Invoice(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Return5_Click(object sender, EventArgs e)
        {
            MainMenu main = new MainMenu(topMenu, staff_id, access_auth, pass);
            this.Close();
            main.Show();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            

        }

    }
    }
