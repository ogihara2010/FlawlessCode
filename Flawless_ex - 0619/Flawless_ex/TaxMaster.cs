using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class TaxMaster : Form
    {
        MasterMaintenanceMenu master;
        public TaxMaster(MasterMaintenanceMenu master)
        {
            InitializeComponent();
            this.master = master;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            master.Show();
        }
    }
}
