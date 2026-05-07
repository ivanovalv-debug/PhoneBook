using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Services;
using PhoneBook.ViewModels;

namespace PhoneBook
{
    /// <summary>
    /// Точка входа приложения.
    /// Настраивает Dependency Injection контейнер.
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 1. Создаём коллекцию сервисов
            var services = new ServiceCollection();

            // 2. Регистрируем сервисы
            // DialogService — Singleton (один экземпляр на всё приложение)
            services.AddSingleton<IDialogService, DialogService>();

            // ViewModel — Transient (новый экземпляр при каждом запросе)
            services.AddTransient<MainViewModel>();

            // MainWindow — Singleton с явной установкой DataContext
            services.AddSingleton<MainWindow>(sp =>
            {
                var window = new MainWindow();
                window.DataContext = sp.GetRequiredService<MainViewModel>();
                return window;
            });

            // 3. Создаём IoC-контейнер
            var serviceProvider = services.BuildServiceProvider();

            // 4. Получаем и показываем главное окно
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
