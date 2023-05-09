namespace BLL.Models;

public class UserLoginDataDTO
{
    public int UserId { get; }

    public string UserToken { get; }

    public UserLoginDataDTO(string userToken, int userId)
    {
        UserToken = userToken;
        UserId = userId;
    }
}