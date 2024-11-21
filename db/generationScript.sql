CREATE TABLE Environments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Description TEXT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Applications (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Path VARCHAR(255) NOT NULL
);

CREATE TABLE EnvironmentApplications (
    EnvironmentId INT NOT NULL,
    ApplicationId INT NOT NULL,
    FOREIGN KEY (EnvironmentId) REFERENCES Environments(Id),
    FOREIGN KEY (ApplicationId) REFERENCES Applications(Id),
    PRIMARY KEY (EnvironmentId, ApplicationId)
);
