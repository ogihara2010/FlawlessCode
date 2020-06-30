using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class ClientMaster_UPD : Form
    {
        MasterMaintenanceMenu master;
        public ClientMaster_UPD(MasterMaintenanceMenu master)
        {
            InitializeComponent();
            this.master = master;
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master);

            this.Close();
            clientmaster.Show();
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master);

            this.Close();
            clientmaster.Show();
        }

        private void Button18_Click_1(object sender, EventArgs e)
        {
            ClientMaster clientmaster = new ClientMaster(master);

            this.Close();
            clientmaster.Show();
        }
    }
}
