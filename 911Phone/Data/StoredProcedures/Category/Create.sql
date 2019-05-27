-- ==========================================================
-- Author:		Kukulyak Taras
-- Create date: 24.05.2019
-- Description: Create Category
-- ==========================================================
CREATE PROCEDURE [dbo].[Categories.Create]
	@Title NVARCHAR(100),
	@ParentId INT
AS
	BEGIN
		DECLARE @Level TINYINT;
		DECLARE @Left SMALLINT;
		DECLARE @Right SMALLINT;
		DECLARE @NewParentRight SMALLINT;

		IF(@ParentId IS NULL)
			BEGIN
				SET @Level = 1;
				SET @Left = (SELECT MAX([dbo].[Categories].[Right]) FROM [dbo].[Categories]) + 1;

				IF(@Left IS NULL)
					SET @Left = 1;

				SET @Right = @Left + 1;

				INSERT INTO [dbo].[Categories] (
					[dbo].[Categories].[Title], [dbo].[Categories].[Left], [dbo].[Categories].[Right], [dbo].[Categories].[Level], 
					[dbo].[Categories].[CreatedAt], [dbo].[Categories].[UpdatedAt])
					VALUES (@Title, @Left, @Right, @Level, getdate(), getdate())
				RETURN;
			END
		ELSE
			BEGIN
				DECLARE @ParentLevel TINYINT;
				DECLARE @ParentRight SMALLINT;
				DECLARE @ParentLeft SMALLINT;

				SET @ParentLevel = (SELECT [dbo].[Categories].[Level] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId);
				SET @ParentRight = (SELECT [dbo].[Categories].[Right] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId);
				SET @ParentLeft = (SELECT [dbo].[Categories].[Left] FROM [dbo].[Categories] WHERE [dbo].[Categories].[Id] = @ParentId);

				IF(@ParentLevel IS NULL OR @ParentRight IS NULL OR @ParentLeft IS NULL)
					Throw 50001, 'Parent Category does not exist', 12;

				IF(@ParentLevel > 2)
					Throw 50001, 'Current Parent Category can not have a child category, because category-level can not be more 3!', 11;

				IF(@ParentLevel = 1)
					BEGIN
						SET @Level = 2;
						SET @Left = (SELECT MAX([dbo].[Categories].[Right]) FROM [dbo].[Categories] 
							WHERE [dbo].[Categories].[Right] < @ParentRight AND [dbo].[Categories].[Left] > @ParentLeft AND [dbo].[Categories].[Level] = 2) + 1;

						IF(@Left IS NULL)
							SET @Left = @ParentLeft + 1;

						SET @Right = @Left + 1;
						SET @NewParentRight = @Right + 1;

						BEGIN TRANSACTION;
							INSERT INTO [dbo].[Categories] (
								[dbo].[Categories].[Title], [dbo].[Categories].[Left], [dbo].[Categories].[Right], [dbo].[Categories].[Level], 
								[dbo].[Categories].[CreatedAt], [dbo].[Categories].[UpdatedAt])
								VALUES (@Title, @Left, @Right, @Level, getdate(), getdate())

							UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = @NewParentRight 
								WHERE [dbo].[Categories].[Id] = @ParentId;
							UPDATE [dbo].[Categories] SET [dbo].[Categories].[Left] = ([dbo].[Categories].[Left] + 2),[dbo].[Categories].[Right] = ([dbo].[Categories].[Right] + 2)  
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] > @ParentRight;
						IF (@@error <> 0)
							ROLLBACK
						COMMIT;

						RETURN;
					END


				IF(@ParentLevel = 2)
					BEGIN
						SET @Level = 3;
						SET @Left = (SELECT MAX([dbo].[Categories].[Right]) FROM [dbo].[Categories] 
							WHERE [dbo].[Categories].[Right] < @ParentRight AND [dbo].[Categories].[Left] > @ParentLeft AND [dbo].[Categories].[Level] = 3) + 1;

						IF(@Left IS NULL)
							SET @Left = @ParentLeft + 1;
							
						SET @Right = @Left + 1;
						SET @NewParentRight = @Right + 1;

						BEGIN TRANSACTION;

							UPDATE [dbo].[Categories] SET [dbo].[Categories].[Left] = ([dbo].[Categories].[Left] + 2),[dbo].[Categories].[Right] = ([dbo].[Categories].[Right] + 2)  
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] > @ParentRight;
							UPDATE [dbo].[Categories] SET [dbo].[Categories].[Right] = ([dbo].[Categories].[Right] + 2)  
								FROM [dbo].[Categories] WHERE [dbo].[Categories].[Left] < @ParentRight AND [dbo].[Categories].[Right] >= @ParentRight;

							INSERT INTO [dbo].[Categories] (
								[dbo].[Categories].[Title], [dbo].[Categories].[Left], [dbo].[Categories].[Right], [dbo].[Categories].[Level], 
								[dbo].[Categories].[CreatedAt], [dbo].[Categories].[UpdatedAt])
								VALUES (@Title, @Left, @Right, @Level, getdate(), getdate());
						IF (@@error <> 0)
							ROLLBACK
						COMMIT;
						RETURN;
					END


			END
	END