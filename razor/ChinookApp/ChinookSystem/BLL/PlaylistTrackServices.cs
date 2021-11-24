using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Models;
using ChinookSystem.DAL;
using ChinookSystem.Entities;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTrackServices
    {
        #region Constructor andDI variable setup
        private readonly ChinookContext _context;

        internal PlaylisTrackServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<PLTrackInfo> Tracks_GetPlaylistforUser(string playlistname, string username)
        {
            IEnumerable<PLTrackInfo> info = _context.PlaylistTracks
                           .Where(x => x.Playlist.Name.Equals(playlistname) && x.Playlist.UserName.Equals(username))
                                        .Select(x => new PLTrackInfo
                                        {
                                            TrackId = x.TrackId,
                                            TrackNumber = x.TrackNumber,
                                            Song = x.Track.Name,
                                            Milliseconds = x.Track.Milliseconds
                                        })
                                        .OrderBy(x => x.TrackNumber);
            return info.ToList();
        }
        #endregion

        #region Playlist Track Maintnence
        public void PlaylistTrack_AddTrack(string playlistname,
                                           string username,
                                           int trackid)
        {
            Playlist playlistExist = null;
            PlaylistTrack playlisttrackexist = null;
            int tracknumber = 0;

            if (string.IsNullOrWhiteSpace(playlistname))
            {
                throw new Exception("Playlist name is missing. Unable to add new track.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("username is missing. Unable to add new track.");
            }

            playlistExist = _context.Playlists
                                    .Where(x => x.Name.Equals(playlistname)
                                            && x.UserName.Equals(username))
                                    .FirstOrDefault();
            if (playlistExist == null)
            {
                playlistExist = new Playlist()
                {
                    Name = playlistname,
                    UserName = username
                };
                _context.Playlists.Add(playlistExist);
                tracknumber = 1;
            }
            else
            {
                playlisttrackexist = _context.PlaylistTracks
                           .Where(x => x.Playlist.Name.Equals(playlistname) && x.Playlist.UserName.Equals(username) && x.TrackId == trackid)
                                        .FirstOrDefault();
                if (playlisttrackexist != null)
                {
                    throw new Exception("Track already on playlist");
                }
                else
                {
                    tracknumber = _context.PlaylistTracks
                           .Where(x => x.Playlist.Name.Equals(playlistname) && x.Playlist.UserName.Equals(username))
                           .Select(x => x.TrackNumber)             
                           .Max();
                    tracknumber++;
                }
            }
        }

        #endregion
    }
}
