using Company.G03.PL.Models;
using Company.G03.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Company.G03.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IScopedService Scop01 { get; }
        public IScopedService Scop02 { get; }
        public ITransientService Tran01 { get; }
        public ITransientService Tran02 { get; }
        public ISingletonService Sing01 { get; }
        public ISingletonService Sing02 { get; }

        public HomeController(ILogger<HomeController> logger,
            IScopedService scop01,
            IScopedService scop02,
            ITransientService tran01,
            ITransientService tran02,
            ISingletonService sing01,
            ISingletonService sing02)
        {
            _logger = logger;
            Scop01 = scop01;
            Scop02 = scop02;
            Tran01 = tran01;
            Tran02 = tran02;
            Sing01 = sing01;
            Sing02 = sing02;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Scop01 :: {Scop01.GetGuid()}\n");
            builder.Append($"Scop02 :: {Scop02.GetGuid()}\n\n");

            builder.Append($"Tran01 :: {Tran01.GetGuid()}\n");
            builder.Append($"Tran02 :: {Tran02.GetGuid()}\n\n");

            builder.Append($"Sing01 :: {Sing01.GetGuid()}\n");
            builder.Append($"Sing02 :: {Sing02.GetGuid()}\n\n");

            return builder.ToString();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
