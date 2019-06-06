-- ==========================================================
-- Author:		Kukulyak Taras
-- Create date: 6.06.2019
-- Description: Sort Down Category
-- ==========================================================
CREATE PROCEDURE [dbo].[Categories.SortDown]
	@CategoryId INT
AS
	BEGIN
		DECLARE @MyLevel AS SMALLINT = (SELECT [dbo].[Categories].[Level] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyLeft AS SMALLINT = (SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyRight AS SMALLINT = (SELECT [dbo].[Categories].[Right] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyDiff AS SMALLINT = (@MyRight - @MyLeft) + 1;
		DECLARE @ParentRight AS iNT;

		IF (@MyLevel = 1)
			BEGIN
				SET @ParentRight = (SELECT MAX([dbo].[Categories].[Right]) FROM [dbo].[Categories]);
			END
		ELSE
			BEGIN
				SET @ParentRight = (SELECT [dbo].[Categories].[Right] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < @MyLeft AND [dbo].[Categories].[Right] > @MyRight 
										AND [dbo].[Categories].[Level] = (@MyLevel - 1));
			END
		


		DECLARE @PreviousLeft AS INT = (SELECT MIN([dbo].[Categories].[Left]) FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] > @MyLeft 
													AND [dbo].[Categories].[Right] > @MyRight AND [dbo].[Categories].[Level] = @MyLevel AND [dbo].[Categories].[Right] <= @ParentRight);
		DECLARE @PreviousRight AS INT = (SELECT MIN([dbo].[Categories].[Right]) FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] > @MyLeft 
													AND [dbo].[Categories].[Right] > @MyRight AND [dbo].[Categories].[Level] = @MyLevel AND [dbo].[Categories].[Right] <= @ParentRight);
		DECLARE @AllDiff AS SMALLINT = (@PreviousRight - @PreviousLeft) + 1;
		
		IF (@PreviousLeft IS NULL OR @PreviousRight IS NULL)
			Throw 50001, 'Current Category does not move to down, because it is on bottom.', 6;

		BEGIN TRANSACTION;
			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = (0 - [dbo].[Categories].[Right]), [dbo].[Categories].[Left] = (0 - [dbo].[Categories].[Left])  
						FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] BETWEEN @MyLeft AND @MyRight;

			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] - @MyDiff), [dbo].[Categories].[Left] = ([dbo].[Categories].[Left] - @MyDiff)   
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Right] <= @PreviousRight AND [dbo].[Categories].[Left] >= @PreviousLeft;

			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Left] = ((0 - [dbo].[Categories].[Left]) + @AllDiff),
													[dbo].[Categories].[Right] = ((0 - [dbo].[Categories].[Right]) + @AllDiff)
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < 0;

			IF (@@error <> 0)
					ROLLBACK
		COMMIT;

		RETURN;
	END