CREATE TABLE [dbo].[Checkin] (
    [Last Login]          VARCHAR (50)   NULL,
    [Application Running] NVARCHAR (100) NULL,
    [User Name]           NVARCHAR (50)  NULL,
    [Computer Name]       NVARCHAR (50)  NULL,
    [Version]             NCHAR (10)     NULL
);


GO
CREATE TRIGGER [tr_dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '6a046095-1f9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ef20dd79-57c1-4e3c-948c-a94de2667d98/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END
GO
CREATE TRIGGER [tr_dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '9ccbf278-229c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_01071d8a-be9f-4043-918e-818e0a41f067/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END
GO
CREATE TRIGGER [tr_dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'cd1d5260-269c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_5f17eb5b-c032-475c-8a6e-a43a474efa75/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END
GO
CREATE TRIGGER [tr_dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'ab6b0a3d-289c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_db22aec1-bcf3-4252-8e43-7e3013359043/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END
GO
CREATE TRIGGER [tr_dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '7e6e123f-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_76c8b33a-878c-4c83-bfbc-4adcedde591c/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END
GO
CREATE TRIGGER [tr_dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '05a9fdbe-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_f39201de-df11-46db-b383-7cae77ac1604/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END
GO
CREATE TRIGGER [tr_dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '4bb8afce-299c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_81fd4676-48db-40a8-9a6f-def18f924ed5/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END
GO
CREATE TRIGGER [tr_dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/Application Running] (0x)
                END

                ;SEND ON CONVERSATION 'b6ed8701-2d9c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_fd93834c-dfbe-46ee-815a-d153036a1d3f/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END
GO
CREATE TRIGGER [tr_dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50), [Application Running] nvarchar(100))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    DECLARE @var3 nvarchar(100)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name], [Application Running] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name],[Application Running] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name],[Application Running] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name],[Application Running] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name],[Application Running] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name], @var3 = [Application Running]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/User Name] (0x)
                END
                IF @var3 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Application Running] (CONVERT(NVARCHAR(MAX), @var3))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/Application Running] (0x)
                END

                ;SEND ON CONVERSATION '6413a4d0-339c-ea11-80f0-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_2bc26403-fb3d-4c03-a05f-e81fd529ef4a/EndMessage] (0x)
            END

            SET @rowsToProcess = @rowsToProcess - 1
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000)
        DECLARE @ErrorSeverity INT
        DECLARE @ErrorState INT

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE()

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState) 
    END CATCH
END