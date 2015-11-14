CREATE PROCEDURE [dbo].[spGeocacheDelete]
	@GeocacheId INT
AS

SET NOCOUNT ON	

BEGIN TRY

	BEGIN TRAN

	DELETE FROM [Spatial]
	WHERE [CacheId] = @GeocacheId

	DELETE FROM [Cache]
	WHERE [Id] = @GeocacheId

	COMMIT

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
            ROLLBACK
    END

	DECLARE @Parameters NVARCHAR(4000) = ''
	SET @Parameters += '@GeocacheId=' + ISNULL(CAST(@GeocacheId AS NVARCHAR(20)), '')

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

