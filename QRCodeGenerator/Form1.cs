using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCodeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            resetImage();
        }

        /// <summary>
        /// Resets the canvas image to a blank white background
        /// </summary>
        private void resetImage()
        {
            Bitmap canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(canvas);
            lock(g)
            {
                g.Clear(Color.White);
            }
            g.Dispose();
            pictureBox.Image = canvas;
            pictureBox.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
