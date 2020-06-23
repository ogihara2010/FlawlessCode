using System;
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
