using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema2_MemoryGame.Commands;
using Tema2_MemoryGame.Model;
using Tema2_MemoryGame.Services;

namespace Tema2_MemoryGame.ViewModel
{
    public class MenuVM : INotifyPropertyChanged
    {
        private byte boardRows;
        private byte boardColumns;
        private string boardDimensions;
        private string screenMessage;
        private int time;
        public User CurrentUser { get; set; }
        public string Category { get; set; }
        public int Time 
        {
            get => time;
            set
            {
                time = value;
                NotifyPropertyChanged(nameof(Time));
            }
        }
        public string ScreenMessage       
        {
            get => screenMessage;
            set
            {
                screenMessage = value;
                NotifyPropertyChanged(nameof(ScreenMessage));
            }
        }
        public string BoardDimensions
        {
            get => boardDimensions;
            set
            {
                boardDimensions = value;
                NotifyPropertyChanged(nameof(BoardDimensions));
            }
        }
        public byte BoardRows
        {
            get => boardRows;
            set
            {
                boardRows = value;
            }
        }
        public byte BoardColumns
        {
            get => boardColumns;
            set
            {
                boardColumns = value;
            }
        }

        ICommand selectCategoryCommand;
        public ICommand SelectCategoryCommand
        {
            get
            {
                selectCategoryCommand ??= new RelayCommand(SelectCategory);
                return selectCategoryCommand;
            }
        }
        void SelectCategory(object parameter)
        {
            Category = parameter as string;
            NotifyPropertyChanged(nameof(Category));
        }
        public event EventHandler NewGame;
        ICommand newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                newGameCommand ??= new RelayCommand(StartNewGame);
                return newGameCommand;
            }
        }
        void StartNewGame(object parameter)
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler OpenStatisticsWindow;
        ICommand showStatisticsCommand;
        public ICommand ShowStatisticsCommand
        {
            get
            {
                showStatisticsCommand ??= new RelayCommand(ShowStatistics);
                return showStatisticsCommand;
            }
        }
        void ShowStatistics(object parameter)
        {
            OpenStatisticsWindow?.Invoke(this, EventArgs.Empty);
        }
        ICommand openGameCommand;
        public ICommand OpenGameCommand
        {
            get
            {
                openGameCommand ??= new RelayCommand(OpenGame);
                return openGameCommand;
            }
        }
        void OpenGame(object parameter)
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, $"saved_games\\saved_game_{CurrentUser.Username}.xml");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("No saved game.");
                return;
            }
            SerializationActions serializer = new SerializationActions(filePath);
            GameInfo gameInfo = serializer.DeserializeGameInfo(filePath);

            NewGame?.Invoke(this, new GameInfoEventArgs(gameInfo));
        }

        ICommand openAboutBoxCommand;
        public ICommand OpenAboutBoxCommand
        {
            get
            {
                openAboutBoxCommand ??= new RelayCommand(OpenAboutBox);
                return openAboutBoxCommand;
            }
        }
        void OpenAboutBox(object parameter)
        {
            string aboutText = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, $"about.txt"));
            MessageBox.Show(aboutText, "About");
        }

        ICommand changeDimensionCommand;
        public ICommand ChangeDimensionCommand
        {
            get
            {
                changeDimensionCommand ??= new RelayCommand(ChangeDimension);
                return changeDimensionCommand;
            }
        }
        void ChangeDimension(object parameter)
        {
            BoardDimensions = (parameter as string);
            BoardRows = Byte.Parse(BoardDimensions.Substring(0, 1));
            BoardColumns = Byte.Parse(BoardDimensions.Substring(2, 1));
            NotifyPropertyChanged(nameof(BoardRows));
            NotifyPropertyChanged(nameof(BoardColumns));
        }

        public event EventHandler ExitClicked;
        ICommand closeWindowCommand;
        public ICommand CloseWindowCommand
        {
            get
            {
                closeWindowCommand ??= new RelayCommand(CloseWindow);
                return closeWindowCommand;
            }
        }
        void CloseWindow(object parameter)
        {
            ExitClicked?.Invoke(this, EventArgs.Empty);
        }
        public MenuVM(User currentUser)
        {
            BoardDimensions = "4x4";
            BoardRows = 4;
            BoardColumns = 4;
            Category = "Flowers";
            Time = 60;
            CurrentUser = currentUser;
        }
        public MenuVM()
        {
            BoardDimensions = "4x4";
            BoardRows = 4;
            BoardColumns = 4;
            Category = "Flowers";
            Time = 60;
       
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
