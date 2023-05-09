using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Utility;

public class JwtAuthOptions
{
    public static string Audience { get; set; } = "http://localhost:4200/";
    public static string Issuer { get; set; } = "https://localhost:44354";
    private static string Key { get; set; } = "v890zyhy301y2hjgbiuftg97vgfqw9j0ycash9qb23rkjh";
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}