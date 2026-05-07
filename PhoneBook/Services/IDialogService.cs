namespace PhoneBook.Services
{
    /// <summary>
    /// Интерфейс сервиса диалоговых окон.
    /// Абстрагирует взаимодействие с пользователем для устранения
    /// жёсткой связности ViewModel с UI-библиотеками WPF.
    /// Позволяет тестировать ViewModel без показа реальных окон.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Показывает информационное сообщение.
        /// </summary>
        void ShowInfo(string message, string title = "Информация");

        /// <summary>
        /// Показывает предупреждение.
        /// </summary>
        void ShowWarning(string message, string title = "Предупреждение");

        /// <summary>
        /// Показывает сообщение об ошибке.
        /// </summary>
        void ShowError(string message, string title = "Ошибка");

        /// <summary>
        /// Запрашивает подтверждение действия (Да/Нет).
        /// </summary>
        /// <returns>True, если пользователь нажал "Да"</returns>
        bool ShowConfirmation(string message, string title = "Подтверждение");
    }
}