using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace TaskMgr {
    public partial class Form1 : Form
    {
        string selectedItem = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            tabControl1.Width = Size.Width;
            int btnNewX = Size.Width - 35 - btnEndTask.Width;
            int btnNewY = Size.Height - 78 - btnEndTask.Height;
            int lnkNewY = Size.Height - 83 - lnkDetail.Height;
            int picNewY = Size.Height - 85 - picUpDown.Height;
            btnEndTask.SetBounds(btnNewX, btnNewY, btnEndTask.Width, btnEndTask.Height);
            lnkDetail.SetBounds(lnkDetail.Location.X, lnkNewY, lnkDetail.Width, lnkDetail.Height);
            picUpDown.SetBounds(picUpDown.Location.X, picNewY, picUpDown.Width, picUpDown.Height);

            listView1.SetBounds(-30, 0, Width, Height - 130);

            int lineY = Size.Height - 130;
            Color lineColor = Color.FromArgb(160, 160, 160);
            Graphics g = this.CreateGraphics();
            g.Clear(BackColor);
            g.DrawLine(new Pen(lineColor, 3), new Point(0, lineY), new Point(Size.Width, lineY));
        }

        private void picUpDown_Click(object sender, EventArgs e)
        {
            if (lnkDetail.Text == "More details") {
                picUpDown.Image = Properties.Resources.up;
                lnkDetail.Text = "Fewer details";
            } else {
                picUpDown.Image = Properties.Resources.down;
                lnkDetail.Text = "More details";

            }
        }

        private void picUpDown_Click(object sender, MouseEventArgs e) {
            if (lnkDetail.Text == "More details") {
                picUpDown.Image = Properties.Resources.up;
                lnkDetail.Text = "Fewer details";
            } else {
                picUpDown.Image = Properties.Resources.down;
                lnkDetail.Text = "More details";

            }
        }

        private void Form1_Shown(object sender, EventArgs e) {
            tabControl1.Width = Size.Width;
            int btnNewX = Size.Width - 35 - btnEndTask.Width;
            int btnNewY = Size.Height - 78 - btnEndTask.Height;
            int lnkNewY = Size.Height - 83 - lnkDetail.Height;
            int picNewY = Size.Height - 85 - picUpDown.Height;
            btnEndTask.SetBounds(btnNewX, btnNewY, btnEndTask.Width, btnEndTask.Height);
            lnkDetail.SetBounds(lnkDetail.Location.X, lnkNewY, lnkDetail.Width, lnkDetail.Height);
            picUpDown.SetBounds(picUpDown.Location.X, picNewY, picUpDown.Width, picUpDown.Height);

            listView1.SetBounds(-30, 0, Width, Height - 130);

            int lineY = Size.Height - 130;
            Color lineColor = Color.FromArgb(160, 160, 160);
            Graphics g = this.CreateGraphics();
            g.Clear(BackColor);
            g.DrawLine(new Pen(lineColor, 3), new Point(0, lineY), new Point(Size.Width, lineY));
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void timer1_Tick(object sender, EventArgs e) {
            listView1.Clear();
            listView1.BeginUpdate();

            ImageList imgList = new ImageList {
                ImageSize = new Size(41, 41)
            };
            
            var windows = WindowEnumerator.FindAll();

            var myId = Process.GetCurrentProcess().Id;

            foreach (var window in windows) {
                try {
                    int processId = window.PID;
                    if (processId == myId) {
                        continue;
                    }
                    Process process = Process.GetProcessById(processId);
                    string processFileName = process.MainModule.FileName;
                    Icon processIco = null;
                    if (processFileName != ".") {
                        processIco = Icon.ExtractAssociatedIcon(processFileName);
                    }
                    imgList.Images.Add(processId.ToString(), processIco);
                } catch (Exception) {
                    continue;
                }
            }

            listView1.SmallImageList = imgList;
            listView1.LargeImageList = imgList;
            listView1.StateImageList = imgList;
            listView1.View = View.List;

            foreach (var window in windows) {
                int processId = window.PID;
                if (processId == myId) {
                    continue;
                }
                try {
                    Process process = Process.GetProcessById(processId);
                    string processName = process.ProcessName;
                    listView1.Items.Add("   " + processName, processId.ToString());
                } catch (Exception) {
                    continue;
                }
            }

            ListViewItem item = null;
            if (selectedItem != "" && selectedItem != null) {
                item = listView1.FindItemWithText(selectedItem);
            }
            if (item != null) {
                listView1.Items[listView1.Items.IndexOf(item)].Selected = true;
            }

            listView1.EndUpdate();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView1.SelectedItems.Count > 0) {
                selectedItem = listView1.SelectedItems[0].Text;
                btnEndTask.Enabled = true;
            } else {
                selectedItem = null;
                btnEndTask.Enabled = false;
            }
        }

        private void btnEndTask_Click(object sender, EventArgs e) {
            ListViewItem item = listView1.SelectedItems[0];
            int pid = int.Parse(item.ImageKey);
            Process process = Process.GetProcessById(pid);
            process.Kill();
        }
    }
}
