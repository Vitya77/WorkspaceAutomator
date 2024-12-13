using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class Workspace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Program> Programs { get; set; }
}

public class Program
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FilePath { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Program> Programs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=workspace.db");
    }
}
