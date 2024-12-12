using System.Linq;
using System.Windows;

namespace WorkspaceManagerWPF
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _dbContext;
        private readonly ProgramManager _programManager;

        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new AppDbContext();
            _programManager = new ProgramManager(_dbContext);
            _dbContext.Database.EnsureCreated();
            LoadWorkspaces();
        }

        private void LoadWorkspaces()
        {
            WorkspaceComboBox.ItemsSource = _programManager.GetAllWorkspaces().Select(workSpace => workSpace.Name).ToList();
            RunWorkspaceComboBox.ItemsSource = _programManager.GetAllWorkspaces().Select(workSpace => workSpace.Name).ToList();
        }

        private void OnAddWorkspaceClick(object sender, RoutedEventArgs e)
        {
            string workspaceName = WorkspaceNameTextBox.Text;
            if (!string.IsNullOrEmpty(workspaceName))
            {
                _programManager.AddWorkspace(workspaceName);
                LoadWorkspaces();
            }
        }

        private void OnAddProgramClick(object sender, RoutedEventArgs e)
        {
            if (WorkspaceComboBox.SelectedItem is Workspace selectedWorkspace)
            {
                string programName = ProgramNameTextBox.Text;
                string programPath = ProgramPathTextBox.Text;
                if (!string.IsNullOrEmpty(programName) && !string.IsNullOrEmpty(programPath))
                {
                    _programManager.AddProgramToWorkspace(selectedWorkspace.Id, programName, programPath);
                }
            }
        }

        private void OnRunWorkspaceClick(object sender, RoutedEventArgs e)
        {
            if (RunWorkspaceComboBox.SelectedItem is Workspace selectedWorkspace)
            {
                _programManager.RunWorkspace(selectedWorkspace.Id);
            }
        }
    }
}
