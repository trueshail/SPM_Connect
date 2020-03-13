
CREATE Procedure  [dbo].[ShowAllRecordsBetween]
@datestart datetime,
@dateto datetime

as
SELECT * FROM [dbo].[EFTHome]

		where 
		 EmailSent = 'No'AND (
		(left(PaymentDate,11) BETWEEN @datestart AND @dateto) OR 
		(left(PaymentDate,11) BETWEEN @datestart AND @dateto) OR 
		(left(PaymentDate,11) <= @datestart AND PaymentDate >= @dateto))
		
		order by PaymentDate
