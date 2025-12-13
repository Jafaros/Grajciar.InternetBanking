using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize(Roles = "Customer")]
    public abstract class UserBaseController : ControllerBase
    {
    }
}
