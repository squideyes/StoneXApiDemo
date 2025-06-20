namespace StoneXApiDemo;

public class LogonResponse
{
    public string Session { get; set; }
    public bool PasswordChangeRequired { get; set; }
    public bool AllowedAccountOperator { get; set; }
    public int StatusCode { get; set; }
    public bool Is2FAEnabled { get; set; }
    public object TwoFAToken { get; set; }
    public object[] Additional2FAMethods { get; set; }
}

// kyle.minick@stonex.com