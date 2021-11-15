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
    public class CRUDAlbumModel : PageModel
    {
        #region Service variable(s), FeedBack, and DI constructor
        private readonly AlbumServices _albumservices;
        private readonly ArtistServices _artistservices;

        [TempData]
        public string FeedBackMessage { get; set; }

        public string ErrorMessage { get; set; }

        public CRUDAlbumModel(AlbumServices albumservices,
                              ArtistServices artistservices)
        {
            _albumservices = albumservices;
            _artistservices = artistservices;
        }
        #endregion

        #region Form Variables
        [BindProperty(SupportsGet = true)]
        public int? albumid { get; set; }

        [BindProperty]
        public List<SelectionList> Artists { get; set; }

        [BindProperty]
        public AlbumItem Album { get; set; }
        #endregion

        public void OnGet()
        {
            Artists = _artistservices.Artist_List();
            if(albumid.HasValue)
            {
                Album = _albumservices.GetAlbumById((int)albumid);
            }
        }


        public IActionResult OnPostNew()
        {
            try
            {
                albumid = _albumservices.AddAlbum(Album);
                FeedBackMessage = $"Album ({albumid}) has been added";
                
                return RedirectToPage(new { albumid = albumid });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Artists = _artistservices.Artist_List();
                return Page();
            }

        }

        public IActionResult OnPostUpdate()
        {
            try
            {
                if (albumid.HasValue)
                {
                    int rowaffected = _albumservices.UpdateAlbum(Album);
                    if (rowaffected > 0)
                    {
                        FeedBackMessage = "Album has been updated";
                    }
                    else
                    {
                        FeedBackMessage = "No albums updates.  Album does not exist";
                    }
                }
                else
                {
                    FeedBackMessage = "Find an album to maintain before attempting the update";
                }
                return RedirectToPage(new { albumid = albumid });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Artists = _artistservices.Artist_List();
                return Page();
            }
            
        }

        public IActionResult OnPostDelete()
        {
            try
            {
                if (albumid.HasValue)
                {
                    int rowaffected = _albumservices.DeleteAlbum(Album);
                    if (rowaffected > 0)
                    {
                        FeedBackMessage = "Album has been removed";
                    }
                    else
                    {
                        FeedBackMessage = "No albums deleted.  Album does not exist";
                    }
                    albumid = null;
                }
                else
                {
                    FeedBackMessage = "Find an album to review before attempting the delete";
                }
                return RedirectToPage(new { albumid = albumid });
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                Artists = _artistservices.Artist_List();
                return Page();
            }
        }

        private Exception GetInnerException (Exception ex)
        {
            while(ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}
