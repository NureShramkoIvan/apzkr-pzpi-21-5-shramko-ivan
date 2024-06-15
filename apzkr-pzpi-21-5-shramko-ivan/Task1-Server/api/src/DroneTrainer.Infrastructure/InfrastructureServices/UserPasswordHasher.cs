using DroneTrainer.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace DroneTrainer.Infrastructure.InfrastructureServices;

internal sealed class UserPasswordHasher : IPasswordHasher<User>
{
    private const short _saltSize = 128 / 8;
    private const short _keySize = 256 / 8;
    private const int _iterations = 25000;
    private const char _separator = '.';
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;

    public string HashPassword(User user, string password)
    {
        var salt = RandomNumberGenerator.GetBytes(_saltSize);

        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            _iterations,
            _hashAlgorithmName,
            _keySize);

        var hasStr = Convert.ToBase64String(hashBytes);
        var slatstr = Convert.ToBase64String(salt);

        var result = string.Join(
            _separator,
            Convert.ToBase64String(hashBytes),
            Convert.ToBase64String(salt));

        return result;
    }

    public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        var passwordItems = hashedPassword.Split(_separator);

        var existsingPasswordHash = Convert.FromBase64String(passwordItems[0]);
        var salt = Convert.FromBase64String(passwordItems[1]);

        var providerPasswordHash = Rfc2898DeriveBytes.Pbkdf2(
            providedPassword,
            salt,
            _iterations,
            _hashAlgorithmName,
            _keySize);

        return CryptographicOperations.FixedTimeEquals(existsingPasswordHash, providerPasswordHash)
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }
}
