using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacesApiTest
{
    public class ImageUtility
    {
        public static byte[] ConvertToBytes(string imagePath)
        {
            MemoryStream memoryStream = new MemoryStream();
            using (FileStream fs = new(imagePath, FileMode.Open))
            {
                fs.CopyTo(memoryStream);
            }
            return memoryStream.ToArray();
        }
        public static void FromBytesToImage(byte[] imageBytes, string fileName)
        {
            using var ms = new MemoryStream(imageBytes);
            Image img = Image.FromStream(ms);
            img.Save(fileName + ".jpg", ImageFormat.Jpeg);
        }
    }
}
