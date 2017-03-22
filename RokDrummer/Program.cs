using System;
using System.Windows.Forms;
using Un4seen.Bass;

namespace RokDrummer
{
    static class Program
    {
        private const string bKey = "2X14232420202322";
        private const string user = "nemo";
        private const string domain = "keepitfishy";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BassNet.Registration(user + "@" + domain + ".com", bKey);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
