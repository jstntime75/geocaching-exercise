CREATE PROCEDURE [dbo].[spGeocacheAdd]
	@Name VARCHAR(50),
	@Description VARCHAR(1024),
	@Latitude FLOAT,
	@Longitude FLOAT
AS

SET NOCOUNT ON	

BEGIN TRY

	DECLARE @CacheId INT

	BEGIN TRAN

	INSERT INTO [Cache] ([Name], [Description])
	VALUES (@Name, @Description)

	SET @CacheId = SCOPE_IDENTITY()

	INSERT INTO [Spatial] ([CacheId], [Location])
	VALUES (@CacheId, geography::STPointFromText('POINT(' + CAST(@Longitude AS VARCHAR(20)) + ' ' + CAST(@Latitude AS VARCHAR(20)) + ')', 4326))

	COMMIT

	SELECT @CacheId AS [Id]

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
            ROLLBACK
    END

	DECLARE @Parameters NVARCHAR(4000) = ''
	SET @Parameters += '@Name=' + ISNULL(@Name, '')
	SET @Parameters += ',@Description=' + ISNULL(@Description, '')
	SET @Parameters += ',@Latitude=' + ISNULL(CAST(@Latitude AS NVARCHAR(10)), '')
	SET @Parameters += ',@Longitude=' + ISNULL(CAST(@Longitude AS NVARCHAR(10)), '')

	INSERT INTO [ErrorLog]
	(
		[STORED_PROC],
		[ERROR_NUMBER] ,
		[ERROR_SEVERITY] ,
		[ERROR_STATE] ,
		[ERROR_PROCEDURE] ,
		[ERROR_LINE] ,
		[ERROR_MESSAGE],
		[PARAMETERS]
	)
	SELECT Object_Name(@@PROCID),
		ERROR_NUMBER() AS ErrorNumber  
		,ERROR_SEVERITY() AS ErrorSeverity
		,ERROR_STATE() AS ErrorState 
		,ERROR_PROCEDURE() AS ErrorProcedure
		,ERROR_LINE() AS ErrorLine
		,ERROR_MESSAGE() AS ErrorMessage
		,@Parameters

END CATCH

