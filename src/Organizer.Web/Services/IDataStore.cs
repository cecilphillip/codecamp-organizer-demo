using Organizer.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organizer.Web.Services
{
    public interface IDataStore
    {
        Task<IEnumerable<SessionViewModel>> GetSessionsAsync();
        Task<IEnumerable<SpeakerViewModel>> GetSpeakersAsync();
        Task<IEnumerable<string>> GetTracksAsync();
        Task<IEnumerable<string>> GetSpeakerNamesAsync();
        Task<IEnumerable<SessionViewModel>> GetSessionsAsync(string track);
        Task<IEnumerable<SessionViewModel>> GetSpeakerSessionsAsync(string speakerName);
        Task<SpeakerViewModel> GetSpeaker(string speakerName);
        Task<IDictionary<string, IEnumerable<string>>> GetSpeakerNamesAndTracks();
    }
}
