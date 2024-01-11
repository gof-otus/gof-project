namespace Finik.AuthService.Contracts
{
    public record HashedPassword(string Hash, string Salt);
}
