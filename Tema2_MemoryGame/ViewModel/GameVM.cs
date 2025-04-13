using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Tema2_MemoryGame.Commands;
using Tema2_MemoryGame.Model;
using Tema2_MemoryGame.Services;

namespace Tema2_MemoryGame.ViewModel
{
    public class GameVM : INotifyPropertyChanged
    {
        private ObservableCollection<ObservableCollection<Card>> cards;
        public ObservableCollection<ObservableCollection<Card>> Cards
        {
            get => cards;
            set
            {
                cards = value;
                NotifyPropertyChanged(nameof(Cards));
            }
        }

        GameServices services;
        public User CurrentUser { get; set; }
        public  int TotalTime { get; set; }
        public byte MatchedCards { get; set;  }
        public GameCategory Category { get; set; }
        public byte BoardRows { get; set; }
        public byte BoardColumns { get; set; }
        private List<Card> turnedCards = new();

        ICommand saveGameCommand;
        public ICommand SaveGameCommand
        {
            get
            {
                saveGameCommand ??= new RelayCommand(SaveGame);
                return saveGameCommand;
            }
        }
        void SaveGame(object parameter)
        {
            CategoryToStringConverter converter = new CategoryToStringConverter();
            GameInfo gameInfo = new GameInfo(Category, Cards, Int32.Parse(parameter as string), TotalTime,CurrentUser.Username, MatchedCards);

            string filePath = Path.Combine(AppContext.BaseDirectory, $"saved_games\\saved_game_{CurrentUser.Username}.xml");
            SerializationActions serilizer = new SerializationActions(filePath);
            serilizer.SerializeGameInfo(gameInfo, filePath);
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
        
        ICommand turnOverCardCommand;
        public ICommand TurnOverCardCommand
        {
            get
            {
                turnOverCardCommand ??= new RelayCommand(TurnOverCard);
                return turnOverCardCommand;
            }
        }
        void TurnOverCard(object parameter)
        {
            if (turnedCards.Count > 0 && (parameter as Card) == turnedCards[turnedCards.Count - 1])
                return;

            (parameter as Card).ShowFront = !(parameter as Card).ShowFront;
            NotifyPropertyChanged(nameof(Cards));
    

            turnedCards.Add((parameter as Card));
            if (turnedCards.Count == 2)
            {

                if (turnedCards[0] != turnedCards[1] && turnedCards[0].ImagePath == turnedCards[1].ImagePath)
                {
                    turnedCards[0].NotMatched = false;
                    turnedCards[1].NotMatched = false;

                    MatchedCards += 2;
                    turnedCards.Clear();
                }
            }
            else if (turnedCards.Count == 3)
            {
                turnedCards[0].ShowFront = false;
                turnedCards[1].ShowFront = false;

                Card lastCard = turnedCards[2];
                turnedCards.Clear();
                turnedCards.Add(lastCard);
                
            }
            NotifyPropertyChanged(nameof(Card.ImagePath));
            NotifyPropertyChanged("ImagePath");
        }
        public GameVM(GameCategory category, byte boardRows, byte boardColumns, User currentUser, int delay, byte matchedCards, ObservableCollection<ObservableCollection<Card>> cards = null)
        {
            Category = category;
            BoardRows = boardRows;
            BoardColumns = boardColumns;
            MatchedCards = matchedCards;

            if (cards != null)
            {
                Cards = cards;
            }
            else
            {
                Cards = new ObservableCollection<ObservableCollection<Card>>();
                services = new GameServices(Cards, boardRows, boardColumns, Category);
                services.InitializeCards();
            }
            CurrentUser = currentUser;
            TotalTime = delay;
            NotifyPropertyChanged(nameof(Cards));
        }
        public GameVM()
        {
            Category = GameCategory.Flowers;
            BoardRows = 4;
            BoardColumns = 4;
            Cards = new ObservableCollection<ObservableCollection<Card>>();

            services = new GameServices(Cards, BoardRows, BoardColumns, Category);
            services.InitializeCards();

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
