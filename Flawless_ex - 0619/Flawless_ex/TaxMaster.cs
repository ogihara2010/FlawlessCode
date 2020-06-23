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
