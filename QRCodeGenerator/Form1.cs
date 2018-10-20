using QRLibrary;
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
        private QRCode code { get; set; } = new QRCode();

        public Form1()
        {
            InitializeComponent();
            resetImage();
        }

        /// <summary>
        /// Resets the canvas image to a blank white background
        /// </summary>
        private void resetImage(int width = -1, int height = -1)
        {
            if (width < 0)
                width = pictureBox.Width;
            if (height < 0)
                height = pictureBox.Height;
            Bitmap canvas = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(canvas);
            lock(g)
            {
                g.Clear(Color.White);
            }
            g.Dispose();
            pictureBox.Image = canvas;
            pictureBox.Invalidate();
        }

        private void paintCode()
        {
            bool[,] layout = code.GetBooleanMatrix();
            Bitmap canvas = new Bitmap((layout.GetUpperBound(0) + 1) * 10, (layout.GetUpperBound(1) + 1) * 10);
            Graphics g = Graphics.FromImage(canvas);
            Brush b = new SolidBrush(Color.Black);
            lock(g)
            {
                g.Clear(Color.White);

                for(int i = 0; i <= layout.GetUpperBound(0); i++)
                {
                    for(int j = 0; j <= layout.GetUpperBound(1); j++)
                    {
                        if (layout[i, j])
                            g.FillRectangle(b, i * 10, j * 10, 10, 10);
                    }
                }
            }
            b.Dispose();
            g.Dispose();
            pictureBox.Image = canvas;
            pictureBox.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            code.EncodedData.Clear();
            code.EncodeString(textBox.Text);
            paintCode();
        }
    }
}
