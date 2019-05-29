-- ==========================================================
-- Author:		Kukulyak Taras
-- Create date: 28.05.2019
-- Description: Delete Category
-- ==========================================================
CREATE PROCEDURE [dbo].[Categories.Delete]
	@CategoryId INT
AS
	BEGIN
		DECLARE @Left AS SMALLINT = (SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);
		DECLARE @Right AS SMALLINT = (SELECT [dbo].[Categories].[Right] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId);

		IF(@Left IS NULL OR @Right IS NULL)
			Throw 50001, 'Current Category does not exist', 1;

		DECLARE @Row AS INT = (SELECT COUNT([dbo].[Categories].[Id]) FROM [dbo].[Categories] 
			WHERE [dbo].[Categories].[Left] > @Left AND [dbo].[Categories].[Right] < @Right);
		IF(@Row > 0)
			Throw 50001, 'Current Category has children', 3;

		BEGIN TRANSACTION;
			DELETE FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @CategoryId;
			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Left] = ([dbo].[Categories].[Left] - 2), [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] - 2)  
				FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] > @Left AND [dbo].[Categories].[Right] > @Right;
			UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] - 2)
				FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < @Left AND [dbo].[Categories].[Right] > @Right;
		IF (@@error <> 0)
			ROLLBACK
		COMMIT;

		RETURN;
	END
