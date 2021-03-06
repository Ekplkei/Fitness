using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.BL.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Serializable]public class User
    {
        #region Свойства
        public string Name { get; }

        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Вес
        /// </summary>
        public double Weight { get; set; } //Вес

        /// <summary>
        /// Рост
        /// </summary>
        public double Height { get; set; }

        public int Age { get { return yearBirth(); } }
        #endregion
        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="name">Имя.</param>
        /// <param name="gender">Пол.</param>
        /// <param name="birthDate">Дата рождения.</param>
        /// <param name="weight">Вес.</param>
        /// <param name="height">Рост.</param>
        public User(string name,
                    Gender gender,
                    DateTime birthDate,
                    double weight,
                    double height)
        {
            #region Проверка условий
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Имя пользователя не может быть пустым или null", nameof(name));
            if (gender == null) throw new ArgumentNullException("Пол не может быть null", nameof(gender));
            if (birthDate<DateTime.Parse("01.01.1900") || birthDate > DateTime.Now) throw new ArgumentException("Невозможная дата рождения", nameof(birthDate));
            if (weight<=0) throw new ArgumentException("Невозможный вес (<=0)", nameof(weight));
            if (height<=0) throw new ArgumentException("Невозможный рост (<=0)", nameof(height));
            #endregion
            Name = name;
            Gender = gender;
            BirthDate = birthDate;
            Weight = weight;
            Height = height;
        }
        public int yearBirth()
        {
            if (DateTime.Now.Month < BirthDate.Month || (DateTime.Now.Month == BirthDate.Month && (DateTime.Now.Day < BirthDate.Day))) return DateTime.Now.Year - BirthDate.Year - 1;
            else return DateTime.Now.Year - BirthDate.Year;
        }
        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Имя пользователя не может быть пустым или null", nameof(name));
            Name = name;
        }
        public override string ToString()
        {
            return Name + " " + Age;
        }
    }
}
