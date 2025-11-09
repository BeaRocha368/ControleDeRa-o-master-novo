CREATE DATABASE ControleDeRacaoDB

USE ControleDeRacaoDB

CREATE TABLE Pets (
    Id INT PRIMARY KEY IDENTITY(1,1),
	Nome VARCHAR(100) NOT NULL,
    Idade INT NOT NULL,
    Peso FLOAT NOT NULL, -- Usando FLOAT para mapear o double C#
    MarcaRacao VARCHAR(100) NULL,
	CodigoAcesso UNIQUEIDENTIFIER NOT NULL,
	DataCriacao DATETIME2 NOT NULL
);

CREATE TABLE Racoes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PetId INT NOT NULL,
    ConsumoDiarioKg DECIMAL(8, 2) NOT NULL,
    EstoqueAtualKg DECIMAL(8, 2) NOT NULL,
    UltimaCompraKg DECIMAL(8, 2) NOT NULL,
    DataAtualizacao DATETIME2 NOT NULL,
    CONSTRAINT FK_Racoes_Pets FOREIGN KEY (PetId) 
    REFERENCES Pets(Id)
    ON DELETE CASCADE -- Se o Pet for deletado, deleta a Racao
);

IF OBJECT_ID('Racoes', 'U') IS NOT NULL
    DROP TABLE Racoes;

CREATE TABLE Racoes (
    Id INT PRIMARY KEY NOT NULL, -- Não precisa de IDENTITY(1,1), pois vamos forçar o Id = 1
    ConsumoDiarioKg DECIMAL(8, 2) NOT NULL,
    EstoqueAtualKg DECIMAL(8, 2) NOT NULL,
    UltimaCompraKg DECIMAL(8, 2) NOT NULL,
    DataAtualizacao DATETIME2 NOT NULL
);

INSERT INTO Racoes (Id, ConsumoDiarioKg, EstoqueAtualKg, UltimaCompraKg, DataAtualizacao)
VALUES (1, 0.00, 0.00, 0.00, GETDATE());

UPDATE Racoes
SET EstoqueAtualKg = 0.00,
    DataAtualizacao = GETDATE()
WHERE Id = 1;

DELETE FROM Pets;

ALTER TABLE Pets
ALTER COLUMN CodigoAcesso VARCHAR(6) NOT NULL;

select * from Pets
select * from Racoes
