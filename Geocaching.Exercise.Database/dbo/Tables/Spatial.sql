CREATE TABLE [dbo].[Spatial] (
    [CacheId]  INT               NOT NULL,
    [Location] [sys].[geography] NOT NULL,
    CONSTRAINT [PK_Spatial] PRIMARY KEY CLUSTERED ([CacheId] ASC),
    CONSTRAINT [FK_Spatial_Cache] FOREIGN KEY ([CacheId]) REFERENCES [dbo].[Cache] ([Id])
);

