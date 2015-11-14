SET IDENTITY_INSERT dbo.[Cache] ON
INSERT INTO [Cache] ([Id], [Name], [Description])
VALUES (1, N'Justin''s House', N'Somewhere in Bellevue!'), 
       (2, N'Microsoft Headquarters', N'1 Microsoft Way'),
       (3, N'Woodland Park Zoo', N'Watch out for the Rhinos!'),
       (4, N'Seattle Library', N'Read a book!'),
       (5, N'Kerry Park', N'Frasier lives somewhere close'),
       (6, N'Seattle Aquarium', N'My patronus is an otter... just like Hermione.'),
       (7, N'Madison Park Beach', N'Don''t try to drive and park here. Like, ever.'),
       (8, N'Top Pot doughnuts', N'Ooh the cute barista is back!')
SET IDENTITY_INSERT dbo.[Cache] OFF

INSERT INTO [Spatial] ([CacheId], [Location])
VALUES (1, geography::STPointFromText('POINT(-122.1348127 47.6025836)', 4326)), 
       (2, geography::STPointFromText('POINT(-122.1305697 47.6395867)', 4326)),
       (3, geography::STPointFromText('POINT(-122.3531259 47.6689069)', 4326)),
       (4, geography::STPointFromText('POINT(-122.3324203 47.6071363)', 4326)),
       (5, geography::STPointFromText('POINT(-122.3600792 47.6291400)', 4326)),
       (6, geography::STPointFromText('POINT(-122.3429989 47.6078782)', 4326)),
       (7, geography::STPointFromText('POINT(-122.2767698 47.6356864)', 4326)),
       (8, geography::STPointFromText('POINT(-122.3412367 47.6151980)', 4326))

