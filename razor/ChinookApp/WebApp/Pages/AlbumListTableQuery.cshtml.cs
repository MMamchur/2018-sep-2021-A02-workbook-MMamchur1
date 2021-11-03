using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional namespaces
using ChinookSystem.BLL;
using ChinookSystem.Models;
using Microsoft.Extensions.Logging;
using WebApp.Helpers;
#endregion

namespace WebApp.Pages
{
    public class AlbumListTableQueryModel : PageModel
    {
        #region Private variable and DI constructor
        private readonly AlbumServices _albumservices;
        private readonly GenreServices _genreservices;
        [TempData]
        public string FeedBackMessage { get; set; }

        public AlbumListTableQueryModel(AlbumServices albumservices,
                                        GenreServices genreservices)
        {
            _albumservices = albumservices;
            _genreservices = genreservices;
        }
        #endregion

        [BindProperty]
        public List<AlbumItem> Albums { get; set; }

        [BindProperty]
        public List<SelectionList> Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? genreid { get; set; }

        private const int PAGE_SIZE = 5;
        public Paginator Pager { get; set; }
        public int TotalCount { get; set; }


        public void OnGet(int? currentPage)
        {
            Genres = _genreservices.GenreList();
            if (genreid.HasValue && genreid > 0)
            {
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;
                PageState current = new(pageNumber, PAGE_SIZE);
                int totalcount;

                Albums = _albumservices.GetAlbumsByGenre((int)genreid, pageNumber, PAGE_SIZE, out totalcount);
                TotalCount = totalcount;
                Pager = new(TotalCount, current);
            }
        }

        public IActionResult OnPost()
        {
            if(!genreid.HasValue || genreid == 0)
            {
                FeedBackMessage = "You did not select a genre.";
            }
            return RedirectToPage(new { genreid = genreid });
        }
    }
}
