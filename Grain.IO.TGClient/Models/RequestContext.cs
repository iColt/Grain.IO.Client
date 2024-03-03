namespace Grain.IO.TGClient.Models;

public class RequestContext(int userId, string userName, bool isPremium, bool exist)
{
    public int UserId { get; set; } = userId;

    public string UserName { get; set; } = userName;

    public bool IsPremium { get; set; } = isPremium;

    public bool Exist { get; set; } = exist;
}
