using QRLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCodeGenerator
{
    public partial class Form1 : Form
    {
        private QRCode code { get; set; } = new QRCode();
        private Tuple<int, int> prevSize { get; set; }
        private string fileData = "";
        private bool useFileData = false;

        public Form1()
        {
            InitializeComponent();
            prevSize = new Tuple<int, int>(Width, Height);
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
            int pixel_size = 1;
            bool[,] layout = code.GetBooleanMatrix();
            Bitmap canvas = new Bitmap((layout.GetUpperBound(0) + 1) * pixel_size, (layout.GetUpperBound(1) + 1) * pixel_size);
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
                            g.FillRectangle(b, j * pixel_size, i * pixel_size, pixel_size, pixel_size);
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
            code.EncodeString((useFileData) ? fileData : textBox.Text);
            useFileData = false;
            paintCode();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Bitmap Image|*.bmp";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                pictureBox.Image.Save(saveFileDialog1.FileName);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Document|*.txt";
            openFileDialog1.Title = "Select a text file to import";
            openFileDialog1.ShowDialog();

            if(openFileDialog1.FileName != "")
            {
                fileData = File.ReadAllText(openFileDialog1.FileName);
                useFileData = true;
                textBox.Text = (fileData.Length >= 50000) ? "File Imported contents too big not shown" : fileData;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Tuple<int, int> difference = new Tuple<int, int>(Width - prevSize.Item1, Height - prevSize.Item2);
            textBox.Height += difference.Item2;
            pictureBox.Height += difference.Item2;
            pictureBox.Width += difference.Item1;
            textBox.Invalidate();
            pictureBox.Invalidate();
            paintCode();
        }
    }
}
