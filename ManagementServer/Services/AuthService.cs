using System.Security.Cryptography;
using System.Text.Json;
using ManagementServer.Models;

namespace ManagementServer.Services;

public class AuthService
{
    private readonly string _usersFilePath;
    private readonly Dictionary<string, string> _activeTokens = new();
    private readonly object _lock = new();

    private const string AdminRegistrationCode = "MarkAdmin2026";

    public AuthService()
    {
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        Directory.CreateDirectory(dataFolder);
        _usersFilePath = Path.Combine(dataFolder, "users.json");
    }

    public bool ValidateAdminCode(string adminCode)
    {
        return adminCode == AdminRegistrationCode;
    }

    public (bool Success, string Message) Register(string adminCode, string fullName, string username, string password)
    {
        lock (_lock)
        {
            if (!ValidateAdminCode(adminCode))
            {
                return (false, "Érvénytelen admin kód. Keresd fel a rendszergazdát.");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                return (false, "A teljes név kötelező.");
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "A felhasználónév és a jelszó kötelező.");
            }

            var users = LoadUsers();

            if (users.Any(u => u.Username.Equals(username, StringComparison.Ordinal)))
            {
                return (false, "Ez a felhasználónév már foglalt.");
            }

            users.Add(new UserAccount
            {
                FullName = fullName.Trim(),
                Username = username.Trim(),
                PasswordHash = HashPassword(password),
                CreatedAt = DateTime.UtcNow
            });

            SaveUsers(users);

            return (true, "Sikeres regisztráció.");
        }
    }

    public (bool Success, string Message, string Token, string FullName, string Username) Login(string username, string password)
    {
        lock (_lock)
        {
            var users = LoadUsers();

            var user = users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.Ordinal));

            if (user == null)
            {
                return (false, "Hibás felhasználónév vagy jelszó.", "", "", "");
            }

            if (!VerifyPassword(password, user.PasswordHash))
            {
                return (false, "Hibás felhasználónév vagy jelszó.", "", "", "");
            }

            string token = Guid.NewGuid().ToString("N");
            _activeTokens[token] = user.Username;

            return (true, "Sikeres bejelentkezés.", token, user.FullName, user.Username);   
        }
    }

    public bool ValidateToken(string token)
    {
        lock (_lock)
        {
            return !string.IsNullOrWhiteSpace(token) && _activeTokens.ContainsKey(token);
        }
    }

    public string? GetUsernameByToken(string token)
    {
        lock (_lock)
        {
            return _activeTokens.TryGetValue(token, out var username) ? username : null;
        }
    }

    private List<UserAccount> LoadUsers()
    {
        if (!File.Exists(_usersFilePath))
        {
            return new List<UserAccount>();
        }

        string json = File.ReadAllText(_usersFilePath);
        return JsonSerializer.Deserialize<List<UserAccount>>(json) ?? new List<UserAccount>();
    }

    private void SaveUsers(List<UserAccount> users)
    {
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(_usersFilePath, json);
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(32);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        string[] parts = storedHash.Split('.');
        if (parts.Length != 2)
        {
            return false;
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] expectedHash = Convert.FromBase64String(parts[1]);

        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256);

        byte[] actualHash = pbkdf2.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
    }
    public void Logout(string token)
    {
        lock (_lock)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                _activeTokens.Remove(token);
            }
        }
    }
}