﻿namespace Finik.Contracts;

public class UserDto
{
    public int Id { get; set; } 
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? NickName { get; set; }
    public required string Email { get; set; }
}
