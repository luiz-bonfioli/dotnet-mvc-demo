namespace Demo.Infra.Security;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{

    private readonly IConfiguration _configuration;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _configuration = configuration;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!HasAuthorizationHeader())
            return AuthenticateResult.Fail("Missing Authorization Header");

        try
        {
            var credentials = TryParseAuthorizationHeader();
            if (credentials == null)
                return AuthenticateResult.Fail("Invalid Authorization Header");

            var (username, password) = credentials.Value;

            if (!IsAuthenticatedUser(username, password))
                return AuthenticateResult.Fail("Invalid username or password");

            var ticket = CreateAuthenticationTicket(username);
            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
    private bool HasAuthorizationHeader()
    {
        return Request.Headers.ContainsKey("Authorization");
    }

    private (string Username, string Password)? TryParseAuthorizationHeader()
    {
        if (!AuthenticationHeaderValue.TryParse(Request.Headers.Authorization, out var authHeader) || authHeader == null)
            return null;

        var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter!));
        var tokens = decoded.Split(':', 2); 

        if (tokens.Length != 2)
            return null;

        return (tokens[0], tokens[1]);
    }

    private bool IsAuthenticatedUser(string username, string password)
    {
        var configuredUsername = _configuration["BasicAuth:Username"];
        var configuredPassword = _configuration["BasicAuth:Password"];
        return username == configuredUsername && password == configuredPassword;
    }

    private AuthenticationTicket CreateAuthenticationTicket(string username)
    {
        var claims = new[] { new Claim(ClaimTypes.Name, username) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        return new AuthenticationTicket(principal, Scheme.Name);
    }

}
