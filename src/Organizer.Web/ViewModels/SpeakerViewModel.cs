using System.Collections.Generic;

namespace Organizer.Web.ViewModels
{
    public class SpeakerViewModel
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Bio { get; set; }

        public IEnumerable<SessionViewModel> Sessions { get; set; }
    }
}
