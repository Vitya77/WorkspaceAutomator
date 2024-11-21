using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Interop;
using WorkspaceAutomator.Models;



public class NewWorkspaceViewModel
{
    public ObservableCollection<FileModel> SelectedFiles { get; set; } = new ObservableCollection<FileModel>();

    public ICommand AddFileCommand { get; }
    public ICommand RemoveFileCommand { get; }

    public NewWorkspaceViewModel()
    {
        // Оновлений тип команди для підтримки параметрів
        AddFileCommand = new RelayCommand<object>(AddFile);
        RemoveFileCommand = new RelayCommand<FileModel>(RemoveFile);
    }

    private void AddFile(object obj)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*",
            Multiselect = false
        };

        if (openFileDialog.ShowDialog() == true)
        {
            var icon = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
            SelectedFiles.Add(new FileModel
            {
                Path = openFileDialog.FileName,
                Name = System.IO.Path.GetFileName(openFileDialog.FileName),
                Icon = Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            });
        }
    }

    private void RemoveFile(FileModel file)
    {
        SelectedFiles.Remove(file);
    }
}
