using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;

namespace ArchitectureToVector
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();

            System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\OCEOperatingData\Drawings\Drawing1.png");

            this.pbxInput.Image = img;

            byte[] imgBytes = null;

            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                imgBytes = ms.ToArray();
            }

            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));

            var x = this.pbxInput.Image.PixelFormat;

            Bitmap bm = (Bitmap)_imageConverter.ConvertFrom(imgBytes);

            this.pbxInput.Image = bm;
        }
    }
}
