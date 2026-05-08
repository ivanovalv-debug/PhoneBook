using System.Windows.Controls;  
using PhoneBook.ViewModels;
using System.Windows;

namespace PhoneBook.Views
{
    /// <summary>
    /// VIEW (Представление) - главное окно приложения.
    /// Отвечает только за отображение интерфейса.
    /// Вся бизнес-логика находится в ViewModel.
    /// В MVVM code-behind должен быть минимальным.
    /// </summary>
    public partial class ContactsListView : UserControl
    {
        public ContactsListView()
        {
            InitializeComponent();      
        }
    }
}