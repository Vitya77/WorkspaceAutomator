using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorkspaceAutomator
{
    /// <summary>
    /// Логика взаимодействия для NewWorkspaceWindow.xaml
    /// </summary>
    public partial class NewWorkspaceWindow : Window
    {
        public NewWorkspaceWindow()
        {
            InitializeComponent();
            DataContext = new NewWorkspaceViewModel();
        }

        private void ChooseFile(object sender, RoutedEventArgs e)
        {
            // Створюємо нове вікно вибору файлу
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Оберіть файл",
                Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*", // Фільтр для відображення лише потрібних файлів
                Multiselect = false // Дозволити обирати лише один файл
            };

            // Якщо користувач обрав файл і натиснув "OK"
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName; // Отримуємо шлях до обраного файлу
                MessageBox.Show($"Ви обрали: {selectedFilePath}", "Файл обрано", MessageBoxButton.OK, MessageBoxImage.Information);

                // Додайте логіку, щоб зберегти шлях до файлу або використати його
            }
        }
    }
}
