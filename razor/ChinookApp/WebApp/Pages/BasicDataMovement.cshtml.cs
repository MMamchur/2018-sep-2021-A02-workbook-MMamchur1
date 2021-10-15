using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class BasicDataMovementModel : PageModel
    {
        public string MyName;
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
    }
}
