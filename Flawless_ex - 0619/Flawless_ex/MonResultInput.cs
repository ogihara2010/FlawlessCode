using System.Windows.Forms;

namespace Flawless_ex
{
    public partial class MonResultInput : Form
    {
        int staff_id;
        MainMenu mainMenu;
        TopMenu topMenu;
        string access_auth;
        string pass;
        public MonResultInput()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            MainMenu main = new MainMenu(topMenu, staff_id, access_auth, pass);
            this.Close();
            main.Show();
        }
    }
}
