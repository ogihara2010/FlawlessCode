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
    public partial class ClientMaster : Form
    {
        MasterMaintenanceMenu masterMenu;
        MasterMaintenanceMenu_BC masterMenu_BC;
        public ClientMaster(MasterMaintenanceMenu mster)
        {
            InitializeComponent();
            masterMenu = mster;
        }
        public ClientMaster(MasterMaintenanceMenu_BC mster_BC)
        {
            InitializeComponent();
            masterMenu_BC = mster_BC;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            masterMenu.Show();
        }

        private void Add_Click(object sender, EventArgs e)
        {

        }

        private void Search1_Click(object sender, EventArgs e)
        {

        }
    }
}