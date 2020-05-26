
CREATE procedure [dbo].[GetReleaseSuggestions]
@job varchar(30),
@assyno varchar(30),
@wo varchar(30)

as

SELECT a.item AS ItemId, 
       eb.description, 
       eb.manufacturer, 
       eb.manufactureritemnumber, 
       CONVERT(INT, a.qte_ass) AS Qty,
	   a.Ordre as [Order] 
FROM  (SELECT e.* 
       FROM   [SPMDB].[dbo].mrpres e 
       WHERE  NOT EXISTS (SELECT * 
                          FROM   [SPM_Database].[dbo].[woreleasedetails] wd 
                          WHERE  wd.jobno = @job 
                                 AND wd.assyno = @assyno 
                                 AND wd.itemid = e.item
								 AND wd.[Order] = e.Ordre) 
              AND e.job = @job 
              AND e.piece = @assyno 
              AND e.woprec = @wo
			  AND e.RML_Active = '1')a 
      INNER JOIN [SPM_Database].[dbo].[inventory] eb 
              ON a.item = eb.itemnumber 
ORDER BY a.Ordre		
