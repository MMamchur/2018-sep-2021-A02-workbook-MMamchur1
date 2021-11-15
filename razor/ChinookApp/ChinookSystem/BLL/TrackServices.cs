using System;
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
    public class TrackServices
    {
        #region Constructor andDI variable setup
        private readonly ChinookContext _context;

        internal TrackServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<TrackInfo> Tracks_GetByArtistAlbum(string argsearch, string argvalue, int pageNumber, int pageSize, out int totalcount)
        {
            IEnumerable<TrackInfo> info = _context.Tracks
                           .Where(x => x.Album.Title.Contains(argvalue) && argsearch.Equals("Album") ||
                                        x.Album.Artist.Name.Contains(argvalue) && argsearch.Equals("Artist")).Select(x => new TrackInfo
                                        {
                                            TrackId = x.TrackId,
                                            Name = x.Name,
                                            AlbumTitle = x.Album.Title,
                                            ArtistName = x.Album.Artist.Name,
                                            Milliseconds = x.Milliseconds,
                                            UnitPrice = x.UnitPrice
                                        });
            if (argsearch.Equals("Artist"))
            {
                info = info
                        .OrderBy(x => x.ArtistName)
                        .ThenBy(x => x.Name);
            }
            else
            {
                info = info
                        .OrderBy(x => x.AlbumTitle)
                        .ThenBy(x => x.Name);
            }
            totalcount = info.Count();
            int skipRows = (pageNumber - 1) * pageSize;
            return info.Skip(skipRows).Take(pageSize).ToList();
        }
        #endregion
    }
}