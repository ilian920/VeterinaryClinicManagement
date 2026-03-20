using VeterinaryClinic.Services.Helpers;

namespace VeterinaryClinic.Tests.Services;

public class PasswordHelperTests
{
    [Fact]
    public void HashPassword_ReturnsDifferentStringThanInput()
    {
        var password = "Admin123!";
        var hash = PasswordHelper.HashPassword(password);
        Assert.NotEqual(password, hash);
    }

    [Fact]
    public void HashPassword_SamePasswordProducesDifferentHashes_DueToSalt()
    {
        // With PBKDF2 + random salt, each hash of the same password is unique
        var password = "Owner123!";
        var hash1 = PasswordHelper.HashPassword(password);
        var hash2 = PasswordHelper.HashPassword(password);
        // Hashes differ (different salt) but both verify correctly
        Assert.NotEqual(hash1, hash2);
        Assert.True(PasswordHelper.VerifyPassword(password, hash1));
        Assert.True(PasswordHelper.VerifyPassword(password, hash2));
    }

    [Fact]
    public void HashPassword_DifferentPasswordsProduceDifferentHashes()
    {
        var hash1 = PasswordHelper.HashPassword("Password1!");
        var hash2 = PasswordHelper.HashPassword("Password2!");
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void VerifyPassword_CorrectPasswordReturnsTrue()
    {
        var password = "SecurePass!";
        var hash = PasswordHelper.HashPassword(password);
        Assert.True(PasswordHelper.VerifyPassword(password, hash));
    }

    [Fact]
    public void VerifyPassword_WrongPasswordReturnsFalse()
    {
        var hash = PasswordHelper.HashPassword("CorrectPassword");
        Assert.False(PasswordHelper.VerifyPassword("WrongPassword", hash));
    }

    [Fact]
    public void VerifyPassword_EmptyPasswordWithItsOwnHash_ReturnsTrue()
    {
        var hash = PasswordHelper.HashPassword("");
        Assert.True(PasswordHelper.VerifyPassword("", hash));
    }
}
