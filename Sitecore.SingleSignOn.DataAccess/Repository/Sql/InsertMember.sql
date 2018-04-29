INSERT INTO Member
(
	[Id],
	[Fullname],
	[Username],
	[Password],
	[CreatedAt],
	[UpdatedAt]
)
VALUES
(
	@Id,
	@Fullname,
	@Username,
	@Password,
	@CreatedAt,
	@UpdatedAt
)