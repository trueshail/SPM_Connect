CREATE TABLE [dbo].[Checkin] (
    [Last Login]          VARCHAR (50)   NULL,
    [Application Running] NVARCHAR (100) NULL,
    [User Name]           NVARCHAR (50)  NULL,
    [Computer Name]       NVARCHAR (50)  NULL,
    [Version]             NCHAR (10)     NULL
);


GO
CREATE TRIGGER [tr_dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'f2005609-2165-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_21574ebe-3ca9-4528-8bd4-61c2cba8a343/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/User Name] (0x)
                END

                ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/User Name] (0x)
                END

                ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/User Name] (0x)
                END

                ;SEND ON CONVERSATION '3dd35d01-2862-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_ddd376ca-0554-4374-9206-562ccc6d7645/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/User Name] (0x)
                END

                ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/User Name] (0x)
                END

                ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/User Name] (0x)
                END

                ;SEND ON CONVERSATION '137bbdf9-2265-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_59d95ec9-2e0d-48f6-902d-11249b05c74b/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'd3c0fd22-2565-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_56072cf0-7b21-4797-9979-30becd6137ea/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'd0e9c3ec-2665-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_0a354242-2ce4-40cd-b612-344aacc5d176/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'ee06be28-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3e3f5be-e135-4db6-b60b-d917fd920f7b/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'ecdad671-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_9970dfbe-87c7-4beb-bf53-fbfe9e3c71d3/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/User Name] (0x)
                END

                ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/User Name] (0x)
                END

                ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/User Name] (0x)
                END

                ;SEND ON CONVERSATION '8b49bdc0-2765-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_d3677660-6179-4050-8f1b-fd083f235bd1/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/User Name] (0x)
                END

                ;SEND ON CONVERSATION 'c037ac67-2965-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_309b3fd4-5842-46ef-9542-c28adcb61488/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/User Name] (0x)
                END

                ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/User Name] (0x)
                END

                ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/User Name] (0x)
                END

                ;SEND ON CONVERSATION '97ff8e58-2a65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_68fb4600-5f2d-4921-8376-3f4f0adee754/EndMessage] (0x)
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
CREATE TRIGGER [tr_dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9_Sender] ON [dbo].[Checkin] AFTER insert, update, delete AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @rowsToProcess INT
    DECLARE @currentRow INT
    DECLARE @records XML
    DECLARE @theMessageContainer NVARCHAR(MAX)
    DECLARE @dmlType NVARCHAR(10)
    DECLARE @modifiedRecordsTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @exceptTable TABLE ([RowNumber] INT, [Computer Name] nvarchar(50), [User Name] nvarchar(50))
	DECLARE @deletedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @insertedTable TABLE ([RowNumber] INT IDENTITY(1, 1), [Computer Name] nvarchar(50), [User Name] nvarchar(50))
    DECLARE @var1 nvarchar(50)
    DECLARE @var2 nvarchar(50)
    
    IF NOT EXISTS(SELECT 1 FROM INSERTED)
    BEGIN
        SET @dmlType = 'Delete'
        INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM DELETED 
    END
    ELSE
    BEGIN
        IF NOT EXISTS(SELECT * FROM DELETED)
        BEGIN
            SET @dmlType = 'Insert'
            INSERT INTO @modifiedRecordsTable SELECT [Computer Name], [User Name] FROM INSERTED 
        END
        ELSE
        BEGIN
            SET @dmlType = 'Update';
            INSERT INTO @deletedTable SELECT [Computer Name],[User Name] FROM DELETED
            INSERT INTO @insertedTable SELECT [Computer Name],[User Name] FROM INSERTED
            INSERT INTO @exceptTable SELECT [RowNumber],[Computer Name],[User Name] FROM @insertedTable EXCEPT SELECT [RowNumber],[Computer Name],[User Name] FROM @deletedTable

            INSERT INTO @modifiedRecordsTable SELECT [Computer Name],[User Name] FROM @exceptTable e 
        END
    END

    SELECT @rowsToProcess = COUNT(1) FROM @modifiedRecordsTable    

    BEGIN TRY
        WHILE @rowsToProcess > 0
        BEGIN
            SELECT	@var1 = [Computer Name], @var2 = [User Name]
            FROM	@modifiedRecordsTable
            WHERE	[RowNumber] = @rowsToProcess
                
            IF @dmlType = 'Insert' 
            BEGIN
                ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/StartMessage/Insert] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/User Name] (0x)
                END

                ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/EndMessage] (0x)
            END
        
            IF @dmlType = 'Update'
            BEGIN
                ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/StartMessage/Update] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/User Name] (0x)
                END

                ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/EndMessage] (0x)
            END

            IF @dmlType = 'Delete'
            BEGIN
                ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/StartMessage/Delete] (CONVERT(NVARCHAR, @dmlType))

                IF @var1 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/Computer Name] (CONVERT(NVARCHAR(MAX), @var1))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/Computer Name] (0x)
                END
                IF @var2 IS NOT NULL BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/User Name] (CONVERT(NVARCHAR(MAX), @var2))
                END
                ELSE BEGIN
                    ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/User Name] (0x)
                END

                ;SEND ON CONVERSATION '3aa408a4-2b65-ea11-80eb-000c29e14dc0' MESSAGE TYPE [dbo_Checkin_1c84deb9-0b74-4733-a72d-a3ec8cf65fd9/EndMessage] (0x)
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