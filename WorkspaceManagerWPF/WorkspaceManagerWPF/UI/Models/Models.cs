using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WorkspaceManagerWPF.Models
{
    public class Models
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public ImageSource Icon { get; set; }
    }

    public class Programs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
    }


    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<EnvironmentApplication> EnvironmentApplications { get; set; }
    }


    public class Application
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public ICollection<EnvironmentApplication> EnvironmentApplications { get; set; }
    }

    public class EnvironmentApplication
    {
        public int EnvironmentId { get; set; }
        public Environment Environment { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
