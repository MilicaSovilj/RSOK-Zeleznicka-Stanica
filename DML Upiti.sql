SET IDENTITY_INSERT [dbo].[Korisnik] ON 

INSERT [dbo].[Korisnik] ([IDKorisnika], [Ime], [Prezime], [Email], [Username], [Pass], [Role]) VALUES (1, N'Momirka', N'Trivun', N'momirka.trivun@gmail.com', N'TrivunMo', N'123', N'Admin')
INSERT [dbo].[Korisnik] ([IDKorisnika], [Ime], [Prezime], [Email], [Username], [Pass], [Role]) VALUES (4, N'1', N'1', N'1', N'1', N'1', N'Admin')
INSERT [dbo].[Korisnik] ([IDKorisnika], [Ime], [Prezime], [Email], [Username], [Pass], [Role]) VALUES (6, N'2', N'2', N'2', N'2', N'2', NULL)
INSERT [dbo].[Korisnik] ([IDKorisnika], [Ime], [Prezime], [Email], [Username], [Pass], [Role]) VALUES (7, N'Milica', N'Sovilj', N'da', N'3', N'3', NULL)
SET IDENTITY_INSERT [dbo].[Korisnik] OFF


INSERT [dbo].[Prevoznik] ( [NazivPrevoznika], [adresa]) VALUES (N'Srbija Voz', N'Nemanjina 6, Beograd')
INSERT [dbo].[Prevoznik] ( [NazivPrevoznika], [adresa]) VALUES (N'Slovenian railways', N'Trg Osvobodilne fronte 7, 1000 Ljubljana, Slovenia')


INSERT [dbo].[Vozac] ([IDVozaca], [Prezime], [Ime], [Email], [Telefon]) VALUES (N'1', N'Mijatov', N'Nikola', N'nmij@gmail.com', N'+3816954123658')
INSERT [dbo].[Vozac] ([IDVozaca], [Prezime], [Ime], [Email], [Telefon]) VALUES (N'2', N'Jovanovic', N'Dusan', N'dusanjov81@gmail.com', N'0659687452')


SET IDENTITY_INSERT [dbo].[Voznja] ON 

INSERT [dbo].[Voznja] ([IDVoznje], [NazivPrevoznika], [Datum], [Vreme], [Polazak], [Dolazak], [IDVozaca]) VALUES (1, N'Srbija voz', CAST(N'2023-12-11' AS Date), CAST(N'12:00:00' AS time), N'Beograd', N'Bar',1)
INSERT [dbo].[Voznja] ([IDVoznje], [NazivPrevoznika], [Datum], [Vreme], [Polazak], [Dolazak], [IDVozaca]) VALUES (2, N'Slovenian railways', CAST(N'2023-09-29' AS Date),CAST(N'13:00:00' AS time), N'Ljubljana', N'Beograd',1)
SET IDENTITY_INSERT [dbo].[Voznja] OFF
GO


INSERT [dbo].[Rezervacija] ([IDKorisnika], [IDVoznje]) VALUES (7, 1)

