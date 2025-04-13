using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Tema2_MemoryGame.Model;
using Tema2_MemoryGame.ViewModel;

namespace Tema2_MemoryGame.Services
{
    internal class SerializationActions
    {
        public ObservableCollection<User> users;
        public string FilePath;
        public SerializationActions(string filePath, ObservableCollection<User> users = null)
        {
            this.users = users;
            FilePath = filePath;
        }
        public void SerializeObject(ObservableCollection<User> entity)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<User>));
            FileStream file = new FileStream("users.xml", FileMode.Create);
            xmlSerializer.Serialize(file, entity);
            file.Dispose();
        }
        public void SerializeObject(User entity, string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
            FileStream file = new FileStream(filePath, FileMode.Create);
            xmlSerializer.Serialize(file, entity);
            file.Dispose();
        }
        public void DeserializeObject(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return;
            string[] userFiles = Directory.GetFiles(directoryPath);
            if(users!= null) users.Clear();

            foreach (var filePath in userFiles)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
                using (FileStream file = new FileStream(filePath, FileMode.Open))
                {
                    if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
                        continue;

                    User user = (User)xmlSerializer.Deserialize(file);
                    users.Add(user);
                }
            }
        }
        public void SerializeGameInfo(GameInfo game, string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameInfo));
            using (FileStream file = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(file, game);
            }
        }
        public GameInfo DeserializeGameInfo(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameInfo));
            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
                return (GameInfo)xmlSerializer.Deserialize(file);
            }
        }
    }
}
