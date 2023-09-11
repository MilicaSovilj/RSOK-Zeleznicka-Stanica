CREATE DATABASE Zeleznicka__Stanica;

GO

USE Zeleznicka__Stanica;

GO

CREATE TABLE Korisnik
(
IDKorisnika int identity(1,1) not null primary key,
Prezime nvarchar(30) not null,
Ime nvarchar(30) not null,
Email nvarchar(255) not null,
Username nvarchar(255) not null,
Pass nvarchar (255) not null,
Role nvarchar(255) null
);

GO

CREATE TABLE Rezervacija
(
	IDKorisnika int not null,
	IDVoznje int not null
) ;

GO

CREATE TABLE Voznja
(
	IDVoznje int identity(1,1) not null primary key,
	NazivPrevoznika nvarchar(255) not null,
	Datum datetime not null,
	Vreme time not null,
	Polazak nvarchar(50) not null,
	Dolazak nvarchar(50) not null,
	IDVozaca nvarchar(70) not null
) ;

GO

CREATE TABLE Prevoznik
(
	 NazivPrevoznika nvarchar(255) not null primary key,
	 adresa nvarchar(255) not null
) ;

GO

CREATE TABLE Vozac
(
	IDVozaca nvarchar(70) not null primary key,
	Prezime nvarchar(30) not null,
	Ime nvarchar(30) not null,
	Email nvarchar(255) not null,
	Telefon varchar(50) not null
) ; 

GO

ALTER TABLE Rezervacija
ADD CONSTRAINT FK_Rezervacija_Korisnik FOREIGN KEY (IDKorisnika)
REFERENCES Korisnik (IDKorisnika)

GO

ALTER TABLE Rezervacija
ADD CONSTRAINT FK_Rezervacija_Voznja FOREIGN KEY (IDVoznje)
REFERENCES Voznja (IDVoznje)

GO

ALTER TABLE Voznja
ADD CONSTRAINT FK_Voznja_Prevoznik FOREIGN KEY (NazivPrevoznika)
REFERENCES Prevoznik (NazivPrevoznika)

GO

ALTER TABLE Voznja
ADD CONSTRAINT FK_Voznja_Vozac FOREIGN KEY (IDVozaca)
REFERENCES Vozac (IDVozaca)




