using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace exercise_3_frontend.Pages
{
    public class RecipeModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public RecipeModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}