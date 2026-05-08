namespace PhoneBook.ViewModels
{
    public class AboutViewModel : ObservableObject
    {
        public string AppName => "Телефонная книга MVVM";
        public string Version => "Версия 3.0 (Shell + Навигация)";
        public string Description => "Приложение демонстрирует архитектуру Shell с навигацией ViewModel-First";
    }
}