using System.Windows;
using PhoneBook.ViewModels;

namespace PhoneBook
{
    /// <summary>
    /// VIEW (Представление) - главное окно приложения.
    /// Отвечает только за отображение интерфейса.
    /// Вся бизнес-логика находится в ViewModel.
    /// В MVVM code-behind должен быть минимальным.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Установка DataContext для привязки данных
            DataContext = new MainViewModel();
        }
    }
}