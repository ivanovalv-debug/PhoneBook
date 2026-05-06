
using PhoneBook.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneBook.Models
{
    /// <summary>
    /// MODEL (Модель) - представляет данные контакта.
    /// Отвечает за хранение бизнес-данных и их валидацию.
    /// Не зависит от View и ViewModel.
    /// </summary>
    public class Contact : ObservableObject
    {
        private string _name = string.Empty;
        private string _phone = string.Empty;

        public Contact(string name, string phone)
        {
            if (!Validate(name, phone))
                throw new ArgumentException("Некорректные данные контакта");

            _name = name.Trim();
            _phone = phone.Trim();
        }

        /// <summary>
        /// Имя контакта. Изменение свойства автоматически
        /// уведомляет View через INotifyPropertyChanged.
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <summary>
        /// Номер телефона. Изменение свойства автоматически
        /// уведомляет View через INotifyPropertyChanged.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        /// <summary>
        /// Валидация данных: имя не пустое, 
        /// телефон соответствует формату.
        /// </summary>
        public static bool Validate(string name, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            string cleaned = phone.Replace(" ", "")
                                  .Replace("-", "")
                                  .Replace("(", "")
                                  .Replace(")", "");

            if (string.IsNullOrEmpty(cleaned))
                return false;

            if (cleaned.StartsWith("+"))
                cleaned = cleaned.Substring(1);

            foreach (char c in cleaned)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            int length = cleaned.Length;

            if (length == 11)
                return cleaned[0] == '7' || cleaned[0] == '8';
            else if (length == 10)
                return true;

            return false;
        }
    }
}