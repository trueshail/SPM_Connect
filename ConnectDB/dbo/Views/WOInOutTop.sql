create view [WOInOutTop]
as

  WITH cte AS (
    SELECT *,
        ROW_NUMBER() OVER (
            PARTITION BY 
                [WO]
            ORDER BY
			Id DESC
        ) row_num
     FROM 
         [SPM_Database].[dbo].[WOInOut]
)
select * from cte
WHERE row_num = 1;