-- ==========================================================
-- Author:		Kukulyak Taras
-- Create date: 19.06.2019
-- Description: List Shop
-- ==========================================================
CREATE PROCEDURE [dbo].[Categories.ListShop]
	@ShopId INT
AS
	BEGIN
		DECLARE @RowsToProcess  int
		DECLARE @CurrentRow     int
		DECLARE @Parent TABLE 
		(RowID int not null primary key identity(1,1), Id INT, Title NVARCHAR(100), [Left] SMALLINT, [Right] SMALLINT,
		[Level] TINYINT, CreatedAt DATETIME, UpdatedAt DATETIME)

		INSERT INTO @Parent (Id, Title, [Left], [Right], [Level], CreatedAt, UpdatedAt)
		SELECT [dbo].[Categories].[Id], [dbo].[Categories].[Title], [dbo].[Categories].[Left], [dbo].[Categories].[Right], [dbo].[Categories].[Level],
		[dbo].[Categories].[CreatedAt], [dbo].[Categories].[UpdatedAt] FROM Categories
			join ShopCategory on Categories.Id = ShopCategory.CategoryId 
		Where ShopCategory.ShopId = @ShopId
		SET @RowsToProcess=@@ROWCOUNT

		SET @CurrentRow=0
		WHILE @CurrentRow<@RowsToProcess
		BEGIN
			SET @CurrentRow=@CurrentRow+1;
			DECLARE @Left AS INT = (SELECT [Left] FROM @Parent WHERE RowID=@CurrentRow);
			DECLARE @Right AS INT = (SELECT [Right] FROM @Parent WHERE RowID=@CurrentRow);

			INSERT INTO @Parent (Id, Title, [Left], [Right], [Level], CreatedAt, UpdatedAt)
			SELECT [dbo].[Categories].[Id], [dbo].[Categories].[Title], [dbo].[Categories].[Left], [dbo].[Categories].[Right],
			[dbo].[Categories].[Level], [dbo].[Categories].[CreatedAt], [dbo].[Categories].[UpdatedAt]  FROM Categories
				Where [dbo].[Categories].[Left] > @Left AND [dbo].[Categories].[Right] < @Right
		END
		SELECT * FROM @Parent Order By [Left];
END

