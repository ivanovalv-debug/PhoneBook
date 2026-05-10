using System.Windows.Input;
using PhoneBook.Models;      
using PhoneBook.Services;    

namespace PhoneBook.ViewModels
{
    public class ContactEditViewModel : ObservableObject, INavigationAware
    {
        private readonly INavigationService _navigation;
        private readonly IDialogService _dialogService;
        private readonly ContactsListViewModel _contactsListViewModel;
        private Contact _contact = null!;

        public string EditName
        {
            get => _contact.Name;
            set
            {
                _contact.Name = value;
                OnPropertyChanged();
            }
        }

        public string EditPhone
        {
            get => _contact.Phone;
            set
            {
                _contact.Phone = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ContactEditViewModel(
            INavigationService navigation,
            IDialogService dialogService,
            ContactsListViewModel contactsListViewModel)
        {
            _navigation = navigation;
            _dialogService = dialogService;
            _contactsListViewModel = contactsListViewModel;

            SaveCommand = new RelayCommand(SaveContact);
            CancelCommand = new RelayCommand(() => _navigation.NavigateTo<ContactsListViewModel>());
        }

        private void SaveContact()
        {
            // 1. Валидация (логика в модели)
            if (!Contact.Validate(EditName, EditPhone))
            {
                _dialogService.ShowError(
                    "Некорректные данные контакта.\n\n" +
                    "• Имя не должно быть пустым\n" +
                    "• Телефон должен содержать 10 или 11 цифр\n" +
                    "• Телефон может начинаться с 7 или 8",
                    "Ошибка валидации");
                return;
            }

            // 2. Проверка на дубликаты (логика в модели, исключаем текущий контакт)
            if (Contact.IsDuplicate(EditPhone, _contactsListViewModel.Contacts, _contact))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует!");
                return;
            }

            // 3. Сохранение (данные уже обновлены через свойства)
            _navigation.NavigateTo<ContactsListViewModel>();
        }

        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is Contact c)
            {
                _contact = c;
            }
        }
    }
}