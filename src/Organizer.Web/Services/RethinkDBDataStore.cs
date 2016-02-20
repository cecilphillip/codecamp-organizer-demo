using Microsoft.Extensions.OptionsModel;
using Organizer.Web.Config;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using System.Linq;
using System.Threading.Tasks;
using Organizer.Web.ViewModels;
using System.Collections.Generic;

namespace Organizer.Web.Services
{
    public class RethinkDBDataStore : IDataStore
    {
        private const string SESSIONS_TABLE_NAME = "sessions";
        private const string SPEAKERS_DEFINITION_TABLE_NAME = "speakers";
        private IOptions<RethinkDBConfig> _config;

        protected static Connection _conn;
        protected static RethinkDB _r = RethinkDB.R;

        public RethinkDBDataStore(IOptions<RethinkDBConfig> config)
        {
            _config = config;

            if (_conn == null)
            {
                _conn = _r.Connection()
                          .Hostname(config.Value.ConnectionHostName)
                          .Port(config.Value.ConnectionPort)
                          .Connect();
            }
        }

        public async Task<IEnumerable<SessionViewModel>> GetSessionsAsync()
        {
            var result = (await _r.Db(_config.Value.DatabaseName)
                .Table(SESSIONS_TABLE_NAME).EqJoin("speaker_id", _r.Db(_config.Value.DatabaseName).Table(SPEAKERS_DEFINITION_TABLE_NAME))
                .Map(ss => new
                {
                    Name = ss["left"]["name"],
                    Track = ss["left"]["track"],
                    Speaker = new
                    {
                        Name = ss["right"]["name"],
                        Company = ss["right"]["company"],
                        Bio = ss["right"]["bio"]
                    }
                })
                .RunCursorAsync<SessionViewModel>(_conn)).ToList();
            return result;
        }

        public async Task<IEnumerable<SpeakerViewModel>> GetSpeakersAsync()
        {
            var result = (await _r.Db(_config.Value.DatabaseName)
             .Table(SPEAKERS_DEFINITION_TABLE_NAME)
             .Map(sp => sp.Merge(new
             {
                 sessions = _r.Db(_config.Value.DatabaseName)
                                     .Table(SESSIONS_TABLE_NAME)
                                     .Filter(ss => ss["speaker_id"].Eq(sp["id"])).CoerceTo("array")
             }))
             .RunCursorAsync<SpeakerViewModel>(_conn)).ToList();
            return result;
        }
        public async Task<IDictionary<string, IEnumerable<string>>> GetSpeakerNamesAndTracks()
        {
            var rsult = await _r.Expr(new
            {
                tracks = _r.Db(_config.Value.DatabaseName).Table("sessions")["track"].Distinct().CoerceTo("array"),
                speakers = _r.Db(_config.Value.DatabaseName).Table("speakers")["name"].CoerceTo("array")
            }).RunAtomAsync<IDictionary<string, IEnumerable<string>>>(_conn);

            return rsult;

        }


        public async Task<IEnumerable<string>> GetSpeakerNamesAsync()
        {
            var result = (await _r.Db(_config.Value.DatabaseName)
             .Table(SPEAKERS_DEFINITION_TABLE_NAME)
             .GetField("name")
             .RunResultAsync<IEnumerable<string>>(_conn));
            return result;
        }

        public async Task<IEnumerable<string>> GetTracksAsync()
        {
            var result = (await _r.Db(_config.Value.DatabaseName)
             .Table(SESSIONS_TABLE_NAME).GetField("track").Distinct()

             .RunResultAsync<IEnumerable<string>>(_conn));

            return result;
        }

        public async Task<IEnumerable<SessionViewModel>> GetSessionsAsync(string track)
        {
            var result = (await _r.Db(_config.Value.DatabaseName)
             .Table(SESSIONS_TABLE_NAME)
             .Filter(s => s["track"].Eq(track)).EqJoin("speaker_id", _r.Db(_config.Value.DatabaseName).Table(SPEAKERS_DEFINITION_TABLE_NAME))
             .Map(ss => new
             {
                 Name = ss["left"]["name"],
                 Track = ss["left"]["track"],
                 Speaker = new
                 {
                     Name = ss["right"]["name"],
                     Company = ss["right"]["company"],
                     Bio = ss["right"]["bio"]
                 }
             })
             .RunCursorAsync<SessionViewModel>(_conn)).ToList();
            return result;
        }

        public async Task<IEnumerable<SessionViewModel>> GetSpeakerSessionsAsync(string speakerName)
        {
            var result = (await _r.Db(_config.Value.DatabaseName).Table(SPEAKERS_DEFINITION_TABLE_NAME)
             .EqJoin("id", _r.Db(_config.Value.DatabaseName).Table(SESSIONS_TABLE_NAME)).optArg("index", "speaker_id")
             .Filter(ss => ss["left"]["name"].Eq(speakerName))
             .Map(ss => ss["right"].Merge(new
             {
                 speaker = ss["left"]
             }))
             .RunCursorAsync<SessionViewModel>(_conn)).ToList();
            return result;
        }

        public async Task<SpeakerViewModel> GetSpeaker(string speakerName)
        {
            var result = (await _r.Db(_config.Value.DatabaseName)
              .Table(SPEAKERS_DEFINITION_TABLE_NAME)
              .Filter(s => s["name"].Eq(speakerName))
              .Map(sp => sp.Merge(new
              {
                  sessions = _r.Db(_config.Value.DatabaseName)
                                      .Table(SESSIONS_TABLE_NAME)
                                      .Filter(ss => ss["speaker_id"].Eq(sp["id"])).CoerceTo("array")
              }))
              .RunCursorAsync<SpeakerViewModel>(_conn)).SingleOrDefault();
            return result;
        }


    }
}
