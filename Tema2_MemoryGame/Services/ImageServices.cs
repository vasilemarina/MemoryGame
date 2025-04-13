using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_MemoryGame.Services
{
    class ImageServices
    {
        public static void LoadImages(List<string> imagePaths, string imagesDirectoryPath)
        {
            if(Directory.Exists(imagesDirectoryPath))
            {
                string[] images = Directory.GetFiles(imagesDirectoryPath);
                imagePaths.AddRange(images);
            }
        }
    }
}
