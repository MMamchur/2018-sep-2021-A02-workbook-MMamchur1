using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class BasicDataMovementModel : PageModel
    {
        public string MyName;

        [TempData]
        public string FeedBackMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? theInput { get; set; }

        public void OnGet()
        {
            Random rnd = new Random();
            int oddeven = rnd.Next(0, 25);
            if (oddeven % 2 == 0)
            {
                MyName = $"Matthew is {oddeven}";
            }
            else
            {
                MyName = null;
            }
        }

        public IActionResult OnPost()
        {
            Thread.Sleep(2000);
            string buttonValue = Request.Form["theButton"];
            FeedBackMessage = buttonValue;
            return RedirectToPage();
        } 
        public IActionResult OnPostAButton()
        {
            Thread.Sleep(1000);
            FeedBackMessage = $"You pressed the A button with an input value of {theInput}";
            return RedirectToPage(new { theInput = theInput });
        }

        public IActionResult OnPostBButton()
        {
            Thread.Sleep(1000);
            FeedBackMessage = $"You pressed the B button with an input value of {theInput}";
            return RedirectToPage(new { theInput = theInput });
        }
    }
}
