using System;
using System.Windows.Forms;
using System.Data;
using Npgsql;

namespace Flawless_ex
{
    public partial class MonResult : Form
    {
        MainMenu mainMenu;
        int staff_id;
        DataTable dt = new DataTable();
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

        private void MonResult_Load(object sender, EventArgs e)
        {
            
        }
    }
}
