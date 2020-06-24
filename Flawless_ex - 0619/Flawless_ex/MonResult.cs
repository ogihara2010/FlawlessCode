using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class MonResult : Form
    {
        MainMenu mainMenu;
        int staff_id;
        public MonResult(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Return3_Click(object sender, EventArgs e)//戻るボタン
        {
            this.Close();
            mainMenu.Show();
        }

        private void choice_Click(object sender, EventArgs e)//選択ボタン
        {

        }
    }
}
