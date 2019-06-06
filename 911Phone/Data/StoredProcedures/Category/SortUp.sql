-- ==========================================================
-- Author:		Kukulyak Taras
-- Create date: 6.06.2019
-- Description: Sort Up Category
-- ==========================================================
CREATE PROCEDURE [dbo].[Categories.SortUp]
	@CategoryId INT
AS
	BEGIN
		IF((SELECT [dbo].[Categories].[Id] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId) IS NULL)
			Throw 50001, 'Current Category does not exist', 1;

		DECLARE @MyLevel AS SMALLINT = (SELECT [dbo].[Categories].[Level] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyLeft AS SMALLINT = (SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyRight AS SMALLINT = (SELECT [dbo].[Categories].[Right] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyDiff AS SMALLINT = (@MyRight - @MyLeft) + 1;
		DECLARE @AllDiff AS SMALLINT;
		DECLARE @ParentLeft AS SMALLINT;

		IF (@MyLevel = 1)
			BEGIN
				SET @ParentLeft = (SELECT MAX([dbo].[Categories].[Left]) FROM [dbo].[Categories] WHERE [dbo].[Categories].[Level] = 1 AND [dbo].[Categories].[Left] < @MyLeft);
			END
		ELSE
			BEGIN
				SET @ParentLeft = (SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] 
											WHERE [dbo].[Categories].[Left] < @MyLeft AND [dbo].[Categories].[Right] > @MyRight AND [dbo].[Categories].[Level] = (@MyLevel - 1));
			END

		IF (@MyLevel = 3)
			BEGIN 
				SET @AllDiff = @MyDiff;
			END
		ELSE
			BEGIN
				SET @AllDiff = @MyLeft - (SELECT MAX([dbo].[Categories].[Left]) FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < @MyLeft 
													AND [dbo].[Categories].[Right] < @MyRight AND [dbo].[Categories].[Level] = @MyLevel);
			END
		
		DECLARE @PreviousLeft AS INT = (SELECT MAX([dbo].[Categories].[Left]) FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < @MyLeft 
													AND [dbo].[Categories].[Right] < @MyRight AND [dbo].[Categories].[Level] = @MyLevel AND [dbo].[Categories].[Left] >= @ParentLeft);
		DECLARE @PreviousRight AS INT = (SELECT MAX([dbo].[Categories].[Right]) FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < @MyLeft 
													AND [dbo].[Categories].[Right] < @MyRight AND [dbo].[Categories].[Level] = @MyLevel AND [dbo].[Categories].[Left] >= @ParentLeft);

		IF (@PreviousLeft IS NULL OR @PreviousRight IS NULL)
			Throw 50001, 'Current Category does not move to up, because it is on top.', 6;


		BEGIN TRANSACTION;
			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = (0 - [dbo].[Categories].[Right]), [dbo].[Categories].[Left] = (0 - [dbo].[Categories].[Left])  
						FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] BETWEEN @MyLeft AND @MyRight;

			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] + @MyDiff), [dbo].[Categories].[Left] = ([dbo].[Categories].[Left] + @MyDiff)   
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Right] <= @PreviousRight AND [dbo].[Categories].[Left] >= @PreviousLeft;

			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Left] = ((0 - [dbo].[Categories].[Left]) - @AllDiff),
													[dbo].[Categories].[Right] = ((0 - [dbo].[Categories].[Right]) - @AllDiff)
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < 0;

			IF (@@error <> 0)
					ROLLBACK
		COMMIT;

		RETURN;
	END