using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using QRLibrary;
using Android.Graphics;

namespace AndroidApp
{
    public partial class MainPage : ContentPage
    {
        private QRCode code;

        public MainPage()
        {
            InitializeComponent();
            textEditor.TextChanged += TextEditor_TextChanged;
            code = new QRCode();
        }

        private void paintCode()
        {
            int pixel_size = 1;
            bool[,] layout = code.GetBooleanMatrix();

            BmpMaker canvas = new BmpMaker((layout.GetUpperBound(0) + 1), (layout.GetUpperBound(1) + 1));
            
            for (int i = 0; i <= layout.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= layout.GetUpperBound(1); j++)
                {
                    if (layout[i, j])
                        canvas.SetPixel(j * pixel_size, i * pixel_size, Xamarin.Forms.Color.Black);
                }
            }
            canvasImage.Source = canvas.Generate();
        }

        private void TextEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            code.EncodeString(textEditor.Text);
            paintCode();
        }
    }
}
