namespace AuthService.API.Common;

public class ApplicationConfiguration
{
    public ConnectionStrings ConnectionStrings { get; set; }

    public PasswordSettings PasswordSettings { get; set; }
}

public class ConnectionStrings
{
    public string PostgreSQL { get; set; }
}

public class PasswordSettings
{
    public int SaltSize { get; set; }
    public int KeySize { get; set; }
    public int Iterations { get; set; }
    public string PrivateKey { get; set; }
}
