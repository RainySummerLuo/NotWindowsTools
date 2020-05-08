using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AboutWin {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Version winver = Environment.OSVersion.Version;
            int majorVer = winver.Major;
            int minorver = winver.Minor;
            int revisver = winver.Revision;
            string winstr = Environment.OSVersion.ServicePack;

            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            ManagementObject mo = moc.OfType<ManagementObject>().FirstOrDefault();
            string wincap = mo["Caption"].ToString();

            string relsever = null;
            string buildver = null;
            string upbldver = null;
            string usrName = null;
            string orgName = null;
            using (var hklmKey = Microsoft.Win32.Registry.LocalMachine)
            using (var subKey = hklmKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion")) {
                if (subKey != null) {
                    relsever = subKey.GetValue("ReleaseId") as string;
                    buildver = subKey.GetValue("CurrentBuildNumber") as string;
                    upbldver = subKey.GetValue("UBR").ToString() as string;
                    usrName = subKey.GetValue("RegisteredOwner") as string;
                    orgName = subKey.GetValue("RegisteredOrganization") as string;
                }
            }

            lblVersion.Text = "Version " + relsever + " (OS Build " + buildver + "." + upbldver + ")";

            lblCopyright.Text = "The " + wincap.Replace("Microsoft ", "") + 
                " operating system and its user interface are protected by trademark and other pending or existing intellectual property rights in the United States and other countries/regions.";

            lblUsr.Text = usrName;
            lblOrg.Text = orgName == null ? "org name" : orgName ;
        }

        private void Form1_Shown(object sender, EventArgs e) {
            int lineY = 125;
            Color lineColor = Color.FromArgb(160, 160, 160);
            Pen linePen = new Pen(lineColor);
            Graphics g = this.CreateGraphics();
            g.Clear(BackColor);
            g.DrawLine(linePen, new Point(24, lineY), new Point(Width - 48, lineY));
            g.DrawLine(new Pen(Color.White), new Point(24, lineY + 1), new Point(Width - 48, lineY + 1));
        }
    }
}
