﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Models;
using ChinookSystem.DAL;
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
            throw new Exception("adding track BLL");
        }

        #endregion
    }
}
