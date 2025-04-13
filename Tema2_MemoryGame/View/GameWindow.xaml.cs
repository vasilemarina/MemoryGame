using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
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
using System.Windows.Threading;
using Tema2_MemoryGame.Model;
using Tema2_MemoryGame.Services;
using Tema2_MemoryGame.ViewModel;

namespace Tema2_MemoryGame.View
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int delay;
        private DateTime deadline;
        public GameWindow(GameCategory category, byte boardRows, byte boardColumns, User currentUser, int delay, byte matchedCards=0, ObservableCollection<ObservableCollection<Card>> cards = null, List<Card> TurnedCards=null)
        {
            InitializeComponent();


            DataContext = new GameVM(category, boardRows, boardColumns, currentUser, delay, matchedCards, cards);

            this.delay = delay;
            remainingTimeLabel.Content = delay.ToString();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            StartTimer();

            (DataContext as GameVM).ExitClicked += CloseWindow;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int secondsRemaining = (deadline - DateTime.Now).Seconds;
            if (secondsRemaining == 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.IsEnabled = false;
            }
            remainingTimeLabel.Content = secondsRemaining.ToString();
            if (secondsRemaining == 0)
                EndGame("Out of time!");
            else if ((DataContext as GameVM).MatchedCards == (DataContext as GameVM).BoardRows * (DataContext as GameVM).BoardColumns)
            {
                dispatcherTimer.Stop();
                (DataContext as GameVM).CurrentUser.WonGames++;
                EndGame("You won!");
            }
        }
        private void StartTimer()
        {
            deadline = DateTime.Now.AddSeconds(delay);
            dispatcherTimer.Start();
        }
        private void EndGame(string message)
        {
            (DataContext as GameVM).CurrentUser.PlayedGames++;
            MenuWindow menuWindow = new MenuWindow(message, (DataContext as GameVM).CurrentUser);
            menuWindow.Show();

            ObservableCollection<User> users = new ObservableCollection<User>();
            UsersServices usersServices = new UsersServices(users);
            usersServices.UpdateUser((DataContext as GameVM).CurrentUser);
            Close();
        }
        private void CloseWindow(object sender, EventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow("", (sender as GameVM).CurrentUser);
            menuWindow.Show();
            Close();
        }
    }
}
