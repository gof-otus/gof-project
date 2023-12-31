﻿namespace Finik.AuthService.Contracts;

public class UserDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? NickName { get; set; }
    public required string Email { get; set; }
    public required HashedPassword HashedPassword { get; set; }
    public Role Role { get; set; }
}
