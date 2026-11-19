using System.Security.Cryptography;

namespace OpenNodeDesk.Services;

/// <summary>保护口令哈希：PBKDF2-SHA256 + 随机盐，不可逆，仅用于本地校验。</summary>
public static class ProtectionHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public static (string SaltBase64, string HashBase64) Create(string password)
    {
        var salt = new byte[SaltSize];
        RandomNumberGenerator.Fill(salt);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
        return (Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public static bool Verify(string password, string saltBase64, string hashBase64)
    {
        if (string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(saltBase64) ||
            string.IsNullOrWhiteSpace(hashBase64))
        {
            return false;
        }

        try
        {
            var salt = Convert.FromBase64String(saltBase64);
            var expected = Convert.FromBase64String(hashBase64);
            var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    // 恢复口令字符集：去掉容易看错的 I / O / 0 / 1
    private const string RecoveryAlphabet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
    private const int RecoveryGroups = 4;
    private const int RecoveryGroupLength = 4;

    /// <summary>生成便于抄写的恢复口令，形如 XXXX-XXXX-XXXX-XXXX。</summary>
    public static string GenerateRecoveryCode()
    {
        var groups = new string[RecoveryGroups];
        for (int g = 0; g < RecoveryGroups; g++)
        {
            var chars = new char[RecoveryGroupLength];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = RecoveryAlphabet[RandomNumberGenerator.GetInt32(RecoveryAlphabet.Length)];
            }
            groups[g] = new string(chars);
        }
        return string.Join('-', groups);
    }

    /// <summary>规范化恢复口令：忽略大小写、空格与分隔符，容忍用户抄写时的格式差异。</summary>
    public static string NormalizeRecoveryCode(string? code)
    {
        if (string.IsNullOrEmpty(code)) return "";

        var chars = code.Where(c => c != '-' && !char.IsWhiteSpace(c)).ToArray();
        return new string(chars).ToUpperInvariant();
    }

    /// <summary>校验恢复口令（按规范化后的字符串比对）。</summary>
    public static bool VerifyRecovery(string? code, string saltBase64, string hashBase64) =>
        Verify(NormalizeRecoveryCode(code), saltBase64, hashBase64);
}
