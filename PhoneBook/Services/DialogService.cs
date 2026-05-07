using System.Windows;
using PhoneBook.Views;

namespace PhoneBook.Services
{
    /// <summary>
    /// Реализация сервиса диалоговых окон с красивым UI.
    /// Использует кастомное окно CustomDialog вместо стандартного MessageBox.
    /// </summary>
    public class DialogService : IDialogService
    {
        public void ShowInfo(string message, string title = "Информация")
        {
            var dialog = new CustomDialog(message, title, CustomDialog.DialogType.Info);
            dialog.Owner = Application.Current.MainWindow;
            dialog.ShowDialog();
        }

        public void ShowWarning(string message, string title = "Предупреждение")
        {
            var dialog = new CustomDialog(message, title, CustomDialog.DialogType.Warning);
            dialog.Owner = Application.Current.MainWindow;
            dialog.ShowDialog();
        }

        public void ShowError(string message, string title = "Ошибка")
        {
            var dialog = new CustomDialog(message, title, CustomDialog.DialogType.Error);
            dialog.Owner = Application.Current.MainWindow;
            dialog.ShowDialog();
        }

        public bool ShowConfirmation(string message, string title = "Подтверждение")
        {
            var dialog = new CustomDialog(message, title, CustomDialog.DialogType.Confirmation, showCancel: true);
            dialog.Owner = Application.Current.MainWindow;
            var result = dialog.ShowDialog();
            return result == true;
        }
    }
}