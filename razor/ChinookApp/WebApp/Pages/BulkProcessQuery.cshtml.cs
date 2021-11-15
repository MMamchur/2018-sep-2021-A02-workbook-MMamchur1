using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class BulkProcessQueryModel : PageModel
    {
        [TempData]
        public string FeedBackMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string queryarg { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPostArtist()
        {
            if (string.IsNullOrWhiteSpace(queryarg)){
                FeedBackMessage = "Suppy a value for your track list search";
            }
            else
            {
                return RedirectToPage($"/BulkProcess", new { argsearch = "Artist", argvalue = queryarg });
            }
            return Page();
        }
        public IActionResult OnPostAlbum()
        {
            if (string.IsNullOrWhiteSpace(queryarg))
            {
                FeedBackMessage = "Suppy a value for your track list search";
            }
            else
            {
                return RedirectToPage($"/BulkProcess", new { argsearch = "Album", argvalue = queryarg });
            }
            return Page();
        }
    }
}
