-- ==========================================================
-- Author:		Kukulyak Taras
-- Create date: 29.05.2019
-- Description: Update Category
-- ==========================================================
CREATE PROCEDURE [dbo].[Categories.Update]
	@CategoryId INT,
	@Title NVARCHAR(100),
	@ParentId INT
AS
	BEGIN
		IF((SELECT [dbo].[Categories].[Id] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId) IS NULL)
			Throw 50001, 'Current Category does not exist', 1;

		IF ((SELECT [dbo].[Categories].[Level] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId) = 3)
			Throw 50001, 'Level Categories will exceed 3 level.', 5;

		IF (@ParentId IS NOT NULL)
			BEGIN
				DECLARE @ParentCheckId AS INT = (SELECT [dbo].[Categories].[Id] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId);
				IF(@ParentCheckId IS NULL)
					Throw 50001, 'Current Parent Category does not exist', 1;
			END

		DECLARE @MyLevel AS SMALLINT = (SELECT [dbo].[Categories].[Level] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyLeft AS SMALLINT = (SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyRight AS SMALLINT = (SELECT [dbo].[Categories].[Right] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @MyDiff AS SMALLINT = (@MyRight - @MyLeft) + 1;

		DECLARE @ParentCurrentId AS INT = (SELECT TOP 1 [dbo].[Categories].[Id] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < @MyLeft
		AND [dbo].[Categories].[Right] > @MyRight AND [dbo].[Categories].[Level] = @MyLevel - 1);
		IF (@ParentId = @ParentCurrentId OR @ParentId IS NULL)
			BEGIN
				UPDATE [dbo].[Categories] SET [dbo].[Categories].[Title] = @Title, [dbo].[Categories].[UpdatedAt] = getdate() WHERE [dbo].[Categories].[Id] = @CategoryId;
				RETURN;
			END

		DECLARE @Row AS INT = (SELECT COUNT([dbo].[Categories].[Id]) FROM [dbo].[Categories] 
			WHERE [dbo].[Categories].[Left] > @MyLeft AND [dbo].[Categories].[Right] < @MyRight);
			IF(@Row > 0)
				BEGIN
					IF ((((SELECT MAX([dbo].[Categories].[Level]) FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] > @MyLeft AND [dbo].[Categories].[Right] < @MyRight)
						- @MyLevel) + (SELECT [dbo].[Categories].[Level] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId)) >= 3)
						Throw 50001, 'Level Categories will exceed 3 level.', 5;
				END
				


		IF ((SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId) > @MyLeft 
			AND (SELECT [dbo].[Categories].[Right] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId) < @MyRight)
					Throw 50001, 'Category can not be moved to current parent, because will have circular error.', 4;

		BEGIN TRANSACTION;
			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = (0 - [dbo].[Categories].[Right]), [dbo].[Categories].[Left] = (0 - [dbo].[Categories].[Left])  
						FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] BETWEEN @MyLeft AND @MyRight;
			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] - @MyDiff), [dbo].[Categories].[Left] = ([dbo].[Categories].[Left] - @MyDiff)   
						FROM [dbo].[Categories] WHERE [dbo].[Categories].[Right] > @MyRight AND [dbo].[Categories].[Left] > @MyRight;
			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] - @MyDiff)  
						FROM [dbo].[Categories] WHERE [dbo].[Categories].[Right] > @MyRight AND [dbo].[Categories].[Left] < @MyLeft;

			
			IF (@ParentId IS NULL)
				BEGIN
					DECLARE @MyDiffNull AS TINYINT = ((SELECT MAX([dbo].[Categories].[Right]) FROM [dbo].[Categories]) - @MyLeft) + 1;
					UPDATE [dbo].[Categories] SET [dbo].[Categories].[Left] = (@MyDiffNull + (0 - [dbo].[Categories].[Left])),
													[dbo].[Categories].[Right] = (@MyDiffNull + (0 - [dbo].[Categories].[Right])),
													[dbo].[Categories].[Level] = ([dbo].[Categories].[Level] - (@MyLevel - 0) + 1)
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < 0;
				END
			ELSE
				BEGIN
					DECLARE @ParentLeft AS SMALLINT = (SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId);
					DECLARE @ParentRight AS SMALLINT = (SELECT [dbo].[Categories].[Right] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId);
					DECLARE @ParentLevel AS SMALLINT = (SELECT [dbo].[Categories].[Level] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId);

					UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] + @MyDiff), [dbo].[Categories].[Left] = ([dbo].[Categories].[Left] + @MyDiff)   
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Right] > @ParentRight AND [dbo].[Categories].[Left] > @ParentRight;
					UPDATE [dbo].[Categories] SET [dbo].[Categories].[Left] = ((@ParentRight - @MyLeft) + (0 - [dbo].[Categories].[Left])),
													[dbo].[Categories].[Right] = ((@ParentRight - @MyLeft) + (0 - [dbo].[Categories].[Right])),
													[dbo].[Categories].[Level] = ([dbo].[Categories].[Level] - (@MyLevel - (SELECT [dbo].[Categories].[Level] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId)) + 1)
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < 0;
					DECLARE @MyNewLeft AS SMALLINT = (SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
					UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] + @MyDiff)   
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Right] >= @MyNewLeft AND [dbo].[Categories].[Left] < @MyNewLeft;
				END

			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Title] = @Title, [dbo].[Categories].[UpdatedAt] = getdate() WHERE [dbo].[Categories].[Id] = @CategoryId;
			IF (@@error <> 0)
					ROLLBACK
		COMMIT;
			
		RETURN;
	END