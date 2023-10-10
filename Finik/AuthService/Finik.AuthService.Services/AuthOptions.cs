﻿namespace Finik.AuthService.Services;

public class AuthOptions
{
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
}