using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grajciar.InternetBanking.WebAPI.Areas.Controllers
{
    [Authorize(Roles = "Customer")]
    public abstract class BaseController : ControllerBase
    {
    }
}
