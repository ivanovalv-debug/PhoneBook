using System.Collections.ObjectModel;
using System.Windows.Input;
using PhoneBook.Models;

namespace PhoneBook.ViewModels
{
    /// <summary>
    /// ViewModel приложения "Телефонная книга".
    /// Посредник между View и Model.
    /// </summary>
    public class MainViewModel : ObservableObject
    {
        // Коллекция контактов (автоматически обновляет UI при добавлении/удалении)
        public ObservableCollection<Contact> Contacts { get; }

        private string _name = string.Empty;
        private string _phone = string.Empty;
        private Contact? _selectedContact;

        // Свойство для привязки к полю ввода имени
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        // Свойство для привязки к полю ввода телефона
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        // Свойство для хранения выбранного в таблице контакта
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        // Команда добавления (без параметра)
        public ICommand AddCommand { get; }

        // Команда удаления (с параметром - принимаем объект контакта)
        public ICommand DeleteCommand { get; }

        public MainViewModel()
        {
            Contacts = new ObservableCollection<Contact>();

            // Инициализация команд
            AddCommand = new RelayCommand(AddContact, CanAddContact);

            // ИСПОЛЬЗУЕМ RelayCommand<object?> для передачи параметра (выбранного контакта)
            DeleteCommand = new RelayCommand<object?>(DeleteContact, CanDeleteContact);
        }

        // Логика добавления
        private void AddContact()
        {
            if (Contact.Validate(Name, Phone))
            {
                Contacts.Add(new Contact(Name, Phone));
                // Очистка полей после успешного добавления
                Name = string.Empty;
                Phone = string.Empty;
            }
        }

        // Проверка доступности кнопки "Добавить"
        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }

        // Логика удаления (ПРИНИМАЕТ ПАРАМЕТР)
        private void DeleteContact(object? parameter)
        {
            // Приводим параметр к типу Contact
            if (parameter is Contact contact)
            {
                Contacts.Remove(contact);
            }
        }

        // Проверка доступности кнопки "Удалить"
        private bool CanDeleteContact(object? parameter)
        {
            // Кнопка активна только если передан контакт (т.е. что-то выбрано в таблице)
            return parameter is Contact;
        }
    }
}