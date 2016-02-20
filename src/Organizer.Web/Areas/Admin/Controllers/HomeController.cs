using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Organizer.Web.Services;
using System.Threading.Tasks;

namespace Organizer.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminAccess")]
    public class HomeController : Controller
    {
        private readonly IDataStore _dataStore;

        public HomeController(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public IActionResult Index() => View();
       

        public async Task<IActionResult> Submissions()
        {
            var sessions = await _dataStore.GetSessionsAsync();
            return View(sessions);
        }

        public async Task<IActionResult> Speakers()
        {
            var speakers = await _dataStore.GetSpeakersAsync();
            return View(speakers);
        }
    }
}
