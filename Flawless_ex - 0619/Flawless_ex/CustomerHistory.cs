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
    public partial class CustomerHistory : Form
    {
        int staff_id;
        MainMenu mainMenu;
        public CustomerHistory(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }

        private void CustomerHistory_Load(object sender, EventArgs e)
        {

        }

        private void dataSelectButton_Click(object sender, EventArgs e)//検索ボタン
        {
            DataSearchResults dataSearch = new DataSearchResults(this);
            this.Hide();
            dataSearch.Show();
        }
    }
}
