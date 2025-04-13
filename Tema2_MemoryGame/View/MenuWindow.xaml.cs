using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tema2_MemoryGame.Model;
using Tema2_MemoryGame.Services;
using Tema2_MemoryGame.ViewModel;

namespace Tema2_MemoryGame.View
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {

        public MenuWindow(string screenMessage, User currentUser)
        {
            InitializeComponent();
            (DataContext as MenuVM).ExitClicked += CloseWindow;
            (DataContext as MenuVM).OpenStatisticsWindow += OpenStatisticsWindow;
            (DataContext as MenuVM).NewGame += OpenGame;
            (DataContext as MenuVM).Time = 60;
            (DataContext as MenuVM).ScreenMessage = screenMessage;
            (DataContext as MenuVM).CurrentUser = currentUser;
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            Close();
        }
        private void OpenStatisticsWindow(object sender, EventArgs e)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            UsersServices usersServices = new UsersServices(users);
            StatisticsWindow statisticsWindow = new StatisticsWindow(users);
            statisticsWindow.Show();
        }
        private void OpenGame(object sender, EventArgs e)
        {
            CategoryToStringConverter converter = new CategoryToStringConverter();
            GameWindow gameWindow;
            if (e is GameInfoEventArgs gameInfoEventArgs)
            {
                 gameWindow = new GameWindow(gameInfoEventArgs.GameInfo.Category, (byte)gameInfoEventArgs.GameInfo.Cards.Count,
                     (byte)gameInfoEventArgs.GameInfo.Cards[0].Count, (DataContext as MenuVM).CurrentUser, gameInfoEventArgs.GameInfo.RemainingTime, (byte)gameInfoEventArgs.GameInfo.MatchedCards, gameInfoEventArgs.GameInfo.Cards);
            }
            else
            {
                 gameWindow = new GameWindow(converter.ConvertBack((DataContext as MenuVM).Category), (DataContext as MenuVM).BoardRows,
                     (DataContext as MenuVM).BoardColumns, (DataContext as MenuVM).CurrentUser, Int32.Parse(setTimeTextBox.Text));
            }

            gameWindow.Show();
            Close(); 
        }
    }
}
