using Microsoft.AspNetCore.Antiforgery;
using Interview.Controllers;

namespace Interview.Web.Host.Controllers
{
    public class AntiForgeryController : InterviewControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
