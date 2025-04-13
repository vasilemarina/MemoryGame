using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema2_MemoryGame.Model;

namespace Tema2_MemoryGame.Services
{
    internal class UsersServices
    {
        ObservableCollection<User> users;
        public ObservableCollection<User> Users { get { return users; } }
        private SerializationActions serializer;
        public UsersServices(ObservableCollection<User> users)
        {
            this.users = users;
            serializer = new SerializationActions("users.xml", users);
            string filePath = Path.Combine(AppContext.BaseDirectory, "users");
            serializer.DeserializeObject(filePath);
        }
        public void AddUser(string username, string profilePicturePath)
        {
            User newUser = new User(username, profilePicturePath);
            users.Add(newUser);
            serializer.SerializeObject(users);

            CreateUserFile(newUser);
        }
        public void DeleteUser(User user)
        {
            users.Remove(user);
            serializer.SerializeObject(users);
            string fileName = Path.Combine(AppContext.BaseDirectory, $"users\\{user.Username}.xml");
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
        private void CreateUserFile(User newUser)
        {
            string fileName = Path.Combine(AppContext.BaseDirectory, $"users\\{newUser.Username}.xml");
            serializer.SerializeObject(newUser, fileName);
        }
        public void UpdateUser(User updatedUser)
        {
            User existingUser = users.FirstOrDefault(u => u.Username == updatedUser.Username);

            if (existingUser != null)
            {
               
                existingUser.Username = updatedUser.Username;
                existingUser.ProfilePicturePath = updatedUser.ProfilePicturePath;

                serializer.SerializeObject(users);

                string userFilePath = Path.Combine(AppContext.BaseDirectory, $"users\\{ updatedUser.Username}.xml");
                serializer.SerializeObject(updatedUser, userFilePath);
            }

        }
        public void RemovePastUsersGames()
        {
            string savedGamesFolder = Path.Combine(AppContext.BaseDirectory, "saved_games");
            if (!Directory.Exists(savedGamesFolder))
                return;

            var savedGameFiles = Directory.GetFiles(savedGamesFolder, "saved_game_*.xml");

            foreach (var file in savedGameFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                string[] parts = fileName.Split('_');
                if (parts.Length > 0)
                {
                    string usernameFromFile = parts[0];
                    bool deletedUser = true;
                    foreach(User user in users)
                    if (parts[2] == user.Username)
                    {
                            deletedUser = false;
                        
                    }
                    if(deletedUser)
                        File.Delete(file);
                }
            }
        }
    }
}
