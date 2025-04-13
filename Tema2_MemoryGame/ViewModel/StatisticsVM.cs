using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tema2_MemoryGame.Model;

namespace Tema2_MemoryGame.ViewModel
{
    public class StatisticsVM : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }

        public StatisticsVM(ObservableCollection<User> users)
        {
            this.Users = users;
            NotifyPropertyChanged(nameof(Users));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
