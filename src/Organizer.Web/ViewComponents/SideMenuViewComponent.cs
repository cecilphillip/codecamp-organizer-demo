using Microsoft.AspNet.Mvc;
using Organizer.Web.Services;
using System.Threading.Tasks;

namespace Organizer.Web.ViewComponents
{
    [ViewComponent(Name = "SideMenu")]
    public class SideMenuViewComponent : ViewComponent
    {
        private readonly IDataStore _dataStore;

        public SideMenuViewComponent(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menuData = await _dataStore.GetSpeakerNamesAndTracks();
            return View(menuData);
        }
    }
}
