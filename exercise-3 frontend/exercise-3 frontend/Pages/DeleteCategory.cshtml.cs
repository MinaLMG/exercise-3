using exercise_3_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace exercise_3_frontend.Pages
{
    public class DeleteCategoryModel : PageModel
    {
        public HttpClient HttpClient = new();
        [BindProperty]
        public Guid ID { get; set; }
        private readonly IConfiguration Configuration;

        public DeleteCategoryModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<IActionResult> OnPost()
        {
            var res3 = await HttpClient.DeleteAsync(Configuration["BaseUrl"]+"categories/" + ID);
            return Redirect("/Categories");
        }
    }
}

