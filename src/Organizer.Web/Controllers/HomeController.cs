using Microsoft.AspNet.Mvc;
using Organizer.Web.Services;
using System.Threading.Tasks;

namespace Organizer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataStore _dataStore;

        public HomeController(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<IActionResult> Index()
        {
            if (Request.Query.ContainsKey("track"))
            {
                var track = Request.Query["track"];
                var sessionsByTrack = await _dataStore.GetSessionsAsync(track);
                return View(sessionsByTrack);
            }
            else if (Request.Query.ContainsKey("speaker"))
            {
                var speakerName = Request.Query["speaker"];
                var speakerSession = await _dataStore.GetSpeakerSessionsAsync(speakerName);
                return View(speakerSession);
            }
         
                var sessions = await _dataStore.GetSessionsAsync();
                return View(sessions);
            
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
