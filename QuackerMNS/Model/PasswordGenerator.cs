using System;
using System.Linq;
using System.Security.Cryptography;

public static class PasswordGenerator
{
    private static readonly int DefaultPasswordLength = 12;
    private static readonly string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
    private static readonly string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly string DigitChars = "0123456789";
    private static readonly string SpecialChars = "!@#$%^&*";
    private static readonly string AllChars = LowercaseChars + UppercaseChars + DigitChars + SpecialChars;

    public static string GeneratePassword(int length = 12)
    {
        var random = new RNGCryptoServiceProvider();
        var passwordChars = new char[length];
        var charCategories = new[] { LowercaseChars, UppercaseChars, DigitChars, SpecialChars };

        // Ensure each category is represented at least once
        for (int i = 0; i < charCategories.Length; i++)
        {
            var category = charCategories[i];
            passwordChars[i] = category[GetRandomInt(random, category.Length)];
        }

        // Fill the remaining slots with random characters from all categories
        for (int i = charCategories.Length; i < length; i++)
        {
            passwordChars[i] = AllChars[GetRandomInt(random, AllChars.Length)];
        }

        // Shuffle the password to randomize character order
        return new string(passwordChars.OrderBy(x => GetRandomInt(random, length)).ToArray());
    }

    private static int GetRandomInt(RNGCryptoServiceProvider random, int max)
    {
        byte[] intBytes = new byte[4];
        random.GetBytes(intBytes);
        return Math.Abs(BitConverter.ToInt32(intBytes, 0)) % max;
    }
}