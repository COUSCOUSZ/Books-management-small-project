create database GestionLivre;
go
use GestionLivre;
go
CREATE TABLE Auteur (
    IDAuteur INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NomAuteur VARCHAR(50) NOT NULL UNIQUE,
    EmailAuteur VARCHAR(50) NULL,
    TelephoneAuteur VARCHAR(50) NULL,
    AdresseAuteur VARCHAR(50) NULL
);
go
CREATE TABLE Categorie (
    IDCat INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NomCat VARCHAR(50) NOT NULL UNIQUE,
    DescriptionCat VARCHAR(250) NULL UNIQUE,
);
go
CREATE TABLE Editeur (
    IDEditeur INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NomEditeur VARCHAR(50) NOT NULL UNIQUE,
    DescripEditeur VARCHAR(1000) NULL,
    EmailEditeur VARCHAR(50) NULL,
    telephoneEditeur VARCHAR(50) NULL,
    AdresseEditeur VARCHAR(50) NULL
);
go
CREATE TABLE Livre (
    IDLivre INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Titre VARCHAR(50) NOT NULL UNIQUE,
    ISBN VARCHAR(20) NOT NULL,
    IDEditeur INT NOT NULL FOREIGN KEY REFERENCES Editeur(IDEditeur),
    IDAuteur INT NOT NULL FOREIGN KEY REFERENCES Auteur(IDAuteur),
    IDCat INT NOT NULL FOREIGN KEY REFERENCES Categorie(IDCat),
    DescripLivre VARCHAR(1000) NULL,
    AnneeEdition INT NULL
);
go
-- Insert into Auteur table
INSERT INTO Auteur (NomAuteur, EmailAuteur, TelephoneAuteur, AdresseAuteur)
VALUES
    ('J.K. Rowling', 'jkrowling@email.com', 123456789, 'London, UK'),
    ('George R.R. Martin', 'grrmartin@email.com', 987654321, 'Santa Fe, USA'),
    ('Haruki Murakami', 'harukimurakami@email.com', 555111222, 'Tokyo, Japan');

-- Insert into Categorie table
INSERT INTO Categorie (NomCat, DescriptionCat)
VALUES
    ('Fantasy', 'Books that involve magic, mythical creatures, and other supernatural elements'),
    ('Science Fiction', 'Books that explore futuristic concepts, advanced technology, and outer space'),
    ('Magical Realism', 'Books that blend realistic settings with magical elements');

-- Insert into Editeur table
INSERT INTO Editeur (NomEditeur, DescripEditeur, EmailEditeur, TelephoneEditeur, AdresseEditeur)
VALUES
    ('Bloomsbury', 'Independent publisher known for publishing the Harry Potter series', 'info@bloomsbury.com', 111222333, 'London, UK'),
    ('Bantam Books', 'American publishing house, a division of Penguin Random House', 'info@bantambooks.com', 444555666, 'New York, USA'),
    ('Shinchosha', 'Japanese publishing company based in Tokyo', 'info@shinchosha.co.jp', 777888999, 'Tokyo, Japan');

-- Insert into Livre table
INSERT INTO Livre (Titre, ISBN, IDEditeur, IDAuteur, IDCat, DescripLivre, AnneeEdition)
VALUES
    ('Harry Potter and the Philosopher''s Stone', '978-0747532743', 1, 1, 1, 'The first book in the Harry Potter series', 1997),
    ('A Game of Thrones', '978-0553381689', 2, 2, 1, 'The first book in A Song of Ice and Fire series', 1996),
    ('Norwegian Wood', '978-0375704024', 3, 3, 3, 'A novel by Haruki Murakami', 1987);


go
SELECT * FROM Auteur;
SELECT * FROM Categorie;
SELECT * FROM Editeur;

SELECT * FROM Livre;
SELECT
    Livre.Titre,
    Livre.ISBN,
    Livre.AnneeEdition,
    Auteur.NomAuteur AS NomAuteur,
    Categorie.NomCat AS NomCategorie
FROM Livre
INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur
INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat;
SELECT * FROM Livre WHERE IDEditeur = 1; 
SELECT * FROM Livre WHERE IDAuteur = 2; 
SELECT * FROM Livre WHERE IDCat = 2;



INSERT INTO Auteur (NomAuteur, EmailAuteur, TelephoneAuteur, AdresseAuteur)
VALUES('J.K. Rowleing', 'jkrowling@email.com', 123456789, 'London, UK');

DBCC CHECKIDENT ('Auteur', RESEED, 5);
DBCC CHECKIDENT ('Categorie', RESEED, 5);
DBCC CHECKIDENT ('Editeur', RESEED, 4);
DBCC CHECKIDENT ('Livre', RESEED,4);

SELECT Livre.IDLivre, Livre.Titre, Livre.ISBN, Editeur.NomEditeur AS EditeurName, Auteur.NomAuteur AS AuteurName, Categorie.NomCat AS CategorieName, Livre.DescripLivre, Livre.AnneeEdition FROM Livre INNER JOIN Editeur ON Livre.IDEditeur = Editeur.IDEditeur INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat;

SELECT Livre.IDLivre, Livre.Titre, Livre.ISBN, Editeur.NomEditeur AS EditeurName, Auteur.NomAuteur AS AuteurName, Categorie.NomCat AS CategorieName, Livre.DescripLivre, Livre.AnneeEdition FROM Livre INNER JOIN Editeur ON Livre.IDEditeur = Editeur.IDEditeur INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat WHERE Livre.IDLivre = 2;



SELECT * FROM Auteur WHERE CONCAT(NomAuteur,EmailAuteur,TelephoneAuteur,AdresseAuteur)  LIKE '%0698%' ORDER BY NomAuteur DESC

SELECT * FROM Categorie WHERE CONCAT(NomCat,DescriptionCat)  LIKE '%da7%' ORDER BY NomCat DESC

SELECT * FROM Editeur WHERE CONCAT(NomEditeur,DescripEditeur,TelephoneEditeur,AdresseEditeur)  LIKE '%bloom%' ORDER BY NomEditeur DESC


select Livre.IDLivre,
Livre.ISBN,
NomEditeur AS EditeurName,
Auteur.NomAuteur AS AuteurName,
Categorie.NomCat AS CategorieName,
Editeur.IDEditeur AS EditeurID,
Auteur.IDAuteur AS AuteurID,
Categorie.IDCat AS CategorieID,
Livre.DescripLivre,
Livre.AnneeEdition 
from Livre INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur 
INNER JOIN Editeur ON Livre.IDEditeur = Editeur.IDEditeur
INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat
WHERE CONCAT(Livre.ISBN, NomEditeur, Auteur.NomAuteur, Categorie.NomCat, Livre.DescripLivre, Livre.AnneeEdition,Livre.Titre) LIKE '%game%';


SELECT Livre.IDLivre, Livre.ISBN, NomEditeur AS EditeurName, Auteur.NomAuteur AS AuteurName, Categorie.NomCat AS CategorieName, Editeur.IDEditeur AS EditeurID, Auteur.IDAuteur AS AuteurID, Categorie.IDCat AS CategorieID, Livre.DescripLivre, Livre.AnneeEdition, Livre.Titre FROM Livre INNER JOIN Auteur ON Livre.IDAuteur = Auteur.IDAuteur INNER JOIN Editeur ON Livre.IDEditeur = Editeur.IDEditeur INNER JOIN Categorie ON Livre.IDCat = Categorie.IDCat WHERE CONCAT(Livre.ISBN, NomEditeur, Auteur.NomAuteur, Categorie.NomCat, Livre.DescripLivre, Livre.AnneeEdition, Livre.Titre) LIKE '%j.k%'

SELECT count(*) from Auteur