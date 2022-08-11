using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace exercise_3_frontend.Pages
{
    public class DeleteRecipeModel : PageModel
    {
        public HttpClient HttpClient = new();
        [BindProperty]
        public Guid ID { get; set; }
        private readonly IConfiguration Configuration;

        public DeleteRecipeModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<IActionResult> OnPost()
        {
            var res= await HttpClient.DeleteAsync(Configuration["BaseUrl"]+"recipes/" + ID);
            if ((int)res.StatusCode == 200)
                return Redirect("/recipes?ReqResult=success&Msg=the recipe has been deleted successfully");
            else
                return Redirect("/recipes?ReqResult=failure&Msg=something went wrong with your request .. you can retry after some seconds");
        }
    }
}
