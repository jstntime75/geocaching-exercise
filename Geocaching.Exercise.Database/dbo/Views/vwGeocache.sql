CREATE VIEW [dbo].[vwGeocache]
AS
SELECT        c.[Id], 
              c.[Name], 
			  c.[Description],
			  ISNULL(s.[Location].[Lat], 0.0) AS [Latitude],
			  ISNULL(s.[Location].[Long], 0.0) AS [Longitude]
FROM          dbo.[Cache] AS c 
				INNER JOIN dbo.[Spatial] AS s 
					ON s.[CacheId] = c.[Id]





