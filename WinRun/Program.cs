using System;
using System.Windows.Forms;

namespace WinRun {
    static class Program {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);

        private const int SPI_SETKEYBOARDCUES = 4107; //100B

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            SystemParametersInfo(SPI_SETKEYBOARDCUES, 0, 1, 0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            SystemParametersInfo(SPI_SETKEYBOARDCUES, 0, 0, 0);
        }
    }
}
