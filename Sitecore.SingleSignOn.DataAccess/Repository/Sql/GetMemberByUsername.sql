SELECT TOP 1
[Id],
[Fullname],
[Username],
[Password],
[CreatedAt],
[UpdatedAt]
FROM Member
Where Username = @Username