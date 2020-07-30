using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class NextMonth : Form
    {
        int staff_id;
        MainMenu mainMenu;
        Statement statement;
        string staff_name;
        int type;
        string slipNumber;
        public NextMonth(MainMenu main, int id)
        {
            InitializeComponent();
            staff_id = id;
            mainMenu = main;
        }

        private void Return4_Click(object sender, EventArgs e)
        {
            this.Close();
            mainMenu.Show();
        }

        private void Choice2_Click(object sender, EventArgs e)
        {
            RecordList recordList = new RecordList(statement, staff_id, staff_name, type, slipNumber);
            this.Close();
            recordList.Show();
        }

        private void NextMonth_Load(object sender, EventArgs e)
        {

        }
    }
}
