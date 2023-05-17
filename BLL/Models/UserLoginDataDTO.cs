namespace BLL.Models;

public class UserLoginDataDTO
{
    public int UserId { get; }

    public string UserToken { get; }

    public List<string> Roles { get; set; }

    public UserLoginDataDTO(string userToken, int userId, List<string> roles)
    {
        UserToken = userToken;
        UserId = userId;
        Roles = roles;
    }
}