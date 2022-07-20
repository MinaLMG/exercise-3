using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace exercise_3_frontend.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public CategoryModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}