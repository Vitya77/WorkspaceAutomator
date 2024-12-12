using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Interop;
using WorkspaceManagerWPF.Models;
using System.Linq;
using System;
using System.ComponentModel;



public class NewWorkspaceViewModel : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string _environmentName;
    public string EnvironmentName
    {
        get => _environmentName;
        set
        {
            _environmentName = value;
            OnPropertyChanged(nameof(EnvironmentName)); // Якщо реалізовано INotifyPropertyChanged
        }
    }

    public ObservableCollection<Models> SelectedFiles { get; set; } = new ObservableCollection<Models>();

    public ICommand AddFileCommand { get; }
    public ICommand RemoveFileCommand { get; }

    public ICommand CreateEnvironmentCommand { get; }

    public NewWorkspaceViewModel()
    {
        // Оновлений тип команди для підтримки параметрів
        AddFileCommand = new RelayCommand<object>(AddFile);
        RemoveFileCommand = new RelayCommand<Models>(RemoveFile);
        CreateEnvironmentCommand = new RelayCommand<object>(obj => createEnvironment());
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
            //var icon = Icon.ExtractAssociatedIcon(openFileDialog.FileName);
            SelectedFiles.Add(new Models
            {
                Path = openFileDialog.FileName,
                Name = System.IO.Path.GetFileName(openFileDialog.FileName),
                //Icon = Imaging.CreateBitmapSourceFromHIcon(
                //    icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())
            });
        }
    }

    private void RemoveFile(Models file)
    {
        SelectedFiles.Remove(file);
    }

    private void createEnvironment()
    {
        if (string.IsNullOrWhiteSpace(EnvironmentName))
        {
            MessageBox.Show("Please enter a valid environment name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (SelectedFiles.Count == 0)
        {
            MessageBox.Show("Please select at least one application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // Формування списку програм
        var programs = SelectedFiles.Select(f => new Program
        {
            Name = f.Name,
            FilePath = f.Path
        }).ToList();

        try
        {
            // Виклик методу ProgramManager для додавання робочого простору
            var programManager = new ProgramManager(new AppDbContext());
            programManager.AddWorkspace(EnvironmentName, programs);

            MessageBox.Show("Environment created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Очистка після успішного збереження
            EnvironmentName = string.Empty;
            SelectedFiles.Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


}
