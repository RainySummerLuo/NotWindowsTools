using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinRun {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e) {
            int rectY = 190;
            Color rectColor = Color.FromArgb(240, 240, 240);
            Rectangle rect = new Rectangle(0, rectY, Width, Height - rectY);
            Brush rectBrush = new SolidBrush(rectColor);
            Graphics g = this.CreateGraphics();
            g.Clear(BackColor);
            g.FillRectangle(rectBrush, rect);

            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "";
        }

        private void btnOk_Click(object sender, EventArgs e) {
            string exe = comboBox1.Text;
            int idxEpt = exe.IndexOf(' ');
            try {
                if (idxEpt == -1) {
                    Process.Start(exe);
                } else {
                    string exeA = exe.Substring(0, idxEpt);
                    string exeB = exe.Substring(idxEpt + 1);
                    Process.Start(exeA, exeB);
                }
                Application.Exit();
            } catch {
                MessageBox.Show("Windows cannot find '" + comboBox1.Text + "'. Make sure you typed the name correctly, and then try again.", 
                    comboBox1.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnCcl_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void btnBrw_Click(object sender, EventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog {
                Multiselect = false,
                Title = "Browse",
                Filter = "Programs (*.exe;*.pif;*.com;*.bat;*.cmd)|*.exe;*.pif;*.com;*.bat;*.cmd"
            };
            string file = null;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                file = dialog.FileName;
            }
            comboBox1.Text = file;
        }
    }
}
