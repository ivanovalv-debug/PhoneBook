using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.ViewModels;

namespace PhoneBook.Models
{
    /// <summary>
    /// MODEL (Модель) - представляет данные контакта.
    /// Отвечает за хранение бизнес-данных, их валидацию и нормализацию.
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
        /// Имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <summary>
        /// Номер телефона.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        /// <summary>
        /// Приводит номер к единому формату для сравнения.
        /// Убирает разделители и заменяет 8 на 7 в начале.
        /// </summary>
        public static string NormalizePhone(string phone)
        {
            string cleaned = phone.Replace(" ", "")
                                  .Replace("-", "")
                                  .Replace("(", "")
                                  .Replace(")", "")
                                  .Replace("+", "")
                                  .Trim();

            // Нормализация кода страны: 8 → 7
            if (cleaned.StartsWith("8"))
            {
                cleaned = "7" + cleaned.Substring(1);
            }

            return cleaned;
        }

        /// <summary>
        /// Проверяет, является ли номер дубликатом среди существующих контактов.
        /// </summary>
        public static bool IsDuplicate(string phone, IEnumerable<Contact> existingContacts, Contact? excludeContact = null)
        {
            string normalizedPhone = NormalizePhone(phone);

            return existingContacts.Any(c =>
                c != excludeContact && NormalizePhone(c.Phone) == normalizedPhone);
        }

        /// <summary>
        /// Валидация данных: имя не пустое, телефон соответствует формату.
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
                return cleaned[0] != '7' && cleaned[0] != '8';

            return false;
        }
    }
}