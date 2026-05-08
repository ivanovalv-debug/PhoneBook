using System.Windows;

namespace PhoneBook 
{
    /// <summary>
    /// Главное окно-оболочка (Shell) приложения.
    /// Содержит меню навигации и ContentControl для динамической подгрузки экранов.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // DataContext устанавливается из DI-контейнера в App.xaml.cs
        }
    }
}
