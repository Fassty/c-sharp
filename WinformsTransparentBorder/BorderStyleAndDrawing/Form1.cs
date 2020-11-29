using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BorderStyleAndDrawing
{
    public partial class Form1 : Form
    {
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        static extern bool ReleaseCapture();


        GraphicsPath gp = new GraphicsPath();
        GraphicsPath hide = new GraphicsPath();
        Pen pen = new Pen(Color.Black);
        Pen transparency = new Pen(Color.LawnGreen, 10);
        Brush b = new SolidBrush(Color.Aquamarine);

        public Form1()
        {
            InitializeComponent();
            gp = RoundedRect(new Rectangle(0, 0, Width, Height), 30);
            Region = new Region(new Rectangle(0,0,Width,Height));
            Region.Intersect(gp);
            this.TransparencyKey = Color.LawnGreen;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(transparency,370,5,454,5);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location.X, bounds.Location.Y+10, diameter, diameter);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            //path.AddArc(arc, 270, 90);
            /**/
            Point x0 = new Point(370, 10);
            Point x1 = new Point(370, 0);
            Point x2 = new Point(454,0);
            Point x3 = new Point(454, 10);
            path.AddLine(x0,x1);
            path.AddLine(x1,x2);
            path.AddLine(x2,x3); /**/

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void CloseButton_MouseHover(object sender, EventArgs e)
        {
            CloseButton.Height = 42;
            CloseButton.Top = 0;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.Height = 32;
            CloseButton.Top = 10;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void MinimizeButton_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MinimizeButton_MouseHover(object sender, EventArgs e)
        {
            MinimizeButton.Height = 42;
            MinimizeButton.Top = 0;
        }

        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            MinimizeButton.Height = 32;
            MinimizeButton.Top = 10;
        }
    }
}
