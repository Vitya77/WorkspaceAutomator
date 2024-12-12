using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

public class ProgramManager
{
    private readonly AppDbContext _context;

    public ProgramManager(AppDbContext context)
    {
        _context = context;
    }

    public void AddWorkspace(string workspaceName)
    {
        var workspace = new Workspace { Name = workspaceName, Programs = new List<Program>() };
        _context.Workspaces.Add(workspace);
        _context.SaveChanges();
    }

    public void AddProgramToWorkspace(int workspaceId, string programName, string filePath)
    {
        var workspace = _context.Workspaces.Include(w => w.Programs).FirstOrDefault(w => w.Id == workspaceId);
        if (workspace != null)
        {
            var program = new Program { Name = programName, FilePath = filePath };
            workspace.Programs.Add(program);
            _context.SaveChanges();
        }
    }

    public void RunWorkspace(int workspaceId)
    {
        var workspace = _context.Workspaces.Include(w => w.Programs).FirstOrDefault(w => w.Id == workspaceId);
        if (workspace != null)
        {
            foreach (var program in workspace.Programs)
            {
                System.Diagnostics.Process.Start(program.FilePath); // Запуск програми
            }
        }
    }

    public IQueryable<Workspace> GetAllWorkspaces()
    {
        return _context.Workspaces.Include(w => w.Programs);
    }
}
