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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkspaceManagerWPF.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WorkspaceName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void createWorkSpaceWindow(object sender, RoutedEventArgs e)
        {
            // Створюємо екземпляр нового вікна
            NewWorkspaceWindow newWorkspaceWindow = new NewWorkspaceWindow();

            // Відкриваємо нове вікно
            newWorkspaceWindow.Show(); // Для модального вікна
            // або використовуйте newWorkspaceWindow.Show(); якщо потрібне немодальне вікно
        }

    }
}
