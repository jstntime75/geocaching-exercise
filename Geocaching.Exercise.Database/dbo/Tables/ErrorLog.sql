﻿CREATE TABLE [dbo].[ErrorLog]
(
	[ErrorLogId] INT NOT NULL IDENTITY(1,1),
	[ERROR_DATE] SMALLDATETIME NOT NULL CONSTRAINT DF_ErrorLog_Error_Date DEFAULT getutcdate(),
	[STORED_PROC] NVARCHAR(100) NULL,
	[ERROR_NUMBER] INT,
	[ERROR_SEVERITY] INT,
	[ERROR_STATE] INT,
	[ERROR_PROCEDURE] VARCHAR(255),
	[ERROR_LINE] INT,
	[ERROR_MESSAGE] VARCHAR(1000),
	[PARAMETERS] VARCHAR(4000) NULL, 
    CONSTRAINT [PK_ErrorLog] PRIMARY KEY ([ErrorLogId]), 
)
