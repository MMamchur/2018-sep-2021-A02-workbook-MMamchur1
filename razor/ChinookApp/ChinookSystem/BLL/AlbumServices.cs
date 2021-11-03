using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using ChinookSystem.Models;
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public List<AlbumItem> GetAlbumsByGenre(int genreid, int pageNumber, int PageSize, out int totalcount)
        {
            IEnumerable<AlbumItem> albums = _context.Tracks
            .Where(x => x.GenreId == genreid && x.AlbumId.HasValue)
            .Select(x => new AlbumItem
            {
                AlbumId = (int)x.AlbumId,
                Title = x.Album.Title,
                ArtistId = x.Album.ArtistId,
                ReleaseYear = x.Album.ReleaseYear,
                ReleaseLabel = x.Album.ReleaseLabel
            })
            .Distinct()
            .OrderBy(x => x.Title);
            totalcount = albums.Count();
            int skipRows = (pageNumber - 1) * PageSize;
            return albums.Skip(skipRows).Take(PageSize).ToList();
        }

        public List<AlbumItem> GetAlbumsByTitle(string title, int pageNumber, int PageSize, out int totalcount)
        {
            IEnumerable<AlbumItem> albums = _context.Albums
            .Where(x => x.Title.Contains(title))
            .Select(x => new AlbumItem
            {
                AlbumId = x.AlbumId,
                Title = x.Title,
                ArtistId = x.ArtistId,
                ReleaseYear = x.ReleaseYear,
                ReleaseLabel = x.ReleaseLabel
            })
            .OrderBy(x => x.Title);
            totalcount = albums.Count();
            int skipRows = (pageNumber - 1) * PageSize;
            return albums.Skip(skipRows).Take(PageSize).ToList();
        }
        #endregion

        #region Add, Update and delete

        public int AddAlbum(AlbumItem item)
        {
            Album exist = _context.Albums
                                    .Where(x => x.Title.Equals(item.Title)
                                            && x.ArtistId == item.ArtistId
                                            && x.ReleaseYear == item.ReleaseYear)
                                    .FirstOrDefault();
            if (exist == null)
            {
                throw new Exception("Album already exists on file");
            }

            exist = new Album
            {
                Title = item.Title,
                ArtistId = item.ArtistId,
                ReleaseYear = item.ReleaseYear,
                ReleaseLabel = item.ReleaseLabel
            };
            _context.Add(exist);
            _context.SaveChanges();
            return exist.AlbumId;
        }

        public int UpdateAlbum(AlbumItem item)
        {
            Album exist = _context.Albums
                                    .Where(x => x.ArtistId == item.ArtistId)
                                    .FirstOrDefault();
            if (exist == null)
            {
                throw new Exception("Album does not exist on file");
            }

            exist = new Album
            {
                AlbumId = item.AlbumId,
                Title = item.Title,
                ArtistId = item.ArtistId,
                ReleaseYear = item.ReleaseYear,
                ReleaseLabel = item.ReleaseLabel
            };
            EntityEntry<Album> updating = _context.Entry(exist);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _context.SaveChanges();
        }

        public int DeleteAlbum(AlbumItem item)
        {
            Album exist = _context.Albums
                                    .Where(x => x.ArtistId == item.ArtistId)
                                    .FirstOrDefault();
            if (exist == null)
            {
                throw new Exception("Album already has been removed");
            }

            exist = new Album
            {
                AlbumId = item.AlbumId,
                Title = item.Title,
                ArtistId = item.ArtistId,
                ReleaseYear = item.ReleaseYear,
                ReleaseLabel = item.ReleaseLabel
            };
            EntityEntry<Album> deleting = _context.Entry(exist);
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            return _context.SaveChanges();
        }
        #endregion
    }

}
