using System.Windows.Input;
using PhoneBook.Services;

namespace PhoneBook.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        public INavigationService NavigationService { get; }
        public ICommand ShowContactsCommand { get; }
        public ICommand ShowAboutCommand { get; }

        public MainWindowViewModel(INavigationService navigation)
        {
            NavigationService = navigation;
            ShowContactsCommand = new RelayCommand(() => navigation.NavigateTo<ContactsListViewModel>());
            ShowAboutCommand = new RelayCommand(() => navigation.NavigateTo<AboutViewModel>());

            // Стартовый экран
            navigation.NavigateTo<ContactsListViewModel>();
        }
    }
}