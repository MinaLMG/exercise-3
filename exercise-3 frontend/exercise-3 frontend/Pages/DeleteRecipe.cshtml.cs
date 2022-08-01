using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace exercise_3_frontend.Pages
{
    public class DeleteRecipeModel : PageModel
    {
        public HttpClient HttpClient = new();
        [BindProperty]
        public Guid ID { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var res3 = await HttpClient.DeleteAsync("https://localhost:7295/recipes/" + ID);
            return Redirect("/Recipes");
        }
    }
}
