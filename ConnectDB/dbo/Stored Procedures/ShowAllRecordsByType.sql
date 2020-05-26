

CREATE Procedure  [dbo].[ShowAllRecordsByType]
@filterby varchar

as
SELECT * FROM [dbo].[EFTHome]
WHERE   EmailSent LIKE @filterby+'%'
OR      @filterby = ''		
		
		order by PaymentDate DESC
