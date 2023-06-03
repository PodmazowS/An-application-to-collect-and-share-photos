
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace An_application_to_collect_and_share_photos.Pages
{

    public class testpostModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            string Test = "Test";
            ViewData["mytest"] = Test;
        }
    }
}