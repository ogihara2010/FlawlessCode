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
    public partial class Client_search : Form
    {
        Statement statement1;
        public Client_search(Statement statement)
        {
            InitializeComponent();
            statement1 = statement;
        }

        private void returnButton_Click(object sender, EventArgs e)//戻る
        {
            this.Close();

            statement1.Show();
        }
    }
}
