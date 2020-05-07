using System;
using System.Windows.Forms;

namespace TaskMgr {
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)) {
                //如果是管理员，则直接运行
                Application.Run(new Form1());
            } else {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    //设置启动动作,确保以管理员身份运行
                    Verb = "runas"
                };
                try {
                    System.Diagnostics.Process.Start(startInfo);
                } catch {
                    return;
                }
                //退出
                Application.Exit();
            }
            */
        }
    }
}
