using Npgsql;
using System;
using System.Windows.Forms;

namespace Flawless_ex
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new TopMenu());
            }catch(Npgsql.NpgsqlException ex)
            {
                MessageBox.Show("サーバーとの接続が切断された可能性があります。\r\nシステムを再起動してください。", "接続エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Console.WriteLine(ex);
            }
        }
    }
}
