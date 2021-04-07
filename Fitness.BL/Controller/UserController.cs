using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.BL.Model;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Fitness.BL.Controller
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    public class UserController
    {
        /// <summary>
        /// Пользователь приложения.
        /// </summary>
        public List<User> Users { get; }
        public User CurrentUser { get; }
        public bool IsNewUser { get; } = false;
        /// <summary>
        /// Создание нового контроллера пользователя
        /// </summary>
        /// <param name="user"></param>
        public UserController(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(userName));

            Users = GetUsersData();
            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);

            if(CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
        }

        /// <summary>
        /// Получить сохранённый список пользователей
        /// </summary>
        /// <returns></returns>
        private List<User> GetUsersData()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("User.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length> 0 && formatter.Deserialize(fs) is List<User> users)
                {
                    return users;
                }
                else
                {
                    return new List<User>();
                }
            }
        }
        public void SetNewUserData(string genderName, DateTime birthdDate, double weight = 1, double height = 1)
        {
            // Проверка
            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthdDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
            var g = genderName;
        }
        private void Save()
        {
            var formatter = new BinaryFormatter();
            using( var fs = new FileStream("User.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Users);
            }
        }
    }
}
