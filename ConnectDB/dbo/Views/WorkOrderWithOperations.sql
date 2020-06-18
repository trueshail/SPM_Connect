




CREATE VIEW [dbo].[WorkOrderWithOperations]
AS
SELECT
	a.*
   ,eb2.Famille AS AssyFamily
   ,eb2.Des AS AssyDescription
   ,eb2.Des4 AS AssyManufacturer
   ,eb2.Des2 AS AssyManufacturerItemNumber
   ,eb2.Spec1 AS AssySpare
   ,'2' AS Priority 
FROM (SELECT
		bm.Job
	   ,bm.Woprec
	   ,bm.WO
	   ,bm.Item AS ItemNumber
	   ,CONVERT(INT, bm.Qte_Ass) AS QuantityPerAssembly
	   ,eb.Des AS Description
	   ,eb.Famille AS ItemFamily
	   ,eb.Des4 AS Manufacturer
	   ,eb.Des2 AS ManufacturerItemNumber
	   ,eb.Spec1 AS Spare
	   ,bm.Piece AS AssyNo
	FROM [SPMDB].[dbo].[Mrpres] bm
	INNER JOIN [SPMDB].[dbo].[Edb] eb
		ON bm.Item = eb.Item
	WHERE bm.RML_Active = '1') a
INNER JOIN SPMDB.dbo.Edb eb2
	ON a.AssyNo = eb2.Item
UNION
SELECT
	a.*
   ,eb2.Famille AS AssyFamily
   ,eb2.Des AS AssyDescription
   ,eb2.Des4 AS AssyManufacturer
   ,eb2.Des2 AS AssyManufacturerItemNumber
   ,eb2.Spec1 AS AssySpare
   ,'1' AS Priority
FROM (SELECT
		w.Job
	   ,bm.WO AS Woprec
	   ,w.Code_Id AS WO
	   ,w.Item AS ItemNumber
	   ,CONVERT(INT, w.Qte) AS QuantityPerAssembly
	   ,w.Des_Ope
	   ,w.Complet
	   ,w.Code_Ope
	   ,CONVERT(VARCHAR, w.Datepro) AS Datepro
	   ,CONVERT(VARCHAR, w.Dateliv) AS DateComplet
	   ,bm.Item AS AssyNo
	FROM [SPMDB].[dbo].[Mrpres] bm
	INNER JOIN [SPMDB].[dbo].Wodet w
		ON bm.WO = w.WO
	WHERE bm.RML_Active = '1') a
INNER JOIN SPMDB.dbo.Edb eb2
	ON a.AssyNo = eb2.Item
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'WorkOrderWithOperations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Checkin"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "WOReleaseDetails"
            Begin Extent = 
               Top = 6
               Left = 274
               Bottom = 136
               Right = 452
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'WorkOrderWithOperations';

