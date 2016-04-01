CREATE PROCEDURE Gardens.spCheckLeaderStatus (
    @UserId     				INT,
    @GroupId    				INT
)
AS
BEGIN
        SELECT Leader
        FROM Gardens.GroupMembers
        WHERE GroupID = @GroupId AND UserId = @UserId
 END
 GO
 
 CREATE PROCEDURE Gardens.spUpdateGroupName (
    @GroupID    				INT,
    @OldGroupName		VARCHAR(100),
    @NewGroupName		VARCHAR(100)
)
AS
BEGIN
	UPDATE Gardens.Groups
		SET GroupName = @NewGroupName
		WHERE GroupID = @GroupID AND GroupName = @OldGroupName
	RETURN @@rowcount
END
GO
