using System.Security.Claims;

namespace smart_expenses_web_app.Services;

public class UserService
{
    private readonly ClaimsPrincipal? _userClaimsPrincipal;
    
    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _userClaimsPrincipal = httpContextAccessor.HttpContext?.User;
    }
    
    public string? GetUserId()
    {
        return _userClaimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public string? GetUserName()
    {
        return _userClaimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;
    }

    public string? GetUserEmail()
    {
        return _userClaimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
    }
}