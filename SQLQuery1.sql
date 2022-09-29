CREATE TABLE [dbo].[PrestitoDvd] (
[id] INT IDENTITY NOT NULL,
[inizioPrestito] DATETIME NOT NULL,
[finePrestito] DATETIME NOT NULL,
[dvd_id] INT NOT NULL,
[utente_id] INT NOT NULL,
FOREIGN KEY (utente_id) REFERENCES Utente(id),
FOREIGN KEY (dvd_id) REFERENCES Dvd(id),
PRIMARY KEY CLUSTERED ([id] ASC)
);