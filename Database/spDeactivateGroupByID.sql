CREATE PROCEDURE Gardens.spDeactivateGroupByID(
	@GroupID		INT,
	@Active			INT
)
AS
BEGIN
	UPDATE Gardens.Groups
		SET Active = @Active
		WHERE GroupID = @GroupID
	RETURN @@rowcount
END
GO