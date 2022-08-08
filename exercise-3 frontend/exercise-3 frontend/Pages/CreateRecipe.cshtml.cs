using exercise_3_frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace exercise_3_frontend.Pages
{
    public class CreateRecipeModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Ingredients { get; set; }
        [BindProperty]
        public string Instructions { get; set; }
        [BindProperty]
        public Guid[] Categories { get; set; } = new Guid[0];
        public HttpClient HttpClient = new();
        private readonly IConfiguration Configuration;

        public CreateRecipeModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<IActionResult> OnPost()
        {
            Recipe toAdd = new Recipe("",new(),new(),new());
            
            toAdd.Title = Title.Trim();
            foreach (Guid category in Categories)
            {
                toAdd.Categories.Add(category);
            }

            toAdd.Instructions = new();
            // using the method
            String[] strlist = Instructions.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                if (s.Trim()!="")
                {
                    toAdd.Instructions.Add(s.Trim());
                }
            }
             strlist = Ingredients.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (String s in strlist)
            {
                if( s.Trim()!="")
                toAdd.Ingredients.Add(s.Trim());
            }

            
                var temp = JsonSerializer.Serialize(toAdd);
                var res = await HttpClient.PostAsync(Configuration["BaseUrl"]+"recipes", new StringContent(temp, Encoding.UTF8, "application/json"));
                return Redirect("/Recipes");
            
        }
    }
}
