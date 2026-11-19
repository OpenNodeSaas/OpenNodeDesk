using System.IO;
using OpenNodeDesk.Services;
using Xunit;

namespace OpenNodeDesk.Tests;

public class ProtectionTests
{
    // ---------- ProtectionHasher ----------

    [Fact]
    public void TestCreateReturnsNonEmptyAndDiffersFromPassword()
    {
        var (salt, hash) = ProtectionHasher.Create("P@ssw0rd!");

        Assert.False(string.IsNullOrEmpty(salt));
        Assert.False(string.IsNullOrEmpty(hash));
        Assert.NotEqual("P@ssw0rd!", salt);
        Assert.NotEqual("P@ssw0rd!", hash);
    }

    [Fact]
    public void TestCreateUsesRandomSalt()
    {
        var (salt1, hash1) = ProtectionHasher.Create("same-password");
        var (salt2, hash2) = ProtectionHasher.Create("same-password");

        Assert.NotEqual(salt1, salt2);
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void TestVerifyAcceptsCorrectPasswordAndRejectsWrongOne()
    {
        var (salt, hash) = ProtectionHasher.Create("correct-horse");

        Assert.True(ProtectionHasher.Verify("correct-horse", salt, hash));
        Assert.False(ProtectionHasher.Verify("wrong-horse", salt, hash));
    }

    [Fact]
    public void TestVerifyRejectsEmptySaltOrHash()
    {
        var (salt, hash) = ProtectionHasher.Create("secret");

        Assert.False(ProtectionHasher.Verify("secret", "", hash));
        Assert.False(ProtectionHasher.Verify("secret", salt, ""));
        Assert.False(ProtectionHasher.Verify("secret", "   ", hash));
        Assert.False(ProtectionHasher.Verify("secret", salt, "   "));
    }

    [Fact]
    public void TestVerifyRejectsInvalidBase64WithoutThrowing()
    {
        var exception = Record.Exception(() => ProtectionHasher.Verify("secret", "not-base64!!", "also-not-base64!!"));

        Assert.Null(exception);
        Assert.False(ProtectionHasher.Verify("secret", "not-base64!!", "also-not-base64!!"));
    }

    // ---------- 恢复口令 ----------

    [Fact]
    public void TestGenerateRecoveryCodeFormat()
    {
        var code = ProtectionHasher.GenerateRecoveryCode();
        var groups = code.Split('-');

        Assert.Equal(4, groups.Length);
        Assert.All(groups, g => Assert.Equal(4, g.Length));
        Assert.Matches("^[A-HJ-NP-Z2-9]{4}(-[A-HJ-NP-Z2-9]{4}){3}$", code);
    }

    [Fact]
    public void TestGenerateRecoveryCodeIsRandom()
    {
        Assert.NotEqual(ProtectionHasher.GenerateRecoveryCode(), ProtectionHasher.GenerateRecoveryCode());
    }

    [Fact]
    public void TestNormalizeRecoveryCodeIgnoresCaseSpaceAndDashes()
    {
        Assert.Equal("ABCD2345EFGH6789", ProtectionHasher.NormalizeRecoveryCode("abcd-2345 efgh-6789"));
        Assert.Equal("ABCD2345EFGH6789", ProtectionHasher.NormalizeRecoveryCode("ABCD2345EFGH6789"));
        Assert.Equal("", ProtectionHasher.NormalizeRecoveryCode(null));
        Assert.Equal("", ProtectionHasher.NormalizeRecoveryCode(" - "));
    }

    [Fact]
    public void TestVerifyRecoveryAcceptsReformattedCode()
    {
        var code = ProtectionHasher.GenerateRecoveryCode();
        var (salt, hash) = ProtectionHasher.Create(ProtectionHasher.NormalizeRecoveryCode(code));

        Assert.True(ProtectionHasher.VerifyRecovery(code, salt, hash));
        Assert.True(ProtectionHasher.VerifyRecovery(code.ToLowerInvariant().Replace("-", " "), salt, hash));
        Assert.False(ProtectionHasher.VerifyRecovery("WXYZ-2345-WXYZ-2345", salt, hash));
        Assert.False(ProtectionHasher.VerifyRecovery(null, salt, hash));
        Assert.False(ProtectionHasher.VerifyRecovery(code, "", hash));
        Assert.False(ProtectionHasher.VerifyRecovery(code, salt, ""));
    }

    // ---------- AssetStore ----------

    [Fact]
    public void TestProtectionDefaultsForLegacyJson()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "assets.json");
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        try
        {
            File.WriteAllText(path, "{\"Version\":1,\"Assets\":[],\"Templates\":[]}");

            var loaded = new AssetStore(path).Load();

            Assert.False(loaded.ProtectionEnabled);
            Assert.Equal(10, loaded.ProtectionIdleMinutes);
        }
        finally
        {
            var dir = Path.GetDirectoryName(path)!;
            if (File.Exists(path)) File.Delete(path);
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
        }
    }

    [Fact]
    public void TestProtectionRoundTrip()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "assets.json");
        try
        {
            var store = new AssetStore(path);
            store.Save(new AssetStoreData
            {
                ProtectionEnabled = true,
                ProtectionIdleMinutes = 30,
                ProtectionSalt = "c2FsdA==",
                ProtectionHash = "aGFzaA=="
            });

            var loaded = new AssetStore(path).Load();

            Assert.True(loaded.ProtectionEnabled);
            Assert.Equal(30, loaded.ProtectionIdleMinutes);
            Assert.Equal("c2FsdA==", loaded.ProtectionSalt);
            Assert.Equal("aGFzaA==", loaded.ProtectionHash);
        }
        finally
        {
            var dir = Path.GetDirectoryName(path)!;
            if (File.Exists(path)) File.Delete(path);
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
        }
    }

    [Fact]
    public void TestProtectionIdleMinutesFallsBackToDefault()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "assets.json");
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        try
        {
            File.WriteAllText(path,
                "{\"Version\":1,\"Assets\":[],\"Templates\":[],\"ProtectionIdleMinutes\":7}");

            var loaded = new AssetStore(path).Load();

            Assert.Equal(10, loaded.ProtectionIdleMinutes);
        }
        finally
        {
            var dir = Path.GetDirectoryName(path)!;
            if (File.Exists(path)) File.Delete(path);
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
        }
    }

    [Fact]
    public void TestProtectionEnabledFallsBackWhenSaltOrHashMissing()
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "assets.json");
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        try
        {
            File.WriteAllText(path,
                "{\"Version\":1,\"Assets\":[],\"Templates\":[],\"ProtectionEnabled\":true,\"ProtectionSalt\":\"\",\"ProtectionHash\":\"\"}");

            var loaded = new AssetStore(path).Load();

            Assert.False(loaded.ProtectionEnabled);
        }
        finally
        {
            var dir = Path.GetDirectoryName(path)!;
            if (File.Exists(path)) File.Delete(path);
            if (Directory.Exists(dir)) Directory.Delete(dir, true);
        }
    }
}
