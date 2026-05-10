using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PhoneBook.Models;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    public class ContactsListViewModel : ObservableObject
    {
        private readonly IDialogService _dialog;
        private readonly INavigationService _navigation;

        private ObservableCollection<Contact> _contacts = new();
        private string _name = string.Empty;
        private string _phone = string.Empty;
        private Contact? _selectedContact;

        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set => Set(ref _contacts, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        public Contact? SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public ContactsListViewModel(IDialogService dialog, INavigationService navigation)
        {
            _dialog = dialog;
            _navigation = navigation;

            AddCommand = new RelayCommand(Add, CanAdd);
            DeleteCommand = new RelayCommand<object?>(Delete, p => p is Contact);
            EditCommand = new RelayCommand<object?>(
                p => { if (p is Contact c) _navigation.NavigateTo<ContactEditViewModel>(c); },
                p => p is Contact);
        }

        private void Add()
        {
            if (!Contact.Validate(Name, Phone))
            {
                _dialog.ShowError(
                    "Некорректные данные контакта.\n\n" +
                    "• Имя не должно быть пустым\n" +
                    "• Телефон должен содержать 10 или 11 цифр\n" +
                    "• Телефон может начинаться с 7 или 8",
                    "Ошибка валидации");
                return;
            }

            if (Contact.IsDuplicate(Phone, Contacts))
            {
                _dialog.ShowWarning("Контакт с таким номером уже существует!");
                return;
            }

            Contacts.Add(new Contact(Name, Phone));
            Name = string.Empty;
            Phone = string.Empty;
            _dialog.ShowInfo("Контакт успешно добавлен!");
        }

        private bool CanAdd() =>
            !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Phone);

        private void Delete(object? p)
        {
            if (p is Contact c && _dialog.ShowConfirmation($"Удалить {c.Name}?"))
            {
                Contacts.Remove(c);
                _dialog.ShowInfo("Удалено");
            }
        }
    }
}