using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Nursery_Management_System_WPF
{
    class ImageOperation
    {

        
        public  byte[] ImageToBinary(Image pictureImage)
        {
            byte[] buffer;
            var bitmap = pictureImage.Source as BitmapSource;
            var encoder = new PngBitmapEncoder(); // or one of the other encoders
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                buffer = stream.ToArray();
            }
            return buffer;
        }

        public  ImageSource BinaryToImage(byte[] byteArray)
        {
            if (byteArray == null)
                return null;
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(byteArray);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
    }
}
