using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Tema2_MemoryGame.Commands;
using Tema2_MemoryGame.Model;
using Tema2_MemoryGame.Services;

namespace Tema2_MemoryGame.ViewModel
{
    public class SignInVM : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }
        private User selectedUser;
        public User SelectedUser 
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                NotifyPropertyChanged(nameof(SelectedUser));
            }
        }
        public string NewUsername { get; set; }
        public Visibility CreateNewUserVisibility 
        { 
            get => createNewUserOn ? Visibility.Visible : Visibility.Collapsed;
        }
        public Visibility SelectedUserVisibility
        {
            get => createNewUserOn ? Visibility.Collapsed : Visibility.Visible;
        }
        public bool CreateNewUserOn
        {
            get => createNewUserOn;
            set
            {
                createNewUserOn = value;
                NotifyPropertyChanged(nameof(CreateNewUserVisibility));
                NotifyPropertyChanged(nameof(SelectedUserVisibility));
                NotifyPropertyChanged(nameof(SelectUserOn));
            }
        }
        public bool SelectUserOn
        {
            get => !createNewUserOn;
        }

        private UsersServices usersServices;
        private bool createNewUserOn;
        public string CurrentProfileImagePath
        {
            get
            {
                return ProfileImagesPaths[CurrentProfileImageIndex];
            }
        }
        private int currentProfileImageIndex;
        private int CurrentProfileImageIndex
        {
            get
            {
                return currentProfileImageIndex;
            }
            set
            {
                currentProfileImageIndex = value;
                NotifyPropertyChanged(nameof(CurrentProfileImagePath));
            }
        }
        public string ProfileImagesDirectoryPath;
        public List<string> ProfileImagesPaths { get; set; }
        public SignInVM()
        {
            Users = new ObservableCollection<User>();
            usersServices = new UsersServices(Users);

            createNewUserOn = false;

            ProfileImagesDirectoryPath = Path.Combine(AppContext.BaseDirectory, "user_images");
            ProfileImagesPaths = new List<string>();
            ImageServices.LoadImages(ProfileImagesPaths, ProfileImagesDirectoryPath);
            CurrentProfileImageIndex = 0;

            //usersServices.RemovePastUsersGames();
        }

        ICommand createUserCommand;
        public ICommand CreateUserCommand
        {
            get
            {
                createUserCommand ??= new RelayCommand(TurnOnCreateUser);
                return createUserCommand;
            }
        }
        private void TurnOnCreateUser(object parameter)
        {
            CreateNewUserOn = true;
        }
        ICommand cancelCreateUserCommand;
        public ICommand CancelCreateUserCommand
        {
            get
            {
                cancelCreateUserCommand ??= new RelayCommand(TurnOffCreateUser);
                return cancelCreateUserCommand;
            }
        }
        private void TurnOffCreateUser(object parameter)
        {
            CreateNewUserOn = false;
        }
        ICommand addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                addUserCommand ??= new RelayCommand(AddUser);
                return addUserCommand;
            }
        }
        void AddUser(object parameter)
        {
            usersServices.AddUser(NewUsername, CurrentProfileImagePath);
            CreateNewUserOn = false;
        }
        ICommand deleteUserCommand;
        public ICommand DeleteUserCommand
        {
            get
            {
                deleteUserCommand ??= new RelayCommand(DeleteUser, _ => SelectedUser != null && SelectUserOn);
                return deleteUserCommand;
            }
        }
        void DeleteUser(object parameter)
        {
            usersServices.DeleteUser(SelectedUser);
        }

        public event EventHandler PlayButtonClicked;
        ICommand startGameCommand;
        public ICommand StartGameCommand
        {
            get
            {
                startGameCommand ??= new RelayCommand(StartGame, _ => SelectedUser != null && SelectUserOn);
                return startGameCommand;
            }
        }
        void StartGame(object parameter)
        {
            PlayButtonClicked?.Invoke(this, EventArgs.Empty);
        }
        ICommand exitGameCommand;
        public ICommand ExitGameCommand
        {
            get
            {
                exitGameCommand ??= new RelayCommand(ExitGame);
                return exitGameCommand;
            }
        }
        void ExitGame(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }

        ICommand previousImageCommand;
        public ICommand PreviousImageCommand
        {
            get
            {
                previousImageCommand ??= new RelayCommand(GetPreviousPicture);
                return previousImageCommand;
            }
        }
        private void GetPreviousPicture(object parameter)
        {
            CurrentProfileImageIndex = CurrentProfileImageIndex == 0 ? ProfileImagesPaths.Count - 1 : CurrentProfileImageIndex - 1;
        }

        ICommand nextImageCommand;
        public ICommand NextImageCommand
        {
            get
            {
                nextImageCommand ??= new RelayCommand(GetNextPicture);
                return nextImageCommand;
            }
        }
        private void GetNextPicture(object parameter)
        {
            CurrentProfileImageIndex = (CurrentProfileImageIndex + 1) % ProfileImagesPaths.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
