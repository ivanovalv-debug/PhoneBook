using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhoneBook.Models;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    /// <summary>
    /// ViewModel приложения "Телефонная книга".
    /// Использует Dependency Injection для получения IDialogService.
    /// Это устраняет жёсткую связность с UI и позволяет тестировать ViewModel.
    /// </summary>
    public class ContactsListViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Коллекция контактов для отображения в DataGrid.
        /// ObservableCollection автоматически обновляет UI.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; }

        private string _name = string.Empty;
        private string _phone = string.Empty;
        private Contact? _selectedContact;

        /// <summary>
        /// Свойство для привязки к полю ввода имени.
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <summary>
        /// Свойство для привязки к полю ввода телефона.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        /// <summary>
        /// Выбранный в DataGrid контакт (для удаления).
        /// </summary>
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        /// <summary>
        /// Команда добавления контакта (без параметра).
        /// </summary>
        public ICommand AddCommand { get; }

        /// <summary>
        /// Команда удаления контакта (с параметром).
        /// </summary>
        public ICommand DeleteCommand { get; }

        /// <summary>
        /// Constructor Injection: IoC-контейнер автоматически
        /// передаёт реализацию IDialogService.
        /// </summary>
        public ContactsListViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand<object?>(DeleteContact, CanDeleteContact);
        }

        /// <summary>
        /// Логика добавления нового контакта.
        /// Проверяет дубликаты по номеру телефона.
        /// </summary>
        private void AddContact()
        {
            // Проверка на дубликат по номеру телефона
            if (Contacts.Any(c => c.Phone == Phone.Trim()))
            {
                _dialogService.ShowWarning(
                    "Контакт с таким номером телефона уже существует!",
                    "Дубликат");
                return;
            }

            // Валидация и создание контакта
            if (Contact.Validate(Name, Phone))
            {
                var contact = new Contact(Name, Phone);
                Contacts.Add(contact);

                // Очистка полей ввода
                Name = string.Empty;
                Phone = string.Empty;

                // Информационное сообщение об успехе
                _dialogService.ShowInfo(
                    $"Контакт \"{contact.Name}\" успешно добавлен!",
                    "Добавление контакта");
            }
            else
            {
                _dialogService.ShowError(
                    "Некорректные данные контакта. Проверьте имя и номер телефона.",
                    "Ошибка валидации");
            }
        }

        /// <summary>
        /// Проверка доступности команды добавления.
        /// </summary>
        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);
        }

        /// <summary>
        /// Логика удаления контакта с подтверждением.
        /// </summary>
        private void DeleteContact(object? parameter)
        {
            if (parameter is Contact contact)
            {
                // Запрос подтверждения удаления
                bool confirmed = _dialogService.ShowConfirmation(
                    $"Вы действительно хотите удалить контакт \"{contact.Name}\"?",
                    "Удаление контакта");

                if (confirmed)
                {
                    Contacts.Remove(contact);
                    _dialogService.ShowInfo(
                        "Контакт успешно удалён.",
                        "Удаление");
                }
            }
        }

        /// <summary>
        /// Проверка доступности команды удаления.
        /// </summary>
        private bool CanDeleteContact(object? parameter)
        {
            return parameter is Contact;
        }
    }
}