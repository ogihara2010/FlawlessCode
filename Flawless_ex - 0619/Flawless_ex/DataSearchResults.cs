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
    public partial class DataSearchResults : Form
    {
        CustomerHistory customerHistory;
        public DataSearchResults(CustomerHistory customer)
        {
            InitializeComponent();

            customerHistory = customer;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻るボタン
        {

            this.Close();
            customerHistory.Show();

        }
    }
}
