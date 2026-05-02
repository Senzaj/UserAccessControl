using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserManagementSystem.Configuration;

public class JwtSettings
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public SymmetricSecurityKey GetSymmetricSecurityKey => new (Encoding.UTF8.GetBytes(Secret));
    public int ExpirationInMinutes { get; set; }
}