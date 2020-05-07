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
            Pen linePen = new Pen(rectColor);
            Graphics g = this.CreateGraphics();
            g.Clear(BackColor);
            g.DrawLine(linePen, new Point(0, 0), new Point(Width, 0));
            g.FillRectangle(rectBrush, rect);
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

        private void Form1_Load(object sender, EventArgs e) {
            comboBox1.Text = "";
            btnOk.Enabled = false;
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e) {
            ComboBox combobox = sender as ComboBox;
            SolidBrush brush = new SolidBrush(combobox.ForeColor);
            Font font = combobox.Font;

            SizeF fontSize = e.Graphics.MeasureString("cmd", combobox.Font);

            // 垂直居中
            float top = (float)(e.Bounds.Height - fontSize.Height) / 2;
            if (top <= 0) top = 0f;

            // 输出
            e.DrawBackground();
            e.Graphics.DrawString("cmd", font, brush, new RectangleF(
                e.Bounds.X,    //设置X坐标偏移量
                e.Bounds.Y + top,     //设置Y坐标偏移量
                e.Bounds.Width, e.Bounds.Height), StringFormat.GenericDefault);

            //e.Graphics.DrawString(cmb.GetItemText(cmb.Items[e.Index]), ft, myBrush, e.Bounds, StringFormat.GenericDefault);
            e.DrawFocusRectangle();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e) {
            if (comboBox1.Text == "") {
                btnOk.Enabled = false;
            } else {
                btnOk.Enabled = true;
            }
        }
    }
}
