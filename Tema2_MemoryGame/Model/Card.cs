using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Tema2_MemoryGame.Model
{
    [Serializable]
    public class Card : INotifyPropertyChanged
    {
        private string imagePath;
        private bool showFront;
        private bool notMatched;
        public string ImagePath 
        {  
            get
            {
                return ShowFront ? imagePath : BackImagePath;
            }
            set
            {
                imagePath = value;
                NotifyPropertyChanged(nameof(ImagePath));
            }
        }
        public string Image
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
            }
        }
        public string BackImagePath;
        public bool NotMatched
        {
            get => notMatched;
            set
            {
                notMatched = value;
                NotifyPropertyChanged(nameof(NotMatched));
                ImagePath = imagePath;
            }
        }
        public bool ShowFront
        {
            get=> showFront;
            set
            {
                
                showFront = value;
                NotifyPropertyChanged(nameof(ImagePath));
            }
        }
        public Card(string imagePath)
        { 
            this.ImagePath = imagePath;
            this.Image = imagePath;
            ShowFront = false;
            NotMatched = true;
            BackImagePath = Path.Combine(AppContext.BaseDirectory, $"cards_images\\card_back_image.jpg");
        }
        public Card()
        {
            ShowFront = false;
            NotMatched = true;
            BackImagePath = Path.Combine(AppContext.BaseDirectory, $"cards_images\\card_back_image.jpg");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
