using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PhoneBook.ViewModels
{
    /// <summary>
    /// БАЗОВЫЙ КЛАСС для ViewModel.
    /// Реализует интерфейс INotifyPropertyChanged,
    /// который необходим для механизма Data Binding в WPF.
    /// При изменении свойства View автоматически обновляется.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged для уведомления View
        /// об изменении значения свойства.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Вспомогательный метод для установки значения свойства
        /// и вызова уведомления только если значение изменилось.
        /// </summary>
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
