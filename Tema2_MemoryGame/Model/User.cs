using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tema2_MemoryGame.Model
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string ProfilePicturePath { get; set; }

        public int PlayedGames { get; set; }
        public int WonGames { get; set; }
        public User()
        {
            Username = "";
            ProfilePicturePath = "";
        }
        public User(string username, string profilePicturePath)
        {
            Username = username;
            ProfilePicturePath = profilePicturePath;
        }
    }
}
