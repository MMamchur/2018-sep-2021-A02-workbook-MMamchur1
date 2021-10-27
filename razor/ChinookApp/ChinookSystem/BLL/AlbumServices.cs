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
    public class AlbumServices
    {
        #region Constructor andDI variable setup
        private readonly ChinookContext _context;

        internal AlbumServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public AlbumItem GetAlbumById(int albumid)
        {
            AlbumItem info = _context.Albums
                .Where(x => x.AlbumId == albumid)
                .Select(x => new AlbumItem
                {
                    AlbumId = x.AlbumId,
                    Title = x.Title,
                    ArtistId = x.ArtistId,
                    ReleaseYear = x.ReleaseYear,
                    ReleaseLabel = x.ReleaseLabel
                }).FirstOrDefault();
            return info;
        }
        #endregion
    }
}
