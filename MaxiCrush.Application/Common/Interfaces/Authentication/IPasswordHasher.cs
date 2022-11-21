namespace MaxiCrush.Application.Common.Interfaces.Authentication;

public interface IPasswordHasher
{
    byte[] ComputeHash(string password);
    bool Equivalent(byte[] left, byte[] right);
}
