using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using crypto_random.Models;
using crypto_random.Services;

namespace crypto_random.Controllers;

public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;
    private readonly HomeService _homeService;

    public HomeController(ILogger<HomeController> logger, HomeService homeService) {
        this._logger = logger;
        this._homeService = homeService;
    }

    public IActionResult Index() {

        var lsPlayer = this._homeService.GetListPlayer();
        ViewBag.lsPlayer = lsPlayer;

        return View();
    }

    [HttpPost]
    public IActionResult LoadDataTableTransaction(string playerName) {
        var dataPlayer = this._homeService.GetDataPlayer(playerName);

        return PartialView("DataTableTransation", dataPlayer);
    }

    [HttpPost]
    public IActionResult RandomCoin(string playerName) {
        var resMsg = this._homeService.RandomCoin(playerName);

        return Json(new { resMsg = resMsg });
    }

    public IActionResult ManagePlayer() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
